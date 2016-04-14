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
using System.Threading.Tasks;

namespace PushNotificationService
{
    public partial class iOSPushNotificationService : ServiceBase
    {
        public iOSPushNotificationService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Notification notification = new Notification(ConfigurationManager.AppSettings["CertPath"], ConfigurationManager.AppSettings["CertPassword"]);
            notification.Notify();
        }

        protected override void OnStop()
        {
        }
    }
}
