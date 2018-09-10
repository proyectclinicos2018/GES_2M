using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ges000M;
using System.Configuration;
using AplicacionFalp;
using Falp;
namespace Ges000M
{
    public partial class Frm_Buscar : Form
    {

        Color ColorActivo = Color.Orange;
      
       ConectarFalp CnnFalp;
       Configuration Config;
       string[] Conexion = { "", "", "" };
       string Db_Usuario;
       string PCK = "PCK_GES002M";
       DataTable dt_datos = new DataTable();


       #region  Datos Paciente


       public Int64 v_correlativo = 0;
       public string v_ficha = "";
       public string v_tipo_doc = "";
       public string v_num_doc = "";
       public string v_nombres = "";
       public string v_paterno = "";
       public string v_materno = "";
       public string v_sexo = "";
       public string v_fecha_nac = "";
       public string v_edad= "";
       public string v_ecivil = "";
       public string v_tipo_prev = "";
       public string v_previcion = "";
       public string v_plan = "";
       public string v_cod_tipo_prev = "";
       public string v_cod_previcion = "";
       public string v_cod_plan = "";

       #endregion 

       public Frm_Buscar()
        {
            InitializeComponent();
        }

        private void Frm_Buscar_Load(object sender, EventArgs e)
        {
            Txt_Caso.Focus();
            Opt_Caso.Checked = true;
            Txt_Caso.BackColor = ColorActivo;
            Cargar_Conexion();
           
        }


        private void Cargar_Conexion()
        {
            if (!(CnnFalp != null))
            {
                ExeConfigurationFileMap FileMap = new ExeConfigurationFileMap();
                FileMap.ExeConfigFilename = Application.StartupPath + @"\..\WF.config";
                Config = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None);

                CnnFalp = new ConectarFalp(Config.AppSettings.Settings["dbServer"].Value,//ConfigurationManager.AppSettings["dbServer"],
                                           Config.AppSettings.Settings["dbUser"].Value,//ConfigurationManager.AppSettings["dbUser"],
                                           Config.AppSettings.Settings["dbPass"].Value,//ConfigurationManager.AppSettings["dbPass"],
                                           ConectarFalp.TipoBase.Oracle);

                if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir(); // abre la conexion
                Conexion[0] = Config.AppSettings.Settings["dbServer"].Value;
                Conexion[1] = Config.AppSettings.Settings["dbUser"].Value;
                Conexion[2] = Config.AppSettings.Settings["dbPass"].Value;
                this.Text = this.Text + " [Versión: " + Application.ProductVersion + "] [Conectado: " + Conexion[0] + "]";

            }
            Db_Usuario = ValidaMenu.LeeUsuarioMenu().Equals(string.Empty) ? "SICI" : ValidaMenu.LeeUsuarioMenu();
        }

        private void Opt_Caso_CheckedChanged(object sender, EventArgs e)
        {
            if (Opt_Caso.Checked)
            {
                TxtRut.Text = "";
                TxtDV.Text = "";
                Txt_Caso.Text = "";
                TxtFicha.Text = "";
                TxtNomPac.Text = "";
                Txt_Caso.ReadOnly = false;
                Txt_Caso.BackColor = ColorActivo;
                Txt_Caso.Focus();
            }
            else
            {
                TxtRut.Text = "";
                TxtDV.Text = "";
                TxtNomPac.Text = "";
                TxtFicha.Text = "";
                Txt_Caso.BackColor = Color.White;
                Txt_Caso.Text = string.Empty;
                Txt_Caso.ReadOnly = true;
            }

        }

        private void Opt_Doc_CheckedChanged(object sender, EventArgs e)
        {
            if (Opt_Doc.Checked)
            {
                TxtNomPac.Text = "";

                txttipo_doc.Enabled = true;
               // CboTipoDoc_SelectionChangeCommitted(CboTipoDoc, EventArgs.Empty);
                txttipo_doc.BackColor = ColorActivo;
                TxtRut.BackColor = ColorActivo;
                TxtDV.BackColor = ColorActivo;
                TxtOtroDoc.BackColor = ColorActivo;
                TxtRut.ReadOnly = false;
                TxtDV.ReadOnly = false;
                TxtRut.Focus();
            }
            else
            {
                TxtRut.BackColor = Color.White;
                TxtDV.BackColor = Color.White;
                TxtOtroDoc.BackColor = Color.White;
                TxtRut.ReadOnly = true;
                TxtDV.ReadOnly = true;
                TxtOtroDoc.ReadOnly = true;
                txttipo_doc.Enabled = false;
            }
        }

