using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using HackAtHome.Entities;
using HackAtHome.SAL;
using Newtonsoft;
using Newtonsoft.Json;
using System;

namespace HackAtHomeClient
{
    [Activity(Label = "@string/ApplicationName", MainLauncher = true, Icon = "@drawable/ic_hackathome")]
    public class MainActivity : Activity
    {
        Button Validar;
        public TextView Password;
        public TextView Email;

        ProgressDialog progress;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            Password = FindViewById<EditText>(Resource.Id.editTextPassword);
            Email = FindViewById<EditText>(Resource.Id.editTextEmail);

            progress = new Android.App.ProgressDialog(this);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetMessage("Espere...");
            progress.SetCancelable(false);

            Validar = FindViewById<Button>(Resource.Id.btnValidar);


            var ServiceClient = new ServiceClient();

            Validar.Click += delegate
            {
                //progress.Show();
                ValidateHackatHome();
                Validate();

            };
        }

        private async void ValidateHackatHome()
        {
            MicrosoftServiceClient MSC = new MicrosoftServiceClient();

            Notification.Builder builder = new Notification.Builder(this)
                .SetContentTitle("Resultado")
                .SetContentText("Evidencia Enviada.")
                .SetSmallIcon(Resource.Drawable.ic_hackathome);
            Notification notification = builder.Build();
            NotificationManager notificationManager =
                GetSystemService(Context.NotificationService) as NotificationManager;


            var MicrosoftEvidence = new LabItem
            {
                Email = Email.Text,
                Lab = "Hack@Home",
                DeviceId = Android.Provider.Settings.Secure.GetString(
                    ContentResolver, Android.Provider.Settings.Secure.AndroidId)
            };

                await MSC.SendEvidence(MicrosoftEvidence);
                //notificationManager.Notify(0, notification);          
        }

        private async void Validate()
        {

            var intent = new Intent(this, typeof(EvidenciasActivity));

            var ServiceClient = new ServiceClient();

            ResultInfo Result = await ServiceClient.AutenticateAsync(Email.Text, Password.Text);

            if (Result != null)
            {
                //progress.Hide();
                intent.PutExtra("Result", JsonConvert.SerializeObject(Result));
                StartActivity(intent);

            }
            else
            {
                progress.Hide();
                AlertDialog.Builder Builder = new AlertDialog.Builder(this);
                AlertDialog Alert = Builder.Create();
                Alert.SetTitle("Resultado de la Verificación");
                Alert.SetIcon(Android.Resource.Drawable.StatNotifyError);
                Alert.SetMessage("Ha ocurrido un error");
                Alert.SetButton("Ok", (s, ev) => { });
                //Alert.Show();
            }
        }
    }
}

