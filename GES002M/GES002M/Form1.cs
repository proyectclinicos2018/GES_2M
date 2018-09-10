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

namespace GES002M
{
    public partial class GES002M : Form
    {
        ConectarFalp CnnFalp;
        Configuration Config;
        string[] Conexion = { "", "", "" };
        string Db_Usuario;
        string PCK = "PCK_GES002M";
        Int64 NCaso = 0;
        Int64 NSubetapa = 0;
        long v_correlativo = 0;
        int v_cod_id=0;
        int v_cod_pre = 0;
        string v_observacion = "";
        string usuario = "";
        string mod_pat = "N";
        DataTable dt_historial = new DataTable();
        DataTable dt_caso = new DataTable();
        DataTable dt_patologias = new DataTable();
        DataTable dt_prestaciones = new DataTable();


        public GES002M()
        {
            InitializeComponent();
        }

        private void GES002M_Load(object sender, EventArgs e)
        {
            //gr_patologia.Enabled = false;
            gr_dias.Enabled = false;
            btnAgregar.Enabled = false;
            btn_guardar.Enabled = false;
            Cargar_Conexion();
            Carga_FrmBusqueda();
            txtcantidad.Enabled = false;
            txtvalor.Enabled = false;
            txtcantidad.Text = "1";
            btn_confirmar_pres.Visible = false;
            Habilitar_prest();
           
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
            usuario = ValidaMenu.LeeUsuarioMenu().Equals(string.Empty) ? "SICI" : ValidaMenu.LeeUsuarioMenu();
        }

        private void btnCargaPac_Click(object sender, EventArgs e)
        {
          //  CargaPac();
        }

        public void Carga_FrmBusqueda()
        {
            Frm_Buscar busqueda = new Frm_Buscar();
            this.Show();


            busqueda.ShowDialog(this);
            busqueda.Dispose();
            NCaso = Convert.ToInt64(busqueda.Tag);
            v_correlativo = Convert.ToInt64(busqueda.v_correlativo);
            if (NCaso > 0)
            {
                txtFicha.Text = busqueda.v_ficha;
                txtTipoDoc.Text = busqueda.v_tipo_doc;

                txtDocumento.Text = busqueda.v_num_doc;
                txtNombre.Text = busqueda.v_nombres;
                txtPaterno.Text = busqueda.v_paterno;
                txtMaterno.Text = busqueda.v_materno;
                txtSexo.Text = busqueda.v_sexo;
                txtFechaNac.Text = busqueda.v_fecha_nac;
                txtEdad.Text = busqueda.v_edad;
                txtECivil.Text = busqueda.v_ecivil;
                txtTipoPrev.Text = busqueda.v_tipo_prev;
                txtTipoPrev.Tag = busqueda.v_cod_tipo_prev;
                txtPrevision.Text = busqueda.v_previcion;
                txtPrevision.Tag = busqueda.v_cod_previcion;
                txtPlanPrev.Text = busqueda.v_plan;
                txtPlanPrev.Tag = busqueda.v_cod_plan;

                Cargar_historial_num_caso();             
                txtEtapa.Focus();
                txtDerivador.Enabled = false;
                txtRespaldo.Enabled = false;
                txtDocRespaldo.Enabled = false;

            }
            else
            {
                this.Close();
            }


        }

       #region  Historial

        private void Bloqueo()
        {
            gr_dias.Enabled = false;
            gr_patologia.Enabled = false;
            Gv_Casos.Enabled = false;
            groupBox3.Enabled = false;
            grilla_prestaciones.Enabled = false;
            btnAgregar.Enabled = false;
            btn_guardar.Enabled = false;
            btn_agregar.Enabled = false;
        }
        private void Habilitar()
        {
            gr_dias.Enabled = true;
           // gr_patologia.Enabled = true;
            Gv_Casos.Enabled = true;
            groupBox3.Enabled = true;
            grilla_prestaciones.Enabled = true;
            btnAgregar.Enabled = true;
            btn_guardar.Enabled = true;
            btn_agregar.Enabled = true;
       



        }




        private void Cargar_historial_num_caso()
        {

            if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();

            CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_CARGA_HIST_N_CASO");
            CnnFalp.ParametroBD("PIN_CORRELATIVO", v_correlativo, DbType.Int64, ParameterDirection.Input);
            dt_historial.Load(CnnFalp.ExecuteReader());

            CnnFalp.Cerrar();

            if (dt_historial.Rows.Count == 0)
            {
                MessageBox.Show("Estimado usuario, No existen Num Casos Asociados a este Pacientes.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {

                if (dt_historial.Rows.Count == 1)
                {
                    btnAgregar.Enabled = true;
                    btn_guardar.Enabled = true;
                    Cargar_Listado_Patologias();
                    foreach (DataRow fila in dt_historial.Select(" RESPALDO= " + NCaso))
                    {
                        NCaso = Convert.ToInt64(fila["N_CASO"].ToString());
                        txtDerivador.Text = fila["DERIVADOR"].ToString();
                        txtRespaldo.Text = fila["NOM_TIPO_DOC"].ToString();
                        txtDocRespaldo.Text = fila["RESPALDO"].ToString();
                        txtPatologia.Tag = fila["COD_PATOLOGIA"].ToString();
                        txtPatologia.Text = fila["NOM_PATOLOGIA"].ToString();
                        int cod_estado = Convert.ToInt32(fila["COD_ESTADO"].ToString());
                        string var = fila["ESTADO"].ToString();
                        if (cod_estado==4)
                        {
                            txtnum_caso.ForeColor = Color.Red;
                            txtnum_caso.Text = fila["N_CASO"].ToString();
                            txtestado_caso.ForeColor = Color.Red;
                            txtestado_caso.Text = fila["ESTADO"].ToString();

                            Bloqueo();
                        }

                        else
                        {
                            txtnum_caso.ForeColor = Color.Red;
                            txtnum_caso.Text = fila["N_CASO"].ToString();
                            txtestado_caso.ForeColor = Color.Green;
                            txtestado_caso.Text = fila["ESTADO"].ToString();
                            Habilitar();

                        }
                    }
                    txtPatologia.Enabled = false;
                    btnPatologia.Enabled = false;
                    txtEtapa.Enabled = true;
                    btnEtapa.Enabled = true;
                    txtSubEtapa.Enabled = false;
                    btnSubEtapa.Enabled = false;
                    txtPaquete.Enabled = false;
                    btnPaquete.Enabled = false;
                    Cargar_Listado_Prestaciones();
                    Cargar_Listado_Patologias();

                }
                else
                {

                }

            }
            grilla_historial.DataSource = dt_historial;
            ocultargrilla();
        }

        #region Ocultar Columnas

        protected void ocultargrilla()
        {
            grilla_historial.AutoResizeColumns();
            grilla_historial.Columns["DERIVADOR"].Visible = false;
            grilla_historial.Columns["NUM_DOC"].Visible = false;
            grilla_historial.Columns["RESPALDO"].Visible = false;
            grilla_historial.Columns["COD_ESTADO"].Visible = false;
            grilla_historial.Columns["COD_ESTADO"].Visible = false;
     

            //grilla_alimentos.Columns["Vigencia"].Visible = false;


        }

        #endregion

        private void gilla_historial_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= -1)
            {
               
                    DialogResult opc = MessageBox.Show("Estimado Usuario, Esta seguro de Seleccionar este caso", "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (opc == DialogResult.Yes)
                    {
                     
                        int cod = Convert.ToInt32(grilla_historial.Rows[e.RowIndex].Cells["N_CASO"].Value.ToString());

                        foreach (DataRow fila in dt_historial.Select(" N_CASO= " + cod))
                        {
                            NCaso = Convert.ToInt64(fila["N_CASO"].ToString());
                            txtDerivador.Text = fila["DERIVADOR"].ToString();
                            txtRespaldo.Text = fila["NOM_TIPO_DOC"].ToString();
                            txtDocRespaldo.Text = fila["RESPALDO"].ToString();
                            txtPatologia.Tag = fila["COD_PATOLOGIA"].ToString();
                            txtPatologia.Text = fila["NOM_PATOLOGIA"].ToString();
                            int cod_estado = Convert.ToInt32(fila["COD_ESTADO"].ToString());
                            string var = fila["ESTADO"].ToString();
                            if (cod_estado == 4)
                            {
                                txtnum_caso.ForeColor = Color.Red;
                                txtnum_caso.Text = fila["N_CASO"].ToString();
                                txtestado_caso.ForeColor = Color.Red;
                                txtestado_caso.Text = fila["ESTADO"].ToString();
                                Bloqueo();
                            }

                            else
                            {
                                txtnum_caso.ForeColor = Color.Red;
                                txtnum_caso.Text = fila["N_CASO"].ToString();
                                txtestado_caso.ForeColor = Color.Green;
                                txtestado_caso.Text = fila["ESTADO"].ToString();
                                Habilitar();
                                txtSubEtapa.Enabled = false;
                                btnSubEtapa.Enabled = false;
                                txtPaquete.Enabled = false;
                                btnPaquete.Enabled = false;
                            }
                            
                        }
                        txtPatologia.Enabled = false;
                        btnPatologia.Enabled = false;
 
                        Cargar_Listado_Patologias();
                        Cargar_Listado_Prestaciones();
                    }
               
            }

        }

