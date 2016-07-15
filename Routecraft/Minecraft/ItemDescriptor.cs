using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft
{
    public class ItemDescriptor
    {
        public ItemType ItemType { get; private set; }
        public bool CanEnchant { get; private set; }

        public ItemDescriptor(ItemType itemType, bool canEnchant)
        {
            this.ItemType = itemType;
            this.CanEnchant = canEnchant;
        }
    }
}
