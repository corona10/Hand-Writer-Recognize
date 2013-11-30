using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FontServer
{
    class CCollectionData
    {
        public ObjectId Id { get; set; }
        public ValueKind _valkind = ValueKind.NONE;
        public int[] _sequence = null;

        public CCollectionData()
        {

        }

        public CCollectionData(ValueKind kind, int[] sqe)
        {
            this._valkind = kind;
            this._sequence = sqe;
        }
    }
}
