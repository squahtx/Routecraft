using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Storage
{
    public class NBTTag
    {
        public delegate void NameChangedHandler(NBTTag Tag, string NewName);
        public event NameChangedHandler NameChanged;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string name;
        public NBTTagType Type { get; protected set; }

        protected void DispatchNameChanged(string newName)
        {
            if (this.NameChanged != null)
            {
                this.NameChanged(this, newName);
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (this.name == value)
                {
                    return;
                }
                this.name = value;
                this.DispatchNameChanged(this.name);
            }
        }

        public override string ToString()
        {
            if (this.Name != null && this.Name != "")
            {
                return this.Name.ToString() + " (" + this.Type.ToString() + ")";
            }
            return "(" + this.Type.ToString() + ")";
        }
    }
}
