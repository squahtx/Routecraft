using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Routecraft.Minecraft.Storage.Debugging;

namespace Routecraft.Minecraft.Storage
{
    [DebuggerTypeProxy(typeof(NBTContainerTagProxy))]
    public class NBTContainerTag : NBTTag
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected List<NBTTag> children = new List<NBTTag>();

        public NBTContainerTag()
        {
        }

        public void AddChild(NBTTag tag)
        {
            this.children.Add(tag);
        }

        public T AddChild<T>()
            where T : NBTTag, new()
        {
            T Tag = new T();
            this.children.Add(Tag);

            return Tag;
        }

        public T AddChild<T>(string name)
            where T : NBTTag, new()
        {
            T Tag = new T();
            Tag.Name = name;
            this.children.Add(Tag);

            return Tag;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int ChildCount
        {
            get
            {
                return this.children.Count;
            }
        }

        public IEnumerable<NBTTag> Children
        {
            get
            {
                return this.children;
            }
        }

        public override string ToString()
        {
            if (this.Name != null && this.Name != "")
            {
                return this.Name.ToString() + " (" + this.Type.ToString() + ", Count = " + this.ChildCount + ")";
            }
            return "(" + this.Type.ToString() + ", Count = " + this.ChildCount + ")";
        }
    }
}
