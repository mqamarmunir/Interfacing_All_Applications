using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;

namespace WindowsApplication5
{
	/// <summary>
	/// Summary description for MDIChild.
	/// </summary>
	/// 

	public class MDIChild : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private TabControl tabCtrl;
		private TabPage tabPag;

		public MDIChild()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

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
			// 
			// MDIChild
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.ClientSize = new System.Drawing.Size(292, 266);
			this.Name = "MDIChild";
			this.Text = "MDIChild";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.MDIChild_Closing);
			this.Activated += new System.EventHandler(this.MDIChild_Activated);

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

	}
}
