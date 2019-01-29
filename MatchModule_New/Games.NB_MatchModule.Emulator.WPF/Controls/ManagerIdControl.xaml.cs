using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Games.NB.Match.Emulator.WPF.Mgrs;

namespace Games.NB.Match.Emulator.WPF.Controls
{
    /// <summary>
    /// ManagerIdControl.xaml 的交互逻辑
    /// </summary>
    public partial class ManagerIdControl : UserControl
    {
        public ManagerIdControl()
        {
            InitializeComponent();
        }

        private string _npcName;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ManagerSelectWindow window = new ManagerSelectWindow();
            var result = window.ShowDialog();
            if (result.HasValue && result.Value)
            {
                ManagerId = LocalHelper.CurrentNpc.NpcId;
                IsNpc = true;
                txtHomeId.Text = LocalHelper.CurrentNpc.Name;
                _npcName = LocalHelper.CurrentNpc.Name;
            }
        }

        public Guid ManagerId { get; set; }

        public bool IsNpc { get; set; }

        public bool Check()
        {
            string name = txtHomeId.Text.Trim();
            if (IsNpc && _npcName == name)
            {
                return true;
            }
            else
            {
                IsNpc = false;
                var id = LocalHelper.GetManagerId(name);
                if (id == Guid.Empty)
                {
                    MessageBox.Show(string.Format("账号[{0}]不存在，请检查！",name));
                    return false;
                }
                ManagerId = id;
                return true;
            }
        }

        public void SetName(string name)
        {
            txtHomeId.Text = name;
        }

        private void ManagerIdControl_OnLoaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
