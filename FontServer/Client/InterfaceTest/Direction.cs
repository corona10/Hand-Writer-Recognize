using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceTest
{
    class Direction
    {
        public static int GetDirection(int x1, int y1, int x2, int y2)
        {
            double x3, y3;
            x3 = x2 - x1;
            y3 = y1 - y2;
            double angle = Math.Atan((double)y3 / (double)Math.Abs(x3));
            if (x3 < 0)
            {
                if (angle < -Math.PI * 3 / 8)
                {
                    return 6;
                }
                else if (angle >= -Math.PI * 3 / 8 && angle < -Math.PI / 8)
                {
                    return 5;
                }
                else if (angle >= -Math.PI / 8 && angle < Math.PI / 8)
                {
                    return 4;
                }
                else if (angle >= Math.PI / 8 && angle < Math.PI * 3 / 8)
                {
                    return 3;
                }
                else if (angle >= Math.PI * 3 / 8)
                {
                    return 2;
                }
            }
            else
            {
                if (angle < -Math.PI * 3 / 8)
                {
                    return 6;
                }
                else if (angle >= -Math.PI * 3 / 8 && angle < -Math.PI / 8)
                {
                    return 7;
                }
                else if (angle >= -Math.PI / 8 && angle < Math.PI / 8)
                {
                    return 0;
                }
                else if (angle >= Math.PI / 8 && angle < Math.PI * 3 / 8)
                {
                    return 1;
                }
                else if (angle >= Math.PI * 3 / 8)
                {
                    return 2;
                }
            }

            return -1;
        }
    }
}
