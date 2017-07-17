using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;

namespace Lab08
{
    [Activity(Label = "@string/ApplicationName", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //var ViewGroup = (Android.Views.ViewGroup)
            //    Window.DecorView.FindViewById(Android.Resource.Id.Content);

            //var MainLayout = ViewGroup.GetChildAt(0) as LinearLayout;

            //var HeaderImage = new ImageView(this);

            //HeaderImage.SetImageResource(Resource.Drawable.Xamarin_Diplomado_30);

            //MainLayout.AddView(HeaderImage);

            //var UserNameTextView = new TextView(this);

            //UserNameTextView.Text = GetString(Resource.String.UserName);

            //MainLayout.AddView(UserNameTextView);

            Validate();



        }

        private async void Validate()
        {

            var TextName = FindViewById<TextView>(Resource.Id.UserNameValue);
            var TextStatus = FindViewById<TextView>(Resource.Id.StatusValue);
            var TextToken = FindViewById<TextView>(Resource.Id.TokenValue);

            var ServiceClient = new SALLab08.ServiceClient();
            var StudentEmail = "";
            var Password = "";
            var myDevice = Android.Provider.Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId);

            
            var Result = await ServiceClient.ValidateAsync(StudentEmail, Password, myDevice);
            //result = $"{Result.Status}\n{Result.Fullname}\n{Result.Token}";
            var status = Result.Status;
            if (status.Equals(SALLab08.Status.Success)){
                TextStatus.Text = "Success";
            }
        else{
                TextStatus.Text = "Unsuccess";
            }
            TextName.Text = Result.Fullname;
           
            TextToken.Text = Result.Token;


        }
    }
}

