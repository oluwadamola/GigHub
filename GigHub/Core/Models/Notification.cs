using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Core.Models
{
    public class Notification
    {
        public int NotificationId { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime DateTime { get; private set; }
        public DateTime? OriginalDateTime { get; private set; }
        public string OriginalVanue { get; private set; }

        [Required]
        public Gig Gig { get; private set; }

        public Notification()
        {
            
        }

        private Notification(NotificationType type, Gig gig)
        {
            if(gig == null) throw new ArgumentNullException(nameof(gig));

            Type = type;
            Gig = gig;
            DateTime = DateTime.Now;
        }

        public static Notification GigCreated(Gig gig)
        {
            return new Notification(NotificationType.GigCreated, gig);
        }

        public static Notification GigUpdated(Gig newGig, DateTime originalDateTime, string originalVenue)
        {
            var notification = new Notification(NotificationType.GigUpdated, newGig);

            notification.OriginalDateTime = originalDateTime;
            notification.OriginalVanue = originalVenue;

            return notification;
        }

        public static Notification GigCanceled(Gig gig)
        {
            return new Notification(NotificationType.GigCanceled, gig);
        }
    }
}