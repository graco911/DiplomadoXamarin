using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;

namespace Lab13
{
    [Activity(Label = "Lab13", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            var validate = FindViewById<ImageButton>(Resource.Id.ValidateButton);
            validate.Click += delegate
            {
                ValidarApp();
            };

        }


        public async void ValidarApp()
        {
            var Client = new SALLab13.ServiceClient();
            string Email = "";
            string Password = "";
            var Result = await Client.ValidateAsync(this, Email, Password);

            Android.App.AlertDialog.Builder Builder = new AlertDialog.Builder(this);
            AlertDialog Alert = Builder.Create();
            Alert.SetTitle("Resultado de la Verificación");
            Alert.SetIcon(Resource.Drawable.Icon);
            Alert.SetMessage(
                $"{Result.Status}\n{Result.FullName}\n{Result.Token}");
            Alert.SetButton("Ok", (s, ev) => { });
            Alert.Show();
        }
    }
}

