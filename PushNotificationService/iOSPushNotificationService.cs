using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            var payload1 = new NotificationPayload("a961e67e650a5eb5610027e346d78c4df263ef8f", "Hello !", 1, "default");
            //payload1.AddCustom("CustomKey", "CustomValue");
            var notificationList = new List<NotificationPayload> { payload1 };
            var push = new PushNotification(false, "C:\\OpenSSL\\bin\\aps_development.cer", "test");
            var rejected = push.SendToApple(notificationList);
        }

        protected override void OnStop()
        {
        }
    }
}
