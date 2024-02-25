using System.Text;

namespace StreetFinder.Code
{
    public interface IPhoneticHandler
    {
        bool CompareTo(string pattern);
    }

    public interface IPhoneticAlgorithm
    {
        string Compute(string pattern);
    }

    public static class PhoneticHandlerFactory
    {
        public static IPhoneticHandler GetHandlerForPattern(string pattern)
        {
            return new CodeProjectSoundexSearcher(pattern);
        }
    }

    public abstract class BaseTokenProviderSearcher : IPhoneticHandler
    {
        private readonly HashSet<string> tokens;

        protected BaseTokenProviderSearcher(string pattern)
        {
            this.tokens = PhoneticTokens(pattern);
        }

        public bool CompareTo(string pattern)
        {
            var search = PhoneticTokens(pattern);
            search.IntersectWith(this.tokens);
            return search.Any();
        }
        protected abstract IPhoneticAlgorithm Computer { get; }

        protected virtual HashSet<string> PhoneticTokens(string phrase)
        {
            return phrase.Split(" ").Select(st => Computer.Compute(st)).ToHashSet();
        }
    }

    public class SoundExSearcher : BaseTokenProviderSearcher
    {
        public SoundExSearcher(string pattern) : base(pattern){ }
        protected override IPhoneticAlgorithm Computer => new husseinbeygiSoundex();
    }

    public class CodeProjectSoundexSearcher : BaseTokenProviderSearcher
    {
        public CodeProjectSoundexSearcher(string pattern) : base(pattern) { }
        protected override IPhoneticAlgorithm Computer => new CodeProjectSoundex();
    }


    class husseinbeygiSoundex : IPhoneticAlgorithm
    {
            private static SoundexResourcesModel XSResources = new SoundexResourcesModel();
 
            class CharacterCodes
            {
                public CharacterCodes()
                {
                    _1 = new();
                    _2 = new();
                    _3 = new();
                    _4 = new();
                    _5 = new();
                    _6 = new();
                }

                public List<string> _1 { get; set; }
                public List<string> _2 { get; set; }
                public List<string> _3 { get; set; }
                public List<string> _4 { get; set; }
                public List<string> _5 { get; set; }
                public List<string> _6 { get; set; }
            }

            class SoundexResourcesModel
            {
                public SoundexResourcesModel()
                {
                    CharacterCodes = new();
                    Vowls = new();
                    Maps = new();

                        CharacterCodes._1 = "B/P/F/V".Split('/').ToList();
                        CharacterCodes._2 = "C/G/J/K/Q/S/X/Z".Split('/').ToList();
                        CharacterCodes._3 = "D/T".Split('/').ToList();
                        CharacterCodes._4 = "L".Split('/').ToList();
                        CharacterCodes._5 = "M/N".Split('/').ToList();
                        CharacterCodes._6 = "R".Split('/').ToList();
                        Vowls = "A/E/I/U/Y/O/H/W".Split('/').ToList();
                    
                }
                public CharacterCodes CharacterCodes { get; set; }
                public List<string> Vowls { get; set; }
                public List<KeyValue> Maps { get; set; }

            }

            class KeyValue
            {
                public KeyValue(char key, char value)
                {
                    Key = key;
                    Value = value;
                }
                public char Key { get; set; }
                public char Value { get; set; }
            }

            public string Compute(string word)
            {
                if (string.IsNullOrWhiteSpace(word))
                {
                    return string.Empty;
                }

                word = word.ToUpper();

                var fLetter = word[0];
                word = RemoveTheFirstCharacter(word);
                word = CleanUpTheVowlsCharacter(word);

                var Characters = word.ToList();

                var buffer = new StringBuilder(4);
                buffer.Append(fLetter);

                foreach (var item in Characters)
                {
                    int currCode = GetCharacterCode(item.ToString());
                    buffer.Append(currCode);
                    if (buffer.Length == 4)
                        break;
                }
                AddZeroToEndIfNeeded(buffer);
                return buffer.ToString();
            }

            private static void AddZeroToEndIfNeeded(StringBuilder buffer)
            {
                if (buffer.Length < 4)
                    buffer.Append('0', (4 - buffer.Length));
            }

            private static string RemoveTheFirstCharacter(string word)
            {
                return word.Substring(1);
            }

            private static int GetCharacterCode(string Characters)
            {
                if (XSResources.CharacterCodes._1.FirstOrDefault(x => x == Characters) != null) { return 1; }
                if (XSResources.CharacterCodes._2.FirstOrDefault(x => x == Characters) != null) { return 2; }
                if (XSResources.CharacterCodes._3.FirstOrDefault(x => x == Characters) != null) { return 3; }
                if (XSResources.CharacterCodes._4.FirstOrDefault(x => x == Characters) != null) { return 4; }
                if (XSResources.CharacterCodes._5.FirstOrDefault(x => x == Characters) != null) { return 5; }
                if (XSResources.CharacterCodes._6.FirstOrDefault(x => x == Characters) != null) { return 6; }

                return 0;
            }

            private static string CleanUpTheVowlsCharacter(string word)
            {

                word = RemoveWhitespace(word);

                foreach (var item in XSResources.Vowls)
                {
                    word = word.Replace(item, "");
                }

                return word;
            }

            private static string RemoveWhitespace(string input)
            {
                return new string(input.ToCharArray()
                    .Where(c => !char.IsWhiteSpace(c))
                    .ToArray());
            }
    }
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
