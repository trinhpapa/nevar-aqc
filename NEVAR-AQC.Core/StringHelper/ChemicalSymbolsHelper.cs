using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NEVAR_AQC.Core.StringHelper
{
    public class ChemicalSymbolsHelper
    {
        public static List<KeyValuePair<string, string>> FormatToElementArray(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

            RegexOptions options = RegexOptions.Multiline;
            string pattern = @"<.?su[bp]>";

            var returnArray = new List<KeyValuePair<string, string>>();
            var startIndex = 0;
            var inputLength = input.Length;

            foreach (Match m in Regex.Matches(input, pattern, options))
            {
                if (Regex.IsMatch(m.Value, @"<su[bp]>"))
                {
                    if (input.Substring(startIndex, m.Index - startIndex) != "")
                    {
                        returnArray.Add(new KeyValuePair<string, string>("content", input.Substring(startIndex, m.Index - startIndex)));
                    }
                    startIndex = m.Index + 5;
                }
                if (Regex.IsMatch(m.Value, @"</su[bp]>"))
                {
                    if (Regex.IsMatch(m.Value, @"</sub>"))
                    {
                        returnArray.Add(new KeyValuePair<string, string>("sub", input.Substring(startIndex, m.Index - startIndex)));
                    }
                    if (Regex.IsMatch(m.Value, @"</sup>"))
                    {
                        returnArray.Add(new KeyValuePair<string, string>("sup", input.Substring(startIndex, m.Index - startIndex)));
                    }
                    startIndex = m.Index + 6;
                }
            }
            if (input.Substring(startIndex, inputLength - startIndex) != "")
            {
                returnArray.Add(new KeyValuePair<string, string>("content", input.Substring(startIndex, inputLength - startIndex)));
            }

            return returnArray;
        }
    }
}