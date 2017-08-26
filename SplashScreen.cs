using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsApplication5
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        private void LoadDataFromCliqBackGround_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = Calculate(sender as BackgroundWorker, e);
        }

        private long Calculate(BackgroundWorker instance, DoWorkEventArgs e)
        {
            for (int i = 0; i <= 100; i++)
            {
                if (instance.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    System.Threading.Thread.Sleep(10);
                    instance.ReportProgress(i);
                }
            }
            return 0L;
        }

        private void LoadDataFromCliqBackGround_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label1.Text = e.ProgressPercentage.ToString() + " % completed.";
        }

        private void LoadDataFromCliqBackGround_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                label1.Text = "Done";
                System.Threading.Thread.Sleep(1000);
            //    this.FormClosed += OnSPlashScreenClosed;
                this.Hide();
                MDIParent HomeScreen = new MDIParent();
                HomeScreen.ShowDialog();
                this.Close();
                
            }
            else
            {
                
            }
        }

        private void OnSPlashScreenClosed(object sender,FormClosedEventArgs e)
        {
            
        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            LoadDataFromCliqBackGround.RunWorkerAsync();
        }
    }
}
