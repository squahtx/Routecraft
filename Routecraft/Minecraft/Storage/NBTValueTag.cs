using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft.Storage
{
    public class NBTValueTag : NBTTag
    {
        public delegate void ValueChangedHandler(NBTValueTag Tag, object NewValue);
        public event ValueChangedHandler ValueChanged;

        virtual public object ValueObject { get; set; }
        virtual public Type ValueType { get { return null; } }

        protected void DispatchValueChanged(object NewValue)
        {
            if (this.ValueChanged != null)
            {
                this.ValueChanged(this, NewValue);
            }
        }
    }

    public class NBTValueTag<T> : NBTValueTag
    {
        private T value;
        public T Value
        {
            get
            {
                return this.value;
            }
            set
            {
                if (this.value != null && this.value.Equals(value))
                {
                    return;
                }
                this.value = value;
                this.DispatchValueChanged(this.ValueObject);
            }
        }

        public override object ValueObject
        {
            get
            {
                return this.value;
            }
            set
            {
                this.Value = (T)value;
            }
        }

        public override Type ValueType
        {
            get
            {
                return typeof(T);
            }
        }

        public override string ToString()
        {
            if (this.Name != null)
            {
                return this.Name.ToString() + " (" + this.Type.ToString() + "): " + this.Value.ToString();
            }
            return "(" + this.Type.ToString() + "): " + this.Value.ToString();
        }
    }
}
