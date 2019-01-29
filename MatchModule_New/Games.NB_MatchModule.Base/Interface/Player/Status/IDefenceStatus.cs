/********************************************************************************
 * �ļ�����IDefenceStatus
 * �����ˣ�
 * ����ʱ�䣺2009-12-19 14:05:18
 * �汾��1.0
 * ���ļ��汾�ţ�1.0
 * �����£�
 * ����˵����Represents the contracts of the defence status.
 * ��ʷ�޸ļ�¼��
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Games.NB.Match.Base.Interface.Player.Status
{

    /// <summary>
    /// Represents the contracts of the defence status.
    /// ��ʾ����Ա����ʱ��״̬
    /// </summary>
    public interface IDefenceStatus
    {
        /// <summary>
        /// Represents the defence target.
        /// ��ʾ�˸���Ա�ķ���Ŀ��
        /// </summary>
        IPlayer DefenceTarget { get; set; }
        /// <summary>
        /// Represents the player's defenders.
        /// ��ʾ����Ա�ķ�����
        /// </summary>
        IPlayer[] Defenders { get; }

        /// <summary>
        /// Represents the closest defender.
        /// ��ʾ����ķ�����
        /// </summary>
        IPlayer Defender { get;}
        /// <summary>
        /// ��ʾ����ķ����˵ľ���ƽ��
        /// </summary>
        double DefenderDistancePow { get; }
        /// <summary>
        /// ��ʾЭ���ķ�����
        /// </summary>
        IPlayer HelpDefender { get;  }
        /// <summary>
        /// ��ʾЭ���ķ����˵ľ���ƽ��
        /// </summary>
        double HelpDefenderDistancePow { get; }
        /// <summary>
        /// ˢ�·�����
        /// </summary>
        void RefreshDefenders(bool forceFlag = false);

        /// <summary>
        /// �ɹ����
        /// </summary>
        int SuccFlag { get; set; }
        /// <summary>
        /// ��ʼ�ɹ���
        /// </summary>
        int RawSuccRate { get; set; }
        /// <summary>
        /// ���ճɹ���
        /// </summary>
        int NewSuccRate { get; set; }

    }
}