        private void Opt_Ficha_CheckedChanged(object sender, EventArgs e)
        {
            if (Opt_Ficha.Checked)
            {
                TxtFicha.Text = "";
                TxtRut.Text = "";
                TxtDV.Text = "";
                TxtNomPac.Text = "";
                Txt_Caso.Text = "";
                TxtFicha.ReadOnly = false;
                TxtFicha.BackColor = ColorActivo;
                TxtFicha.Focus();
            }
            else
            {
                TxtRut.Text = "";
                TxtDV.Text = "";
                TxtFicha.BackColor = Color.White;
                TxtFicha.Text = string.Empty;
                TxtFicha.ReadOnly = true;
            }
        }

        private void Btn_Ingresar_Click(object sender, EventArgs e)
        {
            if (Txt_Caso.Text != string.Empty)
            {
                Cargar_datos();
                this.Tag = Txt_Caso.Text;

                this.Close();
                this.Dispose();
            }
            else
            {
                this.Tag = 0;
                this.Close();
                this.Dispose();
            }

        }

        private void Txt_Caso_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
            if (Txt_Caso.Text != "0")
            {
                if (e.KeyChar == 13)
                {
                    P_Valida();
                }
            }
        }


        private void P_Valida()
        {
            Cargar_datos();
            Btn_Ingresar.Enabled = true;
            Btn_Ingresar.Focus();
        }


        private void Cargar_datos()    
        {
            Int64 ficha = TxtFicha.Text.Equals(string.Empty) ? 0 : Convert.ToInt64(TxtFicha.Text);
            Int64 caso = Txt_Caso.Text.Equals(string.Empty) ? 0 : Convert.ToInt64(Txt_Caso.Text);
            Int64  tipo = Convert.ToInt64(txttipo_doc.Tag);
            string doc = TxtRut.Text;
            string div = TxtDV.Text;
            string nombre = TxtNomPac.Text;
            Int64 n_caso = Txt_Caso.Text.Equals(string.Empty) ? 0 : Convert.ToInt64(Txt_Caso.Text);

            if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();

            CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_CARGA_DATOS_PAC");
            CnnFalp.ParametroBD("PIN_NUM_DOC", caso, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_FICHA", ficha, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_TIPO", tipo, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_DOC", doc, DbType.String, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_DIV", div, DbType.String, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_NOMBRE", nombre, DbType.String, ParameterDirection.Input);
 
            dt_datos.Load(CnnFalp.ExecuteReader());

            CnnFalp.Cerrar();
       
            if (dt_datos.Rows.Count > 0)
            {
                foreach (DataRow fila in dt_datos.Rows)
                {
                    v_correlativo = Convert.ToInt64(fila["CORRELATIVO"].ToString());
                    v_ficha = fila["FICHA"].ToString();
                    TxtFicha.Text = fila["FICHA"].ToString();
                    int valor= Convert.ToInt32(fila["V_T_DOC"].ToString());
                    v_tipo_doc = fila["TIPO_DOC"].ToString();
                    txttipo_doc.Text = fila["TIPO_DOC"].ToString();
                    if (valor==1)
                    {
                        TxtRut.Text = fila["V_N_DOC"].ToString();
                        TxtDV.Text = fila["DIV"].ToString();
                        v_num_doc = fila["V_N_DOC"].ToString() + "-" + fila["DIV"].ToString();

                    }
                    else{
                        v_num_doc = fila["NUM_DOC"].ToString();
                        TxtOtroDoc.Text = fila["NUM_DOC"].ToString();
                    }
                    v_nombres = fila["NOMBRES"].ToString();
                    v_paterno = fila["AP_PATERNO"].ToString();
                    v_materno = fila["AP_MATERNO"].ToString();
                    v_sexo = fila["SEXO"].ToString();
                    v_fecha_nac = fila["FECHA_NAC"].ToString();
                    v_edad = fila["EDAD"].ToString();
                    v_ecivil = fila["ECIVIL"].ToString();
                    v_tipo_prev = fila["PREVISION"].ToString();
                    v_cod_tipo_prev = fila["COD_TIPO_PREVISION"].ToString();
                    v_previcion = fila["NOMBRE_PREVISION"].ToString();
                    v_cod_previcion = fila["COD_PREVISION"].ToString();
                    v_plan = fila["PLAN"].ToString();
                    v_cod_plan = fila["COD_PLAN_PREVISIONAL"].ToString();
                    Txt_Caso.Text  = fila["NUM_DOC_RES"].ToString();
                    TxtNomPac.Text = v_nombres + " " + v_paterno + " " + v_materno;
                }

                Btn_Ingresar.Enabled = true;
                Btn_Ingresar.Focus();
            }
            else
            {
                MessageBox.Show("Estimado Usuario, La Información no posee Datos Asociados a lo Ingresado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Txt_Caso.Text = "";
                TxtDV.Text = "";
                TxtFicha.Text = "";
                TxtNomPac.Text = "";
                TxtRut.Text = "";
                txttipo_doc.Text = "";
                Opt_Caso.Checked = true;
                txttipo_doc.BackColor = Color.White;

            }
        }

        private void btn_tipo_doc_Click(object sender, EventArgs e)
        {
            txttipo_doc.Text = "";
            txttipo_doc.Tag = "";
            Cargar_tipo_doc();
        }


        private void Cargar_tipo_doc()
        {
            int cod = 0;
            string nom = "";
            Cargar_Parametros(ref Ayuda, 73, txttipo_doc.Text);
            if (!Ayuda.EOF())
            {
                txttipo_doc.Tag = Convert.ToInt32(Ayuda.Fields(0));
                txttipo_doc.Text = Ayuda.Fields(1);
 
            }

        }

        private void Cargar_Parametros(ref AyudaSpreadNet.AyudaSprNet Ayuda, Int32 COD, string DESCRIPCION)
        {
            string[] NomCol = { "Código", "Descripción" };
            int[] AnchoCol = { 80, 350 };
            Ayuda.Nombre_BD_Datos = CnnFalp.DBNombre;
            Ayuda.Pass = CnnFalp.DBPass;
            Ayuda.User = CnnFalp.DBUser;
            Ayuda.TipoBase = 1;
            Ayuda.NombreColumnas = NomCol;
            Ayuda.AnchoColumnas = AnchoCol;
            Ayuda.TituloConsulta = "CARGAR '" + DESCRIPCION + " '";
            Ayuda.Package = "PCK_GES002M";
            Ayuda.Procedimiento = "P_CARGA_PARAM_GRALES";
            Ayuda.Generar_ParametroBD("PIN_CODIGO", COD, DbType.Int32, ParameterDirection.Input);
            Ayuda.Generar_ParametroBD("PIN_DESCRIPCION", DESCRIPCION, DbType.String, ParameterDirection.Input);
            Ayuda.EjecutarSql();
        }

        private void txttipo_doc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Enter) && (e.KeyChar != (char)Keys.Space))
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == (char)13)
            {
                Cargar_Parametros(ref Ayuda, 73, txttipo_doc.Text);
                if (!Ayuda.EOF())
                {
                    txttipo_doc.Tag = Convert.ToInt32(Ayuda.Fields(0));
                    txttipo_doc.Text = Ayuda.Fields(1);

                }
                if (txttipo_doc.Tag != "")
                {
                    TxtRut.Focus();
                }
                else
                {
                    txttipo_doc.Focus();
                }
            }
        }

        private void TxtRut_KeyPress(object sender, KeyPressEventArgs e)
        {
             if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Enter))
            {
                e.Handled = true;
                return;
            }
             if (e.KeyChar == (char)13)
             {
                 if (TxtRut.Text != "")
                 {
                     TxtDV.Focus();
                 }
                 else
                 {
                     TxtRut.Focus();
                 }
              
             }
        }

        private void TxtDV_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Enter) && (e.KeyChar != (char)Keys.Enter) && e.KeyChar != 'K' && e.KeyChar != 'k')
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == (char)13)
            {
                if (TxtDV.Text != "")
                {
                    Cargar_datos();
                    Txt_Caso.Focus();
                }
                else
                {
                    TxtDV.Focus();
                }

            }
        }

        private void TxtFicha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Enter))
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == (char)13)
            {
                if (TxtRut.Text != "")
                {
                    Cargar_datos();
                    Txt_Caso.Focus();
                }
                else
                {
                    TxtFicha.Focus();
                }

            }
        }

        private void TxtNomPac_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Enter) && (e.KeyChar != (char)Keys.Space))
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == (char)13)
            {
                if (TxtRut.Text != "")
                {
                    Cargar_datos();
                    Txt_Caso.Focus();
                }
                else
                {
                    TxtNomPac.Focus();
                }

            }
        }

    }
}
