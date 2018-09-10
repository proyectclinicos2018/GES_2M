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
    public partial class Frm_Agregar_Observacion_v2 : Form
    {
        ConectarFalp CnnFalp;
        Configuration Config;
        string[] Conexion = { "", "", "" };
        string Db_Usuario;
        DataTable Tbl_Caso = new DataTable();
        string PCK = "PCK_GES002I";
        public string OBSERVACION = "";

        public Frm_Agregar_Observacion_v2(string obs)
        {
            OBSERVACION = obs;
            InitializeComponent();
        }

        public Frm_Agregar_Observacion_v2()
        {
            InitializeComponent();
        }

        private void Frm_Agregar_Observacion_v2_Load(object sender, EventArgs e)
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

            if (OBSERVACION != "")
            {
                txtobservacion.Text = OBSERVACION;
            }
        }

        private void btn_guardar_Click(object sender, EventArgs e)
        {

            OBSERVACION = txtobservacion.Text.TrimEnd();

            MessageBox.Show("Estimado Usuario, Fue Grabado Correctamente la Información ", "Informacion Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            this.Hide();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtobservacion.Text = "";
        }


    }
}
