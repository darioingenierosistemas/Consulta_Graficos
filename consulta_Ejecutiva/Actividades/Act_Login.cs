using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android;
using System.Threading.Tasks;
using AlertDialog = Android.App.AlertDialog;
using Android.Text;
using System;
using consulta_Ejecutiva.Actividades;
using consulta_Ejecutiva.REST;
using consulta_Ejecutiva.BD;

namespace consulta_Ejecutiva
{
    [Activity(Label = "CONSULTA EJECUTIVA", Theme = "@style/AppTheme"
        , MainLauncher = true
        )]
    public class Act_Login : AppCompatActivity
    {
        private ProgressDialog mProgress;

        protected  override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Lay_Login);
            //await TryToGetPermissions();

            Button btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            CheckBox chkMostar = FindViewById<CheckBox>(Resource.Id.chkMostrar);
            EditText edtPass = FindViewById<EditText>(Resource.Id.edtPass);
            edtPass.InputType = InputTypes.TextVariationPassword | InputTypes.ClassText | InputTypes.TextVariationNormal;

            btnLogin.Click += BtnLogin_Click;
            chkMostar.CheckedChange += ChkMostar_CheckedChange;

        }


        private void ChkMostar_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            CheckBox chkMostar = FindViewById<CheckBox>(Resource.Id.chkMostrar);
            EditText edtPass = FindViewById<EditText>(Resource.Id.edtPass);

            if (chkMostar.Checked == true)
            {
                edtPass.InputType = InputTypes.TextVariationVisiblePassword | InputTypes.ClassText | InputTypes.TextVariationNormal;
            }
            else if (chkMostar.Checked == false)
            {

                edtPass.InputType = InputTypes.TextVariationPassword | InputTypes.ClassText | InputTypes.TextVariationNormal;
            }
            edtPass.RequestFocus();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {

            EditText edtUser = FindViewById<EditText>(Resource.Id.edtUser);
            EditText edtPass = FindViewById<EditText>(Resource.Id.edtPass);
            if (!string.IsNullOrEmpty(edtUser.Text.ToString()) && !string.IsNullOrEmpty(edtPass.Text.ToString()))
            {
                VerificarUser();
            }
            else if (string.IsNullOrEmpty(edtUser.Text.ToString()) || string.IsNullOrEmpty(edtPass.Text.ToString()))
            {
                AlertDialog.Builder alertDiag = new AlertDialog.Builder(this);
                alertDiag.SetTitle("ERROR");
                alertDiag.SetMessage("Los campos de USER y PASSWORD no pueden estas vacíos");
                alertDiag.SetNegativeButton("OK", (senderAlert, args) => {
                });
                Dialog diag = alertDiag.Create();
                diag.Show();
            }

        }

        private async void VerificarUser()
        {
            EditText edtUser = FindViewById<EditText>(Resource.Id.edtUser);
            EditText edtPass = FindViewById<EditText>(Resource.Id.edtPass);

            mProgress = new ProgressDialog(this);
            mProgress.SetCancelable(true);
            mProgress.SetMessage("Verificando Usuario");
            mProgress.SetProgressStyle(ProgressDialogStyle.Spinner);
            mProgress.Progress = 0;
            mProgress.Max = 100;

            mProgress.Show();
            try
            {
                string url = URLs.VerificarUser  + edtUser.Text.ToString() + URLs.VerificarUsery1 + edtPass.Text.ToString();
                var GetUser = await url.GetRequestUSER<Usuario_Verificar>();

                if (GetUser.ToString() == "USUARIO VERIFICADO")
                {          
                        StartActivity(typeof(Act_Main));
                }
                else
                {
                    mProgress.Dismiss();
                    AlertDialog.Builder alertDiag = new AlertDialog.Builder(this);
                    alertDiag.SetTitle("ERROR");
                    alertDiag.SetMessage("Usuario o Constraseña invalida");
                    alertDiag.SetPositiveButton("OK", (senderAlert, args) =>
                    {
                        edtUser.Text = "";
                        edtPass.Text = "";
                    });
                    Dialog diag = alertDiag.Create();
                    diag.Show();
                }
            }
            catch (Exception ex)
            {
                mProgress.Dismiss();
                AlertDialog.Builder alertDiag = new AlertDialog.Builder(this);
                alertDiag.SetTitle("ERROR");
                alertDiag.SetMessage(ex.Message);
                alertDiag.SetNegativeButton("OK", (senderAlert, args) =>
                {

                });
                Dialog diag = alertDiag.Create();
                diag.Show();

            }

        }




        async Task TryToGetPermissions()
        {
            if ((int)Build.VERSION.SdkInt >= 23)
            {
                await GetPermissionsAsync();
                return;
            }


        }

        const int RequestLocationId = 0;

        readonly string[] PermissionsGroup =
            {
                            //Se Añaden los Permisos
                            Manifest.Permission.Internet,
                            Manifest.Permission.WriteExternalStorage,
                            Manifest.Permission.ReadExternalStorage,
                            Manifest.Permission.AccessCoarseLocation,
                            Manifest.Permission.AccessFineLocation,
                            Manifest.Permission.Camera


             };

        async Task GetPermissionsAsync()
        {
            const string permission = Manifest.Permission.AccessFineLocation;

            if (CheckSelfPermission(permission) == (int)Android.Content.PM.Permission.Granted)
            {

                return;
            }

            if (ShouldShowRequestPermissionRationale(permission))
            {
                //set alert for executing the task
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Permiso Necesitado");
                alert.SetMessage("La Aplicacion necesita un permiso espcial para continuar");
                alert.SetPositiveButton("Pedir Permiso", (senderAlert, args) =>
                {
                    RequestPermissions(PermissionsGroup, RequestLocationId);
                });

                alert.SetNegativeButton("Cancelar", (senderAlert, args) =>
                {
                    Toast.MakeText(this, "Cancelado!", ToastLength.Short).Show();
                });

                Dialog dialog = alert.Create();
                dialog.Show();


                return;
            }

            RequestPermissions(PermissionsGroup, RequestLocationId);

        }

        public override async void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            switch (requestCode)
            {
                case RequestLocationId:
                    {
                        if (grantResults[0] == (int)Android.Content.PM.Permission.Granted)
                        {
                            Toast.MakeText(this, "Permiso Especial Concedido", ToastLength.Short).Show();

                        }
                        else
                        {
                            //Permission Denied :(
                            Toast.MakeText(this, "Permiso Especial Denegado", ToastLength.Short).Show();

                        }
                    }
                    break;
            }
            //base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}