﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Nbt.Serialization;

namespace MineCase.Nbt.Tags
{
    /// <see cref="NbtTagType.ByteArray"/>
    public sealed class NbtByteArray : NbtTag
    {
        public override NbtTagType TagType => NbtTagType.ByteArray;

        public override bool HasValue => true;

        private sbyte[] _value;

        public sbyte[] Value
        {
            get => _value;
            set => _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NbtByteArray"/> class.<para />
        /// 默认构造函数.
        /// </summary>
        /// <param name="value">要初始化的值.</param>
        /// <param name="name">该 Tag 的名称.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> 为 null.</exception>
        public NbtByteArray(sbyte[] value)
        {
            Value = value;
        }

        private class Serializer : ITagSerializer
        {
            public NbtTag Deserialize(BinaryReader br)
            {
                var value = br.ReadTagBytes(br.ReadUInt16().ToggleEndian());
                return new NbtByteArray(value);
            }

            public void Serialize(NbtTag tag, BinaryWriter bw)
            {
                var nbtByteArray = (NbtByteArray)tag;
                bw.WriteTagValue(nbtByteArray.Value);
            }
        }

        internal static void RegisterSerializer()
        {
            NbtTagSerializer.RegisterTag(NbtTagType.ByteArray, new Serializer());
        }
    }
}
