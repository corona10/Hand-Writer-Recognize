using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FontServer
{
    class ValueKindMap
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
            q = 53,
            r = 54,
            s = 55,
            t = 56,
            u = 57,
            v = 58,
            w = 59,
            x = 60,
            y = 61,
            z = 62,
            NONE = 256
        }

        static public ValueKind mapping(int value)
        {
            ValueKind _value = (ValueKind)value;

            return _value;
        }
    }
}
