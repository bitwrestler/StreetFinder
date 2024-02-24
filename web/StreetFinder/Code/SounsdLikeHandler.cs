using System.Text.RegularExpressions;
using System.Text;

namespace StreetFinder.Code
{
    public class SounsdLikeHandler
    {
        private static readonly Regex rule_12 = new Regex("[^a-z]", RegexOptions.IgnoreCase);
        private static readonly Regex rule_3 = new Regex("[aeihouwy]", RegexOptions.IgnoreCase);


        /// <summary>
        /// 
        ///     Code lifted from https://github.com/haddow64/PhoneticSearch
        /// 
        /// 
        ///     This algorithm is a modified version of the Soundex algorithm that does not
        ///     retain the leading letter and has different letter sets.
        ///     Part of below function based on existing soundex implementation found here:
        ///     http://www.techrepublic.com/blog/software-engineer/how-do-i-implement-the-soundex-function-in-c/#.
        /// </summary>
        /// <param name="inputNames">Called twice for user input and parsing of lines in .txt file</param>
        public static string Soundex(string inputNames)
        {
            var result = new StringBuilder();
            var previousNumber = "";

            var removeSpaces = inputNames.Replace(" ", "");

            // 1.  All non-alphabetic characters are ignored
            // 2.  Word case is not significant
            // Rule one implemented using regular expressions
            // Rule two implemented from RegexOptions.IgnoreCase
            // Slower than the C# implementation of IsLetterOrDigit but more commonly used because its easier to understand and maintain
            var onlyAlphabetic = rule_12.Replace(removeSpaces, "");  

            // 3.  After the first letter, any of the following letters are discarded: A, E, I, H, O, U, W, Y.
            // Rule three stores the first letter in getFirstLetter
            // Then uses regular expressions to remove the defined letters and rejoins the first letter
            var getFirstLetter = onlyAlphabetic.Substring(0, 1);
            var removeLetters = rule_3.Replace(onlyAlphabetic.Remove(0, 1), "");
            inputNames = getFirstLetter + removeLetters;

            // 4.  The following sets of letters are considered equivalent
            // A, E, I, O, U
            // C, G, J, K, Q, S, X, Y, Z
            // B, F, P, V, W
            // D, T
            // M, N
            // All others have no equivalent
            if (inputNames.Length > 0)
            {
                for (var i = 1; i < inputNames.Length; i++)
                {
                    var evaluateLetter = inputNames.Substring(i, 1).ToLower();

                    string currentNumber;
                    if ("aeiou".IndexOf(evaluateLetter, StringComparison.Ordinal) > -1)
                        currentNumber = "1";
                    else if ("cgjkqsxyz".IndexOf(evaluateLetter, StringComparison.Ordinal) > -1)
                        currentNumber = "2";
                    else if ("bfpvw".IndexOf(evaluateLetter, StringComparison.Ordinal) > -1)
                        currentNumber = "3";
                    else if ("dt".IndexOf(evaluateLetter, StringComparison.Ordinal) > -1)
                        currentNumber = "4";
                    else if ("mn".IndexOf(evaluateLetter, StringComparison.Ordinal) > -1)
                        currentNumber = "5";
                    else
                        currentNumber = "6";

                    // 5.  Any consecutive occurrences of equivalent letters (after discarding letters in step 3) 
                    // are considered as a single occurrence
                    if (currentNumber != previousNumber)
                        result.Append(currentNumber);

                    if (result.Length == 4) break;

                    if (currentNumber != "")
                        previousNumber = currentNumber;
                }
            }

            if (result.Length < 4)
                result.Append(new string('0', 4 - result.Length));

            return result.ToString().ToUpper();
        }
    }
}
