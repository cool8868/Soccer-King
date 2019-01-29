using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Interface;
using System.Configuration;

namespace Games.NB.Match.Base.Model
{
    public class PropertyTraceModel
    {
        public PropertyTraceModel(double finalValue, double baseValue, double buffPercent, double buffPoint)
        {
            this.FinalValue = finalValue;
            this.BaseValue = baseValue;
            this.BuffPercent = buffPercent;
            this.BuffPoint = buffPoint;
        }
        public double FinalValue;
        public double BaseValue;
        public double BuffPercent;
        public double BuffPoint;
    }
    public class PropertyTraceCore
    {
        #region Cache
        public static readonly bool TRACEBuffFlag = false;
        IPlayer _player = null;
        Dictionary<int, Dictionary<int, PropertyTraceModel>> _dicTrace = new Dictionary<int, Dictionary<int, PropertyTraceModel>>();
        #endregion

        #region .ctor
        static PropertyTraceCore()
        {
            string cfg = ConfigurationManager.AppSettings["TRACEBuffFlag"] ?? string.Empty;
            bool val = TRACEBuffFlag;
            if (!bool.TryParse(cfg, out TRACEBuffFlag))
                TRACEBuffFlag = val;
        }
        public PropertyTraceCore(IPlayer player)
        {
            this._player = player;
        }
        #endregion

        #region Facade
        public void Trace(int buffId, double finalValue, double baseValue = 0, double buffPercent = 0, double buffPoint = 0)
        {
            int round = _player.Match.Status.Round;
            Dictionary<int, PropertyTraceModel> dicBuff;
            if (!_dicTrace.TryGetValue(round, out dicBuff))
            {
                dicBuff = new Dictionary<int, PropertyTraceModel>();
                _dicTrace[round] = dicBuff;
            }
            dicBuff[buffId] = new PropertyTraceModel(finalValue, baseValue, buffPercent, buffPoint);
        }
        public void TraceFull()
        {
            int round = _player.Match.Status.Round;
            Dictionary<int, PropertyTraceModel> dicBuff;
            if (!_dicTrace.TryGetValue(round, out dicBuff))
            {
                dicBuff = new Dictionary<int, PropertyTraceModel>();
                _dicTrace[round] = dicBuff;
            }
            int buffId = 0;
            foreach (int propId in PropertyCore.BASEPropertyIds)
            {
                buffId = propId < 20 ? (propId + 1000) : propId;
                if (!_dicTrace.ContainsKey(buffId))
                    _player.PropCore[buffId].GetType();
            }
            int stateId = _player.Status.State.ClientId;
            if (stateId == 17 || stateId == 18 || stateId == 20)
                dicBuff[PlayerProperty.ShootingDist] = new PropertyTraceModel(_player.Side == Side.Home ? (210 - _player.Current.X) : _player.Current.X, 0, 0, 0);
            else if (stateId == 23)
                dicBuff[PlayerProperty.ShootingDist] = new PropertyTraceModel(_player.Side == Side.Home ? _player.Current.X : (210 - _player.Current.X), 0, 0, 0);
        }
        public Dictionary<int, Dictionary<int, PropertyTraceModel>> TraceReport
        {
            get { return _dicTrace; }
        }
        public double this[int buffId]
        {
            get
            {
                return this[_player.Match.Status.Round, buffId];
            }
        }
        public double this[int round,int buffId]
        {
            get
            {
                Dictionary<int, PropertyTraceModel> dic = null;
                if (!_dicTrace.TryGetValue(round, out dic))
                    return 0;
                PropertyTraceModel model;
                if (!dic.TryGetValue(buffId, out model) || null == model)
                    return 0;
                return model.FinalValue;
            }
        }
        #endregion
    }
}
