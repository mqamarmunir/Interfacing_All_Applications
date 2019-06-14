using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using WindowsApplication5.CommForms;

namespace WindowsApplication5
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class MDIParent : System.Windows.Forms.Form
    {
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem5;
        private IContainer components;
        private System.Windows.Forms.MenuItem fileMenuItem;
        private System.Windows.Forms.MenuItem winMenuItem;
        private System.Windows.Forms.MenuItem exitMenuItem;
        private System.Windows.Forms.MenuItem cascadeMenuItem;
        private System.Windows.Forms.MenuItem horizonMenuItem;
        private System.Windows.Forms.MenuItem verticalMenuItem;
        private MenuItem menuItem1;
        private static MDIParent instance=new MDIParent();
        public static MDIParent Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MDIParent();
                }
                return instance;
            }
        }
        //int childCount = 1;
        TestAttributes _testAttributeForm;
        private MenuItem menuSerialPort;
        private MenuItem menuTCPClient;
        private MenuItem menuTCPServer;
        MachineRegistration _machineRegistration;
        TCPServer _TCPServer;
        SerialCommunication _SerialCommunicationForm;
        private MDIParent()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();


            if (_machineRegistration == null)
            {
                _machineRegistration = new MachineRegistration();
                //childForm.Text = "MDIChild " + childCount.ToString();
                _machineRegistration.MdiParent = this;

                _machineRegistration.TabCtrl = tabControl1;
                TabPage tp = new TabPage();
                tp.Parent = tabControl1;
                tp.Text = _machineRegistration.Text;
                tp.Show();

                //child Form will now hold a reference value to a tabpage
                _machineRegistration.TabPag = tp;
                _machineRegistration.WindowState = FormWindowState.Maximized;
                //Activate the MDI child form
                _machineRegistration.Show();
                tabControl1.SelectedTab = tp;
                // _testAttributeForm.Dock = DockStyle.Fill; 
                _machineRegistration.FormClosed += _machineRegistration_FormClosed;
            }
            else
                _machineRegistration.Activate();

            //if (_testAttributeForm == null)
            //{
            //    _testAttributeForm = new TestAttributes();
            //    //childForm.Text = "MDIChild " + childCount.ToString();
            //    _testAttributeForm.MdiParent = this;

            //    _testAttributeForm.TabCtrl = tabControl1;
            //    TabPage tp = new TabPage();
            //    tp.Parent = tabControl1;
            //    tp.Text = _testAttributeForm.Text;
            //    tp.Show();

            //    //child Form will now hold a reference value to a tabpage
            //    _testAttributeForm.TabPag = tp;
            //    _testAttributeForm.WindowState = FormWindowState.Maximized;
            //    //Activate the MDI child form
            //    _testAttributeForm.Show();
            //    tabControl1.SelectedTab = tp;
            //   // _testAttributeForm.Dock = DockStyle.Fill; 
            //    _testAttributeForm.FormClosed+=_testAttributeForm_FormClosed;
            //}
            //else
            //    _testAttributeForm.Activate();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        private void _machineRegistration_FormClosed(object sender, FormClosedEventArgs e)
        {
            _machineRegistration = null;
        }

        private void _testAttributeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _testAttributeForm = null;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.fileMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuTCPClient = new System.Windows.Forms.MenuItem();
            this.menuTCPServer = new System.Windows.Forms.MenuItem();
            this.menuSerialPort = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.exitMenuItem = new System.Windows.Forms.MenuItem();
            this.winMenuItem = new System.Windows.Forms.MenuItem();
            this.cascadeMenuItem = new System.Windows.Forms.MenuItem();
            this.horizonMenuItem = new System.Windows.Forms.MenuItem();
            this.verticalMenuItem = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(836, 24);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControl1.TabIndex = 1;
            this.tabControl1.Visible = false;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.fileMenuItem,
            this.winMenuItem});
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.Index = 0;
            this.fileMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuTCPClient,
            this.menuTCPServer,
            this.menuSerialPort,
            this.menuItem5,
            this.exitMenuItem});
            this.fileMenuItem.Text = "&File";
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.Text = "&Register Instrument";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuTCPClient
            // 
            this.menuTCPClient.Index = 1;
            this.menuTCPClient.Text = "Start TCP Client";
            this.menuTCPClient.Click += new System.EventHandler(this.menuTCPClient_Click);
            // 
            // menuTCPServer
            // 
            this.menuTCPServer.Index = 2;
            this.menuTCPServer.Text = "Start TCP Server";
            this.menuTCPServer.Click += new System.EventHandler(this.menuTCPServer_Click);
            // 
            // menuSerialPort
            // 
            this.menuSerialPort.Index = 3;
            this.menuSerialPort.Text = "Listen On COM Ports";
            this.menuSerialPort.Click += new System.EventHandler(this.menuSerialPort_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 4;
            this.menuItem5.Text = "-";
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Index = 5;
            this.exitMenuItem.Text = "E&xit";
            this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
            // 
            // winMenuItem
            // 
            this.winMenuItem.Index = 1;
            this.winMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.cascadeMenuItem,
            this.horizonMenuItem,
            this.verticalMenuItem});
            this.winMenuItem.Text = "&Window";
            // 
            // cascadeMenuItem
            // 
            this.cascadeMenuItem.Index = 0;
            this.cascadeMenuItem.Text = "&Cascade";
            this.cascadeMenuItem.Click += new System.EventHandler(this.cascadeMenuItem_Click);
            // 
            // horizonMenuItem
            // 
            this.horizonMenuItem.Index = 1;
            this.horizonMenuItem.Text = "Tile &Horizontal";
            this.horizonMenuItem.Click += new System.EventHandler(this.horizonMenuItem_Click);
            // 
            // verticalMenuItem
            // 
            this.verticalMenuItem.Index = 2;
            this.verticalMenuItem.Text = "Tile &Vertical";
            this.verticalMenuItem.Click += new System.EventHandler(this.verticalMenuItem_Click);
            // 
            // MDIParent
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(836, 429);
            this.Controls.Add(this.tabControl1);
            this.IsMdiContainer = true;
            this.Menu = this.mainMenu1;
            this.Name = "MDIParent";
            this.Text = "MDI Parent Form";
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new SplashScreen());


        }
        private void NewMenuItem_Click(object sender, System.EventArgs e)
        {
            //Creating MDI child form and initialize its fields
            if (_testAttributeForm == null)
            {
                _testAttributeForm = new TestAttributes();
                //childForm.Text = "MDIChild " + childCount.ToString();
                _testAttributeForm.MdiParent = this;

                _testAttributeForm.TabCtrl = tabControl1;
                TabPage tp = new TabPage();
                tp.Parent = tabControl1;
                tp.Text = _testAttributeForm.Text;
                tp.Show();

                //child Form will now hold a reference value to a tabpage
                _testAttributeForm.TabPag = tp;
                _testAttributeForm.WindowState = FormWindowState.Maximized;
                //Activate the MDI child form
                _testAttributeForm.Show();
                tabControl1.SelectedTab = tp;
            }

            //child Form will now hold a reference value to the tab control


            //Add a Tabpage and enables it

            //childCount++;

            //Activate the newly created Tabpage

        }

        private void exitMenuItem_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            foreach (Form childForm in this.MdiChildren)
            {
                //Check for its corresponding MDI child form
                if (tabControl1.SelectedTab!=null && childForm.Text.Equals(tabControl1.SelectedTab.Text))
                {
                    //Activate the MDI child form
                    childForm.Select();
                    break;
                }
            }
        }

        private void cascadeMenuItem_Click(object sender, System.EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void horizonMenuItem_Click(object sender, System.EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void verticalMenuItem_Click(object sender, System.EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            if (_machineRegistration == null)
            {
                _machineRegistration = new MachineRegistration();
                //childForm.Text = "MDIChild " + childCount.ToString();
                _machineRegistration.MdiParent = this;

                _machineRegistration.TabCtrl = tabControl1;
                TabPage tp = new TabPage();
                tp.Parent = tabControl1;
                tp.Text = _machineRegistration.Text;
                tp.Show();

                //child Form will now hold a reference value to a tabpage
                _machineRegistration.TabPag = tp;
                _machineRegistration.WindowState = FormWindowState.Maximized;
                //Activate the MDI child form
                _machineRegistration.Show();
                tabControl1.SelectedTab = tp;
                // _testAttributeForm.Dock = DockStyle.Fill; 
                _machineRegistration.FormClosed += _machineRegistration_FormClosed;
            }
            else
                _machineRegistration.Activate();
        }

        internal void LoadAttributesForm(int InstrumentID)
        {
            if (_testAttributeForm == null)
            {
                _testAttributeForm = new TestAttributes(InstrumentID);
                //childForm.Text = "MDIChild " + childCount.ToString();
                _testAttributeForm.MdiParent = this;

                _testAttributeForm.TabCtrl = tabControl1;
                TabPage tp = new TabPage();
                tp.Parent = tabControl1;
                tp.Text = _testAttributeForm.Text;
                tp.Show();

                //child Form will now hold a reference value to a tabpage
                _testAttributeForm.TabPag = tp;
                _testAttributeForm.WindowState = FormWindowState.Maximized;
                //Activate the MDI child form
                _testAttributeForm.Show();
                tabControl1.SelectedTab = tp;
            }
            else
            {
                _testAttributeForm.Close();
                _testAttributeForm = null;
                LoadAttributesForm(InstrumentID);
            }
        }

       
        private void menuTCPClient_Click(object sender, EventArgs e)
        {

        }

        private void menuTCPServer_Click(object sender, EventArgs e)
        {
            if (_TCPServer == null)
            {
                _TCPServer = new TCPServer();
                //childForm.Text = "MDIChild " + childCount.ToString();
                _TCPServer.MdiParent = this;

                _TCPServer.TabCtrl = tabControl1;
                TabPage tp = new TabPage();
                tp.Parent = tabControl1;
                tp.Text = _TCPServer.Text;
                tp.Show();

                //child Form will now hold a reference value to a tabpage
                _TCPServer.TabPag = tp;
                _TCPServer.WindowState = FormWindowState.Maximized;
                //Activate the MDI child form
                _TCPServer.Show();
                tabControl1.SelectedTab = tp;
                // _testAttributeForm.Dock = DockStyle.Fill; 
                _TCPServer.FormClosed += _TCPServer_FormClosed; ;
            }
            else
                _TCPServer.Activate();
        }

        private void _TCPServer_FormClosed(object sender, FormClosedEventArgs e)
        {
            _TCPServer.m_StopThread = true;
           
            _TCPServer = null;
            

        }

        private void menuSerialPort_Click(object sender, EventArgs e)
        {
            if (_SerialCommunicationForm == null)
            {
                _SerialCommunicationForm = new SerialCommunication();
                //childForm.Text = "MDIChild " + childCount.ToString();
                _SerialCommunicationForm.MdiParent = this;

                _SerialCommunicationForm.TabCtrl = tabControl1;
                TabPage tp = new TabPage();
                tp.Parent = tabControl1;
                tp.Text = _SerialCommunicationForm.Text;
                tp.Show();

                //child Form will now hold a reference value to a tabpage
                _SerialCommunicationForm.TabPag = tp;
                _SerialCommunicationForm.WindowState = FormWindowState.Maximized;
                //Activate the MDI child form
                _SerialCommunicationForm.Show();
                tabControl1.SelectedTab = tp;
                // _testAttributeForm.Dock = DockStyle.Fill; 
                _SerialCommunicationForm.FormClosed += _SerialCommunicationForm_FormClosed;
            }
            else
                _SerialCommunicationForm.Activate();
        }

        private void _SerialCommunicationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
           // _SerialCommunicationForm.Dispose();
            _SerialCommunicationForm = null;
        }

       
    }
}
