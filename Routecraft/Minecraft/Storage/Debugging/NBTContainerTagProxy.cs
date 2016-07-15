using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Routecraft.Minecraft.Storage;

namespace Routecraft.Minecraft.Storage.Debugging
{
    public class NBTContainerTagProxy
    {
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        List<NBTTag> Children = new List<NBTTag>();

        public NBTContainerTagProxy(NBTContainerTag container)
        {
            this.Children = container.Children.ToList();
        }
    }
}
