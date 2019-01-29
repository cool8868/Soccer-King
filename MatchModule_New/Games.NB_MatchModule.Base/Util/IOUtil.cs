using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Xml.Serialization;
using Games.NB.Match.Base.Interface;

namespace Games.NB.Match.Base.Util
{
    public enum EnumIOLenType
    {
        Byte = 1,
        Short = 2,
        Int = 3,
    }
    public static class IOUtil
    {
        #region Compress
        public static byte[] GZipCompress(byte[] bytes)
        {
            return GZipCompress(bytes, 0, bytes.Length);
        }
        public static byte[] GZipCompress(byte[] bytes, int index, int count)
        {
            using (var stream = new MemoryStream())
            {
                using (var zipStream = new GZipStream(stream, CompressionMode.Compress, false))
                {
                    zipStream.Write(bytes, index, count);
                }
                stream.Close();
                return stream.ToArray();
            }
        }
        public static byte[] GZipDecompress(byte[] bytes)
        {
            return GZipDecompress(bytes, 0, bytes.Length);
        }
        public static byte[] GZipDecompress(byte[] bytes, int index, int count)
        {
            using (var stream = new MemoryStream(bytes, index, count))
            {
                using (var zipStream = new GZipStream(stream, CompressionMode.Decompress, false))
                {
                    using (var dstStream = new MemoryStream())
                    {
                        ReadStream2Stream(dstStream, zipStream, new byte[bytes.Length]);
                        dstStream.Close();
                        return dstStream.ToArray();
                    }
                }
            }
        }
        public static byte[] DeflateCompress(byte[] bytes, int capacity = 65536)
        {
            return DeflateCompress(bytes, 0, bytes.Length, capacity);
        }
        public static byte[] DeflateCompress(byte[] bytes, int index, int count, int capacity = 65536)
        {
            using (var stream = new MemoryStream(capacity))
            {
                using (var zipStream = new DeflateStream(stream, CompressionMode.Compress, false))
                {
                    zipStream.Write(bytes, index, count);
                }
                stream.Close();
                return stream.ToArray();
            }
        }
        public static byte[] DeflateDecompress(byte[] bytes, int capacity = 65536)
        {
            return DeflateDecompress(bytes, 0, bytes.Length, capacity);
        }
        public static byte[] DeflateDecompress(byte[] bytes, int index, int count, int capacity = 65536)
        {
            using (var stream = new MemoryStream(bytes, index, count))
            {
                using (var zipStream = new DeflateStream(stream, CompressionMode.Decompress, false))
                {
                    using (var dstStream = new MemoryStream(capacity))
                    {
                        ReadStream2Stream(dstStream, zipStream);
                        dstStream.Close();
                        return dstStream.ToArray();
                    }
                }
            }
        }
        public static int ReadStream2Buffer(byte[] dst, int offset, int count,Stream src)
        {
            if (dst.Length - offset <= 0)
                return 0;
            int length = 0;
            int readed = 0;
            while (true)
            {
                count = Math.Min(count, dst.Length - offset);
                readed = src.Read(dst, offset, count);
                if (readed == 0)
                    break;
                offset += readed;
                length += readed;
            }
            return length;
        }
        public static int ReadStream2Stream(Stream dst, Stream src, byte[] buffer = null)
        {
            if (null == buffer)
                buffer = new byte[8192];
            int length = 0;
            int readed = 0;
            while (true)
            {
                readed = src.Read(buffer, 0, buffer.Length);
                if (readed == 0)
                    break;
                dst.Write(buffer, 0, readed);
                length += readed;
            }
            return length;
        }
        #endregion

        #region XmlIO
        public static void XmlWrite<T>(string fileName, T t)
        {
            if (File.Exists(fileName))
                File.Delete(fileName);
            using (var stream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                var xmlStream = new XmlSerializer(typeof(T));
                xmlStream.Serialize(stream, t);
                stream.Flush();
            }
        }
        #endregion

        #region BinIO
        public static void BinWrite<T>(string fileName, T t, int verNo) where T : IBinWrite
        {
            if (File.Exists(fileName))
                File.Delete(fileName);
            using (var stream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                using (var writer = new BinaryWriter(stream))
                {
                    t.BinWrite(writer, verNo);
                    stream.Flush();
                }
            }
        }
        public static byte[] BinWrite<T>(T t, int verNo, int capacity = 65536) where T : IBinWrite
        {
            using (var stream = new MemoryStream(capacity))
            {
                using (var writer = new BinaryWriter(stream))
                {
                    t.BinWrite(writer, verNo);
                    stream.Close();
                    return stream.ToArray();
                }
            }
        }
        public static T BinRead<T>(string fileName, int verNo) where T : IBinRead, new()
        {
            if (!File.Exists(fileName))
                return default(T);
            using (var stream = new FileStream(fileName, FileMode.Open))
            {
                using (var reader = new BinaryReader(stream))
                {
                    return BinRead<T>(reader, verNo);
                }
            }
        }
        public static T BinRead<T>(byte[] bytes, int verNo) where T : IBinRead, new()
        {
            using (var stream = new MemoryStream(bytes))
            {
                using (var reader = new BinaryReader(stream))
                {
                    return BinRead<T>(reader, verNo);
                }
            }
        }
        public static T BinRead<T>(BinaryReader reader, int verNo) where T : IBinRead, new()
        {
            var obj = new T();
            obj.BinRead(reader, verNo);
            return obj;
        }
        #endregion

        #region GuidBinIO
        public static void BinWriteGuid(BinaryWriter writer, Guid guid)
        {
            writer.Write(guid.ToByteArray());
        }
        public static Guid BinReadGuid(BinaryReader reader)
        {
            return new Guid(reader.ReadBytes(16));
        }
        #endregion

        #region StringBinIO
        public static void BinWriteShortUTF8(BinaryWriter writer, string val)
        {
            BinWriteString(writer, val, EnumIOLenType.Byte, Encoding.UTF8);
        }
        public static string BinReadShortUTF8(BinaryReader reader)
        {
            return BinReadString(reader, EnumIOLenType.Byte, Encoding.UTF8);
        }
        public static void BinWriteString(BinaryWriter writer, string val, EnumIOLenType lenType, Encoding encode)
        {
            int len = 0;
            byte[] bytes = null;
            if (!string.IsNullOrEmpty(val))
            {
                bytes = encode.GetBytes(val);
                len = bytes.Length;
            }
            switch (lenType)
            {
                case EnumIOLenType.Byte:
                    writer.Write((byte)len);
                    break;
                case EnumIOLenType.Short:
                    writer.Write((short)len);
                    break;
                case EnumIOLenType.Int:
                    writer.Write((int)len);
                    break;
            }
            if (len == 0)
                return;
            writer.Write(bytes);
        }
        public static string BinReadString(BinaryReader reader, EnumIOLenType lenType, Encoding encode)
        {
            int len = 0;
            switch (lenType)
            {
                case EnumIOLenType.Byte:
                    len = reader.ReadByte();
                    break;
                case EnumIOLenType.Short:
                    len = reader.ReadInt16();
                    break;
                case EnumIOLenType.Int:
                    len = reader.ReadInt32();
                    break;
            }
            if (len == 0)
                return string.Empty;
            var bytes = reader.ReadBytes(len);
            return encode.GetString(bytes);
        }
        #endregion
    }
}
