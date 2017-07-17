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
using HackAtHome.Entities;
using Newtonsoft.Json;
using Android.Text;
using Square.Picasso;

namespace HackAtHomeClient
{
    [Activity(Label = "@string/ApplicationName", Icon = "@drawable/ic_hackathome")]
    public class DetalleEvidenciasActivity : Activity
    {
        EvidenceDetail EvidenceDetail;
        Evidence Evidence;
        TextView Nombre;
        TextView Evidencia;
        TextView StatusEvidencia;
        TextView EvidenceDescription;
        ImageView ImageEvidence;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.DetalleEvidencias);

            Nombre = FindViewById<TextView>(Resource.Id.NombreUsuario2);
            Evidencia = FindViewById<TextView>(Resource.Id.textViewEvidencia);
            StatusEvidencia = FindViewById<TextView>(Resource.Id.textViewStatus);
            EvidenceDescription = FindViewById<TextView>(Resource.Id.textViewDescription);
            ImageEvidence = FindViewById<ImageView>(Resource.Id.imageViewEvidence);

            Evidence = JsonConvert.DeserializeObject<Evidence>(Intent.GetStringExtra("Evidence"));
            EvidenceDetail = JsonConvert.DeserializeObject<EvidenceDetail>(Intent.GetStringExtra("EvidenceDetail"));

            Nombre.Text = Intent.GetStringExtra("NombreUsuario");
            Evidencia.Text = Evidence.Title;
            StatusEvidencia.Text = Evidence.Status;
            EvidenceDescription.TextFormatted = Html.FromHtml(EvidenceDetail.Description);
            Picasso.With(this).Load(EvidenceDetail.Url).Into(ImageEvidence);




        }
    }
}