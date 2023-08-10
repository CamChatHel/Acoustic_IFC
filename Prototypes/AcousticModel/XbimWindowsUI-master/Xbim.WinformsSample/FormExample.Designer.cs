namespace Xbim.WinformsSample
{
    partial class FormExample
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.controlHost = new System.Windows.Forms.Integration.ElementHost();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DGV01 = new System.Windows.Forms.DataGridView();
            this.buttonSaveIFC = new System.Windows.Forms.Button();
            this.labelJsonfileAdded = new System.Windows.Forms.Label();
            this.buttonAddJsonfile = new System.Windows.Forms.Button();
            this.textBoxjJsonInfo = new System.Windows.Forms.TextBox();
            this.buttonJsonInformation = new System.Windows.Forms.Button();
            this.textBoxConnections = new System.Windows.Forms.TextBox();
            this.textboxGetGUID = new System.Windows.Forms.TextBox();
            this.textBoxSelectElement = new System.Windows.Forms.TextBox();
            this.buttonSelectElement = new System.Windows.Forms.Button();
            this.buttonGetGUID = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txtEntityLabel = new System.Windows.Forms.Label();
            this.Wert1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Einzahlwert = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f50 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f63 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f80 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f100 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f125 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f160 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f200 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f250 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f315 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f400 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f500 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f630 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f800 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f1000 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f1250 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f1600 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f2000 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f2500 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f3150 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f4000 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f5000 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV01)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Load Bim file";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // controlHost
            // 
            this.controlHost.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.controlHost.Location = new System.Drawing.Point(371, 12);
            this.controlHost.Name = "controlHost";
            this.controlHost.Size = new System.Drawing.Size(1151, 589);
            this.controlHost.TabIndex = 1;
            this.controlHost.Text = "elementHost1";
            this.controlHost.Child = null;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.DGV01);
            this.panel1.Controls.Add(this.buttonSaveIFC);
            this.panel1.Controls.Add(this.labelJsonfileAdded);
            this.panel1.Controls.Add(this.buttonAddJsonfile);
            this.panel1.Controls.Add(this.textBoxjJsonInfo);
            this.panel1.Controls.Add(this.buttonJsonInformation);
            this.panel1.Controls.Add(this.textBoxConnections);
            this.panel1.Controls.Add(this.textboxGetGUID);
            this.panel1.Controls.Add(this.textBoxSelectElement);
            this.panel1.Controls.Add(this.buttonSelectElement);
            this.panel1.Controls.Add(this.buttonGetGUID);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.txtEntityLabel);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.controlHost);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1534, 783);
            this.panel1.TabIndex = 2;
            // 
            // DGV01
            // 
            this.DGV01.AllowUserToAddRows = false;
            this.DGV01.AllowUserToDeleteRows = false;
            this.DGV01.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV01.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Wert1,
            this.Einzahlwert,
            this.f50,
            this.f63,
            this.f80,
            this.f100,
            this.f125,
            this.f160,
            this.f200,
            this.f250,
            this.f315,
            this.f400,
            this.f500,
            this.f630,
            this.f800,
            this.f1000,
            this.f1250,
            this.f1600,
            this.f2000,
            this.f2500,
            this.f3150,
            this.f4000,
            this.f5000});
            this.DGV01.Location = new System.Drawing.Point(11, 607);
            this.DGV01.Name = "DGV01";
            this.DGV01.ReadOnly = true;
            this.DGV01.Size = new System.Drawing.Size(1511, 164);
            this.DGV01.TabIndex = 16;
            this.DGV01.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // buttonSaveIFC
            // 
            this.buttonSaveIFC.Location = new System.Drawing.Point(10, 329);
            this.buttonSaveIFC.Name = "buttonSaveIFC";
            this.buttonSaveIFC.Size = new System.Drawing.Size(219, 21);
            this.buttonSaveIFC.TabIndex = 15;
            this.buttonSaveIFC.Text = "Open Input Data from internal JSON-file";
            this.buttonSaveIFC.UseVisualStyleBackColor = true;
            this.buttonSaveIFC.Click += new System.EventHandler(this.buttonSaveIFC_Click);
            // 
            // labelJsonfileAdded
            // 
            this.labelJsonfileAdded.AutoSize = true;
            this.labelJsonfileAdded.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelJsonfileAdded.Location = new System.Drawing.Point(185, 274);
            this.labelJsonfileAdded.Name = "labelJsonfileAdded";
            this.labelJsonfileAdded.Size = new System.Drawing.Size(0, 13);
            this.labelJsonfileAdded.TabIndex = 14;
            this.labelJsonfileAdded.UseMnemonic = false;
            // 
            // buttonAddJsonfile
            // 
            this.buttonAddJsonfile.Location = new System.Drawing.Point(10, 265);
            this.buttonAddJsonfile.Name = "buttonAddJsonfile";
            this.buttonAddJsonfile.Size = new System.Drawing.Size(169, 31);
            this.buttonAddJsonfile.TabIndex = 13;
            this.buttonAddJsonfile.Text = "Add json file to element";
            this.buttonAddJsonfile.UseVisualStyleBackColor = true;
            this.buttonAddJsonfile.Click += new System.EventHandler(this.buttonAddJsonfile_Click);
            // 
            // textBoxjJsonInfo
            // 
            this.textBoxjJsonInfo.Location = new System.Drawing.Point(10, 356);
            this.textBoxjJsonInfo.Multiline = true;
            this.textBoxjJsonInfo.Name = "textBoxjJsonInfo";
            this.textBoxjJsonInfo.Size = new System.Drawing.Size(355, 245);
            this.textBoxjJsonInfo.TabIndex = 12;
            // 
            // buttonJsonInformation
            // 
            this.buttonJsonInformation.Location = new System.Drawing.Point(10, 302);
            this.buttonJsonInformation.Name = "buttonJsonInformation";
            this.buttonJsonInformation.Size = new System.Drawing.Size(219, 21);
            this.buttonJsonInformation.TabIndex = 11;
            this.buttonJsonInformation.Text = "Load VBAcoustic Results from extern JSON file";
            this.buttonJsonInformation.UseVisualStyleBackColor = true;
            this.buttonJsonInformation.Click += new System.EventHandler(this.buttonJsonInformation_Click);
            // 
            // textBoxConnections
            // 
            this.textBoxConnections.Location = new System.Drawing.Point(10, 194);
            this.textBoxConnections.Multiline = true;
            this.textBoxConnections.Name = "textBoxConnections";
            this.textBoxConnections.Size = new System.Drawing.Size(260, 65);
            this.textBoxConnections.TabIndex = 10;
            // 
            // textboxGetGUID
            // 
            this.textboxGetGUID.Location = new System.Drawing.Point(11, 168);
            this.textboxGetGUID.Name = "textboxGetGUID";
            this.textboxGetGUID.Size = new System.Drawing.Size(259, 20);
            this.textboxGetGUID.TabIndex = 8;
            // 
            // textBoxSelectElement
            // 
            this.textBoxSelectElement.Location = new System.Drawing.Point(128, 98);
            this.textBoxSelectElement.Name = "textBoxSelectElement";
            this.textBoxSelectElement.Size = new System.Drawing.Size(142, 20);
            this.textBoxSelectElement.TabIndex = 7;
            // 
            // buttonSelectElement
            // 
            this.buttonSelectElement.Location = new System.Drawing.Point(10, 90);
            this.buttonSelectElement.Name = "buttonSelectElement";
            this.buttonSelectElement.Size = new System.Drawing.Size(113, 35);
            this.buttonSelectElement.TabIndex = 6;
            this.buttonSelectElement.Text = "Select Element with GUID";
            this.buttonSelectElement.UseVisualStyleBackColor = true;
            this.buttonSelectElement.Click += new System.EventHandler(this.buttonSelectElement_Click);
            // 
            // buttonGetGUID
            // 
            this.buttonGetGUID.Location = new System.Drawing.Point(11, 133);
            this.buttonGetGUID.Name = "buttonGetGUID";
            this.buttonGetGUID.Size = new System.Drawing.Size(109, 29);
            this.buttonGetGUID.TabIndex = 5;
            this.buttonGetGUID.Text = "Get GUID";
            this.buttonGetGUID.UseVisualStyleBackColor = true;
            this.buttonGetGUID.Click += new System.EventHandler(this.buttonGetGUID_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(128, 54);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(112, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Select Walls";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(10, 54);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Select Next Enity";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtEntityLabel
            // 
            this.txtEntityLabel.AutoSize = true;
            this.txtEntityLabel.Location = new System.Drawing.Point(12, 38);
            this.txtEntityLabel.Name = "txtEntityLabel";
            this.txtEntityLabel.Size = new System.Drawing.Size(33, 13);
            this.txtEntityLabel.TabIndex = 2;
            this.txtEntityLabel.Text = "Entity";
            // 
            // Wert1
            // 
            this.Wert1.HeaderText = "Wert";
            this.Wert1.Name = "Wert1";
            this.Wert1.ReadOnly = true;
            this.Wert1.Width = 50;
            // 
            // Einzahlwert
            // 
            this.Einzahlwert.HeaderText = "";
            this.Einzahlwert.Name = "Einzahlwert";
            this.Einzahlwert.ReadOnly = true;
            // 
            // f50
            // 
            this.f50.HeaderText = "50 Hz";
            this.f50.Name = "f50";
            this.f50.ReadOnly = true;
            this.f50.Width = 70;
            // 
            // f63
            // 
            this.f63.HeaderText = "63 Hz";
            this.f63.Name = "f63";
            this.f63.ReadOnly = true;
            this.f63.Width = 70;
            // 
            // f80
            // 
            this.f80.HeaderText = "80 Hz";
            this.f80.Name = "f80";
            this.f80.ReadOnly = true;
            this.f80.Width = 70;
            // 
            // f100
            // 
            this.f100.HeaderText = "100 Hz";
            this.f100.Name = "f100";
            this.f100.ReadOnly = true;
            this.f100.Width = 70;
            // 
            // f125
            // 
            this.f125.HeaderText = "125 Hz";
            this.f125.Name = "f125";
            this.f125.ReadOnly = true;
            this.f125.Width = 70;
            // 
            // f160
            // 
            this.f160.HeaderText = "160 Hz";
            this.f160.Name = "f160";
            this.f160.ReadOnly = true;
            this.f160.Width = 70;
            // 
            // f200
            // 
            this.f200.HeaderText = "200 Hz";
            this.f200.Name = "f200";
            this.f200.ReadOnly = true;
            this.f200.Width = 70;
            // 
            // f250
            // 
            this.f250.HeaderText = "250 Hz";
            this.f250.Name = "f250";
            this.f250.ReadOnly = true;
            this.f250.Width = 70;
            // 
            // f315
            // 
            this.f315.HeaderText = "315 Hz";
            this.f315.Name = "f315";
            this.f315.ReadOnly = true;
            this.f315.Width = 70;
            // 
            // f400
            // 
            this.f400.HeaderText = "400 Hz";
            this.f400.Name = "f400";
            this.f400.ReadOnly = true;
            this.f400.Width = 70;
            // 
            // f500
            // 
            this.f500.HeaderText = "500 Hz";
            this.f500.Name = "f500";
            this.f500.ReadOnly = true;
            this.f500.Width = 70;
            // 
            // f630
            // 
            this.f630.HeaderText = "630 Hz";
            this.f630.Name = "f630";
            this.f630.ReadOnly = true;
            this.f630.Width = 70;
            // 
            // f800
            // 
            this.f800.HeaderText = "800 Hz";
            this.f800.Name = "f800";
            this.f800.ReadOnly = true;
            this.f800.Width = 70;
            // 
            // f1000
            // 
            this.f1000.HeaderText = "1000 Hz";
            this.f1000.Name = "f1000";
            this.f1000.ReadOnly = true;
            this.f1000.Width = 70;
            // 
            // f1250
            // 
            this.f1250.HeaderText = "1250 Hz";
            this.f1250.Name = "f1250";
            this.f1250.ReadOnly = true;
            this.f1250.Width = 70;
            // 
            // f1600
            // 
            this.f1600.HeaderText = "1600 Hz";
            this.f1600.Name = "f1600";
            this.f1600.ReadOnly = true;
            this.f1600.Width = 70;
            // 
            // f2000
            // 
            this.f2000.HeaderText = "2000 Hz";
            this.f2000.Name = "f2000";
            this.f2000.ReadOnly = true;
            this.f2000.Width = 70;
            // 
            // f2500
            // 
            this.f2500.HeaderText = "2500 Hz";
            this.f2500.Name = "f2500";
            this.f2500.ReadOnly = true;
            this.f2500.Width = 70;
            // 
            // f3150
            // 
            this.f3150.HeaderText = "3150 Hz";
            this.f3150.Name = "f3150";
            this.f3150.ReadOnly = true;
            this.f3150.Width = 70;
            // 
            // f4000
            // 
            this.f4000.HeaderText = "4000 Hz";
            this.f4000.Name = "f4000";
            this.f4000.ReadOnly = true;
            this.f4000.Width = 70;
            // 
            // f5000
            // 
            this.f5000.HeaderText = "5000 Hz";
            this.f5000.Name = "f5000";
            this.f5000.ReadOnly = true;
            this.f5000.Width = 70;
            // 
            // FormExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1534, 783);
            this.Controls.Add(this.panel1);
            this.Name = "FormExample";
            this.Text = "Embedding viewer in winform";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV01)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Integration.ElementHost controlHost;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label txtEntityLabel;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
		private System.Windows.Forms.TextBox textboxGetGUID;
		private System.Windows.Forms.TextBox textBoxSelectElement;
		private System.Windows.Forms.Button buttonSelectElement;
		private System.Windows.Forms.Button buttonGetGUID;
		private System.Windows.Forms.TextBox textBoxConnections;
		private System.Windows.Forms.TextBox textBoxjJsonInfo;
		private System.Windows.Forms.Button buttonJsonInformation;
		private System.Windows.Forms.Button buttonAddJsonfile;
		private System.Windows.Forms.Label labelJsonfileAdded;
		private System.Windows.Forms.Button buttonSaveIFC;
		private System.Windows.Forms.DataGridView DGV01;
		private System.Windows.Forms.DataGridViewTextBoxColumn Wert1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Einzahlwert;
		private System.Windows.Forms.DataGridViewTextBoxColumn f50;
		private System.Windows.Forms.DataGridViewTextBoxColumn f63;
		private System.Windows.Forms.DataGridViewTextBoxColumn f80;
		private System.Windows.Forms.DataGridViewTextBoxColumn f100;
		private System.Windows.Forms.DataGridViewTextBoxColumn f125;
		private System.Windows.Forms.DataGridViewTextBoxColumn f160;
		private System.Windows.Forms.DataGridViewTextBoxColumn f200;
		private System.Windows.Forms.DataGridViewTextBoxColumn f250;
		private System.Windows.Forms.DataGridViewTextBoxColumn f315;
		private System.Windows.Forms.DataGridViewTextBoxColumn f400;
		private System.Windows.Forms.DataGridViewTextBoxColumn f500;
		private System.Windows.Forms.DataGridViewTextBoxColumn f630;
		private System.Windows.Forms.DataGridViewTextBoxColumn f800;
		private System.Windows.Forms.DataGridViewTextBoxColumn f1000;
		private System.Windows.Forms.DataGridViewTextBoxColumn f1250;
		private System.Windows.Forms.DataGridViewTextBoxColumn f1600;
		private System.Windows.Forms.DataGridViewTextBoxColumn f2000;
		private System.Windows.Forms.DataGridViewTextBoxColumn f2500;
		private System.Windows.Forms.DataGridViewTextBoxColumn f3150;
		private System.Windows.Forms.DataGridViewTextBoxColumn f4000;
		private System.Windows.Forms.DataGridViewTextBoxColumn f5000;
	}
}
