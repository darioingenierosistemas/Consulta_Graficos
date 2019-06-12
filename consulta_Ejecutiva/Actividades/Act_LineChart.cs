using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
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
        private string codSelect;
        private string nomSelect;
        private string mes;
        private string flag;

        private int anho;
        private string semMes;

        ObservableCollection<ChartData> Data = new ObservableCollection<ChartData>();
        ObservableCollection<ChartData> Data2 = new ObservableCollection<ChartData>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            codSelect = Intent.GetStringExtra("CodSelect");
            nomSelect = Intent.GetStringExtra("NomSelect");
            mes = Intent.GetStringExtra("Mes");
            flag = Intent.GetStringExtra("Flag");

            GetData();
        }

        private async void GetData()
        {
            try
            {
                if (flag == "Departamento")
                {
                    if (mes == "4")
                    {
                        string url = URLs.LonDepMes1 + codSelect + URLs.LonDepMes1y1 + mes;
                        var resultado = await url.GetRequest<List<TABLA_MES1>>();

                        foreach (var datos in resultado)
                        {
                            Data.Add(new ChartData { Semanas = "Semana " + datos.SEMANA, LongPatrullada = datos.LONGITUD_PATRULLADA });
                            Data2.Add(new ChartData { Semanas = "Semana " + datos.SEMANA, LongPatrullada = datos.LONGITUD_ASIGNADA });
                            anho = datos.ANHO;
                        }

                        semMes = "Semanas";
                        CreateLineChart();
                    }

                    else if (mes == "6" || mes == "12")
                    {
                        string url = URLs.LonDepMeses + codSelect + URLs.LonDepMesesy1 + mes;
                        var resultado = await url.GetRequest<List<TABLA_MESES>>();

                        foreach (var datos in resultado)
                        {
                            Data.Add(new ChartData { Semanas = "Mes " + datos.MES, LongPatrullada = datos.LONGITUD_PATRULLADA });
                            Data2.Add(new ChartData { Semanas = "Mes " + datos.MES, LongPatrullada = datos.LONGITUD_ASIGNADA });
                            anho = datos.ANHO;
                        }

                        semMes = "Meses";
                        CreateLineChart();
                    }
                }

                else if (flag == "Unidad")
                {
                    if (mes == "4")
                    {
                        string url = URLs.LonUniMes1 + codSelect + URLs.LonUniMes1y1 + mes;
                        var resultado = await url.GetRequest<List<TABLA_MES1>>();

                        foreach (var datos in resultado)
                        {
                            Data.Add(new ChartData { Semanas = "Semana " + datos.SEMANA, LongPatrullada = datos.LONGITUD_PATRULLADA });
                            Data2.Add(new ChartData { Semanas = "Semana " + datos.SEMANA, LongPatrullada = datos.LONGITUD_ASIGNADA });
                            anho = datos.ANHO;
                        }

                        semMes = "Semanas";
                        CreateLineChart();
                    }

                    else if (mes == "6" || mes == "12")
                    {
                        string url = URLs.LonUniMeses + codSelect + URLs.LonUniMesesy1 + mes;
                        var resultado = await url.GetRequest<List<TABLA_MESES>>();

                        foreach (var datos in resultado)
                        {
                            Data.Add(new ChartData { Semanas = "Mes " + datos.MES, LongPatrullada = datos.LONGITUD_PATRULLADA });
                            Data2.Add(new ChartData { Semanas = "Mes " + datos.MES, LongPatrullada = datos.LONGITUD_ASIGNADA });
                            anho = datos.ANHO;
                        }

                        semMes = "Meses";
                        CreateLineChart();
                    }
                }

                else if (flag == "Contratista")
                {
                    if (mes == "4")
                    {
                        string url = URLs.ConMes1 + codSelect + URLs.ConMes1y1 + mes;
                        var resultado = await url.GetRequest<List<TABLA_MES1>>();

                        foreach (var datos in resultado)
                        {
                            Data.Add(new ChartData { Semanas = "Semana " + datos.SEMANA, LongPatrullada = datos.LONGITUD_PATRULLADA });
                            Data2.Add(new ChartData { Semanas = "Semana " + datos.SEMANA, LongPatrullada = datos.LONGITUD_ASIGNADA });
                            anho = datos.ANHO;
                        }

                        semMes = "Semanas";
                        CreateLineChart();
                    }

                    else if (mes == "6" || mes == "12")
                    {
                        string url = URLs.ConMeses + codSelect + URLs.ConMesesy1 + mes;
                        var resultado = await url.GetRequest<List<TABLA_MESES>>();

                        foreach (var datos in resultado)
                        {
                            Data.Add(new ChartData { Semanas = "Mes " + datos.MES, LongPatrullada = datos.LONGITUD_PATRULLADA });
                            Data2.Add(new ChartData { Semanas = "Mes " + datos.MES, LongPatrullada = datos.LONGITUD_ASIGNADA });
                            anho = datos.ANHO;
                        }

                        semMes = "Meses";
                        CreateLineChart();
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(ApplicationContext, ex.ToString(), ToastLength.Long).Show();
            }
        }

        private void CreateLineChart()
        {
            SfChart chart = new SfChart(this);
            if (flag == "Unidad")
            {
                chart.Title.Text = flag.ToUpper() + " OPERATIVA: " + nomSelect;
            }
            else
            {
                chart.Title.Text = flag.ToUpper() + ": " + nomSelect;
            }
            chart.Title.Typeface = Typeface.DefaultBold;

            CategoryAxis primaryAxis = new CategoryAxis();
            primaryAxis.Title.Text = semMes;
            chart.PrimaryAxis = primaryAxis;
            chart.PrimaryAxis.EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Shift;
            chart.PrimaryAxis.LabelsIntersectAction = AxisLabelsIntersectAction.MultipleRows;

            NumericalAxis secondaryAxis = new NumericalAxis();
            secondaryAxis.Title.Text = "Longitud(m)";
            chart.SecondaryAxis = secondaryAxis;

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

            chart.Series.Add(lineSeries2);
            chart.Series.Add(lineSeries);
            chart.Legend.Title.Text = "Año " + anho;
            chart.Legend.Visibility = Visibility.Visible;

            ChartZoomPanBehavior zoomPanBehavior = new ChartZoomPanBehavior();
            chart.Behaviors.Add(zoomPanBehavior);

            SetContentView(chart);
        }

        public class ChartData
        {
            public string Semanas { get; set; }
            public double LongPatrullada { get; set; }
        }
    }
}