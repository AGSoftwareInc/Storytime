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
        private const string newvote = "A new vote has been cast!";
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
            NotifyVote();
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

                    if (f != null && f.DeviceToken != null)
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

        private void NotifyVote()
        {
            var db = new PetaPoco.Database("AGSoftware");
            var db2 = new PetaPoco.Database("AGSoftware");
            var db3 = new PetaPoco.Database("AGSoftware");
            var db4 = new PetaPoco.Database("AGSoftware");

            foreach (var a in db.Query<Entities.Vote>("Select * From Vote v Where v.UserNotified = 0"))
            {
                var b = db2.SingleOrDefault<Entities.StorytimePost>("Select * from Storytimepost Where StorytimePostId = @0", a.StorytimePostId);
                var c = db2.SingleOrDefault<Entities.Storytime>("Select * from Storytime Where StorytimeId = @0", b.StorytimeId);
                var d = db2.SingleOrDefault<Entities.StorytimeGroup>("Select * from StorytimeGroup Where StorytimeId = @0", c.StorytimeId);
                
                if (c.StorytimeTypeId == 1)
                {
                    var e = db2.SingleOrDefault<Entities.UserGroup>("Select * from UserGroup Where UserGroupId = @0", d.UserGroupId);

                    foreach(var f in db3.Query<Entities.UserGroupUser>("Select * From UserGroupUser Where UserGroupId = @0", e.UserGroupId))
                    {
                        if (f != null)
                        {
                            var g = db4.SingleOrDefault<Entities.AspNetUsers>("Select * from AspNetUsers Where Id = @0", f.UserId);

                            if (g != null && g.DeviceToken != null)
                            {
                                string notificationtext = newvote + " " + g.UserName + " has voted for " + b.PostText;
                                var payload1 = new NotificationPayload(g.DeviceToken, notificationtext, 1, "default");
                                var notificationList = new List<NotificationPayload> { payload1 };
                                var push = new PushNotification(true, CertPath, CertPassword);
                                var rejected = push.SendToApple(notificationList);
                            }
                        }
                    }
                }
                else if(c.StorytimeTypeId == 2)
                {
                    foreach (var h in db3.Query<Entities.StorytimeUserList>("Select * from StorytimeUserList Where StorytimeId = @0", c.StorytimeId))
                    {
                        var i = db4.SingleOrDefault<Entities.AspNetUsers>("Select * from AspNetUsers Where Id = @0", h.UserId);

                        if (i != null && i.DeviceToken != null)
                        {
                            string notificationtext = newvote + " " + i.UserName + " has voted for " + b.PostText;
                            var payload1 = new NotificationPayload(i.DeviceToken, notificationtext, 1, "default");
                            var notificationList = new List<NotificationPayload> { payload1 };
                            var push = new PushNotification(true, CertPath, CertPassword);
                            var rejected = push.SendToApple(notificationList);
                        }
                    }
                }

                a.UserNotified = true;
                db2.Update(a);
            }
        }
    }
}