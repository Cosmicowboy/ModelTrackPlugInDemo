using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModelTrackPlugIn.ModelClasses
{
    class ModelContainers
    {
        private Queue<string> ModelsToProcess = new Queue<string>();
        public List<string> ProblemModels = new List<string>();
        public event EventHandler OnQueueModified;

        private void QueueModified()
        {
            OnQueueModified?.Invoke(this, EventArgs.Empty);
        }

        //Add verification tailored to client
        public void Enqueue(string modelName)
        {
           // if( !modelName.Verify()) return;

            ModelsToProcess.Enqueue(modelName);
            QueueModified();
        }

        public string Dequeue()
        {
            string ModelNameToProcess = ModelsToProcess.Dequeue();
            return ModelNameToProcess;
        }

        public int Count()
        {
            return ModelsToProcess.Count();
        }

        public string Peek()
        {
            return ModelsToProcess.Peek();
        }

        /// <summary>
        /// Wraped to ensure proper handling 
        /// Seems pointless for demo 
        /// </summary>
        /// <param name="currentModel"></param>
        public bool AddModel(string currentModel)
        {
            //verification/ error handling
            try
            {
                ProblemModels.Add(currentModel);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            
        }

        public bool RemoveModel(string currentModel)
        {
            //verification/ error handling
            try
            {
                ProblemModels.Remove(currentModel);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }
    }
}
