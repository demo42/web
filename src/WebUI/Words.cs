using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUI
{
    public class Words
    {
        //Hashset makes it super ineficient for generating a word
        //but really fast at validating it. Maybe it should be an array
        //to switch that optimization?
        private static HashSet<string> _words = new HashSet<string>
        {
            "Services",
            "Such",
            "Meta",
            "Resilient",
            "WOW",
            "Test",
            "Failures",
            "Containers",
            "Stuff"
        };

        private static Random _random = new Random();

        public static string GetWord(int count = 2)
        {
            var s = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                if (i != 0)
                {
                    s.Append("-");
                }

                var index = _random.Next(0, _words.Count);
                s.Append(_words.ElementAt(index));
            }

            return s.ToString();
        }

        public static bool IsWordValid(string word)
        {
            foreach(var segment in word.Split("-"))
            {
                if(!_words.Contains(segment))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
