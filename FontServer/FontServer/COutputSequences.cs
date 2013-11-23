using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FontServer
{
    class COutputSequences
    {
        public ObjectId Id
        {
            get;
            set;
        }

        public int[] OutputSequence
        {
            get;
            set;
        }
    }
}
