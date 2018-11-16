using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using SEOCore.Model;

namespace SEOCore.Controllers
{
    [Produces("application/json")]
    [Route("api/ExternalLink")]
    public class ExternalLinkController : Controller
    {
        [HttpGet("[action]")]
        [Route("{enablestopword}/{Text}")]
        public IEnumerable<ExternalLink> GetExternalLinkList(bool EnableStopWord, string Text)
        {
            List<ExternalLink> ExternalLinkList = new List<ExternalLink>();
            if (Ultilities.UrlCheck(Text))
            {
                WebClient webClient = new WebClient();
                try
                {
                    string HTMLDoc = webClient.DownloadString(Text);

                    MatchCollection MatchedString = Regex.Matches(HTMLDoc, @"(<a.*?>.*?</a>)", RegexOptions.Singleline);

                    foreach (Match MatchedItem in MatchedString)
                    {
                        string Word = MatchedItem.Groups[1].Value;
                        ExternalLink ExternalLinkItem = new ExternalLink();
                        if (!EnableStopWord)
                        {

                            Match MatchHref = Regex.Match(Word, @"href=\""(.*?)\""",
                                   RegexOptions.Singleline);
                            if (MatchHref.Success)
                            {
                                ExternalLinkItem.Link = MatchHref.Groups[1].Value;
                            }

                            string t = Regex.Replace(Word, @"\s*<.*?>\s*", "",
                                RegexOptions.Singleline);
                            ExternalLinkItem.Name = t;
                            ExternalLinkList.Add(ExternalLinkItem);
                        }
                        else
                        {
                            if (!(StopWord.ListStopWord().Any(x => x.SWord.Equals(Word.ToLower()))))
                            {
                                Match MatchHref = Regex.Match(Word, @"href=\""(.*?)\""",
                                    RegexOptions.Singleline);
                                if (MatchHref.Success)
                                {
                                    ExternalLinkItem.Link = MatchHref.Groups[1].Value;
                                }

                                string t = Regex.Replace(Word, @"\s*<.*?>\s*", "",
                                    RegexOptions.Singleline);
                                ExternalLinkItem.Name = t;
                                ExternalLinkList.Add(ExternalLinkItem);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                }
            }

            return ExternalLinkList.OrderBy(x => x.Name);
        }
    }
}