using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PhoneApp
{
    [Activity(Label = "ValidateActivity")]
    public class ValidateActivity : Activity
    {
        // Create your application here

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.validate);

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

            var ServiceClient = new SALLab06.ServiceClient();
            var StudentEmail = TextEmail.Text;
            var Password = TextPassword.Text;
            var myDevice = Android.Provider.Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId);

            string result;
            var Result = await ServiceClient.ValidateAsync(StudentEmail, Password, myDevice);
            result = $"{Result.Status}\n{Result.Fullname}\n{Result.Token}";
            TextSuccess.Text = result;

        }
    }
}