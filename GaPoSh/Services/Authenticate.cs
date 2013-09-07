using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GaPoSh.Data;

namespace GaPoSh.Services
{
    class Authenticate
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
