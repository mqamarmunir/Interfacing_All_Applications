using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;
//using BusinessLayer;
using BusinessLayer;
using BusinessEntities;
//using BusinessService;
using System.Linq;
using System.Collections.Generic;
using DataModel;
using System.Web.Script.Serialization;


namespace WindowsApplication5
{
	/// <summary>
	/// Summary description for MDIChild.
	/// </summary>
	/// 

	public class MachineRegistration : System.Windows.Forms.Form
    {
        private IContainer components;
        private TabControl tabCtrl;
        private Label label1;
        private Label label2;
        private Label label3;
        private DataGridView grdMachines;
        private Panel panel1;
        private Button btnReset;
        private Button btnSave;
        private TabPage tabPag;
        private CheckBox chkActive;
        private ErrorProvider errorProvider1;
        private ComboBox cmbCommMethod;
        private Label label5;
        private Label label6;
        private Label label8;
        private Label label7;
        private TextBox txtModel;
        private TextBox txtName;
        private ComboBox cmbStandard;
        private ComboBox cmbPort;
        private ComboBox cmbBaudRate;
        private TextBox txtRecordTerminator;
        private Label label12;
        private TextBox txtAckChar;
        private Label label11;
        private ComboBox cmbFlowControl;
        private ComboBox cmbDataBits;
        private ComboBox cmbStopBits;
        private Label label4;
        private Label label9;
        private Label label10;
        private ComboBox cmbParity;
        private BindingSource mitinstrumentsBindingSource;
        private Button btnSetDefaults;
        private DataGridViewTextBoxColumn instrumentID;
        private DataGridViewTextBoxColumn instrumentNameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn modelDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn communicationStnadardDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn communicationmethodDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn pORTDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn baudRateDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn parityDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn stopbitDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn dataBitDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn flowControlDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn activeDataGridViewTextBoxColumn;
        private DataGridViewButtonColumn AddTests;

        private readonly UnitOfWork _unitOfWork;
		public MachineRegistration()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            _unitOfWork = new UnitOfWork();
            

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		public TabPage TabPag
		{
			get
			{
				return tabPag;
			}
			set
			{
				tabPag = value;
			}
		}

