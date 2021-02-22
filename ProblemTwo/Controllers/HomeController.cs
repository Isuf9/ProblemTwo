using Google.Apis.Services;
using Google.Apis.Urlshortener.v1;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProblemTwo.Interfaces;
using ProblemTwo.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemTwo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public static readonly string Alphabet = "abcdefghijklmnopqrstuvwxyz0123456789";
        public static readonly int Base = Alphabet.Length;

        private readonly IProductRepository _productRepository;

        public HomeController(ILogger<HomeController> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AllURLsGenerated(int createById)
        {
            var result = _productRepository.GetAllURL(createById);

            return View(result);
        } 
        [Route("Home/readRandomURL/{url}")]
        public IActionResult readRandomURL(string url)
        {
            var result = _productRepository.GetUrlById(url);

            if(result.expireLink < DateTime.Now)
            {
                return View(nameof(UnavailableLink));
            }
            return Redirect(result.longUrl);
        }

        public async Task<IActionResult> GenerateURLshorter(string longUrl, string expireLink)
        {
            var random = new Random();
            int createBy = random.Next(1, 1000);
            ViewBag.CheckList = false;
            var protocolName = HttpContext.Request.Scheme;
            var domainName = HttpContext.Request.Host;

          
            var list = new List<URL>();

            for (int i = 0; i < 5; i++)
            {
                var hash = _productRepository.GenerateShorterUrl(5);

                var url = new URL()
                {
                    longUrl = longUrl,
                    idShorterGenerated = hash,
                    shortUrl = $"{protocolName}://{domainName}/" + "home/readRandomURL" + "/" + hash,
                    createOnDate = DateTime.Now,
                    expireLink = Convert.ToDateTime(expireLink),
                    createById = createBy
                };
                if(i == 0)
                {
                    ViewBag.First = url.shortUrl;
                    ViewBag.CreatedBy = createBy;
                }
                list.Add(url);
            }

            var successful = await _productRepository.SaveShortUrl(list);
            if (successful)
            {
                ViewBag.OldUrl = longUrl;
                ViewBag.ListOfShorterLink = list;
                
                return View(nameof(Index));
            }
            return View(list);
        }
        public IActionResult UnavailableLink()
        {
            return View();
        }
    }
}
