/*****************************************************************************
 * 文件名：Player.xaml.cs
 * 
 * 创建人：
 * 
 * 创建时间：2009-12-9 16:30:39
 * 
 * 功能说明：Represents the main window.
 *  
 * 历史修改记录：
 * 
 * ***************************************************************************/
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Emulator.WPF.Mgrs;

namespace Games.NB.Match.Emulator.WPF
{

    /// <summary>
    /// Interaction logic for Player.xaml    
    /// </summary>
    public partial class Player : UserControl
    {
        public Player()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 球员颜色
        /// </summary>
        public SolidColorBrush PlayerColor
        {
            set
            {
                this.ellipsePlayer.SetValue(Shape.FillProperty, value);
            }
        }

        /// <summary>
        /// 球员角度
        /// </summary>
        public double Angle
        {
            set
            {
                (this.FindName("R1") as RotateTransform).Angle = value;
            }
        }

        /// <summary>
        /// 球员技能
        /// </summary>
        public string Skill
        {
            get { return this.skill.Content.ToString(); }
            set { this.skill.Content = value; }
        }

        /// <summary>
        /// Represents the player's client Id.
        /// 球员的客户端编号
        /// </summary>
        public int ClientId
        {
            set
            {
                this.position.Content += ("_" + value);
            }
        }

        /// <summary>
        /// 标记名称
        /// </summary>
        /// <param name="isHome">是否是主队</param>
        /// <param name="isBallHandler">是否是持球人</param>
        public void MarkName(bool isHome, bool isBallHandler)
        {
            this.position.SetValue(Label.FontWeightProperty, FontWeights.Bold);

            if (isHome)
            {
                this.position.SetValue(Label.ForegroundProperty, new SolidColorBrush(Colors.Red));
            }
            else
            {
                this.position.SetValue(Label.ForegroundProperty, new SolidColorBrush(Colors.Blue));
            }

            if (isBallHandler)
            {
                this.linePlayer.Stroke = new SolidColorBrush(Colors.White);
            }
        }

        /// <summary>
        /// 将球员名称标记为正常
        /// </summary>
        public void MarkNormal()
        {
            this.position.SetValue(Label.FontWeightProperty, FontWeights.Normal);
            this.position.SetValue(Label.ForegroundProperty, new SolidColorBrush(Colors.Black));
            this.linePlayer.Stroke = new SolidColorBrush(Colors.Black);
        }

        /// <summary>
        /// 标记为黄牌
        /// </summary>
        public void MarkYellowCard()
        {
            this.ellipsePlayer.Stroke = new SolidColorBrush(Colors.Yellow);
            this.linePlayer.Stroke = new SolidColorBrush(Colors.Yellow);
        }

        /// <summary>
        /// 标记为受伤或下场
        /// </summary>
        public void Dead()
        {
            this.ellipsePlayer.Fill = new SolidColorBrush(Colors.Black);
            this.linePlayer.Stroke = new SolidColorBrush(Colors.Black);
        }
        /// <summary>
        /// 球员上场
        /// </summary>
        /// <param name="homeFlag"></param>
        /// <param name="yellowFlag"></param>
        public void Active(bool homeFlag, bool yellowFlag)
        {
            if (yellowFlag)
                this.ellipsePlayer.Fill = new SolidColorBrush(Colors.Yellow);
            else
                this.ellipsePlayer.Fill = new SolidColorBrush(homeFlag ? Colors.Red : Colors.Blue);
            this.linePlayer.Stroke = new SolidColorBrush(yellowFlag ? Colors.Yellow : Colors.Black);
        }
        /// <summary>
        /// 球员重置
        /// </summary>
        /// <param name="color"></param>
        public void Reset(System.Windows.Media.Color color)
        {
            this.ellipsePlayer.Fill = new SolidColorBrush(color);
            this.ellipsePlayer.Stroke = new SolidColorBrush(Colors.Black);
            this.linePlayer.Stroke = new SolidColorBrush(Colors.Black);
            MarkNormal();
        }

        /// <summary>
        /// 显示名称
        /// </summary>
        /// <param name="position">位置</param>
        /// <param name="clientId">id</param>
        /// <param name="name">球员名称</param>
        /// <param name="visiblePosition">是否显示位置</param>
        public void DisplayName(Position position, byte clientId, string name, bool visiblePosition)
        {
            this.playername.Content = name;
            this.position.Content = clientId+"|"+EmulatorHelper.GetPositionStr((int)position);
        }

        public void SetTipStatus(bool showState, bool showPosition, bool showDefense, bool showName)
        {
            this.position.Visibility = showPosition ? Visibility.Visible : Visibility.Hidden;
            this.state.Visibility = showState ? Visibility.Visible : Visibility.Hidden;
            this.playername.Visibility = showName ? Visibility.Visible : Visibility.Hidden;
        }

        /// <summary>
        /// 显示球员的当前状态
        /// </summary>
        /// <param name="state">状态的编号</param>
        public void DisplayState(int state)
        {
            if (state == 0)
            {
                this.state.Content = "跑动";
                return;
            }

            if (state == 1)
            {
                this.state.Content = "扑救";
                return;
            }

            if (state == 2)
            {
                this.state.Content = "站立";
                return;
            }

            if (state == 3)
            {
                this.state.Content = "停球";
                return;
            }

            if (state == 4)
            {
                this.state.Content = "抢断";
                return;
            }

            if (state == 5)
            {
                this.state.Content = "铲球";
                return;
            }

            if (state == 6)
            {
                this.state.Content = "带球";
                return;
            }

            if (state == 7)
            {
                this.state.Content = "长传";
                return;
            }

            if (state == 8)
            {
                this.state.Content = "短传";
                return;
            }

            if (state == 9)
            {
                this.state.Content = "射门";
                return;
            }

            if (state == 10)
            {
                this.state.Content = "大力抽射";
                return;
            }

            if (state == 11)
            {
                this.state.Content = "过人";
                return;
            }
            if (state == 12)
            {
                this.state.Content = "走动";
                return;
            }
            if (state == 13)
            {
                this.state.Content = "边线手抛球";
                return;
            }
            if (state == 14)
            {
                this.state.Content = "头球争顶";
                return;
            }
            if (state == 15)
            {
                this.state.Content = "倒挂金钩";
                return;
            }
            if (state == 16)
            {
                this.state.Content = "扑救成功";
                return;
            }
            if (state == 17)
            {
                this.state.Content = "乌龙射门";
                return;
            }
            if (state == 18)
            {
                this.state.Content = "头球传球";
                return;
            }
            if (state == 19)
            {
                this.state.Content = "头球射门";
                return;
            }
            if (state == 20)
            {
                this.state.Content = "头球接球";
                return;
            }
        }
    }
}