		public TabControl TabCtrl
		{
			set
			{
				tabCtrl = value;
			}
		}


		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.grdMachines = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSetDefaults = new System.Windows.Forms.Button();
            this.txtRecordTerminator = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtAckChar = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbFlowControl = new System.Windows.Forms.ComboBox();
            this.cmbDataBits = new System.Windows.Forms.ComboBox();
            this.cmbStopBits = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbParity = new System.Windows.Forms.ComboBox();
            this.cmbBaudRate = new System.Windows.Forms.ComboBox();
            this.cmbPort = new System.Windows.Forms.ComboBox();
            this.cmbStandard = new System.Windows.Forms.ComboBox();
            this.cmbCommMethod = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.AddTests = new System.Windows.Forms.DataGridViewButtonColumn();
            this.instrumentID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.instrumentNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modelDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.communicationStnadardDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.communicationmethodDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pORTDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.baudRateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stopbitDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataBitDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flowControlDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.activeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mitinstrumentsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grdMachines)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mitinstrumentsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.label1.Location = new System.Drawing.Point(262, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Model";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Instrument Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(428, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Comm. Method";
            // 
            // grdMachines
            // 
            this.grdMachines.AllowUserToAddRows = false;
            this.grdMachines.AllowUserToDeleteRows = false;
            this.grdMachines.AllowUserToResizeRows = false;
            this.grdMachines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMachines.AutoGenerateColumns = false;
            this.grdMachines.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdMachines.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdMachines.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.instrumentID,
            this.instrumentNameDataGridViewTextBoxColumn,
            this.modelDataGridViewTextBoxColumn,
            this.communicationStnadardDataGridViewTextBoxColumn,
            this.communicationmethodDataGridViewTextBoxColumn,
            this.pORTDataGridViewTextBoxColumn,
            this.baudRateDataGridViewTextBoxColumn,
            this.parityDataGridViewTextBoxColumn,
            this.stopbitDataGridViewTextBoxColumn,
            this.dataBitDataGridViewTextBoxColumn,
            this.flowControlDataGridViewTextBoxColumn,
            this.activeDataGridViewTextBoxColumn,
            this.AddTests});
            this.grdMachines.DataSource = this.mitinstrumentsBindingSource;
            this.grdMachines.Location = new System.Drawing.Point(4, 162);
            this.grdMachines.Name = "grdMachines";
            this.grdMachines.ReadOnly = true;
            this.grdMachines.Size = new System.Drawing.Size(881, 248);
            this.grdMachines.TabIndex = 7;
            this.grdMachines.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdMachines_CellContentClick);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.btnSetDefaults);
            this.panel1.Controls.Add(this.txtRecordTerminator);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.txtAckChar);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.cmbFlowControl);
            this.panel1.Controls.Add(this.cmbDataBits);
            this.panel1.Controls.Add(this.cmbStopBits);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.cmbParity);
            this.panel1.Controls.Add(this.cmbBaudRate);
            this.panel1.Controls.Add(this.cmbPort);
            this.panel1.Controls.Add(this.cmbStandard);
            this.panel1.Controls.Add(this.cmbCommMethod);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtModel);
            this.panel1.Controls.Add(this.txtName);
            this.panel1.Controls.Add(this.chkActive);
            this.panel1.Controls.Add(this.btnReset);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(881, 153);
            this.panel1.TabIndex = 8;
            // 
            // btnSetDefaults
            // 
            this.btnSetDefaults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSetDefaults.Location = new System.Drawing.Point(590, 125);
            this.btnSetDefaults.Name = "btnSetDefaults";
            this.btnSetDefaults.Size = new System.Drawing.Size(75, 23);
            this.btnSetDefaults.TabIndex = 13;
            this.btnSetDefaults.Text = "Set &Defaults";
            this.btnSetDefaults.UseVisualStyleBackColor = true;
            this.btnSetDefaults.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // txtRecordTerminator
            // 
            this.txtRecordTerminator.Location = new System.Drawing.Point(373, 109);
            this.txtRecordTerminator.Name = "txtRecordTerminator";
            this.txtRecordTerminator.Size = new System.Drawing.Size(117, 20);
            this.txtRecordTerminator.TabIndex = 11;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(264, 112);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(95, 13);
            this.label12.TabIndex = 44;
            this.label12.Text = "Record Terminator";
            // 
            // txtAckChar
            // 
            this.txtAckChar.Location = new System.Drawing.Point(141, 109);
            this.txtAckChar.Name = "txtAckChar";
            this.txtAckChar.Size = new System.Drawing.Size(117, 20);
            this.txtAckChar.TabIndex = 10;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(32, 112);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(101, 13);
            this.label11.TabIndex = 42;
            this.label11.Text = "Ack Character(Dec)";
            // 
            // cmbFlowControl
            // 
            this.cmbFlowControl.FormattingEnabled = true;
            this.cmbFlowControl.Items.AddRange(new object[] {
            "None"});
            this.cmbFlowControl.Location = new System.Drawing.Point(636, 76);
            this.cmbFlowControl.Name = "cmbFlowControl";
            this.cmbFlowControl.Size = new System.Drawing.Size(199, 21);
            this.cmbFlowControl.TabIndex = 9;
            // 
            // cmbDataBits
            // 
            this.cmbDataBits.FormattingEnabled = true;
            this.cmbDataBits.Items.AddRange(new object[] {
            "7",
            "8"});
            this.cmbDataBits.Location = new System.Drawing.Point(326, 76);
            this.cmbDataBits.Name = "cmbDataBits";
            this.cmbDataBits.Size = new System.Drawing.Size(219, 21);
            this.cmbDataBits.TabIndex = 8;
            // 
            // cmbStopBits
            // 
            this.cmbStopBits.FormattingEnabled = true;
            this.cmbStopBits.Items.AddRange(new object[] {
            "None",
            "One"});
            this.cmbStopBits.Location = new System.Drawing.Point(141, 76);
            this.cmbStopBits.Name = "cmbStopBits";
            this.cmbStopBits.Size = new System.Drawing.Size(117, 21);
            this.cmbStopBits.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 37;
            this.label4.Text = "Stop Bit";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.label9.Location = new System.Drawing.Point(262, 80);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 13);
            this.label9.TabIndex = 36;
            this.label9.Text = "Data Bits";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(551, 79);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 13);
            this.label10.TabIndex = 38;
            this.label10.Text = "Flow Control";
            // 
            // cmbParity
            // 
            this.cmbParity.FormattingEnabled = true;
            this.cmbParity.Items.AddRange(new object[] {
            "None"});
            this.cmbParity.Location = new System.Drawing.Point(636, 47);
            this.cmbParity.Name = "cmbParity";
            this.cmbParity.Size = new System.Drawing.Size(199, 21);
            this.cmbParity.TabIndex = 6;
            // 
            // cmbBaudRate
            // 
            this.cmbBaudRate.FormattingEnabled = true;
            this.cmbBaudRate.Items.AddRange(new object[] {
            "4800",
            "9600",
            "14400",
            "19200",
            "28800",
            "38400"});
            this.cmbBaudRate.Location = new System.Drawing.Point(326, 47);
            this.cmbBaudRate.Name = "cmbBaudRate";
            this.cmbBaudRate.Size = new System.Drawing.Size(219, 21);
            this.cmbBaudRate.TabIndex = 5;
            // 
            // cmbPort
            // 
            this.cmbPort.FormattingEnabled = true;
            this.cmbPort.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5"});
            this.cmbPort.Location = new System.Drawing.Point(141, 47);
            this.cmbPort.Name = "cmbPort";
            this.cmbPort.Size = new System.Drawing.Size(117, 21);
            this.cmbPort.TabIndex = 4;
            // 
            // cmbStandard
            // 
            this.cmbStandard.FormattingEnabled = true;
            this.cmbStandard.Items.AddRange(new object[] {
            "ASTM",
            "Other"});
            this.cmbStandard.Location = new System.Drawing.Point(714, 12);
            this.cmbStandard.Name = "cmbStandard";
            this.cmbStandard.Size = new System.Drawing.Size(121, 21);
            this.cmbStandard.TabIndex = 3;
            // 
            // cmbCommMethod
            // 
            this.cmbCommMethod.FormattingEnabled = true;
            this.cmbCommMethod.Items.AddRange(new object[] {
            "Serial",
            "LAN"});
            this.cmbCommMethod.Location = new System.Drawing.Point(508, 13);
            this.cmbCommMethod.Name = "cmbCommMethod";
            this.cmbCommMethod.Size = new System.Drawing.Size(121, 21);
            this.cmbCommMethod.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Comm Port";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.label6.Location = new System.Drawing.Point(262, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "Baud Rate";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(551, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "Parity";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(628, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Comm. Standard";
            // 
            // txtModel
            // 
            this.txtModel.Location = new System.Drawing.Point(303, 17);
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(117, 20);
            this.txtModel.TabIndex = 1;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(141, 17);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(117, 20);
            this.txtName.TabIndex = 0;
            // 
            // chkActive
            // 
            this.chkActive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkActive.AutoSize = true;
            this.chkActive.Checked = true;
            this.chkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActive.Location = new System.Drawing.Point(508, 131);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(56, 17);
            this.chkActive.TabIndex = 12;
            this.chkActive.Text = "Active";
            this.chkActive.UseVisualStyleBackColor = true;
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReset.Location = new System.Drawing.Point(754, 125);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 15;
            this.btnReset.Text = "&Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(671, 125);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // AddTests
            // 
            this.AddTests.HeaderText = "";
            this.AddTests.Name = "AddTests";
            this.AddTests.ReadOnly = true;
            this.AddTests.Text = "Add Tests";
            this.AddTests.UseColumnTextForButtonValue = true;
            // 
            // instrumentID
            // 
            this.instrumentID.DataPropertyName = "InstrumentID";
            this.instrumentID.HeaderText = "InstrumentID";
            this.instrumentID.Name = "instrumentID";
            this.instrumentID.ReadOnly = true;
            this.instrumentID.Visible = false;
            // 
            // instrumentNameDataGridViewTextBoxColumn
            // 
            this.instrumentNameDataGridViewTextBoxColumn.DataPropertyName = "Instrument_Name";
            this.instrumentNameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.instrumentNameDataGridViewTextBoxColumn.Name = "instrumentNameDataGridViewTextBoxColumn";
            this.instrumentNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // modelDataGridViewTextBoxColumn
            // 
            this.modelDataGridViewTextBoxColumn.DataPropertyName = "Model";
            this.modelDataGridViewTextBoxColumn.HeaderText = "Model";
            this.modelDataGridViewTextBoxColumn.Name = "modelDataGridViewTextBoxColumn";
            this.modelDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // communicationStnadardDataGridViewTextBoxColumn
            // 
            this.communicationStnadardDataGridViewTextBoxColumn.DataPropertyName = "Communication_Stnadard";
            this.communicationStnadardDataGridViewTextBoxColumn.HeaderText = "Standard";
            this.communicationStnadardDataGridViewTextBoxColumn.Name = "communicationStnadardDataGridViewTextBoxColumn";
            this.communicationStnadardDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // communicationmethodDataGridViewTextBoxColumn
            // 
            this.communicationmethodDataGridViewTextBoxColumn.DataPropertyName = "Communication_method";
            this.communicationmethodDataGridViewTextBoxColumn.HeaderText = "Comm Method";
            this.communicationmethodDataGridViewTextBoxColumn.Name = "communicationmethodDataGridViewTextBoxColumn";
            this.communicationmethodDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // pORTDataGridViewTextBoxColumn
            // 
            this.pORTDataGridViewTextBoxColumn.DataPropertyName = "PORT";
            this.pORTDataGridViewTextBoxColumn.HeaderText = "PORT";
            this.pORTDataGridViewTextBoxColumn.Name = "pORTDataGridViewTextBoxColumn";
            this.pORTDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // baudRateDataGridViewTextBoxColumn
            // 
            this.baudRateDataGridViewTextBoxColumn.DataPropertyName = "BaudRate";
            this.baudRateDataGridViewTextBoxColumn.HeaderText = "BaudRate";
            this.baudRateDataGridViewTextBoxColumn.Name = "baudRateDataGridViewTextBoxColumn";
            this.baudRateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // parityDataGridViewTextBoxColumn
            // 
            this.parityDataGridViewTextBoxColumn.DataPropertyName = "Parity";
            this.parityDataGridViewTextBoxColumn.HeaderText = "Parity";
            this.parityDataGridViewTextBoxColumn.Name = "parityDataGridViewTextBoxColumn";
            this.parityDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // stopbitDataGridViewTextBoxColumn
            // 
            this.stopbitDataGridViewTextBoxColumn.DataPropertyName = "Stopbit";
            this.stopbitDataGridViewTextBoxColumn.HeaderText = "Stopbit";
            this.stopbitDataGridViewTextBoxColumn.Name = "stopbitDataGridViewTextBoxColumn";
            this.stopbitDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dataBitDataGridViewTextBoxColumn
            // 
            this.dataBitDataGridViewTextBoxColumn.DataPropertyName = "DataBit";
            this.dataBitDataGridViewTextBoxColumn.HeaderText = "DataBit";
            this.dataBitDataGridViewTextBoxColumn.Name = "dataBitDataGridViewTextBoxColumn";
            this.dataBitDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // flowControlDataGridViewTextBoxColumn
            // 
            this.flowControlDataGridViewTextBoxColumn.DataPropertyName = "FlowControl";
            this.flowControlDataGridViewTextBoxColumn.HeaderText = "FlowControl";
            this.flowControlDataGridViewTextBoxColumn.Name = "flowControlDataGridViewTextBoxColumn";
            this.flowControlDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // activeDataGridViewTextBoxColumn
            // 
            this.activeDataGridViewTextBoxColumn.DataPropertyName = "Active";
            this.activeDataGridViewTextBoxColumn.HeaderText = "Active";
            this.activeDataGridViewTextBoxColumn.Name = "activeDataGridViewTextBoxColumn";
            this.activeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // mitinstrumentsBindingSource
            // 
            this.mitinstrumentsBindingSource.DataSource = typeof(DataModel.mi_tinstruments);
            // 
            // MachineRegistration
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.ClientSize = new System.Drawing.Size(897, 422);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.grdMachines);
            this.Name = "MachineRegistration";
            this.Text = "Instrument Registration";
            this.Activated += new System.EventHandler(this.MDIChild_Activated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MDIChild_Closing);
            this.Load += new System.EventHandler(this.TestAttributes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdMachines)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mitinstrumentsBindingSource)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion


        
		private void MDIChild_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			//Destroy the corresponding Tabpage when closing MDI child form
			this.tabPag.Dispose();

			//If no Tabpage left
			if (!tabCtrl.HasChildren)
			{
				tabCtrl.Visible = false;
			}
		}

		private void MDIChild_Activated(object sender, System.EventArgs e)
		{
			//Activate the corresponding Tabpage
			tabCtrl.SelectedTab = tabPag;

			if (!tabCtrl.Visible)
			{
				tabCtrl.Visible = true;
			}
		}

		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("www.codeproject.com");
		}

        private void TestAttributes_Load(object sender, EventArgs e)
        {
            SetDefaults();
           
            BindInstrumentsToGrid();
            
        }

        private void BindInstrumentsToGrid()
        {
            var machines= _unitOfWork.InstrumentsRepository.GetAll();
            grdMachines.DataSource = machines;
        }

        

        

        private void cmbTests_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            //MessageBox.Show(cmbTests.SelectedValue.ToString());
        }

        

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                mi_tinstruments _instrument = new mi_tinstruments
                {
                    Acknowledgement_code = txtAckChar.Text,
                    Active = chkActive.Checked ? "Y" : "N",
                    BaudRate = Convert.ToInt32(cmbBaudRate.Text.Trim()),
                    Bidirectional = "Y",
                    ClientID = System.Configuration.ConfigurationSettings.AppSettings["BranchID"].ToString().Trim(),
                    Communication_method = cmbCommMethod.Text.Trim(),
                    Communication_Stnadard = cmbStandard.Text.Trim(),
                    DataBit = cmbDataBits.Text.Trim(),
                    EnteredBy = 1,
                    EnteredOn = System.DateTime.Now,
                    FlowControl = cmbFlowControl.Text.Trim(),
                    Instrument_Name = txtName.Text.Trim(),
                    Model = txtModel.Text.Trim(),
                    Parity = cmbParity.Text.Trim(),
                    PORT = cmbPort.Text.Trim(),
                    Stopbit = cmbStopBits.Text.Trim(),
                    RecordTerminator=txtRecordTerminator.Text.Trim(),
                    ParsingAlgorithm=1
                };
                _unitOfWork.InstrumentsRepository.Insert(_instrument);
                _unitOfWork.Save();
                BindInstrumentsToGrid();
            }

        }

        private bool ValidateForm()
        {
            
            return true;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {

           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clsBLMain objMai = new clsBLMain();
            DataView dv = objMai.GetAll(7);
            if(dv.Count>0)
            {
                List<cliqresults> lstresults = new List<cliqresults>();
                for (int i = 0; i < dv.Count; i++)
                {
                    lstresults.Add(new cliqresults
                    {
                        BookingID = dv[i]["BookingID"].ToString().Trim(),
                        ClientID = dv[i]["ClientID"].ToString().Trim(),
                        CliqAttributeID = dv[i]["CliqAttributeID"].ToString().Trim(),
                        CliqTestID = dv[i]["CliqTestID"].ToString().Trim(),
                        MachineID = dv[i]["MachineID"].ToString().Trim(),
                        Result = dv[i]["Result"].ToString().Trim()
                    });
                }
                


                var jsonSerialiser = new JavaScriptSerializer();
                
                try
                {
                    var json = jsonSerialiser.Serialize(lstresults);
                    if (!System.IO.File.Exists("E:\\TreesJSON.json"))
                        System.IO.File.Create("E:\\TreesJSON.json");
                    System.IO.File.WriteAllText("E:\\TreesJSON.json", json);
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }

                
            }
        }

        private void grdMachines_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                LoadMachineTestsScreen(Int32.Parse(senderGrid.Rows[e.RowIndex].Cells["InstrumentID"].Value.ToString().Trim()));
            }

        }

        private void LoadMachineTestsScreen(int InstrumentID)
        {
            MDIParent paren = MDIParent.Instance;
            paren.LoadAttributesForm(InstrumentID);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SetDefaults();
        }

        private void SetDefaults()
        {
            cmbCommMethod.SelectedIndex = 0;
            
            cmbBaudRate.SelectedIndex = 1;
            cmbDataBits.SelectedIndex = 1;
            cmbFlowControl.SelectedIndex = 0;
            cmbParity.SelectedIndex = 0;
            cmbPort.SelectedIndex = 0;
            cmbStandard.SelectedIndex = 0;
            cmbStopBits.SelectedIndex = 1;
        }

	}
}
