using Android.App;
using Android.Widget;
using Android.OS;

namespace Lab11
{
    [Activity(Label = "Lab11", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Complex Data;
        int counter = 0;
        public TextView TextStatus;
        public Button ClickCounter;
        public string texto;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Android.Util.Log.Debug("Lab11Log:", "Activity A - OnCreate");

            TextStatus = FindViewById<TextView>(Resource.Id.textInfo);

            var ServiceClient = new SALLab11.ServiceClient();
            var StudentEmail = "";
            var Password = "";
            var myDevice = Android.Provider.Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId);

            ClickCounter = FindViewById<Button>(Resource.Id.ClicksCounter);

            Data = (Complex)this.FragmentManager.FindFragmentByTag("Data");
            if(Data == null)
            {
                Data = new Complex();
                var FragmentTransaction = this.FragmentManager.BeginTransaction();
                FragmentTransaction.Add(Data, "Data");
                FragmentTransaction.Commit();
                Data.Resultado = await ServiceClient.ValidateAsync(StudentEmail, Password, myDevice);
                
            }


            TextStatus.Text = $"{Data.Resultado.Status}\n{Data.Resultado.Fullname}\n{Data.Resultado.Token}";


            if (bundle != null)
            {
                counter = bundle.GetInt("CounterValue", 0);
                Android.Util.Log.Debug("Lab11Log", "Activity A - Recovered Instance State");
            }

            ClickCounter.Text = Resources.GetString(Resource.String.ClicksCounter_Text, counter);

            ClickCounter.Text += $"\n{Data.ToString()}";

            ClickCounter.Click += delegate {
                counter++;
                ClickCounter.Text = Resources.GetString(Resource.String.ClicksCounter_Text, counter);

                Data.Real++;
                Data.Imaginary++;

                ClickCounter.Text += $"\n{Data.ToString()}";
            };

            Android.Util.Log.Debug("Lab11Log:", "Activity A - OnCreate");

            FindViewById<Button>(Resource.Id.StartActivity).Click += delegate
            {
                var ActivityIntent =
                    new Android.Content.Intent(this, typeof(SecondActivity));
                StartActivity(ActivityIntent);
            };
        }

        protected override void OnStart()
        {
            Android.Util.Log.Debug("Lab11Log;", "Activity A - OnStart");
            base.OnStart();
        }

        protected override void OnResume()
        {
            Android.Util.Log.Debug("Lab11Log;", "Activity A - OnResume");
            base.OnStart();
        }

        protected override void OnPause()
        {
            Android.Util.Log.Debug("Lab11Log;", "Activity A - OnPause");
            base.OnStart();
        }

        protected override void OnStop()
        {
            Android.Util.Log.Debug("Lab11Log;", "Activity A - OnStop");
            base.OnStart();
        }

        protected override void OnDestroy()
        {
            Android.Util.Log.Debug("Lab11Log;", "Activity A - OnDestroy");
            base.OnStart();
        }

        protected override void OnRestart()
        {
            Android.Util.Log.Debug("Lab11Log;", "Activity A - OnRestart");
            base.OnStart();
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutInt("CounterValue", counter);
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnSaveInstanceState");

            base.OnSaveInstanceState(outState);
        }

        private async void Validate()
        {
            
            var ServiceClient = new SALLab11.ServiceClient();
            var StudentEmail = "";
            var Password = "";
            var myDevice = Android.Provider.Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId);

            var txtResult = await ServiceClient.ValidateAsync(StudentEmail, Password, myDevice);
            TextStatus.Text = $"{txtResult.Status}\n{txtResult.Fullname}\n{txtResult.Token}";

        }

    }
}