       #endregion 

       #region  Patologias

       #region  Cargar

       #region  Grilla

       private void Cargar_Listado_Patologias()
       {

           if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();
           dt_patologias.Clear();
           CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_CARGA_PATOLOGIAS_PAC");
           CnnFalp.ParametroBD("PIN_CORRELATIVO", v_correlativo, DbType.Int64, ParameterDirection.Input);
           CnnFalp.ParametroBD("PIN_COD_CASO", NCaso, DbType.Int64, ParameterDirection.Input);
           dt_patologias.Load(CnnFalp.ExecuteReader());
           Gv_Casos.DataSource = new DataView(dt_patologias, "VIG ='S'", "", DataViewRowState.CurrentRows);
           CnnFalp.Cerrar();
       }

       private void Gv_Casos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
       {
          
           v_cod_id = Convert.ToInt32(Gv_Casos.Rows[e.RowIndex].Cells["ID_FILA"].Value.ToString());
           Int64 cod_patologia = Convert.ToInt64(Gv_Casos.Rows[e.RowIndex].Cells["COD_PATOLOGIA"].Value.ToString());
           Int64 cod_etapa = Convert.ToInt64(Gv_Casos.Rows[e.RowIndex].Cells["COD_ETAPA"].Value.ToString());
           Int64 cod_sub_etapa = Convert.ToInt64(Gv_Casos.Rows[e.RowIndex].Cells["COD_SUB_ETAPA"].Value.ToString());
           Int64 cod_paquete = Convert.ToInt64(Gv_Casos.Rows[e.RowIndex].Cells["COD_PAQUETE"].Value.ToString());
           string obs = Gv_Casos.Rows[e.RowIndex].Cells["OBSERVACION"].Value.ToString();
           string nom = Gv_Casos.Rows[e.RowIndex].Cells["NOM_PATOLOGIA"].Value.ToString();
           if (e.RowIndex >= 0)
           {
               if (e.ColumnIndex == 17)
               {
                   DialogResult opc = MessageBox.Show("Estimado Usuario, ¿ Esta seguro de Modificar el Estado de la Patología " + nom + " ?", "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                   if (opc == DialogResult.Yes)
                   {

                       Cargar_estado_patologias();
                   }
               }

               else
               {
                   if (e.ColumnIndex == 0)
                   {
                       DialogResult opc = MessageBox.Show("Estimado Usuario, ¿Esta seguro de Eliminar la Patología " + nom + " ?", "Informacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                       if (opc == DialogResult.Yes)
                       {
                           if (v_cod_id == 0)
                           {
                               Gv_Casos.Rows.RemoveAt(Gv_Casos.CurrentRow.Index);
                               dt_patologias.AcceptChanges();
                               MessageBox.Show("Estimado Usuario, Fue Eliminado Correctamente la Patología", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                               limpiar_patologias();
                           }
                           else
                           {
                               foreach (DataRow fila in dt_patologias.Select(" ID_FILA= " + v_cod_id))
                               {

                                   if (fila["VIG"].ToString() == "S")
                                   {
                                       fila["VIG"] = "N";
                                   }
                                   else
                                   {
                                       fila["VIG"] = "S";
                                   }

                               }
                               dt_patologias.AcceptChanges();
                               Gv_Casos.DataSource = new DataView(dt_patologias, "VIG ='S'", "", DataViewRowState.CurrentRows);
                               MessageBox.Show("Estimado Usuario, Fue Eliminado Correctamente la Patología", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                               limpiar_patologias();
                           }
                       }
                   }

                   else
                   {
                       if (e.ColumnIndex == 1)
                       {

                           DialogResult opc = MessageBox.Show("Estimado Usuario, ¿ Esta Seguro de Modificar la Patología "+ nom +" ?", "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                           if (opc == DialogResult.Yes)
                           {
                               txtPatologia.Tag = Convert.ToInt32(Gv_Casos.Rows[e.RowIndex].Cells["COD_PATOLOGIA"].Value.ToString());
                               txtPatologia.Text = Gv_Casos.Rows[e.RowIndex].Cells["NOM_PATOLOGIA"].Value.ToString();
                               txtEtapa.Tag = Convert.ToInt32(Gv_Casos.Rows[e.RowIndex].Cells["COD_ETAPA"].Value.ToString());
                               txtEtapa.Text = Gv_Casos.Rows[e.RowIndex].Cells["NOM_ETAPA"].Value.ToString();
                               txtSubEtapa.Tag = Convert.ToInt32(Gv_Casos.Rows[e.RowIndex].Cells["COD_SUB_ETAPA"].Value.ToString());
                               txtSubEtapa.Text = Gv_Casos.Rows[e.RowIndex].Cells["NOM_SUB_ETAPA"].Value.ToString();
                               txtPaquete.Tag = Convert.ToInt32(Gv_Casos.Rows[e.RowIndex].Cells["COD_PAQUETE"].Value.ToString());
                               txtPaquete.Text = Gv_Casos.Rows[e.RowIndex].Cells["NOM_PAQUETE"].Value.ToString();
                               txtDiasVig.Text = Gv_Casos.Rows[e.RowIndex].Cells["DIAS"].Value.ToString();
                               txtIniGES.Text = Gv_Casos.Rows[e.RowIndex].Cells["FEC_INICIO"].Value.ToString();
                               txtRecepcion.Text = Gv_Casos.Rows[e.RowIndex].Cells["FEC_RECEPCION"].Value.ToString();
                               mod_pat = "S";
                               btnAgregar.Visible = false;
                               btn_confirmar.Visible = true;
                               txtEtapa.Enabled=true;
                               btnEtapa.Enabled=true;
                               txtSubEtapa.Enabled = false;
                               btnSubEtapa.Enabled = false;
                               txtPaquete.Enabled = false;
                               btnPaquete.Enabled = false;


                           }
                       }
                       else
                       {
                           if (e.ColumnIndex == 2)
                           {
                               DialogResult opc = MessageBox.Show("Estimado Usuario,¿ Esta Seguro de Ingresar una Observación ?", "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                               if (opc == DialogResult.Yes)
                               {
                                   if (v_cod_id == 0 && obs == "" || v_cod_id == 0 && obs != "")
                                   {
                                       Frm_Agregar_Observacion_v2 frm = new Frm_Agregar_Observacion_v2(obs);
                                       frm.ShowDialog();
                                       v_observacion = frm.OBSERVACION;

                                       foreach (DataRow xd in dt_patologias.Select("COD_PATOLOGIA=" + cod_patologia + " AND  COD_ETAPA=" + cod_etapa + " AND  COD_SUB_ETAPA=" + cod_sub_etapa + " AND  COD_PAQUETE=" + cod_paquete + ""))
                                       {
                                           xd["OBSERVACION"] = v_observacion;
                                       }
                                       dt_patologias.AcceptChanges();
                                   }
                                   else
                                   {
                                       Frm_Agregar_Observacion frm = new Frm_Agregar_Observacion(v_cod_id, 1);
                                       frm.ShowDialog();

                                   }

                               }
                           }
                       }
                   }

               }
           }
           btn_guardar.Enabled = true;
       }

       #endregion 

       #region  Patalogia

       private void btnPatologia_Click(object sender, EventArgs e)
       {

           Cargar_Parametros(ref Ayuda, 81, txtPatologia.Text.ToUpper(),"Patología");
           if (!Ayuda.EOF())
           {
               txtPatologia.Tag = Convert.ToInt32(Ayuda.Fields(0));
               txtPatologia.Text = Ayuda.Fields(1);
               txtEtapa.Focus();
           }
       }





       private void txtPatologia_KeyPress(object sender, KeyPressEventArgs e)
       {
           if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Enter) && (e.KeyChar != (char)Keys.Space))
           {

               e.Handled = true;
               return;
           }

           if (e.KeyChar == (char)13)
           {
               Cargar_Parametros(ref Ayuda, 81, txtPatologia.Text.ToUpper(),"Patología");
               if (!Ayuda.EOF())
               {
                   txtPatologia.Tag = Convert.ToInt32(Ayuda.Fields(0));
                   txtPatologia.Text = Ayuda.Fields(1);
                   txtEtapa.Focus();
               }
           }
       }


       #endregion

       #region  Etapa

       private void btnEtapa_Click(object sender, EventArgs e)
       {
           txtEtapa.Tag = 0; txtEtapa.Text = "";
           Cargar_Parametros(ref Ayuda, 143, txtEtapa.Text,"Etapa");
           if (!Ayuda.EOF())
           {
               txtEtapa.Tag = Convert.ToInt32(Ayuda.Fields(0));
               txtEtapa.Text = Ayuda.Fields(1);
               btnSubEtapa.Enabled = true;
               txtSubEtapa.Enabled = true;
               txtPaquete.Enabled = false;
               btnPaquete.Enabled = false;
               txtSubEtapa.Focus();
           }
           else
           {
               txtEtapa.Focus();
           }
       }

       private void Cargar_Parametros(ref AyudaSpreadNet.AyudaSprNet Ayuda, Int32 COD, string DESCRIPCION, string titulo)
       {
           string[] NomCol = { "Código", "Descripción" };
           int[] AnchoCol = { 80, 350 };
           Ayuda.Nombre_BD_Datos = CnnFalp.DBNombre;
           Ayuda.Pass = CnnFalp.DBPass;
           Ayuda.User = CnnFalp.DBUser;
           Ayuda.TipoBase = 1;
           Ayuda.NombreColumnas = NomCol;
           Ayuda.AnchoColumnas = AnchoCol;
           Ayuda.TituloConsulta = "Seleccionar '" + titulo + " '";
           Ayuda.Package = "PCK_GES002M";
           Ayuda.Procedimiento = "P_CARGA_PARAM_GRALES";
           Ayuda.Generar_ParametroBD("PIN_CODIGO", COD, DbType.Int32, ParameterDirection.Input);
           Ayuda.Generar_ParametroBD("PIN_DESCRIPCION", DESCRIPCION, DbType.String, ParameterDirection.Input);
           Ayuda.EjecutarSql();
       }

       private void txtEtapa_KeyPress(object sender, KeyPressEventArgs e)
       {
           if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Enter) && (e.KeyChar != (char)Keys.Space))
           {

               e.Handled = true;
               return;
           }
           if (e.KeyChar == (char)13)
           {
               Cargar_Parametros(ref Ayuda, 143, txtEtapa.Text ,"Etapa");
               if (!Ayuda.EOF())
               {
                   txtEtapa.Tag = Convert.ToInt32(Ayuda.Fields(0));
                   txtEtapa.Text = Ayuda.Fields(1);
                   btnSubEtapa.Enabled = true;
                   txtSubEtapa.Enabled = true;
                   txtPaquete.Enabled = false;
                   btnPaquete.Enabled = false;
                   txtSubEtapa.Focus();
               }
               else
               {
                   txtEtapa.Focus();
               }
           }
       }

       #endregion

       #region  Sub Etapas

       private void btnSubEtapa_Click(object sender, EventArgs e)
       {
           txtSubEtapa.Tag = 0; txtSubEtapa.Text = "";
           Cargar_Parametros(ref Ayuda, 144, txtSubEtapa.Text,"Sub Etapa");
           if (!Ayuda.EOF())
           {
               txtSubEtapa.Tag = Convert.ToInt32(Ayuda.Fields(0));
               txtSubEtapa.Text = Ayuda.Fields(1);
               txtPaquete.Enabled = true;
               btnPaquete.Enabled = true;
               txtPaquete.Focus();
           }
           else
           {
               txtSubEtapa.Focus();
           }
       }

       private void txtSubEtapa_KeyPress(object sender, KeyPressEventArgs e)
       {
           if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Enter) && (e.KeyChar != (char)Keys.Space))
           {

               e.Handled = true;
               return;
           }
           if (e.KeyChar == (char)13)
           {
               Cargar_Parametros(ref Ayuda, 144, txtSubEtapa.Text, "Sub Etapa");
               if (!Ayuda.EOF())
               {
                   txtSubEtapa.Tag = Convert.ToInt32(Ayuda.Fields(0));
                   txtSubEtapa.Text = Ayuda.Fields(1);
                   btnPaquete.Enabled = true;
                   txtPaquete.Enabled = true;
                   txtPaquete.Focus();
               }
               else
               {
                   txtSubEtapa.Focus();
               }
           }
       }



