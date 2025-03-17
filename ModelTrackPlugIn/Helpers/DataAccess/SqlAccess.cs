using Microsoft.Data.Sqlite;
using ModelTrackPlugIn.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModelTrackPlugIn.Helpers.DataAccess
{
    class SqlAccess
    {

        public void EnterData(string programmer, string modelName)
        {
            StreamWriter sw = new StreamWriter(@"C:\Users\1234\Desktop\Errors.txt");
            try
            {
                

                using (var connection =
                    new SqliteConnection("Data Source = C:\\Users\\1234\\Desktop\\ModelSignOutDemo.db"))
                {
                    var command = connection.CreateCommand();

                    command.CommandText =
                        @"
                        INSERT INTO Tracking_Data (Date,User_Name,File_Name) VALUES(@Today, @Programmer, @Model)
                        
                    ";
                    command.Parameters.AddWithValue("@Today", DateTime.Now.ToString("d"));
                    command.Parameters.AddWithValue("@Programmer", programmer);
                    command.Parameters.AddWithValue("@Model", modelName);
                    connection.Open();

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                sw.WriteLine(ex.ToString());
                sw.Close();
                MessageBox.Show(ex.Message);
            }
        }
    }
}
