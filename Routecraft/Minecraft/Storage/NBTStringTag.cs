using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Storage
{
    public class NBTStringTag : NBTValueTag<string>
    {
        public NBTStringTag()
        {
            this.Type = NBTTagType.String;
        }
    }
}
