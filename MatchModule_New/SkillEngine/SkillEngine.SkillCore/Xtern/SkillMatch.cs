using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.Extern;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Xtern;
using SkillEngine.SkillBase.Enum;

namespace SkillEngine.SkillCore.Xtern
{
    public class SkillMatch : ISkillMatch
    {
        #region Cache
        const int BUFFERSize = 32;
        readonly RandomHelper _random = new RandomHelper();
        readonly Dictionary<int, Dictionary<int, int>> _dicBuffer = new Dictionary<int, Dictionary<int, int>>(4);
        readonly Dictionary<int, List<int>> _listBuffer = new Dictionary<int, List<int>>(4);
        readonly Dictionary<int, int[]> _arrayBufer = new Dictionary<int, int[]>(4);
        #endregion

        #region ISkillMatch
        public ISkillManager SkillHomeManager
        {
            get;
            set;
        }
        public ISkillManager SkillAwayManager
        {
            get;
            set;
        }
        public ISkillPlayer SkillBallHandler
        {
            get;
            set;
        }
        public short RoundPerSection
        {
            get;
            set;
        }
        public short RoundPerMinute
        {
            get;
            set;
        }
        public short MatchRound
        {
            get;
            set;
        }
        public short MatchRunRound
        {
            get;
            set;
        }
        public bool SkillSkip()
        {
            return false;
        }
        public short GetBuffLast(ISkill srcSkill, ISkillPlayer caster, int last, ISkillPlayer target = null)
        {
            if (last ==(int) EnumBuffLast.Action)
            {
                if (null == caster)
                    caster = srcSkill.Owner as ISkillPlayer;
                if (null == caster)
                    return 2;
                return GetActionLast(caster.DoingState);
            }
            if (last == (int)EnumBuffLast.TillSectionEnd)
            {
                short point = srcSkill.Context.MatchRound;
                short weight = srcSkill.Context.RoundPerSection;
                return Convert.ToInt16(weight - point % weight);
            }
            if (last < 0)
                return (short)last;
            int round = last % 1000;
            int minute = last / 1000;
            return Convert.ToInt16(srcSkill.Context.RoundPerMinute * minute + round);
        }
        public static short GetActionLast(int actionState)
        {
            switch (actionState)
            {
                case 10://DefaultShootState
                case 11://ThreepointShootState
                case 12://SlamdunkState
                case 21://ReboundState
                case 24://RejectionShotState
                case 45://ReboundOffenseState
                case 46://ReboundDefenseState
                    return 4;
                case 13://FreeThrowState
                case 37://SidelineKickoffState
                case 41://InboundsState
                    return 3;
                case 14://ShortPassState
                case 15://LongPassState
                case 34://HandsShortPassState
                case 35://HandsLongPassState
                    return 2;
                default:
                    return 1;
            }
        }
        public Dictionary<int, int> DicBuffer(int hashNo)
        {
            if (hashNo <= 0)
                return new Dictionary<int, int>(BUFFERSize);
            Dictionary<int, int> buffer = null;
            if (!this._dicBuffer.TryGetValue(hashNo, out buffer))
            {
                buffer = new Dictionary<int, int>(BUFFERSize);
                this._dicBuffer[hashNo] = buffer;
            }
            return buffer;
        }
        public List<int> ListBuffer(int hashNo)
        {
            if (hashNo <= 0)
                return new List<int>(BUFFERSize);
            List<int> buffer = null;
            if (!this._listBuffer.TryGetValue(hashNo, out buffer))
            {
                buffer = new List<int>(BUFFERSize);
                this._listBuffer[hashNo] = buffer;
            }
            return buffer;
        }
        public int[] ArrayBuffer(int hashNo)
        {
            if (hashNo <= 0)
                return new int[BUFFERSize];
            int[] buffer = null;
            if (!this._arrayBufer.TryGetValue(hashNo, out buffer))
            {
                buffer = new int[BUFFERSize];
                this._arrayBufer[hashNo] = buffer;
            }
            return buffer;
        }
        public bool RandomBool()
        {
            return _random.RandomBool();
        }
        public byte RandomPercent()
        {
            return _random.RandomPercent();
        }
        public int RandomPercent(int maxPercent)
        {
            return _random.RandomInt32(0, maxPercent);
        }
        public byte RandomByte(byte min, byte max)
        {
            return _random.RandomByte(min, max);
        }
        public int RandomInt32(int min, int max)
        {
            return _random.RandomInt32(min, max);
        }
        #endregion
      
    }
}
