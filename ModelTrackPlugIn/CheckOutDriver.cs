using ModelTrackPlugIn.Managers;
using ModelTrackPlugIn.ModelClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModelTrackPlugIn
{
    class CheckOutDriver
    {
        BackgroundWorker BGWorker;
        ModelContainers ProjectModels;

        public CheckOutDriver(ModelContainers projectModels)
        {
            projectModels.OnQueueModified += HandleQueueModifiedEvent;
        }

        private void HandleQueueModifiedEvent(object sender, EventArgs e)
        {
            if (BGWorker == null)
            {
                BGWorker = new BackgroundWorker();
            }

            if (!BGWorker.IsBusy)
            {
                BGWorker.RunWorkerAsync();
                BGWorker.DoWork += new DoWorkEventHandler(Worker_DoWork);
            }
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                ModelCheckOutManager mCheckOutMngr = new ModelCheckOutManager();

                while (ProjectModels.Count() > 0)
                {
                    string modelName = ProjectModels.Dequeue();

                    ProjectModels.AddModel(modelName);

                    mCheckOutMngr.BuildProgrammerModelProfile(modelName);

                    if (mCheckOutMngr.CompareModelDates())
                    {
                        MessageBox.Show($"Newer Version of {modelName} Exists");

                    }
                    else
                    {
                        mCheckOutMngr.SignOutModelInExcel(); //needs to be sql
                    }

                    ProjectModels.RemoveModel(modelName);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

                BGWorker.Dispose();
            }
        }
    }
}
