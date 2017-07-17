using Android.App;
using Android.Widget;
using Android.OS;

namespace Lab09
{
    [Activity(Label = "Lab09", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);
            Validate();
        }

        private async void Validate()
        {

            var TextName = FindViewById<TextView>(Resource.Id.UserNameValue);
            var TextStatus = FindViewById<TextView>(Resource.Id.StatusValue);
            var TextToken = FindViewById<TextView>(Resource.Id.TokenValue);

            var ServiceClient = new SALLab09.ServiceClient();
            var StudentEmail = "";
            var Password = "";
            var myDevice = Android.Provider.Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId);


            var Result = await ServiceClient.ValidateAsync(StudentEmail, Password, myDevice);
            //result = $"{Result.Status}\n{Result.Fullname}\n{Result.Token}";
            var status = Result.Status;
            if (status.Equals(SALLab09.Status.Success))
            {
                TextStatus.Text = "Success";
            }
            else
            {
                TextStatus.Text = "Unsuccess";
            }
            TextName.Text = Result.Fullname;

            TextToken.Text = Result.Token;


        }
    }
}

