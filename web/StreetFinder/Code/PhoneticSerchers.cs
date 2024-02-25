namespace StreetFinder.Code
{

    public abstract class BaseTokenProviderSearcher : IPhoneticHandler
    {
        private readonly HashSet<string> tokens;

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
            return phrase.Split(" ").Select(st => Computer.Compute(st)).ToHashSet();
        }
    }

    public class GitHubSoundExSearcher : BaseTokenProviderSearcher
    {
        public GitHubSoundExSearcher(string origDataWord) : base(origDataWord){ }
        protected override IPhoneticAlgorithm Computer => new husseinbeygiSoundex();
    }

    public class CodeProjectSoundexSearcher : BaseTokenProviderSearcher
    {
        public CodeProjectSoundexSearcher(string origDataWord) : base(origDataWord) { }
        protected override IPhoneticAlgorithm Computer => new CodeProjectSoundex();
    }

    public class DoubleMetaphoneSearcher : IPhoneticHandler
    {

        public DoubleMetaphoneSearcher(string origDataWord)
        {

        }

        public bool CompareTo(string pattern)
        {
            throw new NotImplementedException();
        }
    }

}
