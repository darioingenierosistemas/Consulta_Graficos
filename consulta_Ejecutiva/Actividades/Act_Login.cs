using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;

namespace consulta_Ejecutiva
{
    [Activity(Label = "LOGIN", Theme = "@style/AppTheme"
        //, MainLauncher = true
        )]
    public class Act_Login : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Lay_Login);
        }
    }
}