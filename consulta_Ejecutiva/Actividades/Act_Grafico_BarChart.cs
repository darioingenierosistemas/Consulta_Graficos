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
using Com.Syncfusion.Charts;
using System.Collections.ObjectModel;
using Android.Graphics;
using consulta_Ejecutiva.REST;
using consulta_Ejecutiva.BD;

namespace consulta_Ejecutiva.Actividades
{
	[Activity(Label = "Act_Grafico_BarChart"
		, MainLauncher = true
		)]
	public class Act_Grafico_BarChart : Activity
	{
		ObservableCollection<ChartData> Data2 = new ObservableCollection<ChartData>();
		ObservableCollection<ChartData> Data3 = new ObservableCollection<ChartData>();

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			GetDataBarChart();


		}

		private async void GetDataBarChart()
		{
			string urlBarChart = URLs.ConMes1 + "1245" + URLs.ConMes1y1 + "4";
			var resultado = await urlBarChart.GetRequest<List<TABLA_MES1>>();
			

			Window.RequestFeature(WindowFeatures.NoTitle);




			SfChart chart = new SfChart(this);
			chart.Title.Text = "Chart";
			chart.SetBackgroundColor(Color.White);

			//Inicializando Semanas 
			CategoryAxis primaryAxis = new CategoryAxis();
			primaryAxis.Title.Text = "Semanas";
			chart.PrimaryAxis = primaryAxis;

			//Inicializando Longitud 
			NumericalAxis secondaryAxis = new NumericalAxis();
			secondaryAxis.Title.Text = "Longitud (mt) ";
			chart.SecondaryAxis = secondaryAxis;

			for (int i = 0; i < 4; i++)
			{
				Data2.Add(new ChartData { Name = "Semana " + resultado[i].SEMANA + "/" +  resultado[i].ANHO, Height = resultado[i].LONGITUD_ASIGNADA });

				Data3.Add(new ChartData { Name = "Semana " + resultado[i].SEMANA + "/" + resultado[i].ANHO, Height = resultado[i].LONGITUD_PATRULLADA });
			}

			ColumnSeries seriesBar = new ColumnSeries();
			seriesBar.ItemsSource = Data2;
			seriesBar.XBindingPath = "Name";
			seriesBar.YBindingPath = "Height";
			seriesBar.Label = "Longitud Asignada";
			seriesBar.DataMarker.ShowLabel = true;
			seriesBar.TooltipEnabled = true;
			//seriesBar.Color = Color.Blue;


			ColumnSeries series = new ColumnSeries();
			series.ItemsSource = Data3;
			series.XBindingPath = "Name";
			series.YBindingPath = "Height";
			series.Label = "Longitud Patrullada";
			series.DataMarker.ShowLabel = true;
			series.TooltipEnabled = true;
			series.Color = Color.Black;
			


			chart.Series.Add(seriesBar);
			chart.Series.Add(series);
			chart.Legend.Visibility = Visibility.Visible;
			SetContentView(chart);
			//}

		}
	}


	public class ChartData
	{
		public string Name { get; set; }

		public double Height { get; set; }
	}



}