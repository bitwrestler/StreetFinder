using System.Text.RegularExpressions;

namespace StreetFinder.Code
{

    public abstract class BaseTokenProviderSearcher : IPhoneticHandler
    {
        private readonly HashSet<string> tokens;

        private readonly Regex NON_ALPHA_PATTERN = new Regex("[^A-Z]");

        protected BaseTokenProviderSearcher(string origDataWord)
        {
            this.tokens = PhoneticTokens(origDataWord);
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
            return phrase.Split(" ").Select(sl=>NON_ALPHA_PATTERN.Replace(sl, "")).Select(st => Computer.Compute(st)).ToHashSet();
        }
    }

    public class CodeProjectSoundexSearcher : BaseTokenProviderSearcher
    {
        public CodeProjectSoundexSearcher(string origDataWord) : base(origDataWord) { }
        protected override IPhoneticAlgorithm Computer => new CodeProjectSoundex();
    }
}
