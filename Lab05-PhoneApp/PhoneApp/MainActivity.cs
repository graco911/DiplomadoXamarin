using Android.App;
using Android.Widget;
using Android.OS;
using System;

namespace PhoneApp
{
    [Activity(Label = "Phone App", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        static readonly System.Collections.Generic.List<string> PhoneNumbers = new System.Collections.Generic.List<string>();
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            var PhoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);
            var TranslateButton = FindViewById<Button>(Resource.Id.TranslateButton);
            var CallButton = FindViewById<Button>(Resource.Id.CallButton);
            var CallHistoryButton = FindViewById<Button>(Resource.Id.CallHistoryButton);
            var Validate = FindViewById<Button>(Resource.Id.ValidateButton);

            CallButton.Enabled = false;

            var TranslateNumber = string.Empty;

            TranslateButton.Click += delegate
            {
                var Translator = new PhoneTranslator();
                TranslateNumber = Translator.ToNumber(PhoneNumberText.Text);
                if (string.IsNullOrWhiteSpace(TranslateNumber))
                {
                    CallButton.Text = "Llamar";
                    CallButton.Enabled = false;
                }
                else
                {
                    CallButton.Text = $"Llamar al {TranslateNumber}";
                    CallButton.Enabled = true;
                }
            };

            CallButton.Click += delegate
            {
                var CallDialog = new AlertDialog.Builder(this);
                CallDialog.SetMessage($"Llamar al numero {TranslateNumber}");
                CallDialog.SetNeutralButton("Llamar", delegate
                {

                    PhoneNumbers.Add(TranslateNumber);
                    CallHistoryButton.Enabled = true;


                    var CallIntent = new Android.Content.Intent(Android.Content.Intent.ActionCall);
                    CallIntent.SetData(Android.Net.Uri.Parse($"tel:{TranslateNumber}"));
                    StartActivity(CallIntent);
                });
                CallDialog.SetNegativeButton("Cancelar", delegate { });
                CallDialog.Show();
            };

            CallHistoryButton.Click += delegate
            {
                var Intent = new Android.Content.Intent(this, typeof(CallHistoryActivity));
                Intent.PutStringArrayListExtra("phone_numbers", PhoneNumbers);
                StartActivity(Intent);
            };

            Validate.Click += delegate
            {
                var ValidateIntent = new Android.Content.Intent(this, typeof(ValidateActivity));
                StartActivity(ValidateIntent);
            };

        }

    }
}

