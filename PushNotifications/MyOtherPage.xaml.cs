using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace PushNotifications
{
	public partial class MyOtherPage : ContentPage
	{

		public MyOtherPage(string mensaje)
		{
			InitializeComponent();

			LblMensaje.Text = mensaje;
		}
	}
}
