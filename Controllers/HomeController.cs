﻿using Bigon.Data;
using Bigon.Infrastructure.Entites;
using Bigon.Infrastructure.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;

namespace BigonWebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _db;
        private readonly IEmailService _emailService;

        public HomeController(DataContext db, IEmailService emailService)
        {
            _db = db;
            _emailService = emailService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe(string email)
        {
            bool isEmail = Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");

            if (!isEmail)
            {
                return Json(new
                {
                    error = true,
                    message = "Zəhmət olmasa düzgün email daxil edin"
                });
            }

            var subscriber = _db.Subscribers.FirstOrDefault(s => s.EmailAddress == email);

            if (subscriber != null && !subscriber.IsApproved)
            {
                return Json(new
                {
                    error = false,
                    message = $"Bu {email}-ə link göndərilmişdir,Zəhmət olmasa təsdiq edin"
                });
            }

            if (subscriber != null && subscriber.IsApproved)
            {
                return Json(new
                {
                    error = false,
                    message = $"Bu {email} artıq abunə olmuşdur"
                });
            }

            var newSubscriber = new Subscriber
            {
                CreatedAt = DateTime.Now,
                EmailAddress = email,
            };

            _db.Subscribers.Add(newSubscriber);
            _db.SaveChanges();

            string token = $"#demo-{newSubscriber.EmailAddress}-{newSubscriber.CreatedAt:yyyy-MM-dd HH:mm:ss.fff}-bigon";
            token = HttpUtility.UrlEncode(token);

            string url = $"{Request.Scheme}://{Request.Host}/subscribe-approve?token={token}";
            string body = $"Please click to link accept subscription <a href=\"{url}\">Click!</a>";

            await _emailService.SendMailAsync(email, "Subscription", body);

            return Ok(new
            {
                success = true,
                message = $"Bu {email}-ə link göndərildi, zəhmət olmasa təsdiq edin"
            });
        }

        [Route("/subscribe-approve")]
        public async Task<IActionResult> SubscribeApprove(string token)
        {
            string pattern = @"#demo-(?<email>[^-]*)-(?<date>\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}.\d{3})-bigon";

            Match match = Regex.Match(token, pattern);

            if (!match.Success)
            {
                return Content("token is broken!");
            }

            string email = match.Groups["email"].Value;
            string dateStr = match.Groups["date"].Value;

            if (!DateTime.TryParseExact(dateStr, "yyyy-MM-dd HH:mm:ss.fff", null, DateTimeStyles.None, out DateTime date))
            {
                return Content("token is broken!");
            }

            var subscriber = await _db.Subscribers
                .FirstOrDefaultAsync(m => m.EmailAddress.Equals(email) && m.CreatedAt == date);

            if (subscriber == null)
            {
                return Content("token is broken!");
            }

            if (!subscriber.IsApproved)
            {
                subscriber.IsApproved = true;
                subscriber.ApprovedAt = DateTime.Now;
            }
            await _db.SaveChangesAsync();


            return Content($"Success: Email: {email}\n" +
                $"Date: {date}");
        }
    }
}
