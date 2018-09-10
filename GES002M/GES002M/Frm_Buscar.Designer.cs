namespace Ges000M
{
    partial class Frm_Buscar
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Buscar));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Ayuda = new AyudaSpreadNet.AyudaSprNet();
            this.btn_tipo_doc = new System.Windows.Forms.Button();
            this.txttipo_doc = new System.Windows.Forms.TextBox();
            this.PanelOtro = new System.Windows.Forms.Panel();
            this.PanelRut = new System.Windows.Forms.Panel();
            this.TxtDV = new System.Windows.Forms.TextBox();
            this.TxtRut = new System.Windows.Forms.TextBox();
            this.TxtOtroDoc = new System.Windows.Forms.TextBox();
            this.TxtFicha = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtNomPac = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Opt_Ficha = new System.Windows.Forms.RadioButton();
            this.Opt_Doc = new System.Windows.Forms.RadioButton();
            this.Opt_Caso = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.Txt_Caso = new System.Windows.Forms.TextBox();
            this.Btn_Ingresar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.PanelOtro.SuspendLayout();
            this.PanelRut.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Ayuda);
            this.groupBox1.Controls.Add(this.btn_tipo_doc);
            this.groupBox1.Controls.Add(this.txttipo_doc);
            this.groupBox1.Controls.Add(this.PanelOtro);
            this.groupBox1.Controls.Add(this.TxtFicha);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.TxtNomPac);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.Opt_Ficha);
            this.groupBox1.Controls.Add(this.Opt_Doc);
            this.groupBox1.Controls.Add(this.Opt_Caso);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.Txt_Caso);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(432, 212);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // Ayuda
            // 
            this.Ayuda.AnchoColumnas = null;
            this.Ayuda.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.Ayuda.Location = new System.Drawing.Point(368, 146);
            this.Ayuda.Multi_Seleccion = false;
            this.Ayuda.Name = "Ayuda";
            this.Ayuda.Nombre_BD_Datos = null;
            this.Ayuda.NombreColumnas = null;
            this.Ayuda.Package = null;
            this.Ayuda.Pass = null;
            this.Ayuda.Procedimiento = null;
            this.Ayuda.Size = new System.Drawing.Size(58, 66);
            this.Ayuda.TabIndex = 6;
            this.Ayuda.TipoBase = 0;
            this.Ayuda.TituloConsulta = null;
            this.Ayuda.User = null;
            this.Ayuda.UseWaitCursor = true;
            this.Ayuda.Visible = false;
            // 
            // btn_tipo_doc
            // 
            this.btn_tipo_doc.Image = ((System.Drawing.Image)(resources.GetObject("btn_tipo_doc.Image")));
            this.btn_tipo_doc.Location = new System.Drawing.Point(119, 82);
            this.btn_tipo_doc.Name = "btn_tipo_doc";
            this.btn_tipo_doc.Size = new System.Drawing.Size(26, 21);
            this.btn_tipo_doc.TabIndex = 40;
            this.btn_tipo_doc.UseVisualStyleBackColor = true;
            this.btn_tipo_doc.Click += new System.EventHandler(this.btn_tipo_doc_Click);
            // 
            // txttipo_doc
            // 
            this.txttipo_doc.BackColor = System.Drawing.SystemColors.Window;
            this.txttipo_doc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txttipo_doc.Location = new System.Drawing.Point(17, 83);
            this.txttipo_doc.MaxLength = 8;
            this.txttipo_doc.Name = "txttipo_doc";
            this.txttipo_doc.ReadOnly = true;
            this.txttipo_doc.Size = new System.Drawing.Size(98, 20);
            this.txttipo_doc.TabIndex = 24;
            this.txttipo_doc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txttipo_doc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txttipo_doc_KeyPress);
            // 
            // PanelOtro
            // 
            this.PanelOtro.Controls.Add(this.PanelRut);
            this.PanelOtro.Controls.Add(this.TxtOtroDoc);
            this.PanelOtro.Location = new System.Drawing.Point(170, 80);
            this.PanelOtro.Name = "PanelOtro";
            this.PanelOtro.Size = new System.Drawing.Size(132, 25);
            this.PanelOtro.TabIndex = 23;
            // 
            // PanelRut
            // 
            this.PanelRut.Controls.Add(this.TxtDV);
            this.PanelRut.Controls.Add(this.TxtRut);
            this.PanelRut.Location = new System.Drawing.Point(3, 0);
            this.PanelRut.Name = "PanelRut";
            this.PanelRut.Size = new System.Drawing.Size(132, 25);
            this.PanelRut.TabIndex = 24;
            // 
            // TxtDV
            // 
            this.TxtDV.BackColor = System.Drawing.SystemColors.Window;
            this.TxtDV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDV.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtDV.Location = new System.Drawing.Point(112, 3);
            this.TxtDV.MaxLength = 1;
            this.TxtDV.Name = "TxtDV";
            this.TxtDV.ReadOnly = true;
            this.TxtDV.Size = new System.Drawing.Size(17, 20);
            this.TxtDV.TabIndex = 17;
            this.TxtDV.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TxtDV.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtDV_KeyPress);
            // 
            // TxtRut
            // 
            this.TxtRut.BackColor = System.Drawing.SystemColors.Window;
            this.TxtRut.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtRut.Location = new System.Drawing.Point(0, 3);
            this.TxtRut.MaxLength = 8;
            this.TxtRut.Name = "TxtRut";
            this.TxtRut.ReadOnly = true;
            this.TxtRut.Size = new System.Drawing.Size(89, 20);
            this.TxtRut.TabIndex = 16;
            this.TxtRut.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtRut.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtRut_KeyPress);
            // 
            // TxtOtroDoc
            // 
            this.TxtOtroDoc.BackColor = System.Drawing.SystemColors.Window;
            this.TxtOtroDoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtOtroDoc.Location = new System.Drawing.Point(4, 3);
            this.TxtOtroDoc.MaxLength = 15;
            this.TxtOtroDoc.Name = "TxtOtroDoc";
            this.TxtOtroDoc.ReadOnly = true;
            this.TxtOtroDoc.Size = new System.Drawing.Size(125, 20);
            this.TxtOtroDoc.TabIndex = 16;
            // 
            // TxtFicha
            // 
            this.TxtFicha.BackColor = System.Drawing.SystemColors.Window;
            this.TxtFicha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtFicha.Location = new System.Drawing.Point(326, 83);
            this.TxtFicha.MaxLength = 8;
            this.TxtFicha.Name = "TxtFicha";
            this.TxtFicha.ReadOnly = true;
            this.TxtFicha.Size = new System.Drawing.Size(86, 20);
            this.TxtFicha.TabIndex = 22;
            this.TxtFicha.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtFicha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtFicha_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(323, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Ficha";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(172, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Número Documento";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Tipo de Documento";
            // 
            // TxtNomPac
            // 
            this.TxtNomPac.BackColor = System.Drawing.SystemColors.Window;
            this.TxtNomPac.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtNomPac.Location = new System.Drawing.Point(17, 122);
            this.TxtNomPac.MaxLength = 30;
            this.TxtNomPac.Name = "TxtNomPac";
            this.TxtNomPac.ReadOnly = true;
            this.TxtNomPac.Size = new System.Drawing.Size(372, 20);
            this.TxtNomPac.TabIndex = 10;
            this.TxtNomPac.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNomPac_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Nombre";
            // 
            // Opt_Ficha
            // 
            this.Opt_Ficha.AutoSize = true;
            this.Opt_Ficha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Opt_Ficha.Location = new System.Drawing.Point(329, 19);
            this.Opt_Ficha.Name = "Opt_Ficha";
            this.Opt_Ficha.Size = new System.Drawing.Size(56, 17);
            this.Opt_Ficha.TabIndex = 8;
            this.Opt_Ficha.Text = "Ficha";
            this.Opt_Ficha.UseVisualStyleBackColor = true;
            this.Opt_Ficha.CheckedChanged += new System.EventHandler(this.Opt_Ficha_CheckedChanged);
            // 
            // Opt_Doc
            // 
            this.Opt_Doc.AutoSize = true;
            this.Opt_Doc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Opt_Doc.Location = new System.Drawing.Point(149, 19);
            this.Opt_Doc.Name = "Opt_Doc";
            this.Opt_Doc.Size = new System.Drawing.Size(135, 17);
            this.Opt_Doc.TabIndex = 6;
            this.Opt_Doc.Text = "Documento Ingreso";
            this.Opt_Doc.UseVisualStyleBackColor = true;
            this.Opt_Doc.CheckedChanged += new System.EventHandler(this.Opt_Doc_CheckedChanged);
            // 
            // Opt_Caso
            // 
            this.Opt_Caso.AutoSize = true;
            this.Opt_Caso.Checked = true;
            this.Opt_Caso.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Opt_Caso.Location = new System.Drawing.Point(17, 19);
            this.Opt_Caso.Name = "Opt_Caso";
            this.Opt_Caso.Size = new System.Drawing.Size(71, 17);
            this.Opt_Caso.TabIndex = 5;
            this.Opt_Caso.TabStop = true;
            this.Opt_Caso.Text = "N° Caso";
            this.Opt_Caso.UseVisualStyleBackColor = true;
            this.Opt_Caso.CheckedChanged += new System.EventHandler(this.Opt_Caso_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(116, 146);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "N°  DOCUMENTO";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Txt_Caso
            // 
            this.Txt_Caso.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Txt_Caso.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.Txt_Caso.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_Caso.Location = new System.Drawing.Point(116, 169);
            this.Txt_Caso.MaxLength = 15;
            this.Txt_Caso.Name = "Txt_Caso";
            this.Txt_Caso.Size = new System.Drawing.Size(201, 35);
            this.Txt_Caso.TabIndex = 2;
            this.Txt_Caso.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Txt_Caso.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Txt_Caso_KeyPress);
            // 
            // Btn_Ingresar
            // 
            this.Btn_Ingresar.Enabled = false;
            this.Btn_Ingresar.Location = new System.Drawing.Point(151, 230);
            this.Btn_Ingresar.Name = "Btn_Ingresar";
            this.Btn_Ingresar.Size = new System.Drawing.Size(145, 36);
            this.Btn_Ingresar.TabIndex = 5;
            this.Btn_Ingresar.Text = "Ingresar";
            this.Btn_Ingresar.UseVisualStyleBackColor = true;
            this.Btn_Ingresar.Click += new System.EventHandler(this.Btn_Ingresar_Click);
            // 
            // Frm_Buscar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(456, 273);
            this.Controls.Add(this.Btn_Ingresar);
            this.Controls.Add(this.groupBox1);
            this.Name = "Frm_Buscar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frm_Buscar";
            this.Load += new System.EventHandler(this.Frm_Buscar_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.PanelOtro.ResumeLayout(false);
            this.PanelOtro.PerformLayout();
            this.PanelRut.ResumeLayout(false);
            this.PanelRut.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel PanelRut;
        private System.Windows.Forms.TextBox TxtDV;
        private System.Windows.Forms.TextBox TxtRut;
        private System.Windows.Forms.Panel PanelOtro;
        private System.Windows.Forms.TextBox TxtOtroDoc;
        private System.Windows.Forms.TextBox TxtFicha;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TxtNomPac;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton Opt_Ficha;
        private System.Windows.Forms.RadioButton Opt_Doc;
        private System.Windows.Forms.RadioButton Opt_Caso;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Txt_Caso;
        private System.Windows.Forms.Button Btn_Ingresar;
        private System.Windows.Forms.TextBox txttipo_doc;
        private System.Windows.Forms.Button btn_tipo_doc;
        private AyudaSpreadNet.AyudaSprNet Ayuda;
    }
}