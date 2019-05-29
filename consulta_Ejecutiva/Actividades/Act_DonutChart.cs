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
using Microcharts;
using Microcharts.Droid;
using SkiaSharp;

namespace consulta_Ejecutiva.Actividades
{
    [Activity(Label = "Act_DonutChart"
         //, MainLauncher =true
         )]
    public class Act_DonutChart : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Lay_DonutChart);

            GraficaDonutChart();
        }

        private static string HexConverter()
        {
            Android.Graphics.Color c = new Android.Graphics.Color((int)(Java.Lang.Math.Random() * 0x1000000));
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }
        private void GraficaDonutChart()
        {
            var entries = new[]
            {
                new Entry(20)
                {
                Label = "Enero",
                ValueLabel = "20%",
                Color = SKColor.Parse(HexConverter())
                },

                new Entry(30)
                {
                Label = "Febrero",
                ValueLabel = "30%",
                Color = SKColor.Parse(HexConverter())
                },

                new Entry(40)
                {
                Label = "Mayo",
                ValueLabel = "40%",
                Color = SKColor.Parse(HexConverter())
                }
            };

            var charview = FindViewById<ChartView>(Resource.Id.DonutChart_);

            var chartviwewDonut = new DonutChart() { Entries = entries };
            charview.Chart = chartviwewDonut;
        }
    }
}