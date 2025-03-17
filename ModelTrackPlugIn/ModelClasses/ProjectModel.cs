using ModelTrackPlugIn.Interfaces;
using System;

namespace ModelTrackPlugIn.ModelClasses
{
    class ProjectModel : IModel
    {
        public string FullName { get; }

        public string ModelDesignDate { get; set; }
        public string FilePath { get; set; }
        public DateTime ModelFileImportDate { get; set; }

        public ProjectModel(string powerMillModelName)
        {
            FullName = powerMillModelName;
        }


    }
}
