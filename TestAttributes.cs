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

	public class TestAttributes : System.Windows.Forms.Form
    {
        private IContainer components;
		private TabControl tabCtrl;
        private ComboBox cmbTests;
        private Label label1;
        private Label label2;
        private ComboBox cmbAttributes;
        private Label label3;
        private TextBox textBox1;
        private DataGridView dataGridView1;
        private Panel panel1;
        private Button btnReset;
        private Button btnSave;
        private TabPage tabPag;
        private BindingSource cliqmachinemappingBindingSource;
        private CheckBox chkActive;
        private ComboBox cmbInstruments;
        private Label label4;
        private BindingSource mitinstrumentsBindingSource;
        private BindingSource cliqmachinemappingBindingSource1;
        private ErrorProvider errorProvider1;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn branchIDDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn testIDDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn testNameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn cliqAttributeIDDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn attributeNameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn machineAttributeCodeDataGridViewTextBoxColumn;
        private DataGridViewCheckBoxColumn activeDataGridViewCheckBoxColumn;
        private BindingSource cliqmachinemappingBindingSource2;
        private Button button1;

        private readonly UnitOfWork _unitOfWork;
        private BindingSource cliqtestsandattributeBindingSource;
        private int _MachineID;
		public TestAttributes()
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
        public TestAttributes(int machineID)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            this._MachineID = machineID;
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
            this.cmbTests = new System.Windows.Forms.ComboBox();
            this.cliqtestsandattributeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cliqmachinemappingBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbAttributes = new System.Windows.Forms.ComboBox();
            this.cliqmachinemappingBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.branchIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.testIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.testNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cliqAttributeIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.attributeNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.machineAttributeCodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.activeDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cliqmachinemappingBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.cmbInstruments = new System.Windows.Forms.ComboBox();
            this.mitinstrumentsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.cliqtestsandattributeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cliqmachinemappingBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cliqmachinemappingBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cliqmachinemappingBindingSource2)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mitinstrumentsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbTests
            // 
            this.cmbTests.DataSource = this.cliqtestsandattributeBindingSource;
            this.cmbTests.DisplayMember = "test_name";
            this.cmbTests.FormattingEnabled = true;
            this.cmbTests.Location = new System.Drawing.Point(191, 12);
            this.cmbTests.Name = "cmbTests";
            this.cmbTests.Size = new System.Drawing.Size(158, 21);
            this.cmbTests.TabIndex = 0;
            this.cmbTests.ValueMember = "test_id";
            this.cmbTests.SelectionChangeCommitted += new System.EventHandler(this.cmbTests_SelectionChangeCommitted);
            // 
            // cliqtestsandattributeBindingSource
            // 
            this.cliqtestsandattributeBindingSource.DataSource = typeof(DataModel.cliqtestsandattribute);
            // 
            // cliqmachinemappingBindingSource1
            // 
            this.cliqmachinemappingBindingSource1.DataSource = typeof(DataModel.cliqmachinemapping);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.label1.Location = new System.Drawing.Point(399, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Cliq Attribute:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Cliq Test:";
            // 
            // cmbAttributes
            // 
            this.cmbAttributes.DataSource = this.cliqtestsandattributeBindingSource;
            this.cmbAttributes.DisplayMember = "att_name";
            this.cmbAttributes.FormattingEnabled = true;
            this.cmbAttributes.Location = new System.Drawing.Point(503, 12);
            this.cmbAttributes.Name = "cmbAttributes";
            this.cmbAttributes.Size = new System.Drawing.Size(158, 21);
            this.cmbAttributes.TabIndex = 3;
            this.cmbAttributes.ValueMember = "att_id";
            // 
            // cliqmachinemappingBindingSource
            // 
            this.cliqmachinemappingBindingSource.DataSource = typeof(DataModel.cliqmachinemapping);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Machine Test Code:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(191, 44);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(158, 20);
            this.textBox1.TabIndex = 6;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.branchIDDataGridViewTextBoxColumn,
            this.testIDDataGridViewTextBoxColumn,
            this.testNameDataGridViewTextBoxColumn,
            this.cliqAttributeIDDataGridViewTextBoxColumn,
            this.attributeNameDataGridViewTextBoxColumn,
            this.machineAttributeCodeDataGridViewTextBoxColumn,
            this.activeDataGridViewCheckBoxColumn});
            this.dataGridView1.DataSource = this.cliqmachinemappingBindingSource2;
            this.dataGridView1.Location = new System.Drawing.Point(4, 118);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(750, 196);
            this.dataGridView1.TabIndex = 7;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
            this.idDataGridViewTextBoxColumn.HeaderText = "id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.Visible = false;
            // 
            // branchIDDataGridViewTextBoxColumn
            // 
            this.branchIDDataGridViewTextBoxColumn.DataPropertyName = "BranchID";
            this.branchIDDataGridViewTextBoxColumn.HeaderText = "BranchID";
            this.branchIDDataGridViewTextBoxColumn.Name = "branchIDDataGridViewTextBoxColumn";
            this.branchIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // testIDDataGridViewTextBoxColumn
            // 
            this.testIDDataGridViewTextBoxColumn.DataPropertyName = "Test_ID";
            this.testIDDataGridViewTextBoxColumn.HeaderText = "Test_ID";
            this.testIDDataGridViewTextBoxColumn.Name = "testIDDataGridViewTextBoxColumn";
            this.testIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // testNameDataGridViewTextBoxColumn
            // 
            this.testNameDataGridViewTextBoxColumn.DataPropertyName = "TestName";
            this.testNameDataGridViewTextBoxColumn.HeaderText = "TestName";
            this.testNameDataGridViewTextBoxColumn.Name = "testNameDataGridViewTextBoxColumn";
            // 
            // cliqAttributeIDDataGridViewTextBoxColumn
            // 
            this.cliqAttributeIDDataGridViewTextBoxColumn.DataPropertyName = "CliqAttributeID";
            this.cliqAttributeIDDataGridViewTextBoxColumn.HeaderText = "CliqAttributeID";
            this.cliqAttributeIDDataGridViewTextBoxColumn.Name = "cliqAttributeIDDataGridViewTextBoxColumn";
            this.cliqAttributeIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // attributeNameDataGridViewTextBoxColumn
            // 
            this.attributeNameDataGridViewTextBoxColumn.DataPropertyName = "AttributeName";
            this.attributeNameDataGridViewTextBoxColumn.HeaderText = "AttributeName";
            this.attributeNameDataGridViewTextBoxColumn.Name = "attributeNameDataGridViewTextBoxColumn";
            // 
            // machineAttributeCodeDataGridViewTextBoxColumn
            // 
            this.machineAttributeCodeDataGridViewTextBoxColumn.DataPropertyName = "MachineAttributeCode";
            this.machineAttributeCodeDataGridViewTextBoxColumn.HeaderText = "MachineAttributeCode";
            this.machineAttributeCodeDataGridViewTextBoxColumn.Name = "machineAttributeCodeDataGridViewTextBoxColumn";
            // 
            // activeDataGridViewCheckBoxColumn
            // 
            this.activeDataGridViewCheckBoxColumn.DataPropertyName = "Active";
            this.activeDataGridViewCheckBoxColumn.HeaderText = "Active";
            this.activeDataGridViewCheckBoxColumn.Name = "activeDataGridViewCheckBoxColumn";
            // 
            // cliqmachinemappingBindingSource2
            // 
            this.cliqmachinemappingBindingSource2.DataSource = typeof(DataModel.cliqmachinemapping);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.cmbInstruments);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.chkActive);
            this.panel1.Controls.Add(this.btnReset);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.cmbAttributes);
            this.panel1.Controls.Add(this.cmbTests);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(750, 100);
            this.panel1.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(35, 72);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbInstruments
            // 
            this.cmbInstruments.DataSource = this.mitinstrumentsBindingSource;
            this.cmbInstruments.DisplayMember = "Instrument_Name";
            this.cmbInstruments.FormattingEnabled = true;
            this.cmbInstruments.Location = new System.Drawing.Point(503, 39);
            this.cmbInstruments.Name = "cmbInstruments";
            this.cmbInstruments.Size = new System.Drawing.Size(158, 21);
            this.cmbInstruments.TabIndex = 11;
            this.cmbInstruments.ValueMember = "InstrumentID";
            // 
            // mitinstrumentsBindingSource
            // 
            this.mitinstrumentsBindingSource.DataSource = typeof(DataModel.mi_tinstruments);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.label4.Location = new System.Drawing.Point(399, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Machine:";
            // 
            // chkActive
            // 
            this.chkActive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkActive.AutoSize = true;
            this.chkActive.Checked = true;
            this.chkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActive.Location = new System.Drawing.Point(441, 78);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(56, 17);
            this.chkActive.TabIndex = 9;
            this.chkActive.Text = "Active";
            this.chkActive.UseVisualStyleBackColor = true;
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReset.Location = new System.Drawing.Point(586, 74);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 8;
            this.btnReset.Text = "&Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(503, 74);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // TestAttributes
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.ClientSize = new System.Drawing.Size(756, 326);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "TestAttributes";
            this.Text = "TestAttributes";
            this.Activated += new System.EventHandler(this.MDIChild_Activated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MDIChild_Closing);
            this.Load += new System.EventHandler(this.TestAttributes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cliqtestsandattributeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cliqmachinemappingBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cliqmachinemappingBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cliqmachinemappingBindingSource2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mitinstrumentsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
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
            BindCliqTestsToComboBox();
            BindInstrumentsToComboBox();
            BindTestsToGrid();
            
        }

        private void BindTestsToGrid()
        {
            var tests = _unitOfWork.CliqMachineMappings.GetMany(x => x.Active == true);
            if (this._MachineID > 0)
            {
                tests = tests.Where(x => x.MachineID == this._MachineID);
            }
           dataGridView1.DataSource = tests.ToList();
        }

        private void BindInstrumentsToComboBox()
        {
            var tests = _unitOfWork.InstrumentsRepository.GetMany(x => x.Active=="Y");//.Select(y => new {TestID=y.,TestName=y.TestName });
            if (this._MachineID > 0)
            {
                tests = tests.Where(x => x.InstrumentID == this._MachineID);
            }

            cmbInstruments.DataSource = tests.ToList();
        }

        
        private void BindCliqTestsToComboBox()
        {
            var tests = _unitOfWork.CliqTestAndAttributesRepository.GetAll().DistinctBy(x=>x.test_id);//.Select(y => new {TestID=y.,TestName=y.TestName });
            //if (this._MachineID > 0)
            //{
            //    tests = tests.Where(x => x.MachineID == _MachineID);
            //}
            
            cmbTests.DataSource = tests.ToList() ;
         
        }

        private void cmbTests_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            //MessageBox.Show(cmbTests.SelectedValue.ToString());
        }

        private void cmbTests_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cliqtestsandattribute obj= cmbTests.SelectedItem as cliqtestsandattribute;
            if (obj != null)
            {
                cmbAttributes.DataSource = _unitOfWork.CliqTestAndAttributesRepository.GetMany(x => x.test_id == obj.test_id);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                cliqmachinemapping obj = _unitOfWork.CliqMachineMappings.GetSingle(x => x.MachineID == Convert.ToInt32(cmbInstruments.SelectedValue) && x.CliqAttributeID == Convert.ToInt32(cmbAttributes.SelectedValue));
                if (obj != null)
                {
                    obj.MachineID = Convert.ToInt32(cmbInstruments.SelectedValue);
                    obj.MachineAttributeCode = textBox1.Text.Trim();
                    obj.Active = chkActive.Checked;

                    _unitOfWork.CliqMachineMappings.UpdateCurrentContext(obj);
                }
                else
                {
                    obj = new cliqmachinemapping
                    {

                        MachineID = Convert.ToInt32(cmbInstruments.SelectedValue),
                        AttributeName = cmbAttributes.Text,
                        BranchID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["BranchID"].ToString().Trim()),
                        CliqAttributeID = Convert.ToInt32(cmbAttributes.SelectedValue),
                        MachineAttributeCode = textBox1.Text,
                        Test_ID = Convert.ToInt32(cmbTests.SelectedValue),
                        TestName = cmbTests.Text,
                        Active = chkActive.Checked,
                    };
                    _unitOfWork.CliqMachineMappings.Insert(obj);
                }
                //
                //var chekthisattrib = _unitOfWork.CliqMachineMappings.GetSingle(x => x.MachineID == obj.MachineID && x.CliqAttributeID == obj.CliqAttributeID);
                //if (chekthisattrib != null)
                //{
                //    obj.id = chekthisattrib.id;
                //    chekthisattrib = obj;
                //    _unitOfWork.CliqMachineMappings.Update(chekthisattrib);
                //}
                //else
                //    _unitOfWork.CliqMachineMappings.Insert(obj);
                _unitOfWork.Save();
                BindTestsToGrid();
                
               // cliqmachinemapping obj=new cliqmachinemapping{
               //     Active=chkActive.Checked,
               //     MachineAttributeCode=textBox1.Text,
               //     MachineID=cmbInstruments.
               // }
            }
        }

        private bool ValidateForm()
        {
            if (cmbTests.SelectedValue.ToString().Trim()=="")
            {
                MessageBox.Show("All mandatory fields not completed");
                errorProvider1.SetError(cmbTests, "Please Select");
                return false;
            }
            if (cmbAttributes.SelectedValue.ToString().Trim()=="")
            {
                MessageBox.Show("All mandatory fields not completed");
                errorProvider1.SetError(cmbAttributes, "Please Select");
                return false;
            }
            if (textBox1.Text.Trim().Length < 1)
            {
                MessageBox.Show("All mandatory fields not completed");
                errorProvider1.SetError(textBox1, "Please Enter");
                return false;
 
            }
            if (cmbInstruments.SelectedValue.ToString().Trim() == "")
            {
                MessageBox.Show("All mandatory fields not completed");
                errorProvider1.SetError(cmbInstruments, "Please Select");
                return false;

            }
            return true;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {

            cmbAttributes.DataSource = null;
            cmbTests.SelectedIndex = 0;
            textBox1.Text = "";
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            clsBLMain objMai = new clsBLMain();
            DataView dv = objMai.GetAll(7);
            if(dv.Count>0)
            {
                List<cliqresults> lstresults = new List<cliqresults>();
                for (int i = 0; i < dv.Count; i++)
                {
                    try
                    {
                        lstresults.Add(new cliqresults
                        {
                            ResultID = Convert.ToInt32(dv[i]["ResultID"].ToString().Trim()),
                            BookingID = dv[i]["BookingID"].ToString().Trim(),
                            ClientID = dv[i]["ClientID"].ToString().Trim(),
                            CliqAttributeID = dv[i]["CliqAttributeID"].ToString().Trim(),
                            CliqTestID = dv[i]["CliqTestID"].ToString().Trim(),
                            MachineID = dv[i]["MachineID"].ToString().Trim(),
                            Result = dv[i]["Result"].ToString().Trim(),
                            MachineAttributeCode = dv[i]["MachineAttributeCode"].ToString().Trim()

                        });
                    }
                    catch (Exception ee)
                    {
 
                    }
                }
                


               // var jsonSerialiser = new JavaScriptSerializer();
                
                try
                {
                    //var json = jsonSerialiser.Serialize(lstresults.Select(x => new { BookingID=x.BookingID,ClientID=x.ClientID,CliqAttributeID=x.CliqAttributeID,CliqTestID=x.CliqTestID,MachineID=x.MachineID,Result=x.Result}).ToList());
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(lstresults.Select(x => new { BookingID = x.BookingID, ClientID = x.ClientID, CliqAttributeID = x.CliqAttributeID, CliqTestID = x.CliqTestID, MachineID = x.MachineID, Result = x.Result }).ToList());
                    
                    //if (!System.IO.File.Exists("E:\\TreesJSON.json"))
                    //    System.IO.File.Create("E:\\TreesJSON.json");
                    //System.IO.File.WriteAllText("E:\\TreesJSON.json", json);
                    var content = await Helper.CallCliqApi("http://192.168.22.16:818/ricapi/site/curl_data?str=" + json.ToString().Trim());
                    
                    if (content.Length > 0)
                    {
                        var cliqresultresponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CliqResultResponse>>(content);
                        if (cliqresultresponse != null && (cliqresultresponse.FirstOrDefault().Code=="3"|| cliqresultresponse.FirstOrDefault().Code=="1"))
                        {
                            foreach (var result in lstresults)
                            {
                                mi_tresult res = new mi_tresult
                                {
                                    ResultID = result.ResultID,
                                    Status = "Y",
                                    senton = System.DateTime.Now,
                                    BookingID=result.BookingID,
                                    machinename=result.MachineID,
                                    Result=result.Result,
                                    AttributeID=result.MachineAttributeCode,
                                    EnteredBy=1,
                                    EnteredOn=System.DateTime.Now,
                                    ClientID=System.Configuration.ConfigurationManager.AppSettings["BranchID"].ToString().Trim(),
                                  //  AttributeID=result.Attribut
                                    
                                    sentto = "http://192.168.22.16:818/ricapi/site/curl_data"
                                };
                                _unitOfWork.ResultsRepository.Update(res,x=>Convert.ToInt32(x.ResultID));
                            }
                            _unitOfWork.Save();
                        }
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }

                
            }
        }

	}
}
