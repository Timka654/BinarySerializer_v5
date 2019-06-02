using BinarySerializer;
using BinarySerializer.DefaultTypes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace BinarySerializer_v5.Test
{
    public class DebugTemp
    {
        public class tempStruct
        {

            [Binary(typeof(BinaryNullable<BinaryInt32,int>))]
            [BinaryScheme("default")]
            public int? q { get; set; }

            [Binary(typeof(BinaryNullable<BinaryInt32,int>))]
            [BinaryScheme("default")]
            public int? q2 { get; set; }
        }

        public class TStruct
        {
            public static void Debug()
            {

                tempStruct r = new tempStruct();

                r.q2 = 13;


                BinarySerializer.BinarySerializer bs = new BinarySerializer.BinarySerializer();

                var buf3 = bs.Serialize("default", r);


               var r1 = bs.Deserialize<tempStruct>("default", buf3);

            }
        }
    }
}
