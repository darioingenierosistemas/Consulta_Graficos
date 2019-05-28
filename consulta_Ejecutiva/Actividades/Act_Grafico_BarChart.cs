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
using Entry = Microcharts.Entry;
using Microcharts.Droid;
using SkiaSharp;

namespace consulta_Ejecutiva.Actividades
{
	[Activity(Label = "Act_Grafico_BarChart"
		//, MainLauncher =true
		)]
	public class Act_Grafico_BarChart : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.Lay_Grafico_BarChart);

			GraficaBarChart();
		}

		private static string HexConverter()
		{
			Android.Graphics.Color c = new Android.Graphics.Color((int)(Java.Lang.Math.Random() * 0x1000000));
			return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
		}
		private void GraficaBarChart()
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

			var charview = FindViewById<ChartView>(Resource.Id.BarChart);

			var chartviwewBar = new BarChart()
            {
                Entries = entries,
                BarAreaAlpha = 130
            };

			charview.Chart = chartviwewBar;



		}
	}
}