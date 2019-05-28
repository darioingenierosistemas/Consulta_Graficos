using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Support.V7.App;

using Android.Support.Design.Widget;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace consulta_Ejecutiva.Actividades
{
    [Activity(Label = "ESTADISTICAS DE PATRULLAJE"
        , MainLauncher = true
        )]
    public class Act_Main : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Lay_Main);
			FloatingActionButton BtnBarChart = FindViewById<FloatingActionButton>(Resource.Id.BtnBarras);
			BtnBarChart.Click += BtnBarChart_Click;

		}

		private void BtnBarChart_Click(object sender, EventArgs e)
		{
			var intent = new Intent(this, typeof(Act_Grafico_BarChart));

			StartActivity(intent);
		}
	}
}