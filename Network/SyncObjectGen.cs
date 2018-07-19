 
 
// ReSharper disable All
using System;
using Equinox.Utils.Stream;
using Equinox.Utils.Network;
namespace Equinox.Utils
{
    public class NetworkISyncByReferenceImpl : SyncByReference, Equinox.Utils.Network.ISyncByReference
    {
        public override bool WriteChanges(MemoryStream stream, bool fullCopy = false)
        {
            var wrote = false;
            stream.Write7BitEncodedInt(0);
            return wrote;
        }
        public override void ReadChanges(MemoryStream stream)
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
    public static class SyncObjectGen
    {
        public static void RegisterAll()
        {
            SyncObjectFactory.Register<Equinox.Utils.NetworkISyncByReferenceImpl>(typeof(Equinox.Utils.Network.ISyncByReference));
        }
    }
}
