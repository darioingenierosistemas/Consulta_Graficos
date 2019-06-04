using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace consulta_Ejecutiva.REST
{
    public class URLs
    {
        public static string ConDep = "http://testlab.g-gis.com:80/Rest_Ejecutivo/ejecutiva/consulta/departamentos/";
        public static string ConUni = "http://testlab.g-gis.com:80/Rest_Ejecutivo/ejecutiva/consulta/unidad/?CodDepartamento=";
        public static string ConCon = "http://testlab.g-gis.com:80/Rest_Ejecutivo/ejecutiva/consulta/contratista/?CodUnidadOperativa=";
        public static string ConMes1 = "http://testlab.g-gis.com:80/Rest_Ejecutivo/ejecutiva/consulta/ConsultaMes1/?CodContratista=";
        public static string ConMeses6 = "http://testlab.g-gis.com:80/Rest_Ejecutivo/ejecutiva/consulta/ConsultaMeses6/?CodContratista=";
        public static string ConMeses12 = "http://testlab.g-gis.com:80/Rest_Ejecutivo/ejecutiva/consulta/ConsultaMeses12/?CodContratista=";
        public static string ConMes1y1 = "&mes=";
        public static string ConMeses6y1 = "&meses=";
        public static string ConMeses12y1 = "&meses=";
    }
}