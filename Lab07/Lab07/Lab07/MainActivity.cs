using Android.App;
using Android.Widget;
using Android.OS;

namespace Lab07
{
    [Activity(Label = "Lab07", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            var ButtonValidate = FindViewById<Button>(Resource.Id.btnRegistro);

            ButtonValidate.Click += delegate
            {
                Validate();
            };


        }

        private async void Validate()
        {

            var TextEmail = FindViewById<EditText>(Resource.Id.editTextEmail);
            var TextPassword = FindViewById<EditText>(Resource.Id.editTextPassword);
            var TextSuccess = FindViewById<TextView>(Resource.Id.textInfo);

            var ServiceClient = new SALLab07.ServiceClient();
            var StudentEmail = TextEmail.Text;
            var Password = TextPassword.Text;
            var myDevice = Android.Provider.Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId);

            string result;
            var Result = await ServiceClient.ValidateAsync(StudentEmail, Password, myDevice);
            result = $"{Result.Status}\n{Result.Fullname}\n{Result.Token}";
            TextSuccess.Text = result;

            var Builder = new Notification.Builder(this)
                .SetContentTitle("Validación de la Actividad")
                .SetContentText(result)
                .SetSmallIcon(Resource.Drawable.Icon);

            Builder.SetCategory(Notification.CategoryMessage);

            var ObjectNotification = Builder.Build();
            var Manager = GetSystemService(
                Android.Content.Context.NotificationService) as NotificationManager;
            

            if(Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                Manager.Notify(0, ObjectNotification);
            }

        }
    }
}

