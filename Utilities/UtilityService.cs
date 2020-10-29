using MedismartsAPI.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MedismartsAPI.Utilities
{
    public partial class ErrorLog
    {
        public int Id { get; set; }
        public string MethodName { get; set; }
        public string ControllerName { get; set; }
        public string Path { get; set; }
        public string Method { get; set; }
        public string RequestBody { get; set; }
        public string ErrorMessage { get; set; }
        public string FileName { get; set; }
        public string LineNumber { get; set; }
        public string CodeLine { get; set; }
        public DateTime? DateOccur { get; set; }
    }
    public interface IUtilityService
    {
        Response GetResponse(string statusCode, string description, dynamic responseData);
        void LogError(string content);
        void LogInfo(string content);
        void LogToDB(ErrorLog errorLog);
        void LogError(Exception es);
        string GetContentToLog(HttpRequest request);
    }

    public class UtilityService : IUtilityService
    {
        private IDbRepo<ErrorLog> errorLogRepo;

        public UtilityService()
        {
            errorLogRepo = new DbRepo<ErrorLog>();
        }

        public Response GetResponse(string statusCode, string description, dynamic responseData)
        {
            return new Response()
            {
                message = statusCode == "00" ? "success" : "failed",
                statusCode = statusCode,
                description = description,
                responseData = responseData
            };
        }

        private void WriteToFile(string fileName, string content)
        {
            // string generatedFileName  = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + "-" + DateTime.Now.Ticks + ".ilog";
            // string filepath  = @"C:\Users\raimi.aliu\Desktop\road to principal programmer\asp_net_core\logs\"+generatedFileName;
            if (File.Exists(fileName))
            {
                FileStream fsopen = new FileStream(fileName, FileMode.Append, FileAccess.Write);
                using (StreamWriter ss = new StreamWriter(fsopen))
                {
                    string newline = "\n";
                    ss.WriteLine(newline);
                    ss.Write(content);
                }

            }
            else
            {

                using (FileStream fs = new FileStream(fileName, FileMode.CreateNew, FileAccess.ReadWrite))
                {
                    //File.WriteAllText(filepath, content);
                    using (StreamWriter ss = new StreamWriter(fs))
                    {
                        ss.WriteLine(content);
                    }

                }
            }

        }

        public void LogError(string content)
        {
            try
            {
                string generatedFileName = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + "-" + DateTime.Now.Hour + $"_{DateTime.Now.Minute}_{DateTime.Now.Second}" + DateTime.Now.Ticks + ".ilog";
                //string filepath = @"C:\Users\raimi.aliu\Desktop\road to principal programmer\asp_net_core\logs\" + generatedFileName;
                Directory.SetCurrentDirectory(".");
                string filepath = Path.Combine(Directory.GetCurrentDirectory(), $"Logs/ErrorLogs/{generatedFileName}");
                WriteToFile(filepath, "ERROR:{{\t\t" + content + "\t\t}}");
            }
            catch (Exception es)
            {
                throw es;
            }


            #region fgh
            /*
            if(File.Exists(filepath)) {
                FileStream fsopen = new FileStream(filepath, FileMode.Open, FileAccess.ReadWrite);
                using(StreamWriter ss = new StreamWriter(fsopen)) {
                        string newline = "\n";
                        ss.WriteLine(newline);
                        ss.Write(content);
                }
                               
            }
            else {
                
                using(FileStream fs = new FileStream(filepath, FileMode.CreateNew, FileAccess.ReadWrite)) {
                    //File.WriteAllText(filepath, content);
                    using(StreamWriter ss = new StreamWriter(fs)) {
                        ss.WriteLine(content);
                    }
                
                }
            }
            */
            #endregion fgh

        }

        public void LogInfo(string content)
        {
            try
            {
                // Directory.SetCurrentDirectory(".");
                string filepath = Path.Combine(Directory.GetCurrentDirectory(), $"Logs/ActivityLog.ilog");
                WriteToFile(filepath, "INFO:{{\t\t"+content+"\t\t}}");
            }
            catch (Exception es)
            {
                throw es;
            }

        }

        public void LogToDB(ErrorLog errorLog)
        {
            try
            {
                errorLogRepo._repo.AddNew(errorLog);
                errorLogRepo.SaveChanges();
            }
            catch (Exception es)
            {
                throw es;
            }
        }

        public void LogError(Exception es)
        {
            LogError(es.Message);
        }

        public string GetContentToLog(HttpRequest request)
        {
            try
            {
                string details = "";
                if (request.Method == "GET")
                {
                    details = $"Method - {request.Method} \t, Path-{request.Path}\t, Date-{DateTime.Now.ToString()}";
                }
                else
                {
                    using (var reader = new StreamReader(request.Body))
                    {
                        var body = reader.ReadToEndAsync().Result;
                        details = $"Method - {request.Method} \t, Path-{request.Path}\t, Date-{DateTime.Now.ToString()} \t-Body={body}";

                        // Do something
                    }

                }


                return details;
            }
            catch (Exception es)
            {
                throw es;
            }
        }
    }
}
