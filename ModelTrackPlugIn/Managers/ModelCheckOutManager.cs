using ModelTrackPlugIn.Helpers.Commands;
using ModelTrackPlugIn.Interfaces;
using ModelTrackPlugIn.ModelClasses;
using ModelTrackPlugIn.Helpers.DataAccess;
using System;
using System.IO;
using System.Windows.Forms;
using ModelTrackPlugIn.Helpers.Extensions;

namespace ModelTrackPlugIn.Managers
{

    public class ModelCheckOutManager
    {
        public Commands PMillCommands;

        private string ProgrammerName { get; set; }
        private IModel TrackedModel { get; set; }

        public ModelCheckOutManager()
        {
            PMillCommands = new Commands();
            GetProgrammerName();
        }

        private void GetProgrammerName()
        {
            ProgrammerName = PMillCommands.GetProgrammerName();
        }

        internal void BuildProgrammerModelProfile(string modelName)
        {
            TrackedModel = new ProjectModel(modelName)
            {
                ModelFileImportDate = PMillCommands.GetImportTime(modelName).FormatTime(),
                FilePath = PMillCommands.GetFilePath(modelName)
            };

        }

        public bool CompareModelDates()
        {
            DateTime modelInDirectory = GetCreationDateTime();

            int compareDates = DateTime.Compare(modelInDirectory, TrackedModel.ModelFileImportDate);

            //>0 = t2 earlier than t1
            if (compareDates > 0)
            {
                //newer model exists
                return true;
            }
            //imported model is newest
            return false;
        }

        private DateTime GetCreationDateTime()
        {

            if (TrackedModel != null)
            {
                DateTime modelInTTJobs = new DateTime();

                try
                {

                    string dirPath = Path.GetDirectoryName(TrackedModel.FilePath);

                    var filePaths =
                        Directory.EnumerateFiles(dirPath, "*", SearchOption.TopDirectoryOnly);
                    foreach (var fileName in filePaths)
                    {
                        if (fileName.Contains(TrackedModel.FullName))
                        {
                            modelInTTJobs = File.GetCreationTime(TrackedModel.FilePath);
                            break;
                        }
                    }

                    return modelInTTJobs;
                }
                catch (Exception ex)
                {

                    MessageBox.Show($"{ex.Message}\n please check if newer model exists in TT Jobs");

                    return DateTime.MinValue;
                }
            }
            else
            {
                throw new Exception("Programmer Model Is Null\nGetCreationDateTime called before programmer model built");
            }
        }

        public void CheckOutModel()
        {
            if (TrackedModel != null)
            {

                SqlAccess sqlAccess = new SqlAccess();

                sqlAccess.EnterData(ProgrammerName, TrackedModel.FullName);
            }
        }
    }
}
