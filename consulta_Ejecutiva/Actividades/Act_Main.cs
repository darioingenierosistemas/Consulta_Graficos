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

			FloatingActionButton BtnBarChart = FindViewById<FloatingActionButton>(Resource.Id.fabBarChart);
			BtnBarChart.Click += BtnBarChart_Click;

            FloatingActionButton btnLineChart = FindViewById<FloatingActionButton>(Resource.Id.fabLineChart);
            btnLineChart.Click += BtnLineChart_Click;

            // boton Grafica de torta     
            FloatingActionButton BtnDonutChart = FindViewById<FloatingActionButton>(Resource.Id.fabDonutChart);
            BtnDonutChart.Click += BtnDonutChart_Click;
        }

        private void BtnDonutChart_Click(object sender, EventArgs e)
        {
            var DonutChart_ = new Intent(this, typeof(Act_DonutChart));
            StartActivity(DonutChart_);
        }

        private void BtnBarChart_Click(object sender, EventArgs e)
		{
			var intent = new Intent(this, typeof(Act_Grafico_BarChart));
			StartActivity(intent);
		}

        private void BtnLineChart_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(Act_LineChart));
            StartActivity(intent);
        }
    }
}