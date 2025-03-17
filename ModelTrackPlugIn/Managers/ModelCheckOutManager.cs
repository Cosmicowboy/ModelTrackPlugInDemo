using ModelTrackPlugIn.Helpers.Commands;
using ModelTrackPlugIn.Interfaces;
using ModelTrackPlugIn.ModelClasses;
using System;

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
            TrackedModel = new ProjectModel(modelName);

            PMillCommands.GetImportTime(modelName);

            PMillCommands.GetFilePath(modelName);
        }

        public bool CompareModelDates()
        {
            DateTime modelInTTJobs = GetCreationDateTime();

            int compareDates = DateTime.Compare(modelInTTJobs, ProgrammerModel.ModelFileImportDate);

            //>0 = t2 earlier than t1
            if (compareDates > 0)
            {
                //newer model exists
                return true;
            }
            //imported model is newest
            return false;
        }
    }

}
