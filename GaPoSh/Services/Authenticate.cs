using GaPoSh.Data;

namespace GaPoSh.Services
{
    internal class Authenticate
    {
        public static Instance ServiceAccount(ServiceAuth auth)
        {
            return new Instance(auth);
        }

        public static Instance SimpleAccount(SimpleAuth auth)
        {
            return new Instance(auth);
        }
    }
}