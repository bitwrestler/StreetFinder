namespace StreetFinder.Code
{
    /// <summary>
    /// Implementation based on 
    /// https://www.codeproject.com/articles/13302/soundex-implementation-in-c-and-vb-net
    /// </summary>
    public class CodeProjectSoundex : IPhoneticAlgorithm
    {
        public string Compute(string word)
        {
            return _Compute(word);
        }

        private string _Compute(string word, int length=4)
        {
            // Value to return
            string value = string.Empty;
            // Make sure the word is at least two characters in length
            if (!string.IsNullOrEmpty(word) && word.Length > 1)
                  {
                // Convert the word to all uppercase
                word = word.ToUpper();
                // The current and previous character codes
                int prevCode = 0;
                int currCode = 0;
                // Append the first character
                value += word[0];
                // Loop through all the characters and convert them to the proper character code
                for (int i = 1; i < word.Length; i++)
                        {
                    switch (word[i])
                    {
                        case 'A':
                        case 'E':
                        case 'I':
                        case 'O':
                        case 'U':
                        case 'H':
                        case 'W':
                        case 'Y':
                            currCode = 0;
                            break;
                        case 'B':
                        case 'F':
                        case 'P':
                        case 'V':
                            currCode = 1;
                            break;
                        case 'C':
                        case 'G':
                        case 'J':
                        case 'K':
                        case 'Q':
                        case 'S':
                        case 'X':
                        case 'Z':
                            currCode = 2;
                            break;
                        case 'D':
                        case 'T':
                            currCode = 3;
                            break;
                        case 'L':
                            currCode = 4;
                            break;
                        case 'M':
                        case 'N':
                            currCode = 5;
                            break;
                        case 'R':
                            currCode = 6;
                            break;
                    }
                    // Add only if the current code is not the same as the last one and the current code is not 0 (a vowel)
                    if (currCode != prevCode && currCode != 0)
                                    value += currCode;
                    // Set the new previous character code
                    prevCode = currCode;
                    // If the buffer size meets the length limit, then exit the loop
                    if (value.Length == length)
                        break;
                }
                // Pad the buffer, if required
                value = value.PadRight(length, '0');
            }
            // Return the value
            return value;
        }
    }
}
