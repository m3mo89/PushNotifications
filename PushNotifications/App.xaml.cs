using Xamarin.Forms;

namespace PushNotifications
{
	public partial class App : Application
	{
		public static INavigation Navigation { get; set; }

		public App()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new PushNotificationsPage());


			App.Navigation = MainPage.Navigation;
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
