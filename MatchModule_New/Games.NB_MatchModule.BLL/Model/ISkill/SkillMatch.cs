using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Model;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.BLL.Rules.FreeKickRules;
using SkillEngine.Extern;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Xtern;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Enum.Football;

namespace Games.NB.Match.BLL.Model
{
    public partial class MatchEntity
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
            get { return _homeManager; } 
        }
        public ISkillManager SkillAwayManager
        {
            get { return _awayManager; }
        }
        public ISkillPlayer SkillBallHandler
        {
            get { return _status.BallHandler; }
        }
        public short RoundPerSection
        {
            get { return _status.RoundPerSection; }
        }
        public short RoundPerMinute
        {
            get { return _status.RoundPerMinute; }
        }
        public short MatchRound
        {
            get { return _status.Round; }
        }
        public short MatchRunRound
        {
            get { return _status.Round; }
        }
        public bool SkillSkip()
        {
            if (_status.FoulState == EnumMatchFoulState.Foul
                && _status.FoulPlayer != null)
            {
                this.SaveRpt();
                FreeKickRuleFactory.Instance.Start(_status.FoulPlayer.Manager.Opponent, _football.Current);
                return true;
            }
            return false;
        }
        public short GetBuffLast(ISkill srcSkill, ISkillPlayer caster, int last, ISkillPlayer target = null)
        {
            switch (last)
            {
                case (int)EnumBuffLast.Action:
                    return 2;
                case (int)EnumBuffLast.TillSectionEnd:
                    short point = srcSkill.Context.MatchRound;
                    short weight = srcSkill.Context.RoundPerSection;
                    return Convert.ToInt16(weight - point % weight);
                case (int)EnumFootBallBuffLast.StunLevel1:
                    if (null == target)
                        return (short)last;
                    return Convert.ToInt16(5 + 2 * (200 / ((IPlayer)target).PropCore[PlayerProperty.Balance]));
                case (int)EnumFootBallBuffLast.StunLevel2:
                    if (null == target)
                        return (short)last;
                    return Convert.ToInt16(10 + 3 * (200 / ((IPlayer)target).PropCore[PlayerProperty.Balance]));
            }
            if (last < 0)
                return (short)last;
            int round = last % 1000;
            int minute = last / 1000;
            return Convert.ToInt16(srcSkill.Context.RoundPerMinute * minute + round);
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
