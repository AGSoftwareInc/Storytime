using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PushNotificationService
{
    public partial class iOSPushNotificationService : ServiceBase
    {
        private Thread thread;

        public iOSPushNotificationService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            thread = new Thread(WorkerThreadFunc);
            thread.Name = "My Worker Thread";
            thread.IsBackground = true;
            thread.Start();
        }

        private void WorkerThreadFunc()
        {
            while (1 == 1)
            {
                try
                {
                    bool useapplesandbox;

                    if (ConfigurationManager.AppSettings["UseAppleSandbox"] == "true")
                        useapplesandbox = true;
                    else
                        useapplesandbox = false;

                    Notification notification = new Notification(ConfigurationManager.AppSettings["CertPath"], ConfigurationManager.AppSettings["CertPassword"], useapplesandbox);
                notification.Notify();
                    }
                catch (System.Exception ex)
                { }
                finally { 
                Thread.Sleep(30000);
                }
            }
        }

        protected override void OnStop()
        {
        }
    }
}
