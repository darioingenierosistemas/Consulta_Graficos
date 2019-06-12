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

        public static string ConMes1 = "http://testlab.g-gis.com:80/Rest_Ejecutivo/ejecutiva/consulta/ConsultaMes/?CodContratista=";
        public static string ConMeses = "http://testlab.g-gis.com:80/Rest_Ejecutivo/ejecutiva/consulta/ConsultaMeses/?CodContratista=";

        public static string LonDepMes1 = "http://testlab.g-gis.com:80/Rest_Ejecutivo/ejecutiva/consulta/LongitudAsigDepartamento/?CodDepartamento=";
        public static string LonDepMeses = "http://testlab.g-gis.com:80/Rest_Ejecutivo/ejecutiva/consulta/LongitudAsigDepartamentoMeses/?CodDepartamento=";

        public static string LonUniMes1 = "http://testlab.g-gis.com:80/Rest_Ejecutivo/ejecutiva/consulta/LongitudUnidadSemana/?CodUnidadOperativa=";
        public static string LonUniMeses = "http://testlab.g-gis.com:80/Rest_Ejecutivo/ejecutiva/consulta/LongitudUnidadMeses/?CodUnidadOperativa=";

        public static string VerificarUser = "http://testlab.g-gis.com:80/Rest_Ejecutivo/ejecutiva/consulta/Vaerificar_Usuario/?Usuario=";

        public static string ConMes1y1 = "&semanas=";
        public static string ConMesesy1 = "&meses=";

        public static string LonDepMes1y1 = "&semanas=";
        public static string LonDepMesesy1 = "&meses=";

        public static string LonUniMes1y1 = "&semanas=";
        public static string LonUniMesesy1 = "&meses=";

        public static string VerificarUsery1 = "&Password=";
    }
}