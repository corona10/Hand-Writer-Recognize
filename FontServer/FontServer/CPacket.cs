/**
 @section LICENSE
 
 The MIT License (MIT)

Copyright (c) 2013 Dong-hee,Na <corona10@gmail.com> Jun-woo, Choi <choigo92@gmail.com>  Sun-min, Kim <mh5537@naver.com>

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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FontServer
{
    class CPacket
    {
       
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
