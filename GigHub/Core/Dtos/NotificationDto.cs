using System;
using GigHub.Core.Models;

namespace GigHub.Core.Dtos
{
    public class NotificationDto
    {
        public NotificationType Type { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalVanue { get; set; }
        public GigDto Gig { get; set; }
    }
}