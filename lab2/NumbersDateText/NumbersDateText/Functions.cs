using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;


namespace NumbersDateText
{
    public class Functions
    {
        // 5 task
        public void FindNonEnglishCapitalLetters(string s)
        {
            var selectedLetters = (
                from ch in s
                where char.IsUpper(ch)
                where !(Regex.IsMatch(ch.ToString(), "[A-Z]"))
                orderby ch
                select ch).Distinct().ToList();

            foreach (var letter in selectedLetters)
            {
                Console.Write(letter + " ");
            }
        }


        // 12 task
        public void PrintNonEnglishWords(string s)
        {
            var selectedWords =
                s.Split(" ").
                Where(word => !Regex.IsMatch(word, "[a-zA-Z]+")).
                OrderByDescending(word => word.Length).
                ToList();

            int outputWidth = selectedWords.First().ToString().Length;
            string formatPattern = "{0," + outputWidth + "}";
            foreach (var word in selectedWords)
            {
                Console.WriteLine(String.Format(formatPattern, word));
            }
        }


        // 15 task
        private bool IsVowel(char c)
        {
            // Y/y are not included due to uncertainty
            return "AEIOUaeiou".Contains(c);
        }

        public string ReplaceLettersAfterVowels(string s)
        {
            StringBuilder resultString = new StringBuilder();

            for (int i = 0; i < s.Length; i++)
            {
                if (i != 0)
                {
                    if (IsVowel(s[i - 1]))
                    {
                        if (s[i] == 'z' || s[i] == 'Z')
                        {
                            resultString.Append((char) (s[i] - 25));
                            continue;
                        }

                        resultString.Append((char) (s[i] + 1));
                        continue;
                    }
                }

                resultString.Append(s[i]);
            }

            return resultString.ToString();
        }
    }
}