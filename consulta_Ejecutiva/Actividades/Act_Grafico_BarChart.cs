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
		//, MainLauncher = true
		)]
	public class Act_Grafico_BarChart : Activity
	{
		ObservableCollection<ChartData> Data2 = new ObservableCollection<ChartData>();
		ObservableCollection<ChartData> Data3 = new ObservableCollection<ChartData>();
		private string cod_contratista;
		private string cantidad_meses;
		private string nombre_contratista;
		


		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			cod_contratista = Intent.GetStringExtra("Contratista");
			cantidad_meses = Intent.GetStringExtra("Mes");
			nombre_contratista = Intent.GetStringExtra("NomContratista");
			GetDataBarChart();


		}

		private async void GetDataBarChart()
		{    
			// url para el boton de 1 mes
			string urlBarChart = URLs.ConMes1 + cod_contratista + URLs.ConMes1y1 + cantidad_meses;
			var resultado = await urlBarChart.GetRequest<List<TABLA_MES1>>();
			
			// url para el boton de 6 meses
			string urlBarcharMeses = URLs.ConMeses6 + cod_contratista + URLs.ConMeses6y1 + cantidad_meses;
			var resultadoBtnMeses = await urlBarcharMeses.GetRequest<List<TABLA_MESES>>();
			
			Window.RequestFeature(WindowFeatures.NoTitle);

			SfChart chart = new SfChart(this);
			chart.Title.Text ="CONTRATISTA: " + nombre_contratista;
			chart.Title.Typeface =  Typeface.DefaultBold;
			chart.SetBackgroundColor(Color.White);

			ChartZoomPanBehavior zoomPanBehavior = new ChartZoomPanBehavior();
			chart.Behaviors.Add(zoomPanBehavior);
			zoomPanBehavior.ZoomMode = ZoomMode.X;
			zoomPanBehavior.SelectionZoomingEnabled = true;

			//Inicializando Semanas 
			CategoryAxis primaryAxis = new CategoryAxis();
			chart.PrimaryAxis = primaryAxis;

			//scroll
			//chart.PrimaryAxis = new CategoryAxis()
			//{
			//	AutoScrollingDelta = 6,

			//	AutoScrollingMode = ChartAutoScrollingMode.Start
			//};

			//Inicializando Longitud 
			NumericalAxis secondaryAxis = new NumericalAxis();
			secondaryAxis.Title.Text = "Longitud  (mt) ";
			chart.SecondaryAxis = secondaryAxis;
		
			if (cantidad_meses == "4")
			{
				foreach(var semanas in resultado)

				{
					Data2.Add(new ChartData { Name = "Semana " + semanas.SEMANA, Height = semanas.LONGITUD_ASIGNADA });
					Data3.Add(new ChartData { Name = "Semana " + semanas.SEMANA, Height = semanas.LONGITUD_PATRULLADA });
					chart.Legend.Title.Text = "Año "+ semanas.ANHO;
					primaryAxis.Title.Text = "Semanas";
				}
			}
			else if (cantidad_meses == "6"  || cantidad_meses =="12")
			{
				foreach (var semanasMes in resultadoBtnMeses)
				{
					Data2.Add(new ChartData { Name = "Mes " + semanasMes.MES, Height = semanasMes.LONGITUD_ASIGNADA });
					Data3.Add(new ChartData { Name = "Mes " + semanasMes.MES, Height = semanasMes.LONGITUD_PATRULLADA });
					chart.Legend.Title.Text = "Año " + semanasMes.ANHO;
					primaryAxis.Title.Text = "Meses";

				}

			}
		
			ColumnSeries seriesBar = new ColumnSeries();
			seriesBar.ItemsSource = Data2;
			seriesBar.XBindingPath = "Name";
			seriesBar.YBindingPath = "Height";
			seriesBar.Label = "Longitud Asignada";
			// muestra los  valores que contiene la grafica
			//seriesBar.DataMarker.ShowLabel = true;
			seriesBar.TooltipEnabled = true;
			seriesBar.DataPointSelectionEnabled = true;
			//seriesBar.SelectedDataPointIndex = 2;
			seriesBar.SelectedDataPointColor = Color.Red;
			//	var colors = new List<Color>();
			//	colors.Add(Color.ParseColor("#094AC3"));

			ColumnSeries series = new ColumnSeries();
			series.ItemsSource = Data3;
			series.XBindingPath = "Name";
			series.YBindingPath = "Height";
			series.Label = "Longitud Patrullada";
			//series.DataMarker.ShowLabel = true;
			series.TooltipEnabled = true;
			series.DataPointSelectionEnabled = true;
			//series.SelectedDataPointIndex = 2;
			series.SelectedDataPointColor = Color.Red;
			//series.Color = Color.Black;

			chart.Series.Add(seriesBar);
			chart.Series.Add(series);
			chart.Legend.Visibility = Visibility.Visible;
			SetContentView(chart);
		
		}
	}

	public class ChartData
	{
		public string Name { get; set; }

		public double Height { get; set; }
	}



}