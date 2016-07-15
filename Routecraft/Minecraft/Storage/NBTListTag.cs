using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Storage
{
    public class NBTListTag : NBTContainerTag
    {
        public NBTTagType ChildrenType;

        public NBTListTag()
        {
            this.Type = NBTTagType.List;
            this.ChildrenType = NBTTagType.Compound;
        }
    }
}
