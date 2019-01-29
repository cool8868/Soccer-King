using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillEngine.Extern
{
    public interface IRandom
    {
        bool RandomBool();
        byte RandomPercent();
        int RandomPercent(int maxPercent);
        byte RandomByte(byte min, byte max);
        int RandomInt32(int min, int max);
    }
}
