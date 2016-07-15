using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft
{
    public class MinecraftItemDatabase : ItemDatabase
    {
        public MinecraftItemDatabase()
        {
            this.AddItem(new ItemDescriptor(ItemType.FlintAndSteel, true));
            this.AddItem(new ItemDescriptor(ItemType.Bow, true));
            this.AddItem(new ItemDescriptor(ItemType.FishingRod, true));
            this.AddItem(new ItemDescriptor(ItemType.Shears, true));
            
            this.AddItem(new ItemDescriptor(ItemType.WoodSword, true));
            this.AddItem(new ItemDescriptor(ItemType.WoodShovel, true));
            this.AddItem(new ItemDescriptor(ItemType.WoodPickaxe, true));
            this.AddItem(new ItemDescriptor(ItemType.WoodAxe, true));
            this.AddItem(new ItemDescriptor(ItemType.WoodHoe, true));
            
            this.AddItem(new ItemDescriptor(ItemType.StoneSword, true));
            this.AddItem(new ItemDescriptor(ItemType.StoneShovel, true));
            this.AddItem(new ItemDescriptor(ItemType.StonePickaxe, true));
            this.AddItem(new ItemDescriptor(ItemType.StoneAxe, true));
            this.AddItem(new ItemDescriptor(ItemType.StoneHoe, true));
            
            this.AddItem(new ItemDescriptor(ItemType.IronSword, true));
            this.AddItem(new ItemDescriptor(ItemType.IronShovel, true));
            this.AddItem(new ItemDescriptor(ItemType.IronPickaxe, true));
            this.AddItem(new ItemDescriptor(ItemType.IronAxe, true));
            this.AddItem(new ItemDescriptor(ItemType.IronHoe, true));
            
            this.AddItem(new ItemDescriptor(ItemType.DiamondSword, true));
            this.AddItem(new ItemDescriptor(ItemType.DiamondShovel, true));
            this.AddItem(new ItemDescriptor(ItemType.DiamondPickaxe, true));
            this.AddItem(new ItemDescriptor(ItemType.DiamondAxe, true));
            this.AddItem(new ItemDescriptor(ItemType.DiamondHoe, true));
            
            this.AddItem(new ItemDescriptor(ItemType.GoldSword, true));
            this.AddItem(new ItemDescriptor(ItemType.GoldShovel, true));
            this.AddItem(new ItemDescriptor(ItemType.GoldPickaxe, true));
            this.AddItem(new ItemDescriptor(ItemType.GoldAxe, true));
            this.AddItem(new ItemDescriptor(ItemType.GoldHoe, true));
            
            this.AddItem(new ItemDescriptor(ItemType.LeatherHelmet, true));
            this.AddItem(new ItemDescriptor(ItemType.LeatherChestplate, true));
            this.AddItem(new ItemDescriptor(ItemType.LeatherLeggings, true));
            this.AddItem(new ItemDescriptor(ItemType.LeatherBoots, true));

            this.AddItem(new ItemDescriptor(ItemType.ChainHelmet, true));
            this.AddItem(new ItemDescriptor(ItemType.ChainChestplate, true));
            this.AddItem(new ItemDescriptor(ItemType.ChainLeggings, true));
            this.AddItem(new ItemDescriptor(ItemType.ChainBoots, true));

            this.AddItem(new ItemDescriptor(ItemType.IronHelmet, true));
            this.AddItem(new ItemDescriptor(ItemType.IronChestplate, true));
            this.AddItem(new ItemDescriptor(ItemType.IronLeggings, true));
            this.AddItem(new ItemDescriptor(ItemType.IronBoots, true));
            
            this.AddItem(new ItemDescriptor(ItemType.DiamondHelmet, true));
            this.AddItem(new ItemDescriptor(ItemType.DiamondChestplate, true));
            this.AddItem(new ItemDescriptor(ItemType.DiamondLeggings, true));
            this.AddItem(new ItemDescriptor(ItemType.DiamondBoots, true));
            
            this.AddItem(new ItemDescriptor(ItemType.GoldHelmet, true));
            this.AddItem(new ItemDescriptor(ItemType.GoldChestplate, true));
            this.AddItem(new ItemDescriptor(ItemType.GoldLeggings, true));
            this.AddItem(new ItemDescriptor(ItemType.GoldBoots, true));
        }
    }
}
