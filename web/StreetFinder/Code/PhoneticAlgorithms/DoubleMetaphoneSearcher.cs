using nullpointer.Metaphone;
using token_t = (string[], string[]);

namespace StreetFinder.Code.PhoneticAlgorithms
{
    public class DoubleMetaphoneSearcher : IPhoneticHandler
    {
        private const double MATCH_THRESHOLD = .49999;

        private readonly token_t _tokens;
        public DoubleMetaphoneSearcher(string origDataWord)
        {
            _tokens = PhoneticTokens(origDataWord);
        }

        public bool CompareTo(string pattern)
        {
            return CalcWeight(this._tokens, PhoneticTokens(pattern)) > MATCH_THRESHOLD;
        }
        private static double CalcWeight(token_t dataTokens, token_t searchTokens)
        {
            double weight = 0.0;

            double base_value = 1.0 / dataTokens.Item1.Length;

            foreach (var amatch in dataTokens.Item1.Intersect(searchTokens.Item1))
                weight += base_value;
            if (weight > MATCH_THRESHOLD)
                return weight;
            foreach (var amatch in dataTokens.Item1.Intersect(searchTokens.Item2))
                weight += base_value / 2;
            if (weight > MATCH_THRESHOLD)
                return weight;
            foreach (var amatch in dataTokens.Item2.Intersect(searchTokens.Item2))
                weight += base_value / 4;
            return weight;
        }
        
        private static bool HasValue(string data)
        {
            return ! string.IsNullOrWhiteSpace(data);
        }

        private static token_t PhoneticTokens(string phrase)
        {
            var words = phrase.Split(" ");
            var pri = new List<string>(words.Length);
            var alt = new List<string>(words.Length);
            foreach (var word in words)
            {
                var mp = new DoubleMetaphone(word);
                if(HasValue(mp.PrimaryKey))
                    pri.Add(mp.PrimaryKey);
                if(HasValue(mp.AlternateKey))
                    alt.Add(mp.AlternateKey);
            }
            return new token_t(pri.OrderBy(o => o).ToArray(), alt.OrderBy(o => o).ToArray());
        }
    }
}
