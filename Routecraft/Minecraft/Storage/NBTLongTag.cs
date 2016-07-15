using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Storage
{
    public class NBTLongTag : NBTValueTag<long>
    {
        public NBTLongTag()
        {
            this.Type = NBTTagType.Long;
        }
    }
}
