using Android.App;
using Android.Widget;
using Android.OS;
using System;

namespace Lab14
{
    [Activity(Label = "Lab14", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        public Button Validar;
   
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            Validar = FindViewById<Button>(Resource.Id.buttonValidate);

            Validar.Click += delegate {

                ValidateAsync();

            };

        }

        private async void ValidateAsync()
        {
            var ServiceClient = new SALLab14.ServiceClient();

            var Result = await ServiceClient.ValidateAsync(this);
            //result = $"{Result.Status}\n{Result.Fullname}\n{Result.Token}";
            Android.App.AlertDialog.Builder Builder =
                new AlertDialog.Builder(this);
            AlertDialog Alert = Builder.Create();
            Alert.SetTitle("Resultado de la verificación");
            Alert.SetIcon(Resource.Drawable.Icon);
            Alert.SetMessage(
                $"{Result.Status}\n{Result.FullName}\n{Result.Token}");
            Alert.SetButton("Ok", (s, ev) => { });
            Alert.Show();

        }
    }
}

