using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Storage
{
    public class NBTByteTag : NBTValueTag<byte>
    {
        public NBTByteTag()
        {
            this.Type = NBTTagType.Byte;
        }
    }
}
