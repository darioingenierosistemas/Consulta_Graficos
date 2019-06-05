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
using consulta_Ejecutiva.BD;
using consulta_Ejecutiva.REST;



namespace consulta_Ejecutiva.Actividades
{
    [Activity(Label = "ESTADISTICAS DE PATRULLAJE"
        , MainLauncher = true
    )]
    public class Act_Main : AppCompatActivity
    {
        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

        private string CodContratista;
        private string Mes;

        private bool Bmes1 = false;
        private bool Bmeses6 = false;
        private bool Banho = false;

        private static CheckBox Mes1;
        private static CheckBox Meses6;
        private static CheckBox Anho1;

        private static RadioButton osl;
        private static RadioButton tlo;

        private static Spinner Departamentos;
        private static Spinner Unidad_Operativa;
        private static Spinner Contratista;

        private static int ItemPositionDep;
        private static int ItemPositionUni;
        private static int ItemPositionCon;

        private static string PositionDep;
        private static string PositionUni;
        private static string PositionCon;

        private ArrayAdapter<string> adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Lay_Main);

            Inicializar();
            CheckStatus();
            PoblarSpinnerDep();
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

            Departamentos = FindViewById<Spinner>(Resource.Id.SpnDepartamento1);
            Departamentos.SetSelection(0, false);
            Departamentos.ItemSelected += Departamentos_ItemSelected;

            Unidad_Operativa = FindViewById<Spinner>(Resource.Id.SpnUnidadOperativa1);
            Unidad_Operativa.SetSelection(0, false);
            Unidad_Operativa.ItemSelected += Unidad_Operativa_ItemSelected;

