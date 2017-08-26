using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

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
		private System.Windows.Forms.MenuItem newMenuItem;
		private System.Windows.Forms.MenuItem exitMenuItem;
		private System.Windows.Forms.MenuItem cascadeMenuItem;
		private System.Windows.Forms.MenuItem horizonMenuItem;
		private System.Windows.Forms.MenuItem verticalMenuItem;
		//int childCount = 1;
        TestAttributes _testAttributeForm;
		public MDIParent()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
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
               // _testAttributeForm.Dock = DockStyle.Fill; 
                _testAttributeForm.FormClosed+=_testAttributeForm_FormClosed;
            }
            else
                _testAttributeForm.Activate();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

        private void _testAttributeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _testAttributeForm = null;
        }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
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
            this.newMenuItem = new System.Windows.Forms.MenuItem();
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
            this.newMenuItem,
            this.menuItem5,
            this.exitMenuItem});
            this.fileMenuItem.Text = "&File";
            // 
            // newMenuItem
            // 
            this.newMenuItem.Index = 0;
            this.newMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
            this.newMenuItem.Text = "&New";
            this.newMenuItem.Click += new System.EventHandler(this.NewMenuItem_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 1;
            this.menuItem5.Text = "-";
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Index = 2;
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
			foreach (TestAttributes childForm in this.MdiChildren) 
			{
				//Check for its corresponding MDI child form
				if (childForm.TabPag.Equals(tabControl1.SelectedTab)) 
				{
					//Activate the MDI child form
					childForm.Select();
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
	}
}
