using Autodesk.ProductInterface.PowerMILL;

namespace ModelTrackPlugIn.Helpers.Commands
{
    public class Commands
    {
        private PMAutomation PMill;
        public Commands()
        {
            PMill = new PMAutomation(Autodesk.ProductInterface.InstanceReuse.UseExistingInstance, Modes.WithoutGui);
        }

        public string GetProgrammerName()
        {
            string programmerName = (string)PMill.ExecuteEx("print $project.Programmer");
            return programmerName;
        }

        /// <summary>
        /// Gets the creation time in unix offset from the model
        /// </summary>
        /// <param name="fullModelName"></param>
        /// <returns></returns>
        public string GetImportTime(string fullModelName)
        {
            return PMill.GetPowerMillEntityParameter("model", fullModelName, "ImportTime");
        }

        public string GetFilePath(string modelName)
        {
            return PMill.ActiveProject.Models[modelName].Path;
        }
    }
}
