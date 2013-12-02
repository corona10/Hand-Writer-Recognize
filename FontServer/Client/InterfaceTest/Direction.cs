/**
 @section LICENSE
 
 The MIT License (MIT)

Copyright (c) 2013 Dong-hee,Na <corona10@gmail.com> 
                   Jun-woo, Choi <choigo92@gmail.com>  
                   Sun-min, Kim <mh5537@naver.com>

 Permission is hereby granted, free of charge, to any person obtaining a copy of 
this software and associated documentation files (the "Software"), to deal in the Software without restriction, 
including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
and/or sell copies of the Software, and to permit persons to whom the Software is 
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included
in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. 
IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, 
DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, 
 ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 * **/
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
