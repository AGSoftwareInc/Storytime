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
using MoonAPNS;

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
            var payload1 = new NotificationPayload("3c293a8bc2b5ee95b2d49f058a930574e7e41b7076c6a3129192407af77c5c59", "Happy Vishu!", 1, "default");
            //payload1.AddCustom("CustomKey", "CustomValue");
            var notificationList = new List<NotificationPayload> { payload1 };
            var push = new PushNotification(true, ConfigurationManager.AppSettings["CertPath"], ConfigurationManager.AppSettings["CertPassword"]);
            var rejected = push.SendToApple(notificationList);
        }

        protected override void OnStop()
        {
        }
    }
}
