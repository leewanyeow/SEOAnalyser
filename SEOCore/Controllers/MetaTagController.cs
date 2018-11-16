using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEOCore.Model;

namespace SEOCore.Controllers
{
    [Produces("application/json")]
    [Route("api/MetaTag")]
    public class MetaTagController : Controller
    {
        [HttpGet("[action]")]
        [Route("{enablestopword}/{Text}")]
        public IEnumerable<MetaTag> GetMetaTagList(bool EnableStopWord, string text)
        {
            List<MetaTag> MetaTagList = new List<MetaTag>();
            if (Ultilities.UrlCheck(text))
            {
                try
                {
                    WebClient webClient = new WebClient();
                    string HtmlDoc = webClient.DownloadString(text);

                    MatchCollection MatchedString = Regex.Matches(HtmlDoc, @"(<meta.*?>)", RegexOptions.Singleline);

                    foreach (Match MatchItem in MatchedString)
                    {
                        string Word = MatchItem.Groups[1].Value;
                        if (!EnableStopWord)
                        {

                            MetaTag MetaTagItem = new MetaTag();

                            Match MatchName = Regex.Match(Word, @"name=""(.*?)""", RegexOptions.Singleline);
                            if (MatchName.Success)
                            {
                                if (MatchName.Groups[1].Value != String.Empty)
                                    MetaTagItem.Name = MatchName.Groups[1].Value;
                                else
                                    break;
                            }

                            Match MatchContent = Regex.Match(Word, @"content=""(.*?)""", RegexOptions.Singleline);
                            if (MatchContent.Success)
                            {
                                MetaTagItem.Content = MatchContent.Groups[1].Value;
                            }

                            MetaTagList.Add(MetaTagItem);
                        }
                        else
                        {
                            if (!(StopWord.ListStopWord().Any(x => x.SWord.Equals(Word.ToLower()))))
                            {
                                MetaTag MetaTagItem = new MetaTag();
                                Match MatchName = Regex.Match(Word, @"name=""(.*?)""", RegexOptions.Singleline);
                                if (MatchName.Success)
                                {
                                    if (MatchName.Groups[1].Value != String.Empty)
                                        MetaTagItem.Name = MatchName.Groups[1].Value;
                                    else
                                        break;
                                }

                                Match MatchContent = Regex.Match(Word, @"content=""(.*?)""", RegexOptions.Singleline);
                                if (MatchContent.Success)
                                {
                                    MetaTagItem.Content = MatchContent.Groups[1].Value;
                                }

                                MetaTagList.Add(MetaTagItem);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                }
            }
            return MetaTagList;
        }
    }
}