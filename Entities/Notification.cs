using MoonAPNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushNotificationService
{
    public class Notification
    {
        private const string newstory = "You have been invited to participate in a new Story!";
        private string CertPath = "";
        private string CertPassword = "";

        public Notification (string certpath, string certpassword)
        {
            this.CertPath = certpath;
            this.CertPassword = certpassword;
        }

        public void Notify()
        {
            NotifyNewStory();
        }

        private void NotifyNewStory()
        {
            var db = new PetaPoco.Database("AGSoftware");
            var db2 = new PetaPoco.Database("AGSoftware");
            var db3 = new PetaPoco.Database("AGSoftware");

            foreach (var a in db.Query<Entities.StorytimeUserList>("Select * From StorytimeUserList Where UserNotified = 0"))
            {
                var b = db2.SingleOrDefault<Entities.AspNetUsers>("Select * from AspNetUsers Where Id = @0", a.UserId);

                if (b.DeviceToken != null)
                {
                    var payload1 = new NotificationPayload(b.DeviceToken, newstory, 1, "default");
                    var notificationList = new List<NotificationPayload> { payload1 };
                    var push = new PushNotification(true, CertPath, CertPassword);
                    var rejected = push.SendToApple(notificationList);
                }

                a.UserNotified = true;
                db2.Update(a);
            }

            foreach (var c in db.Query<Entities.StorytimeGroup>("Select * from StorytimeGroup Where UsersNotified = 0"))
            {
                var d = db2.SingleOrDefault<Entities.UserGroup>("Select * from UserGroup Where UserGroupId = @0", c.UserGroupId);

                foreach (var e in db3.Query<Entities.UserGroupUser>("Select * from UserGroupUser Where UserGroupId = @0", d.UserGroupId))
                {
                    var f = db2.SingleOrDefault<Entities.AspNetUsers>("Select * from AspNetUsers Where Id = @0", e.UserId);

                    if (f.DeviceToken != null)
                    {
                        var payload1 = new NotificationPayload(f.DeviceToken, newstory, 1, "default");
                        var notificationList = new List<NotificationPayload> { payload1 };
                        var push = new PushNotification(true, CertPath, CertPassword);
                        var rejected = push.SendToApple(notificationList);
                    }
                }

                c.UsersNotified = true;
                db3.Update(c);
            }
        }
    }
}
