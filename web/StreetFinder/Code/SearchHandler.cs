using System.Text.RegularExpressions;

using evaluatorDelg = System.Func<StreetFinder.Code.StreetRecord, bool>;

namespace StreetFinder.Code
{
    public class SearchHandler
    {
        private evaluatorDelg _func;

        public SearchHandler(string pattern, SearchOptions options) {
            var pat = pattern.ToUpper();
            this._func = MakeHandler(pat,options);
        }

        public bool Search(StreetRecord record)
        {
            return this._func(record);
        }

        private static bool HasWildcard(string pat) => pat.Contains("*");

        private static bool TryPrepareWildcard(string pat, SearchOptions options , out Regex? re)
        {
            if(HasWildcard(pat))
            {
                pat = Regex.Escape(pat).Replace("\\*", ".+");

                switch (options)
                {
                    case SearchOptions.StartsWith:
                        pat = "^" + pat;
                        break;
                    case SearchOptions.EndsWith:
                        pat = pat + "$";
                        break;
                }
                re = new Regex(pat);
                return true;
            }
            re = null;
            return false;
        }

        private evaluatorDelg MakeHandler(string pat, SearchOptions options)
        {
            if (TryPrepareWildcard(pat, options, out Regex? repattern) && repattern is not null)
            { 
                return (StreetRecord r) => repattern.IsMatch(r.Name);
            }
            switch(options)
            {
                case SearchOptions.StartsWith:
                    return (StreetRecord r) => r.Name.StartsWith(pat);
                case SearchOptions.EndsWith:
                    return (StreetRecord r) => r.Name.EndsWith(pat);
                default:
                    return (StreetRecord r) => r.Name.Contains(pat);
            }
        }
    }
}
