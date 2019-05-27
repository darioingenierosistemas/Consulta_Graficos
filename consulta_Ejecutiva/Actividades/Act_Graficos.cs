using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Support.V7.App;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microcharts;
using SkiaSharp;
using Entry = Microcharts.Entry;
using Microcharts.Droid;


namespace consulta_Ejecutiva.Actividades
{
    [Activity(Label = "Graficos"
        //, MainLauncher = true
        )]

    public class Act_Graficos : AppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Lay_Graficos);

            Graficos();

        }

        private static string HexConverter()
        {
            Android.Graphics.Color c = new Android.Graphics.Color((int)(Java.Lang.Math.Random() * 0x1000000));
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        private void Graficos()
        {
            
            var entries = new[]

        {
            new Entry(200)
            {
                Label = "January",
                ValueLabel = "200",
                Color = SKColor.Parse(HexConverter())
            }
        
        };

            var chartView = FindViewById<ChartView>(Resource.Id.chartView);
            var chartview2 = FindViewById<ChartView>(Resource.Id.Chart2);
            var chartview3 = FindViewById<ChartView>(Resource.Id.Chart3);
            var chartview4 = FindViewById<ChartView>(Resource.Id.Chart4);
            var chartview5 = FindViewById<ChartView>(Resource.Id.Chart5);
            var chartview6 = FindViewById<ChartView>(Resource.Id.Chart6);

            var chart = new RadialGaugeChart() { Entries = entries };
            var chart2 = new LineChart() { Entries = entries };
            var chart3 = new DonutChart() { Entries = entries };
            var chart4 = new PointChart() { Entries = entries };
            var chart5 = new RadarChart() { Entries = entries };
            var chart6 = new BarChart() { Entries = entries };

            chartView.Chart = chart;
            chartview2.Chart = chart2;
            chartview4.Chart = chart3;
            chartview3.Chart = chart4;
            chartview5.Chart = chart5;
            chartview6.Chart = chart6;
        }


    }
    
}