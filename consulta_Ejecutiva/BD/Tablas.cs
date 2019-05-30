
using SQLite;

namespace consulta_Ejecutiva.BD
{

    [Table("TABLA_DEPARTAMENTOS")]
    public class TABLA_DEPARTAMENTOS
    {
        private int m_CODIGO;
        public int CODIGO
        {

            get
            {
                return m_CODIGO;
            }
            set
            {
                this.m_CODIGO = value;
            }
        }

        private string m_NOMBRE;
        public string NOMBRE
        {

            get
            {
                return m_NOMBRE;
            }
            set
            {
                this.m_NOMBRE = value;
            }
        }

    }

    [Table("TABLA_UNIDAD_OPERATIVA")]
    public class TABLA_UNIDAD_OPERATIVA
    {
        private int m_OPERATING_UNIT_ID;
        public int OPERATING_UNIT_ID
        {

            get
            {
                return m_OPERATING_UNIT_ID;
            }
            set
            {
                this.m_OPERATING_UNIT_ID = value;
            }
        }

        private string m_OPER_UNIT_CODE;
        public string OPER_UNIT_CODE
        {

            get
            {
                return m_OPER_UNIT_CODE;
            }
            set
            {
                this.m_OPER_UNIT_CODE = value;
            }
        }

    }

    [Table("TABLA_CONTRATISTA")]
    public class TABLA_CONTRATISTA
    {
        private int m_COD_CONTRATISTA;
        public int COD_CONTRATISTA
        {

            get
            {
                return m_COD_CONTRATISTA;
            }
            set
            {
                this.m_COD_CONTRATISTA = value;
            }
        }

        private string m_NOM_CONTRATISTA;
        public string NOM_CONTRATISTA
        {

            get
            {
                return m_NOM_CONTRATISTA;
            }
            set
            {
                this.m_NOM_CONTRATISTA = value;
            }
        }

    }

    [Table("TABLA_MES1")]
    public class TABLA_MES1
    {
        private double m_LONGITUD_ASIGNADA;
        public double LONGITUD_ASIGNADA
        {

            get
            {
                return m_LONGITUD_ASIGNADA;
            }
            set
            {
                this.m_LONGITUD_ASIGNADA = value;
            }
        }

        private double m_LONGITUD_PATRULLADA;
        public double LONGITUD_PATRULLADA
        {

            get
            {
                return m_LONGITUD_PATRULLADA;
            }
            set
            {
                this.m_LONGITUD_PATRULLADA = value;
            }
        }

        private int m_SEMANA;
        public int SEMANA
        {

            get
            {
                return m_SEMANA;
            }
            set
            {
                this.m_SEMANA = value;
            }
        }

    }
}