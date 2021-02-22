using ProblemTwo.Interfaces;
using ProblemTwo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProblemTwo.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationContext _dbContext;

        public ProductRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }


        public List<Products> ExistingProducts()
        {
            var list = new List<Products>()
            {
              new Products(){ idProduct = 1, name = "Msi gaming loptop", price = 1200, category = "Loptop", stock = 10},
              new Products(){ idProduct = 2, name = "Msi loptop", price = 640, category = "Loptop", stock = 7},
              new Products(){ idProduct = 3, name = "Asus ROG", price = 900, category = "Loptop", stock = 5},
              new Products(){ idProduct = 4, name = "Lenovo Think Pad", price = 720, category = "Loptop", stock = 10},
              new Products(){ idProduct = 5, name = "Dell XPS", price = 1500, category = "Loptop", stock = 10},
            };

            return list;
        }
        public List<Products> GetAll()
        {
            var result = ExistingProducts();
            return result;
        }
        public Products GetById(int productId)
        {
            var getAllPriducts = ExistingProducts();

            var product = getAllPriducts.Where(x => x.idProduct == productId).FirstOrDefault();

            return product;
        }



        public async Task<bool> AddURL(URL obj)
        {
            _dbContext.URL.Add(obj);
            var saved = await _dbContext.SaveChangesAsync();
            if (saved > 0)
            {
                return true;
            }
            return false;
        }

        public URL GetUrlById(string id)
        {
            var result = _dbContext.URL.Where(x => x.idShorterGenerated == id).FirstOrDefault();

            return result;
        }

        public async Task<bool> SaveShortUrl(List<URL> list)
        {

            bool successful = false;
            foreach (var item in list)
            {
                var url = new URL()
                {

                    longUrl = item.longUrl,
                    shortUrl = item.shortUrl,
                    createOnDate = DateTime.Now,
                    expireLink = Convert.ToDateTime(item.expireLink),
                    idShorterGenerated = item.idShorterGenerated,
                    createById = item.createById
                };
                successful = await AddURL(url);
            }

            if (successful)
            {
                return true;
            }
            return false;
        }

        public string GenerateShorterUrl(int length)
        {
            var random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public List<URL> GetAllURL(int createById)
        {
            var result = _dbContext.URL.Where(x => x.createById == createById).ToList();
            return result;
        }
    }
}



