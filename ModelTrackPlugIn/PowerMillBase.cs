using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Delcam.Plugins.Framework;
using Delcam.Plugins.Events;
using PowerMILL;
using ModelTrackPlugIn.ModelClasses;


namespace ModelTrackPlugIn
{
    [Guid("8ED76887-F9E8-4DEA-A5FA-72F33232C596")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class PowerMillBase : PluginFrameworkWithoutPanes
    {
        private string PMToken;
        private PowerMILL.PluginServices PluginServices;
        private int ParentWindow;

        ModelContainers PMillModels = new ModelContainers();
        CheckOutDriver ModelCheckOutDriver;

        public EventSubscription TrackModels;
        public EventSubscription ProjectClosing;

        #region PowerMill init variables
        public override string PluginName => TranslationUtils.Translate("Model Track Demo");

        public override string PluginAuthor => "Branden Morningstar";

        public override string PluginDescription => TranslationUtils.Translate("Demo to show how tracking models using events works");

        public override string PluginIconPath => throw new NotImplementedException();

        public override Version PluginVersion => new Version(1, 0, 0);

        public override Version PowerMILLVersion => new Version(25, 0, 1);

        public override bool PluginHasOptions => false;

        public override string PluginAssemblyName => "Tracker Demo";

        public override Guid PluginGuid => new Guid("8ED76887-F9E8-4DEA-A5FA-72F33232C596");
        #endregion

        public override void Initialise(string Token, PowerMILL.PluginServices pServices, int ParentWindow_hwnd)
        {

            base.Initialise(Token, pServices, ParentWindow_hwnd);

            ModelCheckOutDriver = new CheckOutDriver(PMillModels);

            PMToken = Token;
            PluginServices = pServices;
            ParentWindow = ParentWindow_hwnd;

            TrackModels = new Delcam.Plugins.Events.EventSubscription("EntityCreated", "EntityType", "Model", PMEntityCreated);
            ProjectClosing = new Delcam.Plugins.Events.EventSubscription("ProjectClosing", ProblemModels);
            EventUtils.Subscribe(TrackModels);
        }


        private void PMEntityCreated(string eventName, Dictionary<string, string> eventArguments)
        {
            string entityType = eventArguments["EntityType"];
            string entityName = eventArguments["Name"];

            if (entityType == "Model")
            {
                PMillModels.Enqueue(entityName);
            }
        }

        private void ProblemModels(string event_name, Dictionary<string, string> event_arguments)
        {
            if (PMillModels.ProblemModels.Count > 0)
            {
                string problemModelNames = string.Empty;

                foreach (var modelName in PMillModels.ProblemModels)
                {
                    problemModelNames += "\n" + modelName;
                }
                MessageBox.Show("Models not checked out:\n" + problemModelNames);
            }
        }
    }
}
