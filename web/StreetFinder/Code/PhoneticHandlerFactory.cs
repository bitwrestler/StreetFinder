using System.Runtime.CompilerServices;

namespace StreetFinder.Code
{
    public class PhoneticHandlerFactory
    {
        private Func<string, IPhoneticHandler> handlerProvider;

        public PhoneticHandlerFactory()
        {
            handlerProvider = (pattern) => new CodeProjectSoundexSearcher(pattern);
        }

        public PhoneticHandlerFactory(Func<string, IPhoneticHandler> handlerProvider)
        {
            this.handlerProvider = handlerProvider;
        }

        public IPhoneticHandler GetHandlerForPattern(string origDataWord)
        {
            return this.handlerProvider(origDataWord);
        }
    }
}
