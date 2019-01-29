using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Games.NB.Match.Base.Interface
{
    public interface IBinRead
    {
        void BinRead(BinaryReader reader, int verNo);
    }
    public interface IBinWrite
    {
        void BinWrite(BinaryWriter writer, int verNo);
    }
    public interface IBinIO : IBinWrite, IBinRead
    {
    }
}
