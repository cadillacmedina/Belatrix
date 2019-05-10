using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace JobLogger
{
    public class JobLogger
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public JobLogger()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="con"></param>
        /// <param name="logToFile"></param>
        /// <param name="logToConsole"></param>
        /// <param name="logToDatabase"></param>
        /// <returns></returns>
        public bool GenerateLog(ContentLog con, bool logToFile, bool logToConsole, bool logToDatabase)
        {
            bool flag = false;
            string contText = con.ContText;
            int idContent = con.IdContent;
            try
            {
                if (!logToFile && !logToConsole && !logToDatabase)
                    throw new Exception("Invalid configuration");
                if (logToFile)
                    flag = this.JobLoggerFile(contText, idContent);
                if (logToConsole)
                    flag = this.JobLoggerConsole(contText, idContent);
                if (logToDatabase)
                    flag = this.JobLoggerBD(contText, idContent);
                return flag;
            }
            catch
            {
                return false;
            }
            finally
            {
            }
        }

        /// <summary>
        /// This method will use to write a LOG in a DB.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="typeMessage"></param>
        /// <returns></returns>
        private bool JobLoggerBD(string message, int typeMessage)
        {
            #region Variables
            SqlConnection sqlConnection = null;
            SqlCommand command = null;
            string dbConnection = "";
            string commandInsert = "";
            #endregion
            try
            {
                //Stablish a connection with SQL.
                dbConnection = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString.ToString();
                sqlConnection = new SqlConnection(dbConnection);
                sqlConnection.Open();
                commandInsert = ConfigurationManager.AppSettings["CommandInsert"].ToString().Replace("##message##", "'"+message+"'").Replace("##idTypeMessage##", typeMessage.ToString());
                //Execute a command of insert in DB.
                command = new SqlCommand(commandInsert);
                command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                sqlConnection.Dispose();
                sqlConnection.Close();
                return false;
            }
            finally
            {
                //Close connection in DB.
                sqlConnection.Dispose();
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// This method will use to write a LOG in Console.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="typeMessage"></param>
        /// <returns></returns>
        private bool JobLoggerConsole(string message, int typeMessage)
        {
            try
            {
                //to decide the color of a Letter depending of Type of Message.
                switch (typeMessage)
                {
                    case 1:
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                }
                //Write in a line in Console the log.
                Console.WriteLine(DateTime.Now.ToShortDateString() + message);
                System.Diagnostics.Debug.WriteLine(DateTime.Now.ToShortDateString() + message);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
            finally
            {
            }
        }

        /// <summary>
        /// This method will use to write a LOG in a text File.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="typeMessage"></param>
        /// <returns></returns>
        private bool JobLoggerFile(string message, int typeMessage)
        {
            #region Variables
            string content = "";
            DateTime now = DateTime.Now;
            #endregion
            try
            {
                string directory = ConfigurationManager.AppSettings["LogFileDirectory"];
                string path = (directory + "LogFile" + now.ToShortDateString().ToString() + ".txt").Replace("\\", "\\").Replace("/", "");
                if (File.Exists(path))
                {
                    content = File.ReadAllText(path);
                    File.Delete(path);
                }
                object[] objArray = new object[6];
                objArray[0] = (object)content;
                now = DateTime.Now;
                objArray[1] = (object)now.ToShortDateString();
                objArray[2] = (object)" Type of Message: ";
                objArray[3] = (object)typeMessage;
                objArray[4] = (object)", ";
                objArray[5] = (object)message;
                string str3 = string.Concat(objArray);
                File.AppendAllText(path, str3 + Environment.NewLine);
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
            finally
            {
            }
        }
    }
}