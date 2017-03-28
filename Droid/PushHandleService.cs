
using System;
using System.Collections.Generic;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Gcm.Client;

using WindowsAzure.Messaging;
using Xamarin.Forms;

namespace PushNotifications.Droid
{
	[Service] // Must use the service tag
	public class PushHandlerService : GcmServiceBase
	{
		public static string RegistrationID { get; private set; }
		private NotificationHub Hub { get; set; }

		public PushHandlerService() : base(NotificationConstants.SenderId)
		{
			Log.Info(NotificationBroadcastReceiver.TAG, "PushHandlerService() constructor");
		}

		protected override void OnMessage(Context context, Intent intent)
		{
			Log.Info(NotificationBroadcastReceiver.TAG, "GCM Message Received!");

			var msg = new StringBuilder();

			if (intent != null && intent.Extras != null)
			{
				foreach (var key in intent.Extras.KeySet())
					msg.AppendLine(key + "=" + intent.Extras.Get(key).ToString());
			}

			string messageText = intent.Extras.GetString("message");
			if (!string.IsNullOrEmpty(messageText))
			{
				createNotification("New hub message!", messageText);
			}
			else
			{
				createNotification("Unknown message details", msg.ToString());
			}
		}

		protected override void OnRegistered(Context context, string registrationId)
		{
			Log.Verbose(NotificationBroadcastReceiver.TAG, "GCM Registered: " + registrationId);
			RegistrationID = registrationId;

			//createNotification("PushHandlerService-GCM Registered...",
			//					"The device has been Registered!");

			Hub = new NotificationHub(NotificationConstants.HubName, NotificationConstants.ListenConnectionString,
										context);
			try
			{
				Hub.UnregisterAll(registrationId);
			}
			catch (Exception ex)
			{
				Log.Error(NotificationBroadcastReceiver.TAG, ex.Message);
			}

			//var tags = new List<string>() { "falcons" }; // create tags if you want
			var tags = new List<string>() { };

			try
			{
				var hubRegistration = Hub.Register(registrationId, tags.ToArray());
			}
			catch (Exception ex)
			{
				Log.Error(NotificationBroadcastReceiver.TAG, ex.Message);
			}
		}

		protected override void OnUnRegistered(Context context, string registrationId)
		{
			Log.Verbose(NotificationBroadcastReceiver.TAG, "GCM Unregistered: " + registrationId);

			//createNotification("GCM Unregistered...", "The device has been unregistered!");
		}

		protected override bool OnRecoverableError(Context context, string errorId)
		{
			Log.Warn(NotificationBroadcastReceiver.TAG, "Recoverable Error: " + errorId);

			return base.OnRecoverableError(context, errorId);
		}

		protected override void OnError(Context context, string errorId)
		{
			Log.Error(NotificationBroadcastReceiver.TAG, "GCM Error: " + errorId);
		}

		void createNotification(string title, string desc)
		{
			//Create notification
			var notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;

			//Create an intent to show UI
			var uiIntent = new Intent(this, typeof(MainActivity));

			//Create the notification
			var notification = new Notification(Android.Resource.Drawable.SymActionEmail, title);

			//Auto-cancel will remove the notification once the user touches it
			notification.Flags = NotificationFlags.AutoCancel;

			//Set the notification info
			//we use the pending intent, passing our ui intent over, which will get called
			//when the notification is tapped.
			notification.SetLatestEventInfo(this, title, desc, PendingIntent.GetActivity(this, 0, uiIntent, 0));

			//Show the notification
			notificationManager.Notify(1, notification);
			dialogNotify(title, desc);
		}

		protected void dialogNotify(String title, String message)
		{

			MainActivity.instance.RunOnUiThread(() =>
			{
				AlertDialog.Builder dlg = new AlertDialog.Builder(MainActivity.instance);
				AlertDialog alert = dlg.Create();
				alert.SetTitle(title);
				alert.SetButton("Ok", delegate
				{
					/*Puedes agregar el using Xamarin.Forms; y usar las instrucciones de forms
					 * desde el proyecto android de la siguiente forma para ir a una pantalla
					 * navegando de la forma normal*/
					//App.Navigation.PushAsync(new MyOtherPage());//Normal
					/*O usando de la forma de ventana modal
					//App.Navigation.PushModalAsync(new MyOtherPage());//Modal*/

					/*Tambien puedes crear una clase en tu PCL y crear ahi los metodos para
					manejar las notificiaciones*/

					PushNotifications.RecibioNotificacion(message);
					alert.Dismiss();
				});
				alert.SetMessage(message);
				alert.Show();
			});
		}
	}
}
