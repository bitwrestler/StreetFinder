namespace StreetFinder.Code
{
    public static class PhoneticHandlerFactory
    {
        public static IPhoneticHandler GetHandlerForPattern(string pattern)
        {
            return new CodeProjectSoundexSearcher(pattern);
        }
    }
}
