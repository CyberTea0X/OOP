using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ПР1_проект
{
    internal class Vec2
    {
        protected float x, y;
        public Vec2(float x, float y) {
            this.x = x;
            this.y = y;
        }
        public static explicit operator int[](Vec2 param)
        {
            int[] output = { (int) param.x, (int) param.y };
            return output;
        }
        public static implicit operator float[](Vec2 param)
        {
            float[] output = { param.x, param.y };
            return output;
        }
    }
}
