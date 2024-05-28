using Framework.EnterpriseLibrary.Adapters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace SBL.ETL.FileUploader.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private static IConfiguration _configuration;

        private System.Timers.Timer timer;
        public static DateTime ExecutionTime;
        public static int SleepTimeExecution;
        public static TimeSpan TimeSpanExecution;

        public Worker(ILogger<Worker> logger,IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;

            timer = new System.Timers.Timer();
            timer.Enabled = true;
            timer.Interval = Convert.ToInt32(_configuration["ExecutionThreadTime"].ToString()) * 1000;
            timer.Elapsed += new ElapsedEventHandler(ServiceTimer_Tick);
        }        

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return;
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }

        private void ServiceTimer_Tick(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            ExecutionTime = Convert.ToDateTime(_configuration["ExecutionTime"].ToString());
            while (true)
            {
                try
                {
                    TimeSpanExecution = new TimeSpan(ExecutionTime.Ticks - Convert.ToDateTime(DateTime.Now.ToString("HH:mm tt")).Ticks);
                    
                    SleepTimeExecution = (((Convert.ToInt32(TimeSpanExecution.Hours.ToString()) * 60) * 60) + (Convert.ToInt32(TimeSpanExecution.Minutes.ToString()) * 60)) * 1000;

                    if (SleepTimeExecution <= 0)
                        SleepTimeExecution = 86400000 + SleepTimeExecution;

                    LoggingAdapter.WriteLog(Environment.NewLine + "Process SleepTime:" + SleepTimeExecution + " Time " + System.DateTime.Now.ToString());

                    Thread ThSftpFile = new Thread(Process);
                    Thread.Sleep(SleepTimeExecution);
                    ThSftpFile.Start();
                    Thread.Sleep(600000);

                }
                catch (Exception ex)
                {
                    LoggingAdapter.WriteLog("ServiceTimer_Tick:" + "Stack trace:" + ex.StackTrace + Environment.NewLine + "Exception:" + ex.Message);
                    throw ex;
                }
            }
        }

        static void Process()
        {
            //int ScheduledDay = Convert.ToInt32(ConfigurationManager.AppSettings["ExecutionDay"]);
            //bool flag = DateTime.Now.Day.Equals(ScheduledDay);
            //if (flag)
            //{

            string FQCN = _configuration["FQCN"].ToString();
            string MethodName = _configuration["MethodName"].ToString();
            string IsSFTPUpload = _configuration["IsSFTPUpload"].ToString();

            LoggingAdapter.WriteLog("Process Started");
            object returnObject = null;
            MethodInfo mi = null;
            ConstructorInfo ci = null;
            object responder = null;
            Type type = null;
            try
            {
                type = Type.GetType(FQCN);
                mi = type.GetMethod(MethodName);
                ci = type.GetConstructor(Type.EmptyTypes);
                responder = ci.Invoke(null);
                returnObject = mi.Invoke(responder, null);
                if (IsSFTPUpload.Equals("Y"))
                {
                    UploadFileToSFTP();
                }
                Thread.Sleep(30000);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Process:" + "Stack trace:" + ex.StackTrace + Environment.NewLine + "Exception:" + ex.Message);
                throw ex;
            }
            //}
        }

        public static void UploadFileToSFTP()
        {
            //run batch file to upload file to SFTP
            RunBatFile(_configuration["SftpFileUpload"].ToString());
        }

        public static void RunBatFile(string FilePathInfo)
        {
            try
            {
                string[] FileInfo = FilePathInfo.Split(',');
                Process p = null;
                string targetDir;
                targetDir = FileInfo[0];
                p = new Process();
                LoggingAdapter.WriteLog("RunBatFile:" + "FilePath:" + FilePathInfo);
                p.StartInfo.WorkingDirectory = targetDir;
                p.StartInfo.FileName = FileInfo[1];
                p.StartInfo.Arguments = string.Format("ETL File Generator Process");
                p.StartInfo.CreateNoWindow = true;
                if (p.Start())
                {
                    p.WaitForExit(10000);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog(Environment.NewLine + "Error Messages:RunBatFile " + ex.Message);
            }
        }

    }
}
