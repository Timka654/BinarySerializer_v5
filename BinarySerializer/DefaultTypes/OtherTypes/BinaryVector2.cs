﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Linq;
using GrEmit;
using GrEmit.Utils;
#if NOT_UNITY
using System.Numerics;
#else
using UnityEngine;
#endif

namespace BinarySerializer.DefaultTypes
{
    public class BinaryVector2 : IBasicType
    {
        public Type CompareType => typeof(Vector2);

        private MethodInfo writeBitConverterMethodInfo;

        private MethodInfo readBitConverterMethodInfo;

        private ConstructorInfo initialConstructor;

        private FieldInfo xField;

        private FieldInfo yField;

        public BinaryVector2()
        {
#if NOT_UNITY
            xField = typeof(Vector2).GetField("X");
            yField = typeof(Vector2).GetField("Y");
#else
            
            xField = typeof(Vector2).GetField("x");
            yField = typeof(Vector2).GetField("y");
#endif

            initialConstructor = typeof(Vector2).GetConstructor(new Type[] { typeof(float), typeof(float) });
            writeBitConverterMethodInfo = typeof(BitConverter).GetMethod("GetBytes",new Type[] { typeof(float) });
            readBitConverterMethodInfo = typeof(BitConverter).GetMethod("ToSingle", new Type[] { typeof(byte[]), typeof(int) });

        }

        public void GetReadILCode(PropertyData prop, BinaryStruct currentStruct, GroboIL il, GroboIL.Local binaryStruct, GroboIL.Local buffer, GroboIL.Local result, GroboIL.Local typeSize, GroboIL.Local offset, bool listValue)
        {
            var r = il.DeclareLocal(typeof(Vector2));

            if (listValue)
                il.Ldloca(result);
            else
                il.Ldloca(r);

            il.Ldloc(buffer);
            il.Ldloc(offset);
            il.Call(readBitConverterMethodInfo);

            il.Ldloc(buffer);
            il.Ldloc(offset);
            il.Ldc_I4(4);
            il.Add();
            il.Call(readBitConverterMethodInfo);
            il.Call(initialConstructor);

            //if (listValue)
            //    il.Stloc(result);
            //else
            //    il.Stloc(r);

            BinaryStruct.WriteOffsetAppend(il, offset, 8);

            if (!listValue)
            {
                il.Ldloc(result);
                il.Ldloc(r);
                il.Call(prop.Setter, isVirtual: true);
            }
        }

        public void GetWriteILCode(PropertyData prop, BinaryStruct currentStruct, GroboIL il, GroboIL.Local binaryStruct, GroboIL.Local value, GroboIL.Local typeSize, GroboIL.Local buffer, GroboIL.Local offset, bool listValue)
        {
            BinaryStruct.WriteSizeChecker(il, buffer, offset, 8);
            var v = il.DeclareLocal(typeof(Vector2)); 
            var arr = il.DeclareLocal(typeof(byte[]));

            il.Ldloc(value);
            if (!listValue)
                il.Call(prop.Getter);
            il.Stloc(v);
            il.Ldloca(v);
            //il.Dup();
            //il.Box(typeof(Vector2));
            il.Ldfld(xField);

            //il.Dup();
            //il.Pop();
            il.Call(writeBitConverterMethodInfo);
            il.Stloc(arr);

            il.Ldloc(buffer);
            il.Ldloc(offset);
            il.Ldloc(arr);
            il.Ldc_I4(0);
            il.Ldelem(typeof(byte));
            il.Stelem(typeof(byte));

            for (int i = 1; i < 4; i++)
            {
                il.Ldloc(buffer);
                il.Ldloc(offset);
                il.Ldc_I4(i);
                il.Add();
                il.Ldloc(arr);
                il.Ldc_I4(i);
                il.Ldelem(typeof(byte));
                il.Stelem(typeof(byte));
            }
            BinaryStruct.WriteOffsetAppend(il, offset, 4);

            il.Ldloca(v);

            il.Ldfld(yField);
            il.Call(writeBitConverterMethodInfo);
            il.Stloc(arr);

            il.Ldloc(buffer);
            il.Ldloc(offset);
            il.Ldloc(arr);
            il.Ldc_I4(0);
            il.Ldelem(typeof(byte));
            il.Stelem(typeof(byte));

            for (int i = 1; i < 4; i++)
            {
                il.Ldloc(buffer);
                il.Ldloc(offset);
                il.Ldc_I4(i);
                il.Add();
                il.Ldloc(arr);
                il.Ldc_I4(i);
                il.Ldelem(typeof(byte));
                il.Stelem(typeof(byte));
            }
            BinaryStruct.WriteOffsetAppend(il, offset, 4);
        }
    }
}
