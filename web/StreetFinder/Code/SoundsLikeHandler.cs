using System.Text.RegularExpressions;
using System.Text;

namespace StreetFinder.Code
{
    public interface IPhoneticHandler
    {
        IEnumerable<string> PhoneticTokens(string phrase);
    }

    public class SoundsLikeHandler : IPhoneticHandler
    {
        public IEnumerable<string> PhoneticTokens(string phrase)
        {
            return phrase.Split(" ").Select(st => husseinbeygiSoundex.Soundex(st));
        }

        class husseinbeygiSoundex
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

            public static string Soundex(string word)
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
    }
}
