namespace PanTilt123
{
    partial class frmMainWindow
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

        private bool _contentLoaded;
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cbComport = new System.Windows.Forms.ComboBox();
            this.cbBaudrate = new System.Windows.Forms.ComboBox();
            this.btnOpenPort = new System.Windows.Forms.Button();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.btnSetAddr = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtStepSize = new System.Windows.Forms.TextBox();
            this.DataBinding = new System.Windows.Forms.BindingSource(this.components);
            this.btnMovePanToDown = new System.Windows.Forms.Button();
            this.btnMovePanToRight = new System.Windows.Forms.Button();
            this.btnMovePanToUp = new System.Windows.Forms.Button();
            this.btnMovePanToLeft = new System.Windows.Forms.Button();
            this.btnGetTilt = new System.Windows.Forms.Button();
            this.txtGetTilt = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.btnSetTitl = new System.Windows.Forms.Button();
            this.txtBoxSetTilt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnGetPanAngle = new System.Windows.Forms.Button();
            this.txtGetAngle = new System.Windows.Forms.TextBox();
            this.lbGetPan = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSetPan = new System.Windows.Forms.TrackBar();
            this.btnSetPanAngle = new System.Windows.Forms.Button();
            this.txtSetPan = new System.Windows.Forms.TextBox();
            this.lbSetpan = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvDeviceEnumrationInfo = new System.Windows.Forms.DataGridView();
            this.deviceNoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrAddressStringDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serialNumberStringDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hwVersionStringDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fwVersionStringDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uidStringDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deviceEnumrationInfoListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pbScanDevice = new System.Windows.Forms.ProgressBar();
            this.btnGoHome = new System.Windows.Forms.Button();
            this.btnReadSelectedDeviceInfo = new System.Windows.Forms.Button();
            this.btnEnumerateDevices = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btnTiltStop = new System.Windows.Forms.Button();
            this.btnPanStop = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.pbUpdateFirmware = new System.Windows.Forms.ProgressBar();
            this.btnOpenFirmwareFile = new System.Windows.Forms.Button();
            this.btnUpdateFirmwareToSelectedDevice = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.btnSetting = new System.Windows.Forms.Button();
            this.btnLogView = new System.Windows.Forms.Button();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblSerial = new System.Windows.Forms.Label();
            this.lblHw = new System.Windows.Forms.Label();
            this.lblFw = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataBinding)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSetPan)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeviceEnumrationInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deviceEnumrationInfoListBindingSource)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbComport
            // 
            this.cbComport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbComport.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbComport.FormattingEnabled = true;
            this.cbComport.Location = new System.Drawing.Point(12, 12);
            this.cbComport.Name = "cbComport";
            this.cbComport.Size = new System.Drawing.Size(150, 44);
            this.cbComport.TabIndex = 0;
            // 
            // cbBaudrate
            // 
            this.cbBaudrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBaudrate.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBaudrate.FormattingEnabled = true;
            this.cbBaudrate.Items.AddRange(new object[] {
            "9600",
            "19200",
            "57600",
            "115200"});
            this.cbBaudrate.Location = new System.Drawing.Point(12, 62);
            this.cbBaudrate.Name = "cbBaudrate";
            this.cbBaudrate.Size = new System.Drawing.Size(150, 44);
            this.cbBaudrate.TabIndex = 1;
            this.cbBaudrate.SelectionChangeCommitted += new System.EventHandler(this.cbBaudrate_SelectionChangeCommitted);
            // 
            // btnOpenPort
            // 
            this.btnOpenPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenPort.Location = new System.Drawing.Point(12, 112);
            this.btnOpenPort.Name = "btnOpenPort";
            this.btnOpenPort.Size = new System.Drawing.Size(153, 44);
            this.btnOpenPort.TabIndex = 2;
            this.btnOpenPort.Text = "Open";
            this.btnOpenPort.UseVisualStyleBackColor = true;
            this.btnOpenPort.Click += new System.EventHandler(this.btnOpenPort_Click);
            // 
            // txtAddress
            // 
            this.txtAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.Location = new System.Drawing.Point(186, 12);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(166, 41);
            this.txtAddress.TabIndex = 3;
            this.txtAddress.Text = "192.168.1.1";
            // 
            // btnSetAddr
            // 
            this.btnSetAddr.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetAddr.Location = new System.Drawing.Point(186, 62);
            this.btnSetAddr.Name = "btnSetAddr";
            this.btnSetAddr.Size = new System.Drawing.Size(169, 44);
            this.btnSetAddr.TabIndex = 4;
            this.btnSetAddr.Text = "Set Address";
            this.btnSetAddr.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtStepSize);
            this.groupBox1.Controls.Add(this.btnMovePanToDown);
            this.groupBox1.Controls.Add(this.btnMovePanToRight);
            this.groupBox1.Controls.Add(this.btnMovePanToUp);
            this.groupBox1.Controls.Add(this.btnMovePanToLeft);
            this.groupBox1.Controls.Add(this.btnGetTilt);
            this.groupBox1.Controls.Add(this.txtGetTilt);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.trackBar2);
            this.groupBox1.Controls.Add(this.btnSetTitl);
            this.groupBox1.Controls.Add(this.txtBoxSetTilt);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnGetPanAngle);
            this.groupBox1.Controls.Add(this.txtGetAngle);
            this.groupBox1.Controls.Add(this.lbGetPan);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbSetPan);
            this.groupBox1.Controls.Add(this.btnSetPanAngle);
            this.groupBox1.Controls.Add(this.txtSetPan);
            this.groupBox1.Controls.Add(this.lbSetpan);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 162);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1137, 240);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pan Tilt Control";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(1014, 86);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 25);
            this.label8.TabIndex = 28;
            this.label8.Text = "Step size";
            // 
            // txtStepSize
            // 
            this.txtStepSize.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.DataBinding, "TxAngleStepSize", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "0"));
            this.txtStepSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStepSize.Location = new System.Drawing.Point(1019, 114);
            this.txtStepSize.Name = "txtStepSize";
            this.txtStepSize.Size = new System.Drawing.Size(88, 36);
            this.txtStepSize.TabIndex = 27;
            // 
            // DataBinding
            // 
            this.DataBinding.DataSource = typeof(PanTilt123.Class.MainWindowViewModel);
            // 
            // btnMovePanToDown
            // 
            this.btnMovePanToDown.Location = new System.Drawing.Point(865, 158);
            this.btnMovePanToDown.Name = "btnMovePanToDown";
            this.btnMovePanToDown.Size = new System.Drawing.Size(50, 50);
            this.btnMovePanToDown.TabIndex = 26;
            this.btnMovePanToDown.Text = ".";
            this.btnMovePanToDown.UseVisualStyleBackColor = true;
            // 
            // btnMovePanToRight
            // 
            this.btnMovePanToRight.Location = new System.Drawing.Point(918, 102);
            this.btnMovePanToRight.Name = "btnMovePanToRight";
            this.btnMovePanToRight.Size = new System.Drawing.Size(50, 50);
            this.btnMovePanToRight.TabIndex = 25;
            this.btnMovePanToRight.Text = ">";
            this.btnMovePanToRight.UseVisualStyleBackColor = true;
            // 
            // btnMovePanToUp
            // 
            this.btnMovePanToUp.Location = new System.Drawing.Point(865, 44);
            this.btnMovePanToUp.Name = "btnMovePanToUp";
            this.btnMovePanToUp.Size = new System.Drawing.Size(50, 50);
            this.btnMovePanToUp.TabIndex = 24;
            this.btnMovePanToUp.Text = "^";
            this.btnMovePanToUp.UseVisualStyleBackColor = true;
            // 
            // btnMovePanToLeft
            // 
            this.btnMovePanToLeft.Location = new System.Drawing.Point(812, 102);
            this.btnMovePanToLeft.Name = "btnMovePanToLeft";
            this.btnMovePanToLeft.Size = new System.Drawing.Size(50, 50);
            this.btnMovePanToLeft.TabIndex = 6;
            this.btnMovePanToLeft.Text = "<";
            this.btnMovePanToLeft.UseVisualStyleBackColor = true;
            // 
            // btnGetTilt
            // 
            this.btnGetTilt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetTilt.Location = new System.Drawing.Point(641, 150);
            this.btnGetTilt.Name = "btnGetTilt";
            this.btnGetTilt.Size = new System.Drawing.Size(70, 39);
            this.btnGetTilt.TabIndex = 23;
            this.btnGetTilt.Text = "Get";
            this.btnGetTilt.UseVisualStyleBackColor = true;
            // 
            // txtGetTilt
            // 
            this.txtGetTilt.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.DataBinding, "RxTiltAngle", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "0"));
            this.txtGetTilt.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGetTilt.Location = new System.Drawing.Point(641, 103);
            this.txtGetTilt.Name = "txtGetTilt";
            this.txtGetTilt.Size = new System.Drawing.Size(70, 36);
            this.txtGetTilt.TabIndex = 22;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(638, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 25);
            this.label7.TabIndex = 21;
            this.label7.Text = "Get Tilt";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(577, 209);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 20);
            this.label6.TabIndex = 20;
            this.label6.Text = "-80";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(573, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 20);
            this.label5.TabIndex = 19;
            this.label5.Text = "+80";
            // 
            // trackBar2
            // 
            this.trackBar2.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.DataBinding, "TxTiltAngle", true));
            this.trackBar2.LargeChange = 1;
            this.trackBar2.Location = new System.Drawing.Point(576, 32);
            this.trackBar2.Maximum = 80;
            this.trackBar2.Minimum = -80;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar2.Size = new System.Drawing.Size(56, 179);
            this.trackBar2.TabIndex = 18;
            // 
            // btnSetTitl
            // 
            this.btnSetTitl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetTitl.Location = new System.Drawing.Point(486, 150);
            this.btnSetTitl.Name = "btnSetTitl";
            this.btnSetTitl.Size = new System.Drawing.Size(70, 39);
            this.btnSetTitl.TabIndex = 17;
            this.btnSetTitl.Text = "Set";
            this.btnSetTitl.UseVisualStyleBackColor = true;
            // 
            // txtBoxSetTilt
            // 
            this.txtBoxSetTilt.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.DataBinding, "TxTiltAngle", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "0"));
            this.txtBoxSetTilt.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxSetTilt.Location = new System.Drawing.Point(486, 103);
            this.txtBoxSetTilt.Name = "txtBoxSetTilt";
            this.txtBoxSetTilt.Size = new System.Drawing.Size(70, 36);
            this.txtBoxSetTilt.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(477, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 25);
            this.label4.TabIndex = 15;
            this.label4.Text = " Set Tilt";
            // 
            // btnGetPanAngle
            // 
            this.btnGetPanAngle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetPanAngle.Location = new System.Drawing.Point(221, 175);
            this.btnGetPanAngle.Name = "btnGetPanAngle";
            this.btnGetPanAngle.Size = new System.Drawing.Size(75, 39);
            this.btnGetPanAngle.TabIndex = 14;
            this.btnGetPanAngle.Text = "Get";
            this.btnGetPanAngle.UseVisualStyleBackColor = true;
            // 
            // txtGetAngle
            // 
            this.txtGetAngle.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.DataBinding, "RxPanAngle", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "0"));
            this.txtGetAngle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGetAngle.Location = new System.Drawing.Point(145, 175);
            this.txtGetAngle.Name = "txtGetAngle";
            this.txtGetAngle.Size = new System.Drawing.Size(70, 36);
            this.txtGetAngle.TabIndex = 13;
            // 
            // lbGetPan
            // 
            this.lbGetPan.AutoSize = true;
            this.lbGetPan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGetPan.Location = new System.Drawing.Point(55, 185);
            this.lbGetPan.Name = "lbGetPan";
            this.lbGetPan.Size = new System.Drawing.Size(83, 25);
            this.lbGetPan.TabIndex = 12;
            this.lbGetPan.Text = "Get Pan";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(297, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 20);
            this.label2.TabIndex = 11;
            this.label2.Text = "+200";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "-200";
            // 
            // tbSetPan
            // 
            this.tbSetPan.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.DataBinding, "TxPanAngle", true));
            this.tbSetPan.LargeChange = 1;
            this.tbSetPan.Location = new System.Drawing.Point(42, 113);
            this.tbSetPan.Maximum = 200;
            this.tbSetPan.Minimum = -200;
            this.tbSetPan.Name = "tbSetPan";
            this.tbSetPan.Size = new System.Drawing.Size(270, 56);
            this.tbSetPan.TabIndex = 9;
            // 
            // btnSetPanAngle
            // 
            this.btnSetPanAngle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetPanAngle.Location = new System.Drawing.Point(221, 53);
            this.btnSetPanAngle.Name = "btnSetPanAngle";
            this.btnSetPanAngle.Size = new System.Drawing.Size(75, 39);
            this.btnSetPanAngle.TabIndex = 8;
            this.btnSetPanAngle.Text = "Set";
            this.btnSetPanAngle.UseVisualStyleBackColor = true;
            // 
            // txtSetPan
            // 
            this.txtSetPan.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.DataBinding, "TxPanAngle", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "0"));
            this.txtSetPan.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSetPan.Location = new System.Drawing.Point(145, 53);
            this.txtSetPan.Name = "txtSetPan";
            this.txtSetPan.Size = new System.Drawing.Size(70, 36);
            this.txtSetPan.TabIndex = 7;
            // 
            // lbSetpan
            // 
            this.lbSetpan.AutoSize = true;
            this.lbSetpan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSetpan.Location = new System.Drawing.Point(55, 59);
            this.lbSetpan.Name = "lbSetpan";
            this.lbSetpan.Size = new System.Drawing.Size(82, 25);
            this.lbSetpan.TabIndex = 6;
            this.lbSetpan.Text = "Set Pan";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox2.Controls.Add(this.dgvDeviceEnumrationInfo);
            this.groupBox2.Controls.Add(this.pbScanDevice);
            this.groupBox2.Controls.Add(this.btnGoHome);
            this.groupBox2.Controls.Add(this.btnReadSelectedDeviceInfo);
            this.groupBox2.Controls.Add(this.btnEnumerateDevices);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 408);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1137, 245);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Device Management";
            // 
            // dgvDeviceEnumrationInfo
            // 
            this.dgvDeviceEnumrationInfo.AutoGenerateColumns = false;
            this.dgvDeviceEnumrationInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDeviceEnumrationInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.deviceNoDataGridViewTextBoxColumn,
            this.ctrAddressStringDataGridViewTextBoxColumn,
            this.serialNumberStringDataGridViewTextBoxColumn,
            this.hwVersionStringDataGridViewTextBoxColumn,
            this.fwVersionStringDataGridViewTextBoxColumn,
            this.uidStringDataGridViewTextBoxColumn});
            this.dgvDeviceEnumrationInfo.DataSource = this.deviceEnumrationInfoListBindingSource;
            this.dgvDeviceEnumrationInfo.Location = new System.Drawing.Point(201, 35);
            this.dgvDeviceEnumrationInfo.Name = "dgvDeviceEnumrationInfo";
            this.dgvDeviceEnumrationInfo.RowHeadersWidth = 51;
            this.dgvDeviceEnumrationInfo.RowTemplate.Height = 24;
            this.dgvDeviceEnumrationInfo.Size = new System.Drawing.Size(930, 188);
            this.dgvDeviceEnumrationInfo.TabIndex = 4;
            // 
            // deviceNoDataGridViewTextBoxColumn
            // 
            this.deviceNoDataGridViewTextBoxColumn.DataPropertyName = "DeviceNo";
            this.deviceNoDataGridViewTextBoxColumn.HeaderText = "DeviceNo";
            this.deviceNoDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.deviceNoDataGridViewTextBoxColumn.Name = "deviceNoDataGridViewTextBoxColumn";
            this.deviceNoDataGridViewTextBoxColumn.Width = 125;
            // 
            // ctrAddressStringDataGridViewTextBoxColumn
            // 
            this.ctrAddressStringDataGridViewTextBoxColumn.DataPropertyName = "CtrAddressString";
            this.ctrAddressStringDataGridViewTextBoxColumn.HeaderText = "Address";
            this.ctrAddressStringDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.ctrAddressStringDataGridViewTextBoxColumn.Name = "ctrAddressStringDataGridViewTextBoxColumn";
            this.ctrAddressStringDataGridViewTextBoxColumn.Width = 125;
            // 
            // serialNumberStringDataGridViewTextBoxColumn
            // 
            this.serialNumberStringDataGridViewTextBoxColumn.DataPropertyName = "SerialNumberString";
            this.serialNumberStringDataGridViewTextBoxColumn.HeaderText = "SerialNumber";
            this.serialNumberStringDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.serialNumberStringDataGridViewTextBoxColumn.Name = "serialNumberStringDataGridViewTextBoxColumn";
            this.serialNumberStringDataGridViewTextBoxColumn.Width = 125;
            // 
            // hwVersionStringDataGridViewTextBoxColumn
            // 
            this.hwVersionStringDataGridViewTextBoxColumn.DataPropertyName = "HwVersionString";
            this.hwVersionStringDataGridViewTextBoxColumn.HeaderText = "HwVersion";
            this.hwVersionStringDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.hwVersionStringDataGridViewTextBoxColumn.Name = "hwVersionStringDataGridViewTextBoxColumn";
            this.hwVersionStringDataGridViewTextBoxColumn.Width = 125;
            // 
            // fwVersionStringDataGridViewTextBoxColumn
            // 
            this.fwVersionStringDataGridViewTextBoxColumn.DataPropertyName = "FwVersionString";
            this.fwVersionStringDataGridViewTextBoxColumn.HeaderText = "FwVersion";
            this.fwVersionStringDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.fwVersionStringDataGridViewTextBoxColumn.Name = "fwVersionStringDataGridViewTextBoxColumn";
            this.fwVersionStringDataGridViewTextBoxColumn.Width = 125;
            // 
            // uidStringDataGridViewTextBoxColumn
            // 
            this.uidStringDataGridViewTextBoxColumn.DataPropertyName = "UidString";
            this.uidStringDataGridViewTextBoxColumn.HeaderText = "Uid";
            this.uidStringDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.uidStringDataGridViewTextBoxColumn.Name = "uidStringDataGridViewTextBoxColumn";
            this.uidStringDataGridViewTextBoxColumn.Width = 125;
            // 
            // deviceEnumrationInfoListBindingSource
            // 
            this.deviceEnumrationInfoListBindingSource.DataMember = "DeviceEnumrationInfoList";
            this.deviceEnumrationInfoListBindingSource.DataSource = this.DataBinding;
            // 
            // pbScanDevice
            // 
            this.pbScanDevice.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.DataBinding, "ProgressBarDeviceEnumrationValue", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "0"));
            this.pbScanDevice.Location = new System.Drawing.Point(10, 91);
            this.pbScanDevice.Name = "pbScanDevice";
            this.pbScanDevice.Size = new System.Drawing.Size(185, 15);
            this.pbScanDevice.TabIndex = 3;
            // 
            // btnGoHome
            // 
            this.btnGoHome.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoHome.Location = new System.Drawing.Point(10, 173);
            this.btnGoHome.Name = "btnGoHome";
            this.btnGoHome.Size = new System.Drawing.Size(185, 50);
            this.btnGoHome.TabIndex = 2;
            this.btnGoHome.Text = "Go Home";
            this.btnGoHome.UseVisualStyleBackColor = true;
            // 
            // btnReadSelectedDeviceInfo
            // 
            this.btnReadSelectedDeviceInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReadSelectedDeviceInfo.Location = new System.Drawing.Point(10, 117);
            this.btnReadSelectedDeviceInfo.Name = "btnReadSelectedDeviceInfo";
            this.btnReadSelectedDeviceInfo.Size = new System.Drawing.Size(185, 50);
            this.btnReadSelectedDeviceInfo.TabIndex = 1;
            this.btnReadSelectedDeviceInfo.Text = "Read Info";
            this.btnReadSelectedDeviceInfo.UseVisualStyleBackColor = true;
            // 
            // btnEnumerateDevices
            // 
            this.btnEnumerateDevices.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnumerateDevices.Location = new System.Drawing.Point(10, 35);
            this.btnEnumerateDevices.Name = "btnEnumerateDevices";
            this.btnEnumerateDevices.Size = new System.Drawing.Size(185, 50);
            this.btnEnumerateDevices.TabIndex = 0;
            this.btnEnumerateDevices.Text = "Scan";
            this.btnEnumerateDevices.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.button4);
            this.groupBox3.Controls.Add(this.btnTiltStop);
            this.groupBox3.Controls.Add(this.btnPanStop);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(12, 659);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1137, 121);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Advanced Control";
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(334, 45);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(237, 47);
            this.button3.TabIndex = 5;
            this.button3.Text = "Tilt Go home";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(59, 45);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(237, 47);
            this.button4.TabIndex = 4;
            this.button4.Text = "Pan Go home";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // btnTiltStop
            // 
            this.btnTiltStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTiltStop.Location = new System.Drawing.Point(865, 45);
            this.btnTiltStop.Name = "btnTiltStop";
            this.btnTiltStop.Size = new System.Drawing.Size(237, 47);
            this.btnTiltStop.TabIndex = 3;
            this.btnTiltStop.Text = "Tilt Stop";
            this.btnTiltStop.UseVisualStyleBackColor = true;
            // 
            // btnPanStop
            // 
            this.btnPanStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPanStop.Location = new System.Drawing.Point(604, 45);
            this.btnPanStop.Name = "btnPanStop";
            this.btnPanStop.Size = new System.Drawing.Size(237, 47);
            this.btnPanStop.TabIndex = 2;
            this.btnPanStop.Text = "Pan Stop";
            this.btnPanStop.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox4.Controls.Add(this.pbUpdateFirmware);
            this.groupBox4.Controls.Add(this.btnOpenFirmwareFile);
            this.groupBox4.Controls.Add(this.btnUpdateFirmwareToSelectedDevice);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(12, 786);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1137, 107);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Update Firmware";
            // 
            // pbUpdateFirmware
            // 
            this.pbUpdateFirmware.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.DataBinding, "ProgressBarUpdateFwValue", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "0"));
            this.pbUpdateFirmware.Location = new System.Drawing.Point(385, 35);
            this.pbUpdateFirmware.Name = "pbUpdateFirmware";
            this.pbUpdateFirmware.Size = new System.Drawing.Size(746, 50);
            this.pbUpdateFirmware.TabIndex = 2;
            // 
            // btnOpenFirmwareFile
            // 
            this.btnOpenFirmwareFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenFirmwareFile.Location = new System.Drawing.Point(201, 35);
            this.btnOpenFirmwareFile.Name = "btnOpenFirmwareFile";
            this.btnOpenFirmwareFile.Size = new System.Drawing.Size(178, 50);
            this.btnOpenFirmwareFile.TabIndex = 1;
            this.btnOpenFirmwareFile.Text = "Open FW";
            this.btnOpenFirmwareFile.UseVisualStyleBackColor = true;
            // 
            // btnUpdateFirmwareToSelectedDevice
            // 
            this.btnUpdateFirmwareToSelectedDevice.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateFirmwareToSelectedDevice.Location = new System.Drawing.Point(6, 35);
            this.btnUpdateFirmwareToSelectedDevice.Name = "btnUpdateFirmwareToSelectedDevice";
            this.btnUpdateFirmwareToSelectedDevice.Size = new System.Drawing.Size(189, 50);
            this.btnUpdateFirmwareToSelectedDevice.TabIndex = 0;
            this.btnUpdateFirmwareToSelectedDevice.Text = "Update FW";
            this.btnUpdateFirmwareToSelectedDevice.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(3, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 25);
            this.label9.TabIndex = 9;
            this.label9.Text = "Status";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(113, 15);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 25);
            this.label10.TabIndex = 10;
            this.label10.Text = "Addr:";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(304, 15);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(75, 25);
            this.label11.TabIndex = 11;
            this.label11.Text = "Serial:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(539, 17);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(55, 25);
            this.label12.TabIndex = 12;
            this.label12.Text = "HW:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(717, 15);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 25);
            this.label13.TabIndex = 13;
            this.label13.Text = "FW:";
            // 
            // btnSetting
            // 
            this.btnSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetting.Location = new System.Drawing.Point(918, 9);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(105, 40);
            this.btnSetting.TabIndex = 14;
            this.btnSetting.Text = "Setting";
            this.btnSetting.UseVisualStyleBackColor = true;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // btnLogView
            // 
            this.btnLogView.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogView.Location = new System.Drawing.Point(1029, 7);
            this.btnLogView.Name = "btnLogView";
            this.btnLogView.Size = new System.Drawing.Size(105, 40);
            this.btnLogView.TabIndex = 15;
            this.btnLogView.Text = "Log View";
            this.btnLogView.UseVisualStyleBackColor = true;
            this.btnLogView.Click += new System.EventHandler(this.btnLogView_Click);
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddress.Location = new System.Drawing.Point(184, 17);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(45, 20);
            this.lblAddress.TabIndex = 16;
            this.lblAddress.Text = "none";
            // 
            // lblSerial
            // 
            this.lblSerial.AutoSize = true;
            this.lblSerial.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerial.Location = new System.Drawing.Point(382, 17);
            this.lblSerial.Name = "lblSerial";
            this.lblSerial.Size = new System.Drawing.Size(45, 20);
            this.lblSerial.TabIndex = 17;
            this.lblSerial.Text = "none";
            // 
            // lblHw
            // 
            this.lblHw.AutoSize = true;
            this.lblHw.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHw.Location = new System.Drawing.Point(600, 19);
            this.lblHw.Name = "lblHw";
            this.lblHw.Size = new System.Drawing.Size(45, 20);
            this.lblHw.TabIndex = 18;
            this.lblHw.Text = "none";
            // 
            // lblFw
            // 
            this.lblFw.AutoSize = true;
            this.lblFw.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFw.Location = new System.Drawing.Point(785, 17);
            this.lblFw.Name = "lblFw";
            this.lblFw.Size = new System.Drawing.Size(45, 20);
            this.lblFw.TabIndex = 19;
            this.lblFw.Text = "none";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnLogView);
            this.panel1.Controls.Add(this.lblFw);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.lblHw);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.lblSerial);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.lblAddress);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.btnSetting);
            this.panel1.Location = new System.Drawing.Point(12, 899);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1137, 56);
            this.panel1.TabIndex = 20;
            // 
            // frmMainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1161, 974);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSetAddr);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.btnOpenPort);
            this.Controls.Add(this.cbBaudrate);
            this.Controls.Add(this.cbComport);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmMainWindow";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataBinding)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSetPan)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeviceEnumrationInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deviceEnumrationInfoListBindingSource)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbComport;
        private System.Windows.Forms.ComboBox cbBaudrate;
        private System.Windows.Forms.Button btnOpenPort;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Button btnSetAddr;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TrackBar tbSetPan;
        private System.Windows.Forms.Button btnSetPanAngle;
        private System.Windows.Forms.TextBox txtSetPan;
        private System.Windows.Forms.Label lbSetpan;
        private System.Windows.Forms.Button btnGetPanAngle;
        private System.Windows.Forms.TextBox txtGetAngle;
        private System.Windows.Forms.Label lbGetPan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSetTitl;
        private System.Windows.Forms.TextBox txtBoxSetTilt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnMovePanToDown;
        private System.Windows.Forms.Button btnMovePanToRight;
        private System.Windows.Forms.Button btnMovePanToUp;
        private System.Windows.Forms.Button btnMovePanToLeft;
        private System.Windows.Forms.Button btnGetTilt;
        private System.Windows.Forms.TextBox txtGetTilt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtStepSize;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvDeviceEnumrationInfo;
        private System.Windows.Forms.ProgressBar pbScanDevice;
        private System.Windows.Forms.Button btnGoHome;
        private System.Windows.Forms.Button btnReadSelectedDeviceInfo;
        private System.Windows.Forms.Button btnEnumerateDevices;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnTiltStop;
        private System.Windows.Forms.Button btnPanStop;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ProgressBar pbUpdateFirmware;
        private System.Windows.Forms.Button btnOpenFirmwareFile;
        private System.Windows.Forms.Button btnUpdateFirmwareToSelectedDevice;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnSetting;
        private System.Windows.Forms.Button btnLogView;
        private System.Windows.Forms.BindingSource DataBinding;
        private System.Windows.Forms.BindingSource deviceEnumrationInfoListBindingSource;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblSerial;
        private System.Windows.Forms.Label lblHw;
        private System.Windows.Forms.Label lblFw;
        private System.Windows.Forms.DataGridViewTextBoxColumn deviceNoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ctrAddressStringDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn serialNumberStringDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hwVersionStringDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fwVersionStringDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn uidStringDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Panel panel1;
    }
}

