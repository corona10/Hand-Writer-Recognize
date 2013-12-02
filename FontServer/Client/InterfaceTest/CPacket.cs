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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfaceTest
{
    class CPacket
    {
        public enum ValueKind
        {
            ZERO = 0,
            ONE = 1,
            TWO = 2,
            THREE = 3,
            FOUR = 4,
            FIVE = 5,
            SIX = 6,
            SEVEN = 7,
            EIGHT = 8,
            NINE = 9,
            A = 10,
            B = 11,
            C = 12,
            D = 13,
            E = 14,
            F = 15,
            G = 16,
            H = 17,
            I = 18,
            J = 19,
            K = 20,
            L = 21,
            M = 22,
            N = 23,
            O = 24,
            P = 25,
            Q = 26,
            R = 27,
            S = 28,
            T = 29,
            U = 30,
            V = 31,
            W = 32,
            X = 33,
            Y = 34,
            Z = 35,
            a = 36,
            b = 37,
            c = 38,
            d = 39,
            e = 40,
            f = 41,
            g = 42,
            h = 43,
            i = 44,
            j = 45,
            k = 46,
            l = 47,
            m = 48,
            n = 49,
            o = 50,
            p = 51,
            q = 52,
            r = 53,
            s = 54,
            t = 55,
            u = 56,
            v = 57,
            w = 58,
            x = 59,
            y = 60,
            z = 61,
            NONE = 256
        }
        public enum Kind
        {
            NONE,
            TRAINING_SET,
            REQUEST,
            RETURN,
        }
        enum Direction
        {
            up, down, right, left
        };

        public Kind _kind { get; set; }
        public int _datasize { get; set; }
        public ValueKind _value { get; set; }
        public int[] _newSequence { get; set; }

        public CPacket()
        {
            this._kind = Kind.NONE;
            this._newSequence = null;

        }

        public CPacket(Kind kind, ValueKind val, List<int> sequence)
        {
            this._kind = kind;
            this._value = val;
            this._newSequence = sequence.ToArray();
            this._datasize = this._newSequence.Length;
        }
        public CPacket(Kind kind, ValueKind p1, int[] p2)
        {
            // TODO: Complete member initialization
            this._kind = kind;
            this._value = p1;
            this._newSequence = p2;
            this._datasize = p2.Length;
        }

        public void func_SetKind(Kind kind)
        {
            this._kind = kind;
        }
        public Kind func_GetKind()
        {
            return this._kind;
        }

        public void func_SetSequence(List<int> sequence)
        {
            this._newSequence = sequence.ToArray();
        }

        public int[] func_GetSequence()
        {
            return this._newSequence;
        }

        public void func_SetValue(ValueKind val)
        {
            this._value = val;
        }

        public ValueKind func_GetValue()
        {
            return this._value;
        }
    }
}
