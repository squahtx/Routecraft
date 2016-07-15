using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft
{
    public struct Vector3<T>
    {
        public T x;
        public T y;
        public T z;

        public override string ToString()
        {
            return "(" + this.x.ToString() + ", " + this.y.ToString() + ", " + this.z.ToString() + ")";
        }
    }
}
