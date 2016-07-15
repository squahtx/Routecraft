using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft
{
    public struct Vector2I
    {
        public int x;
        public int y;

        public Vector2I(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2I operator +(Vector2I a, Vector2I b)
        {
            return new Vector2I(a.x + b.x, a.y + b.y);
        }

        public static Vector2I operator *(Vector2I v, int c)
        {
            return new Vector2I(v.x * c, v.y * c);
        }

        public static Vector2I operator /(Vector2I v, int d)
        {
            return new Vector2I(v.x / d, v.y / d);
        }

        public static Vector2I operator %(Vector2I v, int mod)
        {
            return new Vector2I(v.x % mod, v.y % mod);
        }

        public static Vector2I operator &(Vector2I v, int mask)
        {
            return new Vector2I(v.x & mask, v.y & mask);
        }

        public static Vector2I operator >>(Vector2I v, int shift)
        {
            return new Vector2I(v.x >> shift, v.y >> shift);
        }

        public override string ToString()
        {
            return "(" + this.x.ToString() + ", " + this.y.ToString() + ")";
        }

        public static implicit operator Vector2I(Vector2<int> v)
        {
            return new Vector2I(v.x, v.y);
        }
    }
}
