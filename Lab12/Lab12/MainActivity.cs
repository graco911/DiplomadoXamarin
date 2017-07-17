using Android.App;
using Android.Widget;
using Android.OS;
using SALLab12;

namespace Lab12
{
    [Activity(Label = "Lab12", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        public TextView TextStatus;

        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            TextStatus = FindViewById<TextView>(Resource.Id.textInfo);

            var ListColors = FindViewById<ListView>(Resource.Id.listView1);
            ListColors.Adapter = new CustomAdapters.ColorAdapter(
                this, Resource.Layout.ListItem, Resource.Id.textView1,
                Resource.Id.textView2, Resource.Id.imageView1);

            var ServiceClient = new SALLab12.ServiceClient();
            var StudentEmail = "";
            var Password = "";
            var myDevice = Android.Provider.Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId);

            var result = await ServiceClient.ValidateAsync(StudentEmail, Password, myDevice);

            TextStatus.Text = $"{result.Status}\n{result.FullName}\n{result.Token}";
            
        }

        public async void Validate()
        {

            var ServiceClient = new SALLab12.ServiceClient();
            var StudentEmail = "caco572@gmail.com";
            var Password = "carlos1723062072";
            var myDevice = Android.Provider.Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId);

            var result =  await ServiceClient.ValidateAsync(StudentEmail, Password, myDevice);
            if(result != null)
            {
                TextStatus.Text = $"{result.Status}\n{result.FullName}\n{result.Token}";
            }
        
        }
    }
}

