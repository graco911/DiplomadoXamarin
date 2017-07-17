using Android.App;
using Android.Widget;
using Android.OS;

namespace AndroidAPP
{

    
    [Activity(Label = "AndroidAPP", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Validate();

            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);
        }


        private async void Validate()
        {

            string StudentEmail = "";
            string Password = "";
            string myDevice = Android.Provider.Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId);

            SALLab02.ServiceClient ServiceClient = new SALLab02.ServiceClient();

            SALLab02.ResultInfo Result = await ServiceClient.ValidateAsync(StudentEmail, Password, myDevice);

            Android.App.AlertDialog.Builder Builder = new AlertDialog.Builder(this);
            AlertDialog Alert = Builder.Create();
            Alert.SetTitle("Resultado de la verificación");
            Alert.SetIcon(Resource.Drawable.Icon);
            Alert.SetMessage($"{Result.Status}\n{Result.Fullname}\n{Result.Token}");
            Alert.SetButton("ok", (s, ev) => { });
            Alert.Show();

        }

    }
}

