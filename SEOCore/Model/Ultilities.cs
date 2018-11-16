using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SEOCore.Model
{
    public class Ultilities
    {
        public static bool UrlCheck(string URL)
        {
            Uri UriResult;
            return Uri.TryCreate(URL, UriKind.Absolute, out UriResult) && (UriResult.Scheme == Uri.UriSchemeHttp || UriResult.Scheme == Uri.UriSchemeHttps); ;
        }

        public static IEnumerable<WordOccurance> ProcessHTMLDoc(string PlainHTML, bool IsStopWord)
        {
            List<WordOccurance> WordOccurList = new List<WordOccurance>();
            HtmlDocument HtmlDoc = new HtmlDocument();
            HtmlDoc.LoadHtml(PlainHTML);
            var TagPResult = HtmlDoc.DocumentNode.SelectNodes("//p");
            string WordPattern = @"\b[^\d\W]+\b";
            if (TagPResult != null)
            {
                foreach (var result in TagPResult)
                {
                    var SplitString = result.InnerText.Split(" ");

                    foreach (var ss in SplitString)
                    {
                        if (!(ss.ToString() == ""))
                        {
                            if (Regex.IsMatch(ss, WordPattern))
                            {
                                if (!IsStopWord)
                                {
                                    if (WordOccurList.FirstOrDefault(x => x.Word.Equals(ss.ToString())) == null)
                                    {
                                        WordOccurList.Add(new WordOccurance() { Word = ss.ToString(), Count = 1 });
                                    }
                                    else
                                    {
                                        WordOccurList.FirstOrDefault(x => x.Word.Equals(ss.ToString())).Count += 1;
                                    }


                                }
                                else
                                {
                                    if (!(StopWord.ListStopWord().Any(x => x.SWord.Equals(ss.ToLower()))))
                                    {
                                        if (WordOccurList.FirstOrDefault(x => x.Word.Equals(ss.ToString())) == null)
                                        {
                                            WordOccurList.Add(new WordOccurance() { Word = ss.ToString(), Count = 1 });
                                        }
                                        else
                                        {
                                            WordOccurList.FirstOrDefault(x => x.Word.Equals(ss.ToString())).Count += 1;
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }

            for (int i = 0; i < 6; i++)
            {
                string NodePath;
                if (i == 0)
                    NodePath = "//h";
                else
                    NodePath = "//h" + i;

                var TagHResult = HtmlDoc.DocumentNode.SelectNodes(NodePath);

                if (TagHResult != null)
                {
                    foreach (var result in TagHResult)
                    {
                        var SplitString = result.InnerText.Split(" ");

                        foreach (var ss in SplitString)
                        {
                            if (!(ss.ToString() == ""))
                            {
                                if (Regex.IsMatch(ss, WordPattern))
                                {
                                    if (!IsStopWord)
                                    {
                                        if (WordOccurList.FirstOrDefault(x => x.Word.Equals(ss.ToString())) == null)
                                        {
                                            WordOccurList.Add(new WordOccurance() { Word = ss.ToString(), Count = 1 });
                                        }
                                        else
                                        {
                                            WordOccurList.FirstOrDefault(x => x.Word.Equals(ss.ToString())).Count += 1;
                                        }


                                    }
                                    else
                                    {
                                        if (!(StopWord.ListStopWord().Any(x => x.SWord.Equals(ss.ToLower()))))
                                        {
                                            if (WordOccurList.FirstOrDefault(x => x.Word.Equals(ss.ToString())) == null)
                                            {
                                                WordOccurList.Add(new WordOccurance() { Word = ss.ToString(), Count = 1 });
                                            }
                                            else
                                            {
                                                WordOccurList.FirstOrDefault(x => x.Word.Equals(ss.ToString())).Count += 1;
                                            }
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }

            var TagTableResult = HtmlDoc.DocumentNode.SelectNodes("//table");
            if (TagTableResult != null)
            {
                foreach (var result in TagTableResult)
                {
                    var SplitString = result.InnerText.Split(" ");

                    foreach (var ss in SplitString)
                    {
                        if (!(ss.ToString() == ""))
                        {
                            if (Regex.IsMatch(ss, WordPattern))
                            {
                                if (!IsStopWord)
                                {
                                    if (WordOccurList.FirstOrDefault(x => x.Word.Equals(ss.ToString())) == null)
                                    {
                                        WordOccurList.Add(new WordOccurance() { Word = ss.ToString(), Count = 1 });
                                    }
                                    else
                                    {
                                        WordOccurList.FirstOrDefault(x => x.Word.Equals(ss.ToString())).Count += 1;
                                    }


                                }
                                else
                                {
                                    if (!(StopWord.ListStopWord().Any(x => x.SWord.Equals(ss.ToLower()))))
                                    {
                                        if (WordOccurList.FirstOrDefault(x => x.Word.Equals(ss.ToString())) == null)
                                        {
                                            WordOccurList.Add(new WordOccurance() { Word = ss.ToString(), Count = 1 });
                                        }
                                        else
                                        {
                                            WordOccurList.FirstOrDefault(x => x.Word.Equals(ss.ToString())).Count += 1;
                                        }
                                    }
                                }
                            }


                        }
                    }
                }
            }



            return WordOccurList;
        }

        public static IEnumerable<WordOccurance> ProcessString(string Text, bool IsStopWord)
        {
            List<WordOccurance> WordOccurList = new List<WordOccurance>();
            var SplitString = Text.Split(" ");
            string WordPattern = @"\b[^\d\W]+\b";
            foreach (var ss in SplitString)
            {
                if (Regex.IsMatch(ss, WordPattern))
                {
                    if (IsStopWord)
                    {
                        if (!(StopWord.ListStopWord().Any(x => x.SWord.Equals(ss.ToLower()))))
                        {
                            if (WordOccurList.FirstOrDefault(x => x.Word.Equals(ss.ToString())) == null)
                            {
                                WordOccurList.Add(new WordOccurance() { Word = ss.ToString(), Count = 1 });
                            }
                            else
                            {
                                WordOccurList.FirstOrDefault(x => x.Word.Equals(ss.ToString())).Count += 1;
                            }
                        }
                    }
                    else
                    {
                        if (WordOccurList.FirstOrDefault(x => x.Word.Equals(ss.ToString())) == null)
                        {
                            WordOccurList.Add(new WordOccurance() { Word = ss.ToString(), Count = 1 });
                        }
                        else
                        {
                            WordOccurList.FirstOrDefault(x => x.Word.Equals(ss.ToString())).Count += 1;
                        }
                    }
                }
            }

            return WordOccurList;
        }
    }
}
