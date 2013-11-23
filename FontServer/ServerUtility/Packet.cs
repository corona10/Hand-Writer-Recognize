using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerUtility
{
    class Packet
    {
        public enum Kind
        {
            NONE,
            TRAINING_SET,
            REQUEST,
            RETURN,
        }

        public Kind _kind { get; set; }
        public int _datasize { get; set; }
        public string _value { get; set; }
        public int[] _newSequence { get; set; }

        public Packet()
        {
            this._kind = Kind.NONE;
            this._newSequence = null;
            this._value = null;
        }

        public Packet(Kind kind, string val, List<int> sequence)
        {
            this._kind = kind;
            this._value = val;
            this._newSequence = sequence.ToArray();
            this._datasize = this._newSequence.Length;
        }
        public Packet(Kind kind, string p1, int[] p2)
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

        public void func_SetValue(string val)
        {
            this._value = val;
        }

        public string func_GetValue()
        {
            return this._value;
        }
    }
}