       #endregion

       #region  Paquetes

       private void btnPaquete_Click(object sender, EventArgs e)
       {
           txtPaquete.Text = ""; txtPaquete.Tag = 0;
           TraePaquetes(ref Ayuda, txtPaquete.Text);
           if (!Ayuda.EOF())
           {
               txtPaquete.Tag = Convert.ToInt32(Ayuda.Fields(0));
               txtPaquete.Text = Ayuda.Fields(1);
               // txtDiasVig.Text = Ayuda.Fields(3);
           }
           else
           {
               txtPaquete.Focus();
           }

       }


       private void TraePaquetes(ref AyudaSpreadNet.AyudaSprNet Ayuda, string DESCRIPCION)
       {
           string[] NomCol = { "Código", "Descripción" };
           int[] AnchoCol = { 50, 320 };
           Ayuda.Nombre_BD_Datos = CnnFalp.DBNombre;
           Ayuda.Pass = CnnFalp.DBPass;
           Ayuda.User = CnnFalp.DBUser;
           Ayuda.TipoBase = 1;
           Ayuda.NombreColumnas = NomCol;
           Ayuda.AnchoColumnas = AnchoCol;
           Ayuda.TituloConsulta = "Seleccionar Paquete";
           Ayuda.Package = "PCK_GES002M";
           Ayuda.Procedimiento = "P_CARGA_PAQUETE1";
           Ayuda.Generar_ParametroBD("PIN_DESCRIPCION", DESCRIPCION, DbType.String, ParameterDirection.Input);

           Ayuda.EjecutarSql();
       }

