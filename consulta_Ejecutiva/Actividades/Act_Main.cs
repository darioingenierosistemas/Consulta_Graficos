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

        private ProgressDialog mProgress;

        private string CodSelect;
        private string Flag;
        private string NomSelect;
        private string Mes;

        private static CheckBox Mes1;
        private static CheckBox Meses6;
        private static CheckBox Anho1;

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

        private List<KeyValuePair<string, string>> DepKey;
        private List<KeyValuePair<string, string>> UniKey;
        private List<KeyValuePair<string, string>> ConKey;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Lay_Main);

            mProgress = new ProgressDialog(this);
            mProgress.SetCancelable(true);
            mProgress.SetMessage("Cargando.....");
            mProgress.SetProgressStyle(ProgressDialogStyle.Spinner);

            mProgress.Show();

            Inicializar();
            CheckStatus();
            PoblarSpinnerDep();

            mProgress.Dismiss();
        }

        private void Inicializar()
        {
          
            FloatingActionButton BtnBarChart = FindViewById<FloatingActionButton>(Resource.Id.fabBarChart);
            BtnBarChart.Click += BtnBarChart_Click;

            FloatingActionButton btnLineChart = FindViewById<FloatingActionButton>(Resource.Id.fabLineChart);
            btnLineChart.Click += BtnLineChart_Click;


            Mes1 = FindViewById<CheckBox>(Resource.Id.btnUnMes);
            Mes1.CheckedChange += Mes1_CheckedChange;

            Meses6 = FindViewById<CheckBox>(Resource.Id.btnSeisMes);
            Meses6.CheckedChange += Meses6_CheckedChange;

            Anho1 = FindViewById<CheckBox>(Resource.Id.btnUnAnho);
            Anho1.CheckedChange += Anho1_CheckedChange;

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
        }

        private async void PoblarSpinnerDep()
        {
            try
            {
                var result = await URLs.ConDep.GetRequest<List<TABLA_DEPARTAMENTOS>>();
                List<string> ListaDepartamentos = new List<string>();
                DepKey = new List<KeyValuePair<string, string>>();
                DepKey.Add(new KeyValuePair<string, string>("0", "--SELECCIONE DEPARTAMENTO--"));
                foreach (var departamento in result)
                {
                    DepKey.Add(new KeyValuePair<string, string>(departamento.CODIGO.ToString(), departamento.NOMBRE));
                }

                foreach (var dep in DepKey)
                {
                    ListaDepartamentos.Add(dep.Value);
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

                var result = await (URLs.ConUni + cod).GetRequest<List<TABLA_UNIDAD_OPERATIVA>>();

                List<string> ListaUnidadOperativa = new List<string>();
                UniKey = new List<KeyValuePair<string, string>>();
                UniKey.Add(new KeyValuePair<string, string>("0", "--SELECCIONE UNIDAD OPERATIVA--"));

                foreach (var unidad in result)
                {
                    if (string.IsNullOrEmpty(unidad.OPER_UNIT_CODE))
                    {
                        unidad.OPER_UNIT_CODE = unidad.OPERATING_UNIT_ID.ToString();
                    }
                    UniKey.Add(new KeyValuePair<string, string>(unidad.OPERATING_UNIT_ID.ToString(), unidad.OPER_UNIT_CODE));
                }

                foreach (var uni in UniKey)
                {
                    ListaUnidadOperativa.Add(uni.Value);
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
                ConKey = new List<KeyValuePair<string, string>>();
                ConKey.Add(new KeyValuePair<string, string>("0", "--SELECCIONE CONTRATISTA--"));

                foreach (var contratista in result)
                {
                    ConKey.Add(new KeyValuePair<string, string>(contratista.COD_CONTRATISTA.ToString(), contratista.NOM_CONTRATISTA));
                }

                foreach (var con in ConKey)
                {
                    ListaContratista.Add(con.Value);
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


        private void BtnBarChart_Click(object sender, EventArgs e)
        {
            if (Mes1.Checked == true || Meses6.Checked == true || Anho1.Checked == true)
            {
                if (ItemPositionDep != 0 && ItemPositionUni != 0 && ItemPositionCon != 0)
                {
                    var intent = new Intent(this, typeof(Act_Grafico_BarChart));
                    intent.PutExtra("CodSelect", CodSelect);
                    intent.PutExtra("Flag", "Contratista");
                    intent.PutExtra("Mes", Mes);
                    intent.PutExtra("NomSelect", NomSelect);
                    StartActivity(intent);
                }
                else if (ItemPositionDep != 0 && ItemPositionUni == 0 && ItemPositionCon == 0)
                {
                    var intent = new Intent(this, typeof(Act_Grafico_BarChart));
                    intent.PutExtra("CodSelect", CodSelect);
                    intent.PutExtra("Flag", "Departamento");
                    intent.PutExtra("Mes", Mes);
                    intent.PutExtra("NomSelect", NomSelect);
                    StartActivity(intent);
                }
                else if (ItemPositionDep != 0 && ItemPositionUni != 0 && ItemPositionCon == 0)
                {
                    var intent = new Intent(this, typeof(Act_Grafico_BarChart));
                    intent.PutExtra("CodSelect", CodSelect);
                    intent.PutExtra("Flag", "Unidad");
                    intent.PutExtra("Mes", Mes);
                    intent.PutExtra("NomSelect", NomSelect);
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
                    var intent = new Intent(this, typeof(Act_LineChart));
                    intent.PutExtra("CodSelect", CodSelect);
                    intent.PutExtra("Flag", "Contratista");
                    intent.PutExtra("Mes", Mes);
                    intent.PutExtra("NomSelect", NomSelect);
                    StartActivity(intent);
                }
                else if (ItemPositionDep != 0 && ItemPositionUni == 0 && ItemPositionCon == 0)
                {
                    var intent = new Intent(this, typeof(Act_LineChart));
                    intent.PutExtra("CodSelect", CodSelect);
                    intent.PutExtra("Flag", "Departamento");
                    intent.PutExtra("Mes", Mes);
                    intent.PutExtra("NomSelect", NomSelect);
                    StartActivity(intent);
                }
                else if (ItemPositionDep != 0 && ItemPositionUni != 0 && ItemPositionCon == 0)
                {
                    var intent = new Intent(this, typeof(Act_LineChart));
                    intent.PutExtra("CodSelect", CodSelect);
                    intent.PutExtra("Flag", "Unidad");
                    intent.PutExtra("Mes", Mes);
                    intent.PutExtra("NomSelect", NomSelect);
                    StartActivity(intent);
                }
            }
        }

        private void Departamentos_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {

            ItemPositionDep = Departamentos.SelectedItemPosition;

            if (ItemPositionDep != 0)
            {
                Spinner spinner = (Spinner)sender;
                string toast = string.Format("{1}", spinner.GetItemAtPosition(e.Position), DepKey[e.Position].Key);
                string toast1 = string.Format("{0}", spinner.GetItemAtPosition(e.Position), DepKey[e.Position].Value);
                CodSelect = toast;
                NomSelect = toast1;
                int cod = Convert.ToInt16(toast);
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
            if (ItemPositionUni != 0)
            {
                Spinner spinner = (Spinner)sender;
                string toast = string.Format("{1}", spinner.GetItemAtPosition(e.Position), UniKey[e.Position].Key);
                string toast1 = string.Format("{0}", spinner.GetItemAtPosition(e.Position), UniKey[e.Position].Value);
                CodSelect = toast;
                NomSelect = toast1;
                int cod = Convert.ToInt16(toast);

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
                Spinner spinner = (Spinner)sender;
                string toast = string.Format("{1}", spinner.GetItemAtPosition(e.Position), ConKey[e.Position].Key);
                string toast1 = string.Format("{0}", spinner.GetItemAtPosition(e.Position), ConKey[e.Position].Value);
                CodSelect = toast;
                NomSelect = toast1;
            }
        }

    }
}