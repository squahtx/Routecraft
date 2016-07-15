using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Routecraft
{
    public struct Vector3I
    {
        public int x;
        public int y;
        public int z;

        public Vector3I(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public override string ToString()
        {
            return "(" + this.x.ToString() + ", " + this.y.ToString() + ", " + this.z.ToString() + ")";
        }

        public int Volume
        {
            get
            {
                return this.x * this.y * this.z;
            }
        }

        public static implicit operator Vector3I(Vector3<int> v)
        {
            return new Vector3I(v.x, v.y, v.z);
        }

        public static Vector3I Origin = new Vector3I(0, 0, 0);
    }
}
