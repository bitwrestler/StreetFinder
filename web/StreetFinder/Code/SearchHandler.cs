using System.Text.RegularExpressions;

using evaluatorDelg = System.Func<StreetFinder.Code.StreetCollection.SearchStruct, bool>;

namespace StreetFinder.Code
{
    public class SearchHandler
    {
        private evaluatorDelg _func;
       
        public SearchHandler(string pattern, SearchOptions options) {
            var pat = pattern.ToUpper();
            this._func = MakeHandler(pat,options);
        }

        public bool Search(StreetCollection.SearchStruct record)
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
            if (options == SearchOptions.Phonetic)
            {
                return (StreetCollection.SearchStruct r) => r.PhoneticHandler.CompareTo(pat);
            }

            if (TryPrepareWildcard(pat, options, out Regex? repattern) && repattern is not null)
            { 
                return (StreetCollection.SearchStruct r) => repattern.IsMatch(r.Street.Name);
            }
            switch(options)
            {
                case SearchOptions.StartsWith:
                    return (StreetCollection.SearchStruct r) => r.Street.Name.StartsWith(pat);
                case SearchOptions.EndsWith:
                    return (StreetCollection.SearchStruct r) => r.Street.Name.EndsWith(pat);
                default:
                    return (StreetCollection.SearchStruct r) => r.Street.Name.Contains(pat);
            }
        }
    }
}
