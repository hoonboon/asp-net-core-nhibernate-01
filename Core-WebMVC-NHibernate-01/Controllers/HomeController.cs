using Core_WebMVC_NHibernate_01.Models;
using Core_WebMVC_NHibernate_01.Models.Books;
using Core_WebMVC_NHibernate_01.Orm;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Core_WebMVC_NHibernate_01.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INHSession _session;

        public HomeController(ILogger<HomeController> logger, INHSession session)
        {
            _logger = logger;
            _session = session;
        }

        public async Task<IActionResult> Index(int pageNo = 1)
        {
            try
            {
                var rowCount = await _session.GetQueryableData<Book>().CountAsync();
                if (rowCount == 0)
                {
                    await _session.RunInTransactionAsync(async () => {
                        var tempCount = 0;
                        while (tempCount < 5)
                        {
                            var tempBook = new Book { Title = $"Test Book {tempCount + 1}" };
                            await _session.SaveAsync<Book>(tempBook);
                            tempCount++;
                        }
                    });
                }

                var books = await _session.GetQueryableData<Book>()
                    .OrderBy(x => x.Title)
                    .GetPagedAsync(pageNo, pageSize: 10);

                return View(books);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Index(): {ex.Message}");
                return View("Error");
            }
        }

        public IActionResult Privacy()
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
