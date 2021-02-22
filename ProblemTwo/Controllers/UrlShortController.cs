using Microsoft.AspNetCore.Mvc;
using ProblemTwo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProblemTwo.Controllers
{
    public class UrlShortController : Controller
    {
        private readonly IProductRepository _productRepository;
        public UrlShortController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult GetShortenUrl(int idProduct, string nameOfProduct, string category, int stock)
        {
            var product =  _productRepository.GetById(idProduct);

            return View(product);
        }
    }
}
