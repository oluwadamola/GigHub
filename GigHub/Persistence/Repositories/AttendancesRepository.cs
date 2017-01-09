using System;
using System.Collections.Generic;
using System.Linq;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Repositories
{
    public class AttendancesRepository : IAttendancesRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendancesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Attendance> GetFutureAttendances(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
                .ToList();
        }

        public bool GetAttendance(Gig gig, string userId)
        {
            return _context.Attendances
                .Any(a => a.GigId == gig.GigId && a.AttendeeId == userId);
        }
    }
}