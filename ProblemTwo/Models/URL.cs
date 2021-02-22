using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProblemTwo.Models
{
    public class URL
    {
        public int id { get; set; }
        public string longUrl { get; set; }
        public string shortUrl { get; set; }
        public DateTime createOnDate { get; set; }
        public DateTime expireLink { get; set; }
        public string idShorterGenerated { get; set; }
        public int createById { get; set; }

      //  public List<URL> list { get; set; }
    }
}