            Contratista = FindViewById<Spinner>(Resource.Id.SpnContratista1);
            Contratista.SetSelection(0, false);
            Contratista.ItemSelected += Contratista_ItemSelected;
        }


        private void CheckStatus()
        {
            Mes1.Checked = false;
            Meses6.Checked = false;
            Anho1.Checked = false;

            osl.Checked = false;
            tlo.Checked = false;
        }

        private async void PoblarSpinnerDep()
        {
            try
            {
                    var result = await URLs.ConDep.GetRequest<List<TABLA_DEPARTAMENTOS>>();

                    List<string> ListaDepartamentos = new List<string>();
                    ListaDepartamentos.Add("--SELECCIONE DEPARTAMENTO--");

                    foreach (var departamento in result)
                    {
                        ListaDepartamentos.Add(departamento.CODIGO+"    "+departamento.NOMBRE.ToString());
                    }

                    adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, ListaDepartamentos);
                    adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                    Departamentos.Adapter = adapter;
                
            }
            catch (Exception ex)
            {
                Toast.MakeText(ApplicationContext, ex.ToString(), ToastLength.Long).Show();
               
            }

        }

        private async void PoblarSpinnerUni(int cod)
        {
            try
            {

                var result= await (URLs.ConUni+cod).GetRequest<List<TABLA_UNIDAD_OPERATIVA>>();

                List<string> ListaUnidadOperativa = new List<string>();
                ListaUnidadOperativa.Add("--SELECCIONE UNIDAD OPERATIVA--");

                foreach (var unidad in result)
                {
                    ListaUnidadOperativa.Add(unidad.OPERATING_UNIT_ID.ToString()+"    "+unidad.OPER_UNIT_CODE);
                }

                adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, ListaUnidadOperativa);
                adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                Unidad_Operativa.Adapter = adapter;

            }
            catch (Exception ex)
            {
                Toast.MakeText(ApplicationContext, ex.ToString(), ToastLength.Long).Show();

            }
        }

        private async void PoblarSpinnerCon(int cod)
        {
            try
            {

                var result = await (URLs.ConCon + cod).GetRequest<List<TABLA_CONTRATISTA>>();

                List<string> ListaContratista = new List<string>();
                ListaContratista.Add("--SELECCIONE CONTRATISTA--");

                foreach (var contratista in result)
                {
                    ListaContratista.Add(contratista.COD_CONTRATISTA+"    "+ contratista.NOM_CONTRATISTA.ToString());
                }

                adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, ListaContratista);
                adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                Contratista.Adapter = adapter;

            }
            catch (Exception ex)
            {
                Toast.MakeText(ApplicationContext, ex.ToString(), ToastLength.Long).Show();

            }
        }

        private void Mes1_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (Mes1.Checked)
            {
                Mes1.Checked = true;
                Meses6.Checked = false;
                Anho1.Checked = false;
                if (Mes1.Checked == true)
                {
                    Mes = "4";

                }
            }
        }

        private void Meses6_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (Meses6.Checked)
            {
                Mes1.Checked = false;
                Meses6.Checked = true;
                Anho1.Checked = false;
                if (Meses6.Checked == true)
                {
                    Mes = "6";
                }
            }
        }

        private void Anho1_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (Anho1.Checked)
            {
                Mes1.Checked = false;
                Meses6.Checked = false;
                Anho1.Checked = true;
                if (Anho1.Checked == true)
                {
                    Mes = "12";
                }
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
            if (Mes1.Checked == true || Meses6.Checked == true || Anho1.Checked == true)
            {
                if (ItemPositionDep != 0 && ItemPositionUni != 0 && ItemPositionCon != 0)
                {
                    //var DonutChart_ = new Intent(this, typeof(Act_DonutChart));
                    //DonutChart_.PutExtra("Contratista", CodContratista);
                    //DonutChart_.PutExtra("Mes", 4);
                    //StartActivity(DonutChart_);
                }
            }
        }

        private void BtnBarChart_Click(object sender, EventArgs e)
        {
            if (Mes1.Checked == true || Meses6.Checked == true || Anho1.Checked == true)
            {
                if (ItemPositionDep != 0 && ItemPositionUni != 0 && ItemPositionCon != 0)
                {
                    var intent = new Intent(this, typeof(Act_Grafico_BarChart));
                    intent.PutExtra("Contratista", CodContratista);
                    intent.PutExtra("Mes", Mes);
                    StartActivity(intent);
                }
            }
        }

        private void BtnLineChart_Click(object sender, EventArgs e)
        {
            if (Mes1.Checked == true || Meses6.Checked == true || Anho1.Checked == true)
            { 
                if(ItemPositionDep != 0 && ItemPositionUni != 0 && ItemPositionCon != 0)
                { 
                //var intent = new Intent(this, typeof(Act_LineChart));
                //intent.PutExtra("Contratista", CodContratista);
                //intent.PutExtra("Mes", 4);
                //StartActivity(intent);
                }
            }
        }

        private void Departamentos_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            
            ItemPositionDep = Departamentos.SelectedItemPosition;

            if (ItemPositionDep != 0)
            {
                PositionDep = Departamentos.GetItemAtPosition(ItemPositionDep).ToString();
                string[] dividir = PositionDep.Split("    ");
                int cod = Convert.ToInt16(dividir[0]);
                try
                {
                    PoblarSpinnerUni(cod);
             
                }
                catch (Exception ex)
                {
                    Toast.MakeText(ApplicationContext, ex.ToString(), ToastLength.Long).Show();

                }
            }
        }

        private void Unidad_Operativa_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {

            ItemPositionUni = Unidad_Operativa.SelectedItemPosition;
            if (ItemPositionUni!=0)
            {
                PositionUni = Unidad_Operativa.GetItemAtPosition(ItemPositionUni).ToString();
                string[] dividir = PositionUni.Split("    ");
                int cod = Convert.ToInt16(dividir[0]);

                try
                {
                    PoblarSpinnerCon(cod);

                }
                catch (Exception ex)
                {
                    Toast.MakeText(ApplicationContext, ex.ToString(), ToastLength.Long).Show();

                }
            }

        }

        private void Contratista_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            ItemPositionCon = Contratista.SelectedItemPosition;
            if (ItemPositionCon != 0)
            {
                PositionCon = Contratista.GetItemAtPosition(ItemPositionCon).ToString();
                string[] dividir = PositionCon.Split("    ");
                CodContratista = dividir[0];
            }
        }

    }
}