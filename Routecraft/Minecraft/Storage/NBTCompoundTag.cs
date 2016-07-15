using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Storage
{
    public class NBTCompoundTag : NBTContainerTag
    {
        public NBTCompoundTag()
        {
            this.Type = NBTTagType.Compound;
        }

        public NBTTag GetChild(string name)
        {
            return this.Children.FirstOrDefault(x => x.Name == name);
        }

        public T GetValue<T>(string name)
        {
            foreach (NBTTag Tag in this.Children)
            {
                if (Tag.Name == name)
                {
                    if (Tag is NBTValueTag)
                    {
                        NBTValueTag ValueTag = (NBTValueTag)Tag;
                        return (T)ValueTag.ValueObject;
                    }
                    else
                    {
                        return default(T);
                    }
                }
            }
            return default(T);
        }
    }
}