       private void txtPaquete_KeyPress(object sender, KeyPressEventArgs e)
       {
           if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Enter) && (e.KeyChar != (char)Keys.Space))
           {

               e.Handled = true;
               return;
           }
           if (e.KeyChar == (char)13)
           {
               TraePaquetes(ref Ayuda, txtPaquete.Text);
               if (!Ayuda.EOF())
               {
                   txtPaquete.Tag = Convert.ToInt32(Ayuda.Fields(0));
                   txtPaquete.Text = Ayuda.Fields(1);
                   txtDiasVig.Enabled = true;
                   txtIniGES.Enabled = true;
                   txtRecepcion.Enabled = true;
                   txtDiasVig.Focus();
               }
               else
               {
                   txtPaquete.Focus();
               }
           }
       }



       #endregion

       #region  Estado

       private void Cargar_estado_patologias()
       {
           int cod = 0;
           string nom = "";
           Cargar_Parametros(ref Ayuda, 148, "","Estado");
           if (!Ayuda.EOF())
           {
               cod = Convert.ToInt32(Ayuda.Fields(0));
               nom = Ayuda.Fields(1);
               mod_estado_pat(cod, nom);
           }

       }
       #endregion

       #endregion


       #region  Botones

       private void btn_confirmar_Click(object sender, EventArgs e)
       {
           DialogResult opc = MessageBox.Show("Estimado Usuario, ¿ Esta seguro de Realizar los Cambios a la Patología " + txtPatologia.Text+ "?", "Informacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

           if (opc == DialogResult.Yes)
           {
               if (Validar_campos_pat())
               {

                   foreach (DataRow fila in dt_patologias.Select(" ID_FILA= '" + v_cod_id + "' AND  COD_ETAPA='" + txtEtapa.Tag + "' AND  COD_SUB_ETAPA='" + txtSubEtapa.Tag + "' AND  COD_PAQUETE='" + txtPaquete.Tag + "'"))
                      
                   {
                       fila["COD_PATOLOGIA"] = txtPatologia.Tag;
                       fila["NOM_PATOLOGIA"] = txtPatologia.Text;
                       fila["COD_ETAPA"] = txtEtapa.Tag;
                       fila["NOM_ETAPA"] = txtEtapa.Text;
                       fila["COD_SUB_ETAPA"] = txtSubEtapa.Tag;
                       fila["NOM_SUB_ETAPA"] = txtSubEtapa.Text;
                       fila["COD_PAQUETE"] = txtPaquete.Tag;
                       fila["NOM_PAQUETE"] = txtPaquete.Text;
                       fila["DIAS"] = txtDiasVig.Text;
                       fila["FECHA_INICIO"] = txtIniGES.Text;
                       fila["FECHA_RECEPCION"] = txtRecepcion.Text;
                       fila["OBSERVACION"] = "";

                       MessageBox.Show("Estimado usuario, Cambio  realizado con éxito", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   }
               }
               else
               {
                   if (mod_pat == "S")
                   {
                       foreach (DataRow fila in dt_patologias.Select(" ID_FILA= '" + v_cod_id + "' AND  COD_ETAPA='" + txtEtapa.Tag + "' AND  COD_SUB_ETAPA='" + txtSubEtapa.Tag + "' AND  COD_PAQUETE='" + txtPaquete.Tag + "'"))
                       {
                           fila["COD_PATOLOGIA"] = txtPatologia.Tag;
                           fila["NOM_PATOLOGIA"] = txtPatologia.Text;
                           fila["COD_ETAPA"] = txtEtapa.Tag;
                           fila["NOM_ETAPA"] = txtEtapa.Text;
                           fila["COD_SUB_ETAPA"] = txtSubEtapa.Tag;
                           fila["NOM_SUB_ETAPA"] = txtSubEtapa.Text;
                           fila["COD_PAQUETE"] = txtPaquete.Tag;
                           fila["NOM_PAQUETE"] = txtPaquete.Text;
                           fila["DIAS"] = txtDiasVig.Text;
                           fila["FECHA_INICIO"] = txtIniGES.Text;
                           fila["FECHA_RECEPCION"] = txtRecepcion.Text;
                           fila["OBSERVACION"] = "";

                           MessageBox.Show("Estimado usuario, Cambio  realizado con éxito", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       }
                   }
                   else
                   {
                       MessageBox.Show("Estimado Usuario, La Etapa, Sub Etapa y Paquete, ya se encuentra Registrada en la Lista", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                               
                   }
               }
               limpiar_patologias();


               btnAgregar.Visible = true;
               btn_confirmar.Visible = false;
               limpiar_patologias();
           }

       }

       private void LimpiarTxt(Control Crontrol)
       {
           foreach (Control c in Crontrol.Controls)
           {
               if (c is TextBox)
               {
                   c.Text = string.Empty;
                   c.Tag = string.Empty;
               }
           }
       }

       private void btnAgregar_Click(object sender, EventArgs e)
       {
            DialogResult opc = MessageBox.Show("Estimado Usuario,¿Esta Seguro de Agregar a la  Patologia " + txtPatologia.Text + " la Etapa "+ txtEtapa.Text +" ?", "Informacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

              if (opc == DialogResult.Yes)
              {
                  try
                  {
                      if (Validar_paquete())
                      {
                          if (Validar_campos_pat())
                          {
                              DataRow fila = dt_patologias.NewRow();
                              fila["ID_FILA"] = 0;
                              fila["COD_PATOLOGIA"] = txtPatologia.Tag;
                              fila["NOM_PATOLOGIA"] = txtPatologia.Text;
                              fila["COD_ETAPA"] = txtEtapa.Tag;
                              fila["NOM_ETAPA"] = txtEtapa.Text;
                              fila["COD_SUB_ETAPA"] = txtSubEtapa.Tag;
                              fila["NOM_SUB_ETAPA"] = txtSubEtapa.Text;
                              fila["COD_PAQUETE"] = txtPaquete.Tag;
                              fila["NOM_PAQUETE"] = txtPaquete.Text;
                              fila["DIAS"] = txtDiasVig.Text.Equals(string.Empty) ? 0 : Convert.ToInt32(txtDiasVig.Text);
                              fila["FECHA_INICIO"] = txtIniGES.Text;
                              fila["FECHA_RECEPCION"] = txtRecepcion.Text;
                              fila["COD_ESTAD"] = 1;

                              fila["VIG"] = "S";
                              fila["NOM_ESTAD"] = "Iniciado";


                              dt_patologias.Rows.Add(fila);
                              Gv_Casos.AutoGenerateColumns = false;
                              Gv_Casos.DataSource = dt_patologias;

                          }
                          limpiar_patologias();
                      }

                      else
                      {
                          MessageBox.Show("Estimado Usuario, El Paquete no esta asociado con esa Institución, por Favor Comunicarse con el área Comercial, para su vinculación  ", "Informacion Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                          txtPaquete.Text = "";
                          txtPaquete.Tag = "";
                      }

                  }
                  catch (Exception ex)
                  {
                     
                      MessageBox.Show(ex.Message, "Error al intentar grabar ");
                  }
              }

              txtEtapa.Focus();

       }

       private Boolean Validar_paquete()
       {
           Boolean var = false;

           DataTable dt = new DataTable();
           if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();

           CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_VALIDAR_PREV_PAQUETE");
           CnnFalp.ParametroBD("PIN_PAQUETE", Convert.ToInt64(txtPaquete.Tag), DbType.Int64, ParameterDirection.Input);
           CnnFalp.ParametroBD("PIN_PREVISION", Convert.ToInt64(txtPrevision.Tag), DbType.Int64, ParameterDirection.Input);

           dt.Load(CnnFalp.ExecuteReader());

           if (dt.Rows.Count > 0)
           {
               var = true;
           }

           return var;
       }

       private void btn_guardar_Click(object sender, EventArgs e)
       {
           if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();
           try
           {

               DialogResult opc = MessageBox.Show("Estimado Usuario, Desea Guardar La información Ingresada?", "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

               if (opc == DialogResult.Yes)
               {

                   if (dt_patologias.Rows.Count > 0)
                   {

                       CnnFalp.IniciarTransaccion();
                       Guardar_Patologias();
                       Guardar_Prestaciones();
                       CnnFalp.ConfirmarTransaccion();
                       CnnFalp.Cerrar();

                       MessageBox.Show("Estimado Usuario, La información fue Guardada Correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   
                   }
                   else
                   {
                       MessageBox.Show("Estimado Usuario, No existen  Patologias Para Guardar en la Lista ", "Información", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   }
               }

               Limpiar();
              // dt_prestaciones.Clear();
           }
           catch (Exception ex)
           {
               MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               CnnFalp.ReversarTransaccion();

           }
       }

       private void btn_limpiar_Click(object sender, EventArgs e)
       {
           Limpiar();

       }

       #endregion

       #region  Metodos

       private void Limpiar()
       {
           limpiar_patologias();
           btnAgregar.Enabled = false;
           txtDiasRestantes.Text = "";
           txtFecTermino.Text = "";
           LimpiarTxt(grpDatosGES);
           LimpiarTxt(gr_patologia);
           dt_patologias.Clear();
           dt_prestaciones.Clear();
           btnAgregar.Enabled = false;
           btn_agregar.Enabled = false;

       }


       private void mod_estado_pat(int cod, string nom)
       {
           foreach (DataRow fila3 in dt_patologias.Select(" ID_FILA ='" + v_cod_id + "'"))
           {
               fila3["COD_ESTAD"] = cod;
               fila3["NOM_ESTAD"] = nom;

               if (cod == 3)
               {
                   fila3["FECHA_TERMINO"] = DateTime.Now.ToString("dd-MM-yyyy");
               }
               else
               {
                   fila3["FECHA_TERMINO"] = null;
               }
           }

           dt_patologias.AcceptChanges();
           Gv_Casos.DataSource = dt_patologias;

       }

       private void limpiar_patologias()
       {
        //   txtPatologia.Tag = "";
         //  txtPatologia.Text = "";
           txtEtapa.Tag = "";
           txtEtapa.Text = "";
           txtSubEtapa.Tag = "";
           txtSubEtapa.Text = "";
           txtPaquete.Tag = "";
           txtPaquete.Text = "";
           txtDiasVig.Text = "";
           txtIniGES.Text = "";
           txtRecepcion.Text = "";
           txtDiasRestantes.Text = "";
           txtFecTermino.Text = "";

           btnAgregar.Visible = true;
           btn_confirmar.Visible = false;
           btnAgregar.Enabled = true;
           mod_pat = "N";
           v_cod_id = 0;
           dt_caso.Clear();
           txtSubEtapa.Enabled = false;
           btnSubEtapa.Enabled = false;
           txtPaquete.Enabled = false;
           btnPaquete.Enabled = false;

         //  dt_patologias.Clear();
       }

       private void Guardar_Patologias()
       {
           int cod_id = 0;
           int cod_patologia=0;
           int cod_etapa = 0;
           int cod_sub_etapa = 0;
           int cod_paquete = 0;
           int cod_estado = 0;
           int dia = 0;

           string fecha_ini = "";
           string fecha_recepcion = "";
           string fecha_termino = "";
           string vigencia = "";
           string observacion="";
           foreach (DataRow fila in dt_patologias.Rows)
           {

               cod_id = Convert.ToInt32(fila["ID_FILA"].ToString());
               cod_patologia = Convert.ToInt32(fila["COD_PATOLOGIA"].ToString());
               cod_etapa = Convert.ToInt32(fila["COD_ETAPA"].ToString());
               cod_sub_etapa = Convert.ToInt32(fila["COD_SUB_ETAPA"].ToString());
               cod_paquete = Convert.ToInt32(fila["COD_PAQUETE"].ToString());
               cod_estado = Convert.ToInt32(fila["COD_ESTAD"].ToString());
               dia = Convert.ToInt32(fila["DIAS"].ToString());
               fecha_ini = fila["FECHA_INICIO"].ToString();
               fecha_recepcion = fila["FECHA_RECEPCION"].ToString();
               fecha_termino = fila["FECHA_TERMINO"].ToString();
               vigencia = fila["VIG"].ToString();
               observacion = fila["OBSERVACION"].ToString();


                CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_INSERTA_DETALLE");

                CnnFalp.ParametroBD("PIN_GCD_ID", Convert.ToInt32(cod_id), DbType.Int32, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_PIN_CASO", Convert.ToInt32(txtnum_caso.Text) , DbType.Int64, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_COD_PATOLOGIA", cod_patologia, DbType.Int32, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_COD_ETAPA", cod_etapa, DbType.Int32, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_COD_SUB_ETAPA", cod_sub_etapa, DbType.Int32, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_COD_PAQUETE", cod_paquete, DbType.Int32, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_DIA", dia, DbType.Int32, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_FECHA_INICIO", fecha_ini, DbType.String, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_FECHA_RECEPCION", fecha_recepcion, DbType.String, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_FECHA_TERMINO", fecha_termino, DbType.String, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_COD_ESTADO", cod_estado, DbType.Int32, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_VIGENCIA", vigencia, DbType.String, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_USUARIO", usuario, DbType.String, ParameterDirection.Input);
                CnnFalp.ParametroBD("PIN_OBSERVACION", observacion, DbType.String, ParameterDirection.Input);
                int registro = CnnFalp.ExecuteNonQuery();

           }
       }

       #endregion 

       #region  Vaidaciones

       private Boolean Validar_campos_pat()
       {
           Boolean var = false;
           if (txtPatologia.Tag != null)
           {
               if (txtEtapa.Tag != null)
               {
                   if (txtSubEtapa.Tag != null)
                   {
                       if (txtPaquete.Tag != null)
                       {
                           
                               int cont = 0;
                               foreach (DataRow fila3 in dt_patologias.Select("COD_ETAPA= '" + Convert.ToInt32(txtEtapa.Tag) + "' and  COD_SUB_ETAPA ='" + Convert.ToInt32(txtSubEtapa.Tag) + "' AND  COD_PAQUETE= '" + Convert.ToInt32(txtPaquete.Tag) + "'"))
                               {
                                   cont++;
                               }

                               if (cont == 0)
                               {
                                   var = true;
                               }
                       }
                       else
                       {
                           MessageBox.Show("Estimado Usuario, El Campo Paquete  esta Vacio", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                           txtcod.Focus();
                       }
                   }
                   else
                   {
                       MessageBox.Show("Estimado Usuario, El Campo Etapa esta Vacio", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       txtcod.Focus();
                   }
               }
               else
               {
                   MessageBox.Show("Estimado Usuario, El Campo Sub Etapa esta Vacio", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   txtcod.Focus();
               }

           }
           else
           {
               MessageBox.Show("Estimado Usuario, El Campo Patologia esta Vacio", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
               txtcod.Focus();
           }

           return var;

       }

       private void txtDiasVig_KeyPress(object sender, KeyPressEventArgs e)
       {
           if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Enter))
           {
               e.Handled = true;

               return;
           }
           else
           {
               if (e.KeyChar == (char)13)
               {
                   txtIniGES.Enabled = true;
                   string fec = txtDiasVig.Text == "" ? fec = "0" : fec = txtDiasVig.Text;
                   txtFecTermino.Text = txtIniGES.Value.AddDays(Convert.ToDouble(fec)).ToShortDateString();

                   DateTime fecha1 = Convert.ToDateTime(txtFecTermino.Text);
                   DateTime fecha2 = Convert.ToDateTime(txtRecepcion.Text);

                   if (Convert.ToDateTime(txtIniGES.Text) <= fecha2)
                   {
                       TimeSpan dias = fecha1.Subtract(fecha2);
                       txtDiasRestantes.Text = dias.Days.ToString();
                   }
                   btnAgregar.Focus();
               }

           }
       }

       private void txtRecepcion_ValueChanged(object sender, EventArgs e)
       {
           string fec = txtDiasVig.Text == "" ? fec = "0" : fec = txtDiasVig.Text;
           txtFecTermino.Text = txtIniGES.Value.AddDays(Convert.ToDouble(fec)).ToShortDateString();

           DateTime fecha1 = Convert.ToDateTime(txtFecTermino.Text);
           DateTime fecha2 = Convert.ToDateTime(txtRecepcion.Text);

           if (Convert.ToDateTime(txtIniGES.Text) <= fecha2)
           {
               TimeSpan dias = fecha1.Subtract(fecha2);
               txtDiasRestantes.Text = dias.Days.ToString();
           }
           else
           {
               MessageBox.Show("Estimado Usuario, Fecha de Recepcion no puede ser menor ", "Informacion Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
               txtRecepcion.Text = txtIniGES.Text;
           } txtDiasRestantes.Text = (Convert.ToDateTime(txtFecTermino.Text).Subtract(txtRecepcion.Value).Days + 1).ToString() + " DIAS";
       }

       private void txtIniGES_ValueChanged(object sender, EventArgs e)
       {
           if (Convert.ToDateTime(txtIniGES.Text) > Convert.ToDateTime(txtRecepcion.Text))
           {
               MessageBox.Show("Estimado Usuario, Fecha de Inicio no puede ser mayor Fecha de Recepción ", "Informacion Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
               txtIniGES.Text = txtRecepcion.Text;
               string fec = txtDiasVig.Text == "" ? fec = "0" : fec = txtDiasVig.Text;
               txtFecTermino.Text = txtIniGES.Value.AddDays(Convert.ToDouble(fec)).ToShortDateString();

               DateTime fecha1 = Convert.ToDateTime(txtFecTermino.Text);
               DateTime fecha2 = Convert.ToDateTime(txtRecepcion.Text);

               if (Convert.ToDateTime(txtIniGES.Text) <= fecha2)
               {
                   TimeSpan dias = fecha1.Subtract(fecha2);
                   txtDiasRestantes.Text = dias.Days.ToString();
               }
           }
       }

       #endregion


       #endregion

       #region  Prestaciones

       #region  Grilla

       private void Cargar_Listado_Prestaciones()
       {
           if (CnnFalp.Estado == ConnectionState.Closed) CnnFalp.Abrir();
           dt_prestaciones.Clear();
           CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_CARGAR_PRESTACIONES_PAC");
           CnnFalp.ParametroBD("PIN_CORRELATIVO", v_correlativo, DbType.Int64, ParameterDirection.Input);
           dt_prestaciones.Load(CnnFalp.ExecuteReader());
           grilla_prestaciones.DataSource = new DataView(dt_prestaciones, "VIGENCIA ='S'", "", DataViewRowState.CurrentRows);
           CnnFalp.Cerrar();
       }

       private void grilla_prestaciones_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
       {
           int v_fila = 0;
           v_cod_pre = 0;
           v_cod_pre = Convert.ToInt32(grilla_prestaciones.Rows[e.RowIndex].Cells["CODIGO"].Value.ToString());
           string des = grilla_prestaciones.Rows[e.RowIndex].Cells["DESCRIPCION"].Value.ToString();
           v_fila = Convert.ToInt32(grilla_prestaciones.Rows[e.RowIndex].Cells["N_FILA"].Value.ToString());
           string  tipo_obs = grilla_prestaciones.Rows[e.RowIndex].Cells["TIPO_OBSERVACION"].Value.ToString();
           string obs = grilla_prestaciones.Rows[e.RowIndex].Cells["OBSERVACION"].Value.ToString();
           if (e.RowIndex >= 0)
           {
               if (e.ColumnIndex == 12)
               {

                   DialogResult opc = MessageBox.Show("Estimado Usuario, Esta seguro de Modificar el Estado de la Prestación", "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                   if (opc == DialogResult.Yes)
                   {

                       Cargar_prestacion();
                   }
               }
               else
               {
                   if (e.ColumnIndex == 1)
                   {

                       DialogResult opc = MessageBox.Show("Estimado Usuario, Esta seguro de Modificar la Cantidad  de la Prestación", "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                       if (opc == DialogResult.Yes)
                       {
                           bloquear_prest();
                           txtcod.Text = v_cod_pre.ToString();
                           txtdescripcion.Text = des;
                           txtcantidad.Text =grilla_prestaciones.Rows[e.RowIndex].Cells["CANTIDAD"].Value.ToString();
                           txtvalor.Text = grilla_prestaciones.Rows[e.RowIndex].Cells["VALOR"].Value.ToString();
                           string val = grilla_prestaciones.Rows[e.RowIndex].Cells["TIPO"].Value.ToString();
                            if(val=="M")
                            {
                                txttipo.Tag = "1";
                                txttipo.Text = "Medicamento";
                            }
                            else
                            {
                                txttipo.Tag = "2";
                                txttipo.Text = "Prestaciones";
                            } 
                       }
                   }
                   else
                   {
                       if (e.ColumnIndex == 0)
                       {
                           DialogResult opc = MessageBox.Show("Estimado Usuario, Esta seguro de Eliminar  la Prestación", "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                           if (opc == DialogResult.Yes)
                           {
                               if (v_fila == 0)
                               {
                                   grilla_prestaciones.Rows.RemoveAt(grilla_prestaciones.CurrentRow.Index);
                                   dt_prestaciones.AcceptChanges();
                                   MessageBox.Show("Estimado Usuario, Fue Eliminado Correctamente la Prestación " + des + "", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);


                               }

                               else
                               {
                                   foreach (DataRow fila in dt_prestaciones.Select(" CODIGO= '" + v_cod_pre + "'"))
                                   {

                                       if (fila["VIGENCIA"].ToString() == "S")
                                       {
                                           fila["VIGENCIA"] = "N";
                                       }
                                       else
                                       {
                                           fila["VIGENCIA"] = "S";
                                       }

                                   }
                                   dt_prestaciones.AcceptChanges();
                                   grilla_prestaciones.DataSource = new DataView(dt_prestaciones, "VIGENCIA ='S'", "", DataViewRowState.CurrentRows);
                                   MessageBox.Show("Estimado Usuario, Fue Eliminado Correctamente la Prestación " + des + "", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                               }
                           }
                       }
                       else
                       {
                           if (e.ColumnIndex == 2)
                           {
                               DialogResult opc = MessageBox.Show("Estimado Usuario,¿Esta Seguro de Ingresar una Observación", "Informacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                               if (opc == DialogResult.Yes)
                               {
                                   if (tipo_obs == "" && obs == "" || tipo_obs == "" && obs != "")
                                   {
                                       Frm_Agregar_Observacion_v2 frm = new Frm_Agregar_Observacion_v2(obs);
                                       frm.ShowDialog();
                                       v_observacion = frm.OBSERVACION;

                                       foreach (DataRow xd in dt_prestaciones.Select("CODIGO=" + v_cod_pre + ""))
                                       {
                                           xd["OBSERVACION"] = v_observacion;

                                       }
                                       dt_prestaciones.AcceptChanges();
                                   }
                                   else
                                   {
                                       Frm_Agregar_Observacion frm = new Frm_Agregar_Observacion(v_fila, 2);
                                       frm.ShowDialog();

                                   }
                               }

                           }
                       }
                   }

               }
           }
       }


       #endregion 

       #region  Tipo Prestacion

       private void btn_tipo_Click(object sender, EventArgs e)
       {
           txttipo.Tag = ""; txttipo.Text = "";
           Cargar_Parametros(ref Ayuda, 147, txttipo.Text.ToUpper(), "Tipo Prestación");
           if (!Ayuda.EOF())
           {
               txttipo.Tag = Convert.ToInt32(Ayuda.Fields(0));
               txttipo.Text = Ayuda.Fields(1);
               txtcod.Focus();
           }
           if (Convert.ToInt32(txttipo.Tag) == 1)
           {
               txtcantidad.Enabled = true;
           }
       }





       private void txttipo_KeyPress(object sender, KeyPressEventArgs e)
       {
           if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Enter) && (e.KeyChar != (char)Keys.Space))
           {

               e.Handled = true;
               return;
           }

           if (e.KeyChar == (char)13)
           {
               Cargar_Parametros(ref Ayuda, 147, txttipo.Text.ToUpper(), "Tipo Prestación");
               if (!Ayuda.EOF())
               {
                   txttipo.Tag = Convert.ToInt32(Ayuda.Fields(0));
                   txttipo.Text = Ayuda.Fields(1);
                   txtcod.Focus();

                  
               }
               if (Convert.ToInt32(txttipo.Tag) == 1)
               {
                   txtcantidad.Enabled = true;
               }
           }
       }


       #endregion

       #region  Cargar Prestaciones

       private void txtcod_KeyPress(object sender, KeyPressEventArgs e)
       {

           if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Enter))
           {

               e.Handled = true;

               return;
           }
           else
           {

               if (e.KeyChar == (char)13)
               {
                   string cod ="";
                   if (txtcod.Text == "") { cod = "0"; } else { cod = txtcod.Text; }
                   Cargar_prestaciones(ref Ayuda, Convert.ToInt32(cod), txtdescripcion.Text.ToUpper().ToString(), Convert.ToInt32(txttipo.Tag));
                   if (!Ayuda.EOF())
                   {

                       txtcod.Text = Ayuda.Fields(0);
                       txtcod.Tag = Ayuda.Fields(0);
                       txtdescripcion.Text = Ayuda.Fields(1);
                       txtdescripcion.Tag = Ayuda.Fields(1);
                       txtvalor.Text = Ayuda.Fields(2);
                       btn_agregar.Focus();

                   }


               }
           }

       }

       private void txtdescripcion_KeyPress(object sender, KeyPressEventArgs e)
       {
           if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Enter))
           {
               e.Handled = true;

               return;
           }
           else
           {

               if (e.KeyChar == (char)13)
               {
                   Cargar_prestaciones(ref Ayuda, Convert.ToInt32(txtcod.Text), txtdescripcion.Text.ToUpper().ToString(), Convert.ToInt32(txttipo.Tag));
                   if (!Ayuda.EOF())
                   {
                       txtcod.Text = Ayuda.Fields(0);
                       txtcod.Tag = Ayuda.Fields(0);
                       txtdescripcion.Text = Ayuda.Fields(1);
                       txtdescripcion.Tag = Ayuda.Fields(1);
                       txtvalor.Text = Ayuda.Fields(2);
                       btn_agregar.Focus();
                   }

               }
           }

       }

       private void Cargar_prestaciones(ref AyudaSpreadNet.AyudaSprNet Ayuda, Int32 cod, string descripcion,int tipo)
       {
           string[] NomCol = { "Codigo", "Descripción", "Valor" };
           int[] AnchoCol = { 80, 350, 50 };
           Ayuda.Nombre_BD_Datos = CnnFalp.DBNombre;
           Ayuda.Pass = CnnFalp.DBPass;
           Ayuda.User = CnnFalp.DBUser;
           Ayuda.TipoBase = 1;
           Ayuda.NombreColumnas = NomCol;
           Ayuda.AnchoColumnas = AnchoCol;
           Ayuda.TituloConsulta = "Prestaciones";
           Ayuda.Package = "PCK_GES002M";
           Ayuda.Procedimiento = "P_CARGA_PRESTACIONES";
           Ayuda.Generar_ParametroBD("PIN_CODIGO", cod, DbType.Int32, ParameterDirection.Input);
           Ayuda.Generar_ParametroBD("PIN_DESCRIPCION", descripcion, DbType.String, ParameterDirection.Input);
           Ayuda.Generar_ParametroBD("PIN_TIPO", tipo, DbType.Int32, ParameterDirection.Input);
           Ayuda.EjecutarSql();
       }


        #endregion 

       #region Botones

       private void btn_limpiar_prestacion_Click(object sender, EventArgs e)
       {
           limpiar_prestaciones();
       }

       private void btn_agregar_Click(object sender, EventArgs e)
       {
           string tipo = "";
           if (Validar_campos_pres())
           {
               DataRow Fila = dt_prestaciones.NewRow();
               Fila["N_FILA"] = "0";
               Fila["CODIGO"] = txtcod.Text.Trim();
               Fila["DESCRIPCION"] = txtdescripcion.Text.Trim();
               if (Convert.ToInt32(txttipo.Tag) == 1) { tipo = "M"; } else { tipo = "P"; }
               Fila["TIPO"] = tipo;
               Fila["CANTIDAD"] = Convert.ToInt32(txtcantidad.Text);
               Fila["VALOR"] = txtvalor.Text.Trim();
               Fila["VALOR_TOTAL"] = Convert.ToInt32(txtcantidad.Text) * Convert.ToInt32(txtvalor.Text);
               Fila["COD_ESTADOS"] = 1;
               Fila["NOM_ESTADOS"] = "Pendientes";
               Fila["VIGENCIA"] = "S";
             


               dt_prestaciones.Rows.Add(Fila);
               grilla_prestaciones.AutoGenerateColumns = false;
               grilla_prestaciones.DataSource = new DataView(dt_prestaciones, "VIGENCIA ='S'", "", DataViewRowState.CurrentRows);
           }
           limpiar_prestaciones();
           txttipo.Focus();
       }

       #endregion

       #region Metodos

       private void bloquear_prest()
       {
           txtcantidad.Enabled = true;
           btn_agregar.Visible = false;
           btn_confirmar_pres.Visible = true;
           txttipo.Enabled = false;
           btn_tipo.Enabled = false;
           txtcod.Enabled = false;
           txtdescripcion.Enabled = false;
           txtvalor.Enabled = false;
       }

       private void Habilitar_prest()
       {
           txtcantidad.Enabled = false;
           btn_agregar.Visible = true;
           btn_confirmar_pres.Visible = false;
           txttipo.Enabled = true;
           btn_tipo.Enabled = true;
           txtcod.Enabled = true;
           txtdescripcion.Enabled = true;
           txtvalor.Enabled = false;
       }

       private void limpiar_prestaciones()
       {
           txttipo.Tag = "";
           txttipo.Text = "";
           txtcod.Tag = "";
           txtcod.Text = "";
           txtdescripcion.Tag = "";
           txtdescripcion.Text = "";
           txtcantidad.Text = "1";
           txtvalor.Text = "";
           txtIniGES.Text = "";
           txtRecepcion.Text = "";
           txtcantidad.Enabled = false;
   

       }

       private void Cargar_prestacion()
       {
           int cod = 0;
           string nom = "";
           Cargar_Parametros(ref Ayuda, 145, "","Prestaciones");
           if (!Ayuda.EOF())
           {
               cod = Convert.ToInt32(Ayuda.Fields(0));
               nom = Ayuda.Fields(1);
               mod_prestacion(cod, nom);
           }

       }

       private void Guardar_Prestaciones()
       {
           int cod_id = 0;
           int cod_prestacion = 0;
           int cantidad = 0;
           int valor = 0;
           int cod_estado = 0;

           string descripcion = "";
           string tipo = "";
           string vigencia = "";
           string v_obs = "";
           foreach (DataRow fila in dt_prestaciones.Rows)
           {
               cod_id = Convert.ToInt32(fila["N_FILA"].ToString());
               cod_prestacion = Convert.ToInt32(fila["CODIGO"].ToString());
               descripcion = fila["DESCRIPCION"].ToString();
               cantidad = Convert.ToInt32(fila["CANTIDAD"].ToString());
               valor = Convert.ToInt32(fila["VALOR"].ToString());
               cod_estado = Convert.ToInt32(fila["COD_ESTADOS"].ToString());
               vigencia = fila["VIGENCIA"].ToString();
               tipo = fila["TIPO"].ToString();
               v_obs = fila["OBSERVACION"].ToString();

        
               CnnFalp.CrearCommand(CommandType.StoredProcedure, PCK + ".P_INSERTA_PRESTACION");

               CnnFalp.ParametroBD("PIN_COD_ID", Convert.ToInt32(cod_id), DbType.Int64, ParameterDirection.Input);
               CnnFalp.ParametroBD("PIN_COD_CASO", Convert.ToInt32(txtnum_caso.Text), DbType.Int64, ParameterDirection.Input);
               CnnFalp.ParametroBD("PIN_COD_PRESTACION", Convert.ToInt32(cod_prestacion), DbType.Int64, ParameterDirection.Input);
               CnnFalp.ParametroBD("PIN_TIPO", tipo, DbType.String, ParameterDirection.Input);
               CnnFalp.ParametroBD("PIN_CANTIDAD", cantidad, DbType.Int64, ParameterDirection.Input);
               CnnFalp.ParametroBD("PIN_VALOR", valor, DbType.Int64, ParameterDirection.Input);
               CnnFalp.ParametroBD("PIN_COD_ESTADO", cod_estado, DbType.Int64, ParameterDirection.Input);
               CnnFalp.ParametroBD("PIN_VIGENCIA", vigencia, DbType.String, ParameterDirection.Input);
               CnnFalp.ParametroBD("PIN_USUARIO", usuario, DbType.String, ParameterDirection.Input);
               CnnFalp.ParametroBD("PIN_OBSERVACION", v_obs, DbType.String, ParameterDirection.Input);
               int registro = CnnFalp.ExecuteNonQuery();

           }
       }
     
       private void mod_prestacion(int cod,string nom)
       {
           foreach (DataRow fila3 in dt_prestaciones.Select(" CODIGO =" + v_cod_pre))
           {
              fila3["COD_ESTADOS"]=cod;
              fila3["NOM_ESTADOS"] = nom;
           }

           dt_prestaciones.AcceptChanges();
           grilla_prestaciones.DataSource = dt_prestaciones;

       }

       private void btn_confirmar_pres_Click(object sender, EventArgs e)
       {
           string des = "";
           foreach (DataRow fila3 in dt_prestaciones.Select(" CODIGO ='" + v_cod_pre + "'"))
           {
               fila3["CANTIDAD"] = Convert.ToInt32(txtcantidad.Text);
               fila3["VALOR_TOTAL"] = Convert.ToInt32(txtcantidad.Text) * Convert.ToInt32(txtvalor.Text);
               des = fila3["DESCRIPCION"].ToString();
           }

           MessageBox.Show("Estimado Usuario, Fue Modificada Correctamente la Prestación " + des + "", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
           v_cod_pre = 0;
           btn_confirmar_pres.Visible = false;
           btn_agregar.Visible = true;
       }



       #endregion

       #region Validaciones

       private Boolean Validar_campos_pres()
       {
           Boolean var = false;

           if (txttipo.Tag != null )
           {
               
               if (txtcod.Tag != null && txtdescripcion.Text != string.Empty)
               {
                   var = true;
               }
               else
               {
                   MessageBox.Show("Estimado usuario, La Prestacion no es Valida", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   txtcod.Focus();
               }
           }

           else
           {
               MessageBox.Show("Estimado Usuario, El Campo Tipo Prestacion esta Vacia", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
               txtcod.Focus();
           }

           return var;

       }


      #endregion

  
 

      #endregion



       private void CambiarBlanco_TextLeave(object sender, EventArgs e)
       {
           TextBox GB = (TextBox)sender;
           GB.BackColor = Color.White;

       }

       private void CambiarColor_TextEnter(object sender, EventArgs e)
       {
           TextBox GB = (TextBox)sender;
           GB.BackColor = Color.FromArgb(255, 224, 192);
       }

       private void CambiarColor_Enter(object sender, EventArgs e)
       {
           GroupBox GB = (GroupBox)sender;
           GB.BackColor = Color.FromArgb(255, 255, 192);
       }

       private void CambiarBlanco_Leave(object sender, EventArgs e)
       {
           GroupBox GB = (GroupBox)sender;
           GB.BackColor = Color.WhiteSmoke;
       }

       private void btn_limpiar_patologia_Click(object sender, EventArgs e)
       {
           limpiar_patologias();
       }

       private void Gv_Casos_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
       {
           if (e.RowIndex < 0)
           {
               e.PaintBackground(e.ClipBounds, false);
               Font drawFont = new Font("Trebuchet MS", 8, FontStyle.Bold);
               SolidBrush drawBrush = new SolidBrush(Color.White);
               StringFormat StrFormat = new StringFormat();
               StrFormat.Alignment = StringAlignment.Center;
               StrFormat.LineAlignment = StringAlignment.Center;

               e.Graphics.DrawImage(Properties.Resources.HeaderGV, e.CellBounds);
               e.Graphics.DrawString(Gv_Casos.Columns[e.ColumnIndex].HeaderText, drawFont, drawBrush, e.CellBounds, StrFormat);

               e.Handled = true;
               drawBrush.Dispose();
           }
       }

       private void grilla_prestaciones_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
       {
           if (e.RowIndex < 0)
           {
               e.PaintBackground(e.ClipBounds, false);
               Font drawFont = new Font("Trebuchet MS", 8, FontStyle.Bold);
               SolidBrush drawBrush = new SolidBrush(Color.White);
               StringFormat StrFormat = new StringFormat();
               StrFormat.Alignment = StringAlignment.Center;
               StrFormat.LineAlignment = StringAlignment.Center;

               e.Graphics.DrawImage(Properties.Resources.HeaderGV, e.CellBounds);
               e.Graphics.DrawString(grilla_prestaciones.Columns[e.ColumnIndex].HeaderText, drawFont, drawBrush, e.CellBounds, StrFormat);

               e.Handled = true;
               drawBrush.Dispose();
           }
       }

   

    }
}
