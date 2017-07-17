using Android.App;
using Android.Widget;
using Android.OS;

namespace Lab10
{
    [Activity(Label = "@string/ApplicationName", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int Counter = 0;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            /*var ContentHeader = FindViewById<TextView>(Resource.Id.ContentHeader);
            ContentHeader.Text = GetText(Resource.String.ContentHeader);

            var ClickMe = FindViewById<Button>(Resource.Id.clickMe);
            var ClickCounter = FindViewById<TextView>(Resource.Id.ClickCounter);*/

            /*ClickMe.Click += delegate
            {
                Counter++;
                ClickCounter.Text = Resources.GetQuantityString(Resource.Plurals.NumberOfClicks, Counter, Counter);

                var Player = Android.Media.MediaPlayer.Create(this, Resource.Raw.sound);
                Player.Start();
            };

            Android.Content.Res.AssetManager Manager = this.Assets;
            using (var Reader = new System.IO.StreamReader(Manager.Open("Contenido.txt")))
            {
                ContentHeader.Text += $"\n\n{Reader.ReadToEnd()}";
            }*/

            var ClickMe = FindViewById<Button>(Resource.Id.btnRegistro);

            ClickMe.Click += delegate
            {
                Validate();
            };
        }

        private async void Validate()
        {

            var TextStatus = FindViewById<TextView>(Resource.Id.textInfo);

            var ServiceClient = new SALLab10.ServiceClient();
            var StudentEmail = FindViewById<EditText>(Resource.Id.editTextEmail);
            //StudentEmail.SetText("graniel.cordova.carlosalberto@gmail.com",TextView.BufferType.Editable);
            var Password = FindViewById<EditText>(Resource.Id.editTextPassword);
            //Password.SetText("carlos1723062072", TextView.BufferType.Editable);
            var myDevice = Android.Provider.Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId);


            var Result = await ServiceClient.ValidateAsync(StudentEmail.Text, Password.Text, myDevice);
            TextStatus.Text = $"{Result.Status}\n{Result.Fullname}\n{Result.Token}";
           
        }
    }
}

