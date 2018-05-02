using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kms.Data.Entities;
using kms.Data;
using kms.Models;
using kms.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using kms.Utils;

namespace kms.Controllers
{
    [KmsController, Authorize]
    public class NotificationsController : Controller
    {
        private int UserId { get { return int.Parse(User.Identity.Name); } }

        private readonly KMSDBContext _db;
        public NotificationsController(KMSDBContext context)
        {
            this._db = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetList(int? offset, int? limit) {
            var lastRead = _db.NotificationsRead.SingleOrDefault(n => n.UserId == UserId);

            var lastTimeRead = (lastRead != null ? lastRead.TimeRead : DateTime.MinValue);

            var notificationsQuery = _db.Notifications.Where(n => n.UserId == UserId).OrderByDescending(n => n.TimeFired);
            var countUnread = await _db.Notifications.Where(n => n.UserId == UserId && n.TimeFired > lastTimeRead).CountAsync();
            var count = await notificationsQuery.CountAsync();
            var notifications = await notificationsQuery.Skip(offset.HasValue ? offset.Value : 0).Take(limit.HasValue ? limit.Value : 50).ToListAsync();

            var results = notifications.Select(n => new NotificationDto(n, (lastRead != null ? n.TimeFired <= lastRead.TimeRead : false)));

            return Ok(new { count, count_unread = countUnread, results });
        }

        [HttpPost("read")]
        public async Task<IActionResult> SetRead() {
            var currentReadMark = await _db.NotificationsRead.SingleOrDefaultAsync(n => n.UserId == UserId);

            if (currentReadMark == null) {
                _db.NotificationsRead.Add(new NotificationsRead { UserId = UserId, TimeRead = DateTime.UtcNow });
            } else {
                currentReadMark.TimeRead = DateTime.UtcNow;
            }

            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAll() {
            _db.Notifications.RemoveRange(_db.Notifications.Where(n => n.UserId == UserId));
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
