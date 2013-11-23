using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fontWriter
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
        public int _value { get; set; }
        public int[] _newSequence { get; set; }

        public CPacket()
        {
            this._kind = Kind.NONE;
            this._newSequence = null;

        }

        public CPacket(Kind kind, int val, List<int> sequence)
        {
            this._kind = kind;
            this._value = val;
            this._newSequence = sequence.ToArray();
            this._datasize = this._newSequence.Length;
        }
        public CPacket(Kind kind, int p1, int[] p2)
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

        public void func_SetValue(int val)
        {
            this._value = val;
        }

        public int func_GetValue()
        {
            return this._value;
        }
    }
}
