using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Windows.Forms;
using AplicacionFalp;
using Falp;


namespace GES002M
{
    public partial class Frm_Agregar_Observacion : Form
    {
        ConectarFalp CnnFalp;
        Configuration Config;
        string[] Conexion = { "", "", "" };
        string Db_Usuario;
        DataTable dt_observacion = new DataTable();
        string PCK = "PCK_GES002M";
        public string OBSERVACION = "";
        Int64 v_caso_det = 0;
        Int64 v_tipo = 0;
        public Frm_Agregar_Observacion()
        {
            InitializeComponent();
        }


        public Frm_Agregar_Observacion(Int64 caso_det,Int64 tipo)
        {
            v_caso_det = caso_det;
            v_tipo = tipo;
            InitializeComponent();
        }

        private void Frm_Agregar_Observacion_Load(object sender, EventArgs e)
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

            Grilla_observ();
            btn_guardar.Enabled = false;
 
        }

        private void btn_agregar_Click(object sender, EventArgs e)
        {
            try
            {
               
                DataRow fila = dt_observacion.NewRow();
                fila["ID"] = 0;
                fila["ID_CASO_DET"] = v_caso_det;
                fila["OBSERVACION"] = txtobservacion.Text.TrimEnd();
                fila["VIGENCIA"] = "S";
                fila["TIPO"] = v_tipo;
                dt_observacion.Rows.Add(fila);
                grilla_coment.AutoGenerateColumns = false;
                grilla_coment.DataSource = dt_observacion;

                MessageBox.Show("Estimado Usuario, Fue Agregado Correctamente La Observación a la Lista ", "Informacion Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btn_guardar.Enabled = true;
                txtobservacion.Text = "";
            }
            catch (Exception ex)
            {
                CnnFalp.ReversarTransaccion();
                MessageBox.Show(ex.Message, "Error al intentar grabar ");
            }        
        }


        private void btn_guardar_Click(object sender, EventArgs e)
        {
          try
            {
                  if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();

                    foreach (DataRow fila in dt_observacion.Rows)
                    {
                        CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_GUARDAR_COMENTARIO");

                        CnnFalp.ParametroBD("PIN_ID", fila["ID"].ToString(), DbType.Int32, ParameterDirection.Input);
                        CnnFalp.ParametroBD("PIN_CASO_DET", fila["ID_CASO_DET"].ToString(), DbType.Int64, ParameterDirection.Input);
                        CnnFalp.ParametroBD("PIN_OBSERVACION", fila["OBSERVACION"].ToString(), DbType.String, ParameterDirection.Input);
                        CnnFalp.ParametroBD("PIN_VIGENCIA", fila["VIGENCIA"].ToString(), DbType.String, ParameterDirection.Input);
                        CnnFalp.ParametroBD("PIN_USUARIO", Db_Usuario, DbType.String, ParameterDirection.Input);
                        CnnFalp.ParametroBD("PIN_TIPO", fila["TIPO"].ToString(), DbType.Int64, ParameterDirection.Input);

                        int registro = CnnFalp.ExecuteNonQuery();

                    }
                    MessageBox.Show("Estimado Usuario, Fue Guardado Correctamente la Información ", "Informacion Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                      
                CnnFalp.Cerrar();
                Grilla_observ();
                btn_guardar.Enabled = false;
               
            }
          catch (Exception ex)
          {
              CnnFalp.ReversarTransaccion();
              MessageBox.Show(ex.Message, "Error al intentar grabar ");
          }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtobservacion.Text = "";
        }


        private void Grilla_observ()
        {
            dt_observacion.Clear();
            if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();

            CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_CARGAR_OBSERVACION");
            CnnFalp.ParametroBD("PIN_CASO_DET", v_caso_det, DbType.Int64, ParameterDirection.Input);
            CnnFalp.ParametroBD("PIN_TIPO", v_tipo, DbType.Int64, ParameterDirection.Input);
            dt_observacion.Load(CnnFalp.ExecuteReader());
            grilla_coment.DataSource = new DataView(dt_observacion, "VIGENCIA ='S'", "", DataViewRowState.CurrentRows);
          
            CnnFalp.Cerrar();
           
        }

       

        private void grilla_coment_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Int64 cod_id = Convert.ToInt64(grilla_coment.Rows[e.RowIndex].Cells["ID"].Value.ToString());
            string nom = grilla_coment.Rows[e.RowIndex].Cells["OBSERV"].Value.ToString();
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 0)
                {
                    DialogResult opc = MessageBox.Show("Estimado Usuario, Esta seguro de Eliminar Esta Observación " +  nom + "", "Informacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (opc == DialogResult.Yes)
                    {
                        btn_guardar.Enabled = true;
                        if (cod_id==0)
                        {
                            DataGridViewRow row = grilla_coment.Rows[e.RowIndex];
                            DataGridViewTextBoxCell Id_fila = row.Cells["ID"] as DataGridViewTextBoxCell;
                            Int64 Fila = Convert.ToInt16(Id_fila.Value);

                            foreach (DataRow xd in dt_observacion.Select("ID = " + Fila))
                            {
                                xd.Delete();
                            }  
                        }
                        else{

                            foreach (DataRow ad in dt_observacion.Select("ID= " + cod_id + ""))
                            {
                                string VIG = ad["VIGENCIA"].ToString();

                                if (VIG == "S")
                                {
                                    ad["VIGENCIA"] = "N";
                                }
                            }

                        }

                        MessageBox.Show("Estimado Usuario, Fue Eliminado Correctamente la Observación ", "Informacion Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dt_observacion.AcceptChanges();
                        grilla_coment.DataSource = new DataView(dt_observacion, "VIGENCIA ='S'", "", DataViewRowState.CurrentRows);
                    }

                }

            }
        }




     

    }
}
