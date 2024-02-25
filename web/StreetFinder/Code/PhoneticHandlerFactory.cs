namespace StreetFinder.Code
{
    public static class PhoneticHandlerFactory
    {
        public static IPhoneticHandler GetHandlerForPattern(string origDataWord)
        {
            return new CodeProjectSoundexSearcher(origDataWord);
        }
    }
}
