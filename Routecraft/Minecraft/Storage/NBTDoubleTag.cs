using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Storage
{
    public class NBTDoubleTag : NBTValueTag<double>
    {
        public NBTDoubleTag()
        {
            this.Type = NBTTagType.Double;
        }
    }
}
