using ByteInsights.Data;
using ByteInsights.Models;
using ByteInsights.Services;
using ByteInsights.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using X.PagedList;

namespace ByteInsights.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IBlogEmailSender _emailSender;

        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, IBlogEmailSender emailSender, ApplicationDbContext context)
        {
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
        }

        public async Task <IActionResult> Index(int? page)
        {
            //var blogs = awo4ait _context.Blogs.Include(b => b.BlogUser).ToListAsync();
            //return View(blogs);


            // X.PageList

            var pageNumber = page ?? 1;
            var pageSize = 5;
            //var blogs = _context.Blogs
            //                    .Where(p => p.Posts.Any(p => p.ReadyStatus == Enums.ReadyStatus.ProductionReady))
            //                    .OrderByDescending(b => b.Created)
            //                    .ToPagedListAsync(pageNumber, pageSize);

            var blogs = _context.Blogs
                                .Include(b => b.BlogUser)
                                .OrderByDescending(b => b.Created)
                                .ToPagedListAsync(pageNumber, pageSize);


            return View(await blogs);
        }

        public IActionResult Contact()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(ContactMe model)
        {
            // This is where we are going to be emailing 
            model.Message = $"{model.Message} <hr/> Phone: {model.Phone}";

            await _emailSender.SendContactEmailAsync(model.Email, model.Name, model.Subject, model.Message);

            return RedirectToAction("Index");
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}