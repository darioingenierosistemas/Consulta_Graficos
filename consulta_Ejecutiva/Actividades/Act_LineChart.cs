using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Syncfusion.Charts;
using consulta_Ejecutiva.BD;
using consulta_Ejecutiva.REST;

namespace consulta_Ejecutiva.Actividades
{
    [Activity(Label = "Act_LineChart")]
    public class Act_LineChart : Activity
    {
        private string codContratista;
        private string mes;
        ObservableCollection<ChartData> Data = new ObservableCollection<ChartData>();
        ObservableCollection<ChartData> Data2 = new ObservableCollection<ChartData>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            codContratista = Intent.GetStringExtra("Contratista");
            mes = Intent.GetStringExtra("Mes");

            GetData();
        }

        private async void GetData()
        {
            try
            {
                string url = URLs.ConMes1 + "1245" + URLs.ConMes1y1 + mes;
                var resultado = await url.GetRequest<List<TABLA_MES1>>();

                SfChart chart = new SfChart(this);

                CategoryAxis primaryAxis = new CategoryAxis();
                primaryAxis.Title.Text = "Semanas";
                chart.PrimaryAxis = primaryAxis;
                chart.PrimaryAxis.EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Shift;
                chart.PrimaryAxis.LabelsIntersectAction = AxisLabelsIntersectAction.MultipleRows;

                NumericalAxis secondaryAxis = new NumericalAxis();
                secondaryAxis.Title.Text = "Longitud";
                chart.SecondaryAxis = secondaryAxis;

                var meses = Convert.ToInt16(mes);
                for (int i = 0; i < meses; i++)
                {
                    Data.Add(new ChartData { Semanas = "Semana " + resultado[i].SEMANA, LongPatrullada = resultado[i].LONGITUD_PATRULLADA });

                    Data2.Add(new ChartData { Semanas = "Semana " + resultado[i].SEMANA, LongPatrullada = resultado[i].LONGITUD_ASIGNADA });
                }

                LineSeries lineSeries = new LineSeries()
                {
                    ItemsSource = Data,
                    XBindingPath = "Semanas",
                    YBindingPath = "LongPatrullada"
                };
                lineSeries.Label = "Longitud Patrullada";
                lineSeries.DataMarker.ShowLabel = true;
                lineSeries.TooltipEnabled = true;

                LineSeries lineSeries2 = new LineSeries()
                {
                    ItemsSource = Data2,
                    XBindingPath = "Semanas",
                    YBindingPath = "LongPatrullada"
                };
                lineSeries2.Label = "Longitud Asignada";
                lineSeries2.DataMarker.ShowLabel = true;
                lineSeries2.TooltipEnabled = true;

                chart.Series.Add(lineSeries);
                chart.Series.Add(lineSeries2);
                chart.Legend.Visibility = Visibility.Visible;

                SetContentView(chart);
            }
            catch (Exception ex)
            {
                Toast.MakeText(ApplicationContext, ex.ToString(), ToastLength.Long).Show();
            }
        }

        public class ChartData
        {
            public string Semanas { get; set; }
            public double LongPatrullada { get; set; }
        }
    }
}