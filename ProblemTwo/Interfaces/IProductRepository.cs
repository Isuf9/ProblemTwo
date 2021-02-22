using ProblemTwo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProblemTwo.Interfaces
{
    public interface IProductRepository
    {
        #region Product Method
        List<Products> GetAll();
        Products GetById(int productId);
        List<Products> ExistingProducts();
        #endregion

        #region URL Method
        List<URL> GetAllURL(int createById);
        Task<bool> SaveShortUrl(List<URL> list);
        URL GetUrlById(string id);
        Task<bool> AddURL(URL obj);
        string GenerateShorterUrl(int length);
        #endregion
    }
}
