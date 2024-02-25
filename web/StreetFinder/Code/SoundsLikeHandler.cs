namespace StreetFinder.Code
{

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
}
