
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Android.Util;
using Gcm.Client;
using WindowsAzure.Messaging;

[assembly: Permission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]

//GET_ACCOUNTS is needed only for Android versions 4.0.3 and below
[assembly: UsesPermission(Name = "android.permission.GET_ACCOUNTS")]
[assembly: UsesPermission(Name = "android.permission.INTERNET")]
[assembly: UsesPermission(Name = "android.permission.WAKE_LOCK")]

namespace PushNotifications.Droid
{
	[BroadcastReceiver(Permission = Gcm.Client.Constants.PERMISSION_GCM_INTENTS)]
	[IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_MESSAGE },
	 Categories = new string[] { "@PACKAGE_NAME@" })]
	[IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK },
	 Categories = new string[] { "@PACKAGE_NAME@" })]
	[IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_LIBRARY_RETRY },
	 Categories = new string[] { "@PACKAGE_NAME@" })]
	public class NotificationBroadcastReceiver : GcmBroadcastReceiverBase<PushHandlerService>
	{
		public static string[] SENDER_IDS = new string[] { NotificationConstants.SenderId };

		public const string TAG = "NotificationBroadcastReceiver-GCM";
	}
	//public class NotificationBroadcastReceiver : GcmBroadcastReceiverBase<PushHandlerService>
	//{
	//	public override void OnReceive(Context context, Intent intent)
	//	{
	//		Toast.MakeText(context, "Received intent!", ToastLength.Short).Show();
	//	}
	//}
}
