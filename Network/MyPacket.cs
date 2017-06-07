using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Equinox.Utils.DotNet;

namespace Equinox.ProceduralWorld.Utils.Network
{
    public abstract class MyPacket
    {
        public abstract void ReadFrom(MyMemoryStream stream);
        public abstract void WriteTo(MyMemoryStream stream);
    }
}
