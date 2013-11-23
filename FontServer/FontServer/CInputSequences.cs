using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FontServer
{
    class CInputSequences
    {
        public ObjectId Id
        {
            get;
            set;
        }

        public int[][] InputSequence
        {
            get;
            set;
        }

        public override string ToString()
        {
            string tmp = "";

            for (int i = 0; i < InputSequence[0].Length; i++)
                tmp = tmp + " " + InputSequence[0][i];


            return tmp;
        }
    }
}
