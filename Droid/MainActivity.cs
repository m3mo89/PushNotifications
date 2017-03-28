using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Android.Util;
using Gcm.Client;
namespace PushNotifications.Droid
{
	public static class NotificationConstants 
	{
		public const string SenderId = "<GoogleProjectNumber>";
		public const string ListenConnectionString = "<Listen connection string>";
		public const string HubName = "<hub name>";
	}

	[Activity(Label = "PushNotifications.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		private void RegisterGCM() 
		{
			GcmClient.CheckDevice(this);
			GcmClient.CheckManifest(this);

			GcmClient.Register(this, NotificationConstants.SenderId);
		}

		protected override void OnCreate(Bundle bundle)
		{
			instance = this;
			
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);

			this.RegisterGCM();

			LoadApplication(new App());
		}

		public static MainActivity instance;
	}
}
