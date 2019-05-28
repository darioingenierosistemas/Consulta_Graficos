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
    [Activity(Label = "Act_LineChart")]
    public class Act_LineChart : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Lay_LineChart);

            var entries = new[]
            {
                 new Entry(20)
                 {
                     Label = "Enero",
                     ValueLabel = "20%",
                     Color = SKColor.Parse("#2c3e50")
                 },
                 new Entry(30)
                 {
                     Label = "Febrero",
                     ValueLabel = "30%",
                     Color = SKColor.Parse("#77d065")
                 },
                 new Entry(40)
                 {
                     Label = "Mayo",
                     ValueLabel = "40%",
                     Color = SKColor.Parse("#b455b6")
                 }
            };

            ChartView cvLineChart = FindViewById<ChartView>(Resource.Id.cvLineChart);
            var chartViewLineChart = new LineChart()
            {
                Entries = entries,
                LineMode = LineMode.Straight,
                LineSize = 8,
                PointMode = PointMode.Square,
                PointSize = 18
            };

            cvLineChart.Chart = chartViewLineChart;
        }
    }
}