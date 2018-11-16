using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using SEOCore.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SEOCore.Controllers
{
    [Route("api/[controller]")]
    public class WordOccuranceController : Controller
    {
        // GET: api/<controller>
        [HttpGet("[action]")]
        [Route("{enablestopword}/{Text}")]
        public IEnumerable<WordOccurance> GetWordOccurance(bool EnableStopWord, string Text)
        {
            List<WordOccurance> wo = new List<WordOccurance>();

            if (Text != string.Empty && Ultilities.UrlCheck(Text))
            {

                WebClient w = new WebClient();
                try
                {
                    string s = w.DownloadString(Text);
                    wo = Ultilities.ProcessHTMLDoc(s, EnableStopWord).ToList();
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                }
            }
            else
            {
                wo = Ultilities.ProcessString(Text, EnableStopWord).ToList();
            }
            return wo.OrderByDescending(x=>x.Count);
        }


    }
}
