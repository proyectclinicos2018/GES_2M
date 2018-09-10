namespace GES002M
{
    partial class Frm_Agregar_Observacion
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.txtobservacion = new System.Windows.Forms.TextBox();
            this.btn_agregar = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grilla_coment = new System.Windows.Forms.DataGridView();
            this.E = new System.Windows.Forms.DataGridViewImageColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID_CASO_DET = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OBSERV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VIGENCIA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIPO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID_PRESTACION_DET = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_guardar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grilla_coment)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLimpiar);
            this.groupBox1.Controls.Add(this.txtobservacion);
            this.groupBox1.Controls.Add(this.btn_agregar);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Location = new System.Drawing.Point(7, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(345, 245);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Image = global::GES002M.Properties.Resources.limpiar;
            this.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpiar.Location = new System.Drawing.Point(250, 199);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(86, 36);
            this.btnLimpiar.TabIndex = 39;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLimpiar.UseVisualStyleBackColor = true;
            // 
            // txtobservacion
            // 
            this.txtobservacion.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.txtobservacion.Location = new System.Drawing.Point(3, 29);
            this.txtobservacion.MaxLength = 200;
            this.txtobservacion.Multiline = true;
            this.txtobservacion.Name = "txtobservacion";
            this.txtobservacion.Size = new System.Drawing.Size(336, 165);
            this.txtobservacion.TabIndex = 6;
            // 
            // btn_agregar
            // 
            this.btn_agregar.Image = global::GES002M.Properties.Resources.add;
            this.btn_agregar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btn_agregar.Location = new System.Drawing.Point(165, 199);
            this.btn_agregar.Name = "btn_agregar";
            this.btn_agregar.Size = new System.Drawing.Size(79, 36);
            this.btn_agregar.TabIndex = 38;
            this.btn_agregar.Text = "Agregar";
            this.btn_agregar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_agregar.UseVisualStyleBackColor = true;
            this.btn_agregar.Click += new System.EventHandler(this.btn_agregar_Click);
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.White;
            this.label23.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.White;
            this.label23.Image = global::GES002M.Properties.Resources.HeaderLarge;
            this.label23.Location = new System.Drawing.Point(0, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(345, 25);
            this.label23.TabIndex = 5;
            this.label23.Text = "AGREGAR OBSERVACION";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grilla_coment);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(358, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(430, 245);
            this.groupBox2.TabIndex = 40;
            this.groupBox2.TabStop = false;
            // 
            // grilla_coment
            // 
            this.grilla_coment.AllowUserToAddRows = false;
            this.grilla_coment.AllowUserToDeleteRows = false;
            this.grilla_coment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grilla_coment.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.E,
            this.ID,
            this.ID_CASO_DET,
            this.OBSERV,
            this.VIGENCIA,
            this.TIPO,
            this.ID_PRESTACION_DET});
            this.grilla_coment.Location = new System.Drawing.Point(0, 28);
            this.grilla_coment.Name = "grilla_coment";
            this.grilla_coment.ReadOnly = true;
            this.grilla_coment.RowHeadersVisible = false;
            this.grilla_coment.Size = new System.Drawing.Size(430, 207);
            this.grilla_coment.TabIndex = 6;
            this.grilla_coment.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grilla_coment_CellDoubleClick);
            // 
            // E
            // 
            this.E.HeaderText = "E";
            this.E.Image = global::GES002M.Properties.Resources.Delete;
            this.E.Name = "E";
            this.E.ReadOnly = true;
            this.E.Width = 25;
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ID.Visible = false;
            // 
            // ID_CASO_DET
            // 
            this.ID_CASO_DET.DataPropertyName = "ID_CASO_DET";
            this.ID_CASO_DET.HeaderText = "CASO_DET";
            this.ID_CASO_DET.Name = "ID_CASO_DET";
            this.ID_CASO_DET.ReadOnly = true;
            this.ID_CASO_DET.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ID_CASO_DET.Visible = false;
            // 
            // OBSERV
            // 
            this.OBSERV.DataPropertyName = "OBSERVACION";
            this.OBSERV.HeaderText = "OBSERVACION";
            this.OBSERV.Name = "OBSERV";
            this.OBSERV.ReadOnly = true;
            this.OBSERV.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.OBSERV.Width = 400;
            // 
            // VIGENCIA
            // 
            this.VIGENCIA.DataPropertyName = "VIGENCIA";
            this.VIGENCIA.HeaderText = "VIGENCIA";
            this.VIGENCIA.Name = "VIGENCIA";
            this.VIGENCIA.ReadOnly = true;
            this.VIGENCIA.Visible = false;
            // 
            // TIPO
            // 
            this.TIPO.DataPropertyName = "TIPO";
            this.TIPO.HeaderText = "TIPO";
            this.TIPO.Name = "TIPO";
            this.TIPO.ReadOnly = true;
            this.TIPO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TIPO.Visible = false;
            // 
            // ID_PRESTACION_DET
            // 
            this.ID_PRESTACION_DET.DataPropertyName = "ID_PRESTACION_DET";
            this.ID_PRESTACION_DET.HeaderText = "ID_PRESTACION_DET";
            this.ID_PRESTACION_DET.Name = "ID_PRESTACION_DET";
            this.ID_PRESTACION_DET.ReadOnly = true;
            this.ID_PRESTACION_DET.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ID_PRESTACION_DET.Visible = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Image = global::GES002M.Properties.Resources.HeaderLarge;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(430, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "LISTADO OBSERVACION";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_guardar
            // 
            this.btn_guardar.Image = global::GES002M.Properties.Resources.save;
            this.btn_guardar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btn_guardar.Location = new System.Drawing.Point(709, 253);
            this.btn_guardar.Name = "btn_guardar";
            this.btn_guardar.Size = new System.Drawing.Size(79, 30);
            this.btn_guardar.TabIndex = 40;
            this.btn_guardar.Text = "Guardar";
            this.btn_guardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_guardar.UseVisualStyleBackColor = true;
            this.btn_guardar.Click += new System.EventHandler(this.btn_guardar_Click);
            // 
            // Frm_Agregar_Observacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 287);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btn_guardar);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_Agregar_Observacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Agregar Observacion";
            this.Load += new System.EventHandler(this.Frm_Agregar_Observacion_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grilla_coment)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btn_agregar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtobservacion;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView grilla_coment;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_guardar;
        private System.Windows.Forms.DataGridViewImageColumn E;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID_CASO_DET;
        private System.Windows.Forms.DataGridViewTextBoxColumn OBSERV;
        private System.Windows.Forms.DataGridViewTextBoxColumn VIGENCIA;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIPO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID_PRESTACION_DET;
    }
}