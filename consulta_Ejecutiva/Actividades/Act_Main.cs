using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Support.V7.App;

using Android.Support.Design.Widget;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace consulta_Ejecutiva.Actividades
{
    [Activity(Label = "ESTADISTICAS DE PATRULLAJE"
        , MainLauncher = true
    )]
    public class Act_Main : AppCompatActivity
    {

        private static CheckBox Mes1;
        private static CheckBox Meses6;
        private static CheckBox Anho1;

        private static RadioButton osl;
        private static RadioButton tlo;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Lay_Main);

            Inicializar();
            CheckStatus();
        }

        private void Inicializar()
        {
            FloatingActionButton BtnBarChart = FindViewById<FloatingActionButton>(Resource.Id.fabBarChart);
            BtnBarChart.Click += BtnBarChart_Click;

            FloatingActionButton btnLineChart = FindViewById<FloatingActionButton>(Resource.Id.fabLineChart);
            btnLineChart.Click += BtnLineChart_Click;

            FloatingActionButton BtnDonutChart = FindViewById<FloatingActionButton>(Resource.Id.fabDonutChart);
            BtnDonutChart.Click += BtnDonutChart_Click;

            Mes1 = FindViewById<CheckBox>(Resource.Id.btnUnMes);
            Mes1.CheckedChange += Mes1_CheckedChange;

            Meses6 = FindViewById<CheckBox>(Resource.Id.btnSeisMes);
            Meses6.CheckedChange += Meses6_CheckedChange;

            Anho1 = FindViewById<CheckBox>(Resource.Id.btnUnAnho);
            Anho1.CheckedChange += Anho1_CheckedChange;

            osl = FindViewById<RadioButton>(Resource.Id.rbtnOSL);
            osl.CheckedChange += Osl_CheckedChange;

            tlo = FindViewById<RadioButton>(Resource.Id.rbtnTLO);
            tlo.CheckedChange += Tlo_CheckedChange;
        }

        private void CheckStatus()
        {
            Mes1.Checked = false;
            Meses6.Checked = false;
            Anho1.Checked = false;

            osl.Checked = false;
            tlo.Checked = false;
        }

        private void Mes1_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (Mes1.Checked)
            {
                Mes1.Checked = true;
                Meses6.Checked = false;
                Anho1.Checked = false;
            }
        }

        private void Meses6_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (Meses6.Checked)
            {
                Mes1.Checked = false;
                Meses6.Checked = true;
                Anho1.Checked = false;
            }
        }

        private void Anho1_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (Anho1.Checked)
            {
                Mes1.Checked = false;
                Meses6.Checked = false;
                Anho1.Checked = true;
            }
        }

        private void Osl_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (osl.Checked)
            {
                osl.Checked = true;
                tlo.Checked = false;
            }
        }

        private void Tlo_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (tlo.Checked)
            {
                osl.Checked = false;
                tlo.Checked = true;
            }
        }

        private void BtnDonutChart_Click(object sender, EventArgs e)
        {
            var DonutChart_ = new Intent(this, typeof(Act_DonutChart));
            StartActivity(DonutChart_);
        }

        private void BtnBarChart_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(Act_Grafico_BarChart));
            StartActivity(intent);
        }

        private void BtnLineChart_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(Act_LineChart));
            StartActivity(intent);
        }
    }
}