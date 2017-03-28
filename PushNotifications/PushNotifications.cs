using System;
using Xamarin.Forms;

namespace PushNotifications
{
	public static class PushNotifications
	{
		public static void RecibioNotificacion(string mensaje)
		{
			try
			{
				Device.BeginInvokeOnMainThread(async () =>
				{
					/*para ir a una pantalla
					 * navegando de la forma normal*/
					await App.Navigation.PushAsync(new MyOtherPage(mensaje));//Normal
					/*O usando de la forma de ventana modal
					//await App.Navigation.PushModalAsync(new MyOtherPage());//Modal*/
				});
			}
			catch { }
		}
	}
}
