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
        private const string newstory = "You have been invited to participate in a new Story: ";
        private const string newvote = "A new vote has been cast!";
        private const string serieswinner = "Series winner:";
        private const string nowinner = "There were no winners for series ";
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
            NotifySeriesWinner();
            NotifyStoryWinner();
        }

        private void NotifyNewStory()
        {
            var db = new PetaPoco.Database("AGSoftware");
            var db2 = new PetaPoco.Database("AGSoftware");
            var db3 = new PetaPoco.Database("AGSoftware");

            foreach (var a in db.Query<Entities.StorytimeUserList>("Select * From StorytimeUserList Where UserNotified = 0"))
            {
                var b = db2.SingleOrDefault<Entities.AspNetUsers>("Select * from AspNetUsers Where Id = @0", a.UserId);
                var storytime = db2.SingleOrDefault<Entities.Storytime>("Select * from Storytime Where StorytimeId = @0", a.StorytimeId);

                if (b.DeviceToken != null)
                {
                    CreatePushNotification(newstory + storytime.StorytimeTitle, b.DeviceToken);
                }

                a.UserNotified = true;
                db2.Update(a);
            }

            foreach (var c in db.Query<Entities.StorytimeGroup>("Select * from StorytimeGroup Where UsersNotified = 0"))
            {
                var d = db2.SingleOrDefault<Entities.UserGroup>("Select * from UserGroup Where UserGroupId = @0", c.UserGroupId);
                var storytime = db2.SingleOrDefault<Entities.Storytime>("Select * from Storytime Where StorytimeId = @0", c.StorytimeId);

                foreach (var e in db3.Query<Entities.UserGroupUser>("Select * from UserGroupUser Where UserGroupId = @0", d.UserGroupId))
                {
                    var f = db2.SingleOrDefault<Entities.AspNetUsers>("Select * from AspNetUsers Where Id = @0", e.UserId);

                    if (f != null && f.DeviceToken != null)
                    {
                        CreatePushNotification(newstory + storytime.StorytimeTitle, f.DeviceToken);
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
                                CreatePushNotification(newvote, g.DeviceToken);
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
                            CreatePushNotification(newvote, i.DeviceToken);
                        }
                    }
                }

                a.UserNotified = true;
                db2.Update(a);
            }
        }

        private void NotifySeriesWinner()
        {
            var db = new PetaPoco.Database("AGSoftware");
            var db2 = new PetaPoco.Database("AGSoftware");
            var db3 = new PetaPoco.Database("AGSoftware");
            var db4 = new PetaPoco.Database("AGSoftware");

            foreach (var a in db.Query<Entities.SeriesWinner>("Select ss.StorytimeSeriesId, s.StorytimeTypeId, s.StorytimeId, ss.SeriesText, u.Username, sp1.Votes From StorytimeSeries ss inner join Storytime s on s.StorytimeId = ss.StorytimeId inner join AspNetUsers u on ss.UserId = u.Id inner join StorytimePost sp1 on sp1.SeriesId = ss.StorytimeSeriesId And sp1.Votes = (Select MAX(sp2.Votes) From StorytimePost sp2 Where sp2.StorytimeId = sp1.StorytimeId) Where ss.UsersNotified = 0 And ss.DateCreated < DateAdd(hh, -24, GetDate())"))
            {
                if (a.StorytimeTypeId == 1)
                {
                    var b = db2.SingleOrDefault<Entities.StorytimeGroup>("Select * from StorytimeGroup Where StorytimeId = @0", a.StorytimeId);
                    var c = db2.SingleOrDefault<Entities.UserGroup>("Select * from UserGroup Where UserGroupId = @0", b.UserGroupId);

                    foreach (var d in db3.Query<Entities.UserGroupUser>("Select * From UserGroupUser Where UserGroupId = @0", c.UserGroupId))
                    {
                        if (d != null)
                        {
                            var e = db4.SingleOrDefault<Entities.AspNetUsers>("Select * from AspNetUsers Where Id = @0", d.UserId);

                            if (e != null && e.DeviceToken != null)
                            {
                                string notificationtext = string.Empty;
                                if (a.Votes > 0)
                                    notificationtext = serieswinner + " " + a.Username + " has won the series " + a.SeriesText + " with " + a.Votes + " votes!";
                                else
                                    notificationtext = nowinner + a.SeriesText;
                                CreatePushNotification(notificationtext, e.DeviceToken);
                            }
                        }
                    }
                }
                else if (a.StorytimeTypeId == 2)
                {
                    foreach (var f in db3.Query<Entities.StorytimeUserList>("Select * from StorytimeUserList Where StorytimeId = @0", a.StorytimeId))
                    {
                        var g = db4.SingleOrDefault<Entities.AspNetUsers>("Select * from AspNetUsers Where Id = @0", f.UserId);

                        if (g != null && g.DeviceToken != null)
                        {
                            string notificationtext = string.Empty;
                            if (a.Votes > 0)
                                notificationtext = serieswinner + " " + a.Username + " has won the series " + a.SeriesText + " with " + a.Votes + " votes!";
                            else
                                notificationtext = nowinner + a.SeriesText;
                            CreatePushNotification(notificationtext, g.DeviceToken);
                        }
                    }
                }

                var storytimeseries = db4.SingleOrDefault<Entities.StorytimeSeries>("Select * from StorytimeSeries Where StorytimeSeriesId = @0", a.StorytimeSeriesId);

                storytimeseries.UsersNotified = true;
                db4.Update(storytimeseries);

            }
        }

        private void NotifyStoryWinner()
        {}

        private void CreatePushNotification(string notificationtext, string devicetoken)
        {
            var payload1 = new NotificationPayload(devicetoken, notificationtext, 1, "default");
            var notificationList = new List<NotificationPayload> { payload1 };
            var push = new PushNotification(true, CertPath, CertPassword);
            var rejected = push.SendToApple(notificationList);
        }
    }
}