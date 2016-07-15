using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft
{
    public class ItemDatabase
    {
        private Dictionary<ItemType, ItemDescriptor> Items = new Dictionary<ItemType, ItemDescriptor>();
        public ItemDescriptor InvalidItem { get; private set; }

        public ItemDatabase()
        {
            this.InvalidItem = new ItemDescriptor(ItemType.Invalid, false);
        }

        public void AddItem(ItemDescriptor item)
        {
            this.Items.Add(item.ItemType, item);
        }

        public ItemDescriptor GetItem(ItemType itemType)
        {
            if (!this.Items.ContainsKey(itemType))
            {
                return this.InvalidItem;
            }
            return this.Items[itemType];
        }
    }
}
