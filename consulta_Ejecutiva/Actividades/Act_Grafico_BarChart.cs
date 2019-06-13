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
		private string cod_general;
		private string cantidad_meses;
		private string nombre_general;
		private string flagGeneral;




		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			cod_general = Intent.GetStringExtra("CodSelect");
			cantidad_meses = Intent.GetStringExtra("Mes");
			nombre_general = Intent.GetStringExtra("NomSelect");
			flagGeneral = Intent.GetStringExtra("Flag");
			GetDataBarChart();


		}

		private async void GetDataBarChart()
		{

			Window.RequestFeature(WindowFeatures.NoTitle);

			SfChart chart = new SfChart(this);
			chart.Title.Text = nombre_general;
			chart.Title.Typeface = Typeface.DefaultBold;
			chart.SetBackgroundColor(Color.White);


			ChartZoomPanBehavior zoomPanBehavior = new ChartZoomPanBehavior();
			chart.Behaviors.Add(zoomPanBehavior);

			//Inicializando Semanas 
			CategoryAxis primaryAxis = new CategoryAxis();
			chart.PrimaryAxis = primaryAxis;

			//Inicializando Longitud 
			NumericalAxis secondaryAxis = new NumericalAxis();
			secondaryAxis.Title.Text = "Longitud(m) ";
			chart.SecondaryAxis = secondaryAxis;
			if (flagGeneral == "Contratista")
			{

				if (cantidad_meses == "4")
				{
					string urlBarChart = URLs.ConMes1 + cod_general + URLs.ConMes1y1 + cantidad_meses;
					var resultado = await urlBarChart.GetRequest<List<TABLA_MES1>>();
					foreach (var semanas in resultado)

					{
						Data2.Add(new ChartData { Name = "Semana " + semanas.SEMANA, Height = semanas.LONGITUD_ASIGNADA });
						Data3.Add(new ChartData { Name = "Semana " + semanas.SEMANA, Height = semanas.LONGITUD_PATRULLADA });
						chart.Legend.Title.Text = "Año " + semanas.ANHO;
						primaryAxis.Title.Text = "Semanas";
					}
				}
				else if (cantidad_meses == "6" || cantidad_meses == "12")
				{
					string urlBarcharMeses = URLs.ConMeses + cod_general + URLs.ConMesesy1 + cantidad_meses;
					var resultadoBtnMeses = await urlBarcharMeses.GetRequest<List<TABLA_MESES>>();

					foreach (var semanasMes in resultadoBtnMeses)
					{
						Data2.Add(new ChartData { Name = "Mes " + semanasMes.MES, Height = semanasMes.LONGITUD_ASIGNADA });
						Data3.Add(new ChartData { Name = "Mes " + semanasMes.MES, Height = semanasMes.LONGITUD_PATRULLADA });
						chart.Legend.Title.Text = "Año " + semanasMes.ANHO;
						primaryAxis.Title.Text = "Meses";

					}

				}
			}
			else if (flagGeneral == "Departamento")
			{
				if (cantidad_meses == "4")
				{
					string urlBarcharDep = URLs.LonDepMes1 + cod_general + URLs.LonDepMes1y1 + cantidad_meses;
					var resultadoBtnDepSemanas = await urlBarcharDep.GetRequest<List<TABLA_MES1>>();

					foreach (var semanaDep in resultadoBtnDepSemanas)
					{
						Data2.Add(new ChartData { Name = "Semana " + semanaDep.SEMANA, Height = semanaDep.LONGITUD_ASIGNADA });
						Data3.Add(new ChartData { Name = "Semana " + semanaDep.SEMANA, Height = semanaDep.LONGITUD_PATRULLADA });
						chart.Legend.Title.Text = "Año " + semanaDep.ANHO;
						primaryAxis.Title.Text = "Semanas";
					}
				}
				else if (cantidad_meses == "6" || cantidad_meses == "12")
				{
					string urlBarcharDepMes = URLs.LonDepMeses + cod_general + URLs.LonDepMesesy1 + cantidad_meses;
					var resultadoBtnDepMes = await urlBarcharDepMes.GetRequest<List<TABLA_MESES>>();

					foreach (var semanaDepmes in resultadoBtnDepMes)
					{
						Data2.Add(new ChartData { Name = "Mes " + semanaDepmes.MES, Height = semanaDepmes.LONGITUD_ASIGNADA });
						Data3.Add(new ChartData { Name = "Mes " + semanaDepmes.MES, Height = semanaDepmes.LONGITUD_PATRULLADA });
						chart.Legend.Title.Text = "Año " + semanaDepmes.ANHO;
						primaryAxis.Title.Text = "Meses";
					}
				}

			}
			else if (flagGeneral == "Unidad")
			{
				if (cantidad_meses == "4")
				{
					string urlBarcharUnisemana = URLs.LonUniMes1 + cod_general + URLs.LonUniMes1y1 + cantidad_meses;
					var resultadoBtnUniSemanas = await urlBarcharUnisemana.GetRequest<List<TABLA_MES1>>();

					foreach (var semanaUni in resultadoBtnUniSemanas)
					{
						Data2.Add(new ChartData { Name = "Semana " + semanaUni.SEMANA, Height = semanaUni.LONGITUD_ASIGNADA });
						Data3.Add(new ChartData { Name = "Semana " + semanaUni.SEMANA, Height = semanaUni.LONGITUD_PATRULLADA });
						chart.Legend.Title.Text = "Año " + semanaUni.ANHO;
						primaryAxis.Title.Text = "Semanas";
					}
				}
				else if (cantidad_meses == "6" || cantidad_meses == "12")
				{
					string urlBtnchartUnimes = URLs.LonUniMeses + cod_general + URLs.LonUniMesesy1 + cantidad_meses;
					var resultadoBtnUniMes = await urlBtnchartUnimes.GetRequest<List<TABLA_MESES>>();

					foreach (var semanaUniMes in resultadoBtnUniMes)
					{
						Data2.Add(new ChartData { Name = "Mes " + semanaUniMes.MES, Height = semanaUniMes.LONGITUD_ASIGNADA });
						Data3.Add(new ChartData { Name = "Mes " + semanaUniMes.MES, Height = semanaUniMes.LONGITUD_PATRULLADA });
						chart.Legend.Title.Text = "Año " + semanaUniMes.ANHO;
						primaryAxis.Title.Text = "Meses";
					}
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
			
			ColumnSeries series = new ColumnSeries();
			series.ItemsSource = Data3;
			series.XBindingPath = "Name";
			series.YBindingPath = "Height";
			series.Label = "Longitud Patrullada";
			series.TooltipEnabled = true;
			
			// probando esto
			chart.SideBySideSeriesPlacement = true;

			chart.Enabled = true;
			chart.Series.Add(seriesBar);
			chart.Series.Add(series);
			chart.Legend.Visibility = Visibility.Visible;
			SetContentView(chart);

		}

        public override void OnBackPressed()
        {

            var intent = new Intent(this, typeof(Act_Main));
            StartActivity(intent);

        }

    }

	public class ChartData
	{
		public string Name { get; set; }

		public double Height { get; set; }

	}



}