﻿using System;
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
        private string codContratista;
        private string nomContratista;
        private string mes;
        private int anho;
        ObservableCollection<ChartData> Data = new ObservableCollection<ChartData>();
        ObservableCollection<ChartData> Data2 = new ObservableCollection<ChartData>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            codContratista = Intent.GetStringExtra("Contratista");
            nomContratista = Intent.GetStringExtra("NomContratista");
            mes = Intent.GetStringExtra("Mes");

            GetData();
        }

        private async void GetData()
        {
            try
            {
                if (mes == "4")
                {
                    string url = URLs.ConMes1 + codContratista + URLs.ConMes1y1 + mes;
                    var resultado = await url.GetRequest<List<TABLA_MES1>>();

                    foreach (var datos in resultado)
                    {
                        Data.Add(new ChartData { Semanas = "Semana " + datos.SEMANA, LongPatrullada = datos.LONGITUD_PATRULLADA });
                        Data2.Add(new ChartData { Semanas = "Semana " + datos.SEMANA, LongPatrullada = datos.LONGITUD_ASIGNADA });
                        anho = datos.ANHO;
                    }

                    CreateLineChart();
                }
                else if (mes == "6")
                {
                    string url = URLs.ConMeses6 + codContratista + URLs.ConMeses6y1 + mes;
                    var resultado = await url.GetRequest<List<TABLA_MESES>>();

                    foreach (var datos in resultado)
                    {
                        Data.Add(new ChartData { Semanas = "Semana " + datos.MES, LongPatrullada = datos.LONGITUD_PATRULLADA });
                        Data2.Add(new ChartData { Semanas = "Semana " + datos.MES, LongPatrullada = datos.LONGITUD_ASIGNADA });
                        anho = datos.ANHO;
                    }

                    CreateLineChart();
                }
                else if (mes == "12")
                {
                    string url = URLs.ConMeses12 + codContratista + URLs.ConMeses12y1 + mes;
                    var resultado = await url.GetRequest<List<TABLA_MESES>>();

                    foreach (var datos in resultado)
                    {
                        Data.Add(new ChartData { Semanas = "Semana " + datos.MES, LongPatrullada = datos.LONGITUD_PATRULLADA });
                        Data2.Add(new ChartData { Semanas = "Semana " + datos.MES, LongPatrullada = datos.LONGITUD_ASIGNADA });
                        anho = datos.ANHO;
                    }

                    CreateLineChart();
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
            chart.Title.Text = "CONTRATISTA: " + nomContratista;
            chart.Title.Typeface = Typeface.DefaultBold;

            CategoryAxis primaryAxis = new CategoryAxis();
            primaryAxis.Title.Text = "Semanas";
            chart.PrimaryAxis = primaryAxis;
            chart.PrimaryAxis.EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Shift;
            chart.PrimaryAxis.LabelsIntersectAction = AxisLabelsIntersectAction.MultipleRows;

            NumericalAxis secondaryAxis = new NumericalAxis();
            secondaryAxis.Title.Text = "Longitud (mt)";
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

            chart.Series.Add(lineSeries);
            chart.Series.Add(lineSeries2);
            chart.Legend.Title.Text = "Año " + anho;
            chart.Legend.Visibility = Visibility.Visible;

            SetContentView(chart);
        }

        public class ChartData
        {
            public string Semanas { get; set; }
            public double LongPatrullada { get; set; }
        }
    }
}