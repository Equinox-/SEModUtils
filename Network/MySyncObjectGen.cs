 
 
// ReSharper disable All
using System;
using Equinox.Utils.Stream;
using Equinox.Utils.Network;
namespace Equinox.Utils
{
    public class NetworkMySyncByReferenceImpl : MySyncByReference, Equinox.Utils.Network.IMySyncByReference
    {
        public override bool WriteChanges(MyMemoryStream stream, bool fullCopy = false)
        {
            var wrote = false;
            stream.Write7BitEncodedInt(0);
            return wrote;
        }
        public override void ReadChanges(MyMemoryStream stream)
        {
            while(true) {
                var id = stream.Read7BitEncodedInt();
                if (id == 0) break;
                switch (id)
                {
                    default:
                        throw new ArgumentException($"Badly formatted ID ({id}) for {GetType()}");
                }
            }
        }
    }
}
namespace Equinox.Utils.Network
{
    public static class MySyncObjectGen
    {
        public static void RegisterAll()
        {
            MySyncObjectFactory.Register<Equinox.Utils.NetworkMySyncByReferenceImpl>(typeof(Equinox.Utils.Network.IMySyncByReference));
        }
    }
}
