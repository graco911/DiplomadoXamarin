using System;
using Android.App;
using Android.OS;
using Android.Widget;
using HackAtHome.CustomAdapters;
using HackAtHome.SAL;
using static Android.Widget.AdapterView;
using HackAtHome.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using Android.Content;
using Android.Content.Res;

namespace HackAtHomeClient
{
    [Activity(Label = "@string/ApplicationName", Icon = "@drawable/ic_hackathome")]
    public class EvidenciasActivity : Activity
    {
        ServiceClient ServiceEvidence = new ServiceClient();
        EvidencesAdapter Adapter;
        ListView ListView;
        TextView Nombre;
        ProgressDialog progress;
        ResultInfo Result;
        List<Evidence> ListEvidence = new List<Evidence>();
        int layout;
        int[] item;
        IParcelable state;
        DataAdapter Data;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Result = JsonConvert.DeserializeObject<ResultInfo>(Intent.GetStringExtra("Result"));

            Data = (DataAdapter)this.FragmentManager.FindFragmentByTag("Data");

            if (Data == null)
            {
                Data = new DataAdapter();
                ListEvidence = await ServiceEvidence.GetEvidencesAsync(Result.Token);
                Data.list = ListEvidence;
                var FragmentTransaction = this.FragmentManager.BeginTransaction();
                FragmentTransaction.Add(Data, "Data");
                FragmentTransaction.Commit();
                
            }

            if(state != null)
            {
                ListView.OnRestoreInstanceState(state);
            }

            // Create your application here
            SetContentView(Resource.Layout.Evidencias);

            progress = new Android.App.ProgressDialog(this);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetMessage("Espere...");
            progress.SetCancelable(false);

            ListView = FindViewById<ListView>(Resource.Id.listViewEvidencias);

            Nombre = FindViewById<TextView>(Resource.Id.NombreUsuario);

            if (IsLandScape(this))
            {
                layout = Resource.Layout.ListItemPort;
                item = new int[] { Resource.Id.textView1, Resource.Id.textView2 };

            }
            else
            {
                layout = Android.Resource.Layout.SimpleListItem2;
                item = new int[] { Android.Resource.Id.Text1, Android.Resource.Id.Text2 };
            }


            Adapter = new EvidencesAdapter(this, Data.list, layout, item[0], item[1]);

            ListView.Adapter = Adapter;

            ListView.ItemClick += OnItemClick;

            Nombre.Text = Result.FullName.ToString();

        }

        private bool IsLandScape(Activity activity)
        {
            var Orientation = activity.WindowManager.DefaultDisplay.Rotation;

            return Orientation == Android.Views.SurfaceOrientation.Rotation90 || Orientation == Android.Views.SurfaceOrientation.Rotation270;

        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            state = ListView.OnSaveInstanceState();
        }

        private async void OnItemClick(object sender, ItemClickEventArgs e)
        {
            progress.Show();
            var intent = new Intent(this, typeof(DetalleEvidenciasActivity));
            var Evidence = await ServiceEvidence.GetEvidenceByIDAsync(Result.Token, ListEvidence[e.Position].EvidenceID);
            
            if(Evidence != null)
            {
                intent.PutExtra("EvidenceDetail", JsonConvert.SerializeObject(Evidence));
                intent.PutExtra("Evidence", JsonConvert.SerializeObject(ListEvidence[e.Position]));
                intent.PutExtra("NombreUsuario", Result.FullName);
                StartActivity(intent);
                progress.Hide();
            }
        }

        protected override void OnPause()
        {
            base.OnPause();
            state = ListView.OnSaveInstanceState();
        }

        public override void OnBackPressed()
        {
            new AlertDialog.Builder(this)
    .SetIcon(Android.Resource.Drawable.IcDialogAlert)
    .SetTitle("Cerrando Hack@Home")
    .SetMessage("Estas seguro que quieres salir de la aplicación")
    .SetPositiveButton("Si", (s, ev) => 
    {
        base.OnBackPressed();
    })
    .SetNegativeButton("No", (s, ev) => { })
    .Show();
    }

    }
}