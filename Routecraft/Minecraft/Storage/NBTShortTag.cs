using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Storage
{
    public class NBTShortTag : NBTValueTag<short>
    {
        public NBTShortTag()
        {
            this.Type = NBTTagType.Short;
        }
    }
}
