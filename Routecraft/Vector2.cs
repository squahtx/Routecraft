using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft
{
    public struct Vector2<T>
    {
        public T x;
        public T y;

        public Vector2(T x, T y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return "(" + this.x.ToString() + ", " + this.y.ToString() + ")";
        }
    }
}
