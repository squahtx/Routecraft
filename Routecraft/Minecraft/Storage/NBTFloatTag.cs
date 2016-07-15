using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Storage
{
    public class NBTFloatTag : NBTValueTag<float>
    {
        public NBTFloatTag()
        {
            this.Type = NBTTagType.Float;
        }
    }
}
