using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Storage
{
    public class NBTIntTag : NBTValueTag<int>
    {
        public NBTIntTag()
        {
            this.Type = NBTTagType.Int;
        }
    }
}
