using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Storage
{
    public class NBTEndTag : NBTTag
    {
        public NBTEndTag()
        {
            this.Type = NBTTagType.End;
        }
    }
}
