using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLayer;
using BusinessEntities;
using DataModel;
using System.Threading.Tasks;
namespace WindowsApplication5
{
    public partial class SplashScreen : Form
    {
        private readonly UnitOfWork _unitofwork;
        public SplashScreen()
        {
            InitializeComponent();
            _unitofwork = new UnitOfWork();
        }

        private async void LoadDataFromCliqBackGround_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = Calculate(sender as BackgroundWorker, e);
        }

        private long Calculate(BackgroundWorker instance, DoWorkEventArgs e)
        {

            if (instance.CancellationPending)
            {
                e.Cancel = true;

            }
            else
            {
                for (int i = 0; i <= 30; i++)
                {
                    System.Threading.Thread.Sleep(10);
                    instance.ReportProgress(i);

                }
                string content = Helper.CallCliqApi("http://olivecliq.com/ricapi/site/TestAtt/bid/2/did/13").Result;
                for (int i = 31; i <= 70; i++)
                {
                    System.Threading.Thread.Sleep(10);
                    instance.ReportProgress(i);

                }

                if (content.Length > 0)
                {
                    var lsttestsfromcliq = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CliqTestsEntity>>(content);
                    var localtestsandattributes = _unitofwork.CliqTestAndAttributesRepository.GetAll();
                    var different = from s in lsttestsfromcliq where !localtestsandattributes.Any(es => es.test_id == s.test_id) select s;
                    for (int i = 71; i <= 90; i++)
                    {
                        System.Threading.Thread.Sleep(10);
                        instance.ReportProgress(i);
                    }

                    if (different.Count() > 0)
                    {
                        foreach (var testatt in different)
                        {
                            var attrinfo=new List<CliqAttributeEntity>();
                            if(testatt.attrubute_info!=null)
                               attrinfo = testatt.attrubute_info.Where(x => x.att_name.ToLower().Trim() != "comment" && x.att_name.ToLower().Trim() != "opinion").ToList();
                            if (attrinfo != null && attrinfo.Count > 0)
                            {
                                foreach (var attr in attrinfo)
                                {
                                        var objCliqtestandattributelocal = new cliqtestsandattribute
                                        {
                                            test_id = testatt.test_id,
                                            test_name = testatt.test_name,
                                            department_id = testatt.department_id,
                                            att_id = attr.att_id,
                                            att_name = attr.att_name
                                        };
                                        _unitofwork.CliqTestAndAttributesRepository.Insert(objCliqtestandattributelocal);
                                    
                                }
                            }
                        }
                        _unitofwork.Save();
                    }
                }



                // System.Threading.Thread.Sleep(2000);
                instance.ReportProgress(100);
            }

            return 0L;
        }

        private void LoadDataFromCliqBackGround_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            if (e.ProgressPercentage >= 30 && e.ProgressPercentage < 70)
            {
                label1.Text = "Loading data from Cliq server. Please wait";
            }
            else if (e.ProgressPercentage >= 70 && e.ProgressPercentage < 90)
            {
                label1.Text = "Comparing local data";
            }
            else if (e.ProgressPercentage >= 90 && e.ProgressPercentage < 100)
            {
                label1.Text = "Inserting the differneces in local DB";
            }
            else if (e.ProgressPercentage == 100)
            {
                label1.Text = "Done";
            }

            //label1.Text = e.UserState.ToString();// +" % completed.";
        }

        private void LoadDataFromCliqBackGround_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                label1.Text = "Done";
                // System.Threading.Thread.Sleep(1000);
                //    this.FormClosed += OnSPlashScreenClosed;
                this.Hide();
                MDIParent HomeScreen = MDIParent.Instance;
                HomeScreen.ShowDialog();
                this.Close();

            }
            else
            {

            }
        }

        private void OnSPlashScreenClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            LoadDataFromCliqBackGround.RunWorkerAsync();
        }
    }
}
