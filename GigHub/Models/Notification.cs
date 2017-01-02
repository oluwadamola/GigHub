using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Notification
    {
        public int NotificationId { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime DateTime { get; private set; }
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalVanue { get; set; }

        [Required]
        public Gig Gig { get; private set; }

        public Notification()
        {
            
        }

        public Notification(NotificationType type, Gig gig)
        {
            if(gig == null) throw new ArgumentNullException(nameof(gig));

            Type = type;
            Gig = gig;
            DateTime = DateTime.Now;
        }
    }
}