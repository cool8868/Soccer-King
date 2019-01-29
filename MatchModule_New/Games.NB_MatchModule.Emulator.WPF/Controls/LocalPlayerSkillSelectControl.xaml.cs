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
using Games.NB.Match.Emulator.WPF.Entity;
using Games.NB.Match.Emulator.WPF.Mgrs;

namespace Games.NB.Match.Emulator.WPF.Controls
{
    /// <summary>
    /// LocalPlayerSkillSelectControl.xaml 的交互逻辑
    /// </summary>
    public partial class LocalPlayerSkillSelectControl : UserControl
    {
        private bool _isInit = false;
        public LocalPlayerSkillSelectControl()
        {
            InitializeComponent();
        }

        public void Init(string header,string skillId)
        {
            groupSkill.Header = header;
            BindControl();
            BindPlayerSkill(cmbSkill, 0, 0, 0);
            ComboBoxHelper.SetSelectItem(cmbSkill,skillId);
            _isInit = true;
        }

        public string GetSkill()
        {
            return ComboBoxHelper.GetSelectValue(cmbSkill);
        }

        public void BindPlayerSkill(ComboBox cmb, int type, int level, int color)
        {
            var allskill = LocalHelper.LocalCache.Skillcards;
            var list = new List<KeyValueComboBoxItem>();
            list.Add(new KeyValueComboBoxItem("", "无"));
            foreach (var entity in allskill)
            {
                if (type != 0 && entity.ActType != type)
                    continue;
                if (level != 0 && entity.SkillLevel != level)
                    continue;
                if (color != 0 && entity.SkillClass != color)
                    continue;
                list.Add(new KeyValueComboBoxItem(entity.SkillCode, entity.ItemName));
            }
            cmb.ItemsSource = list;
            cmb.SelectedIndex = 0;
        }
      

        void BindControl()
        {
            var list = new List<KeyValueComboBoxItem>(4);
            list.Add(new KeyValueComboBoxItem("0", "所有类型"));
            list.Add(new KeyValueComboBoxItem("1", "射门"));
            list.Add(new KeyValueComboBoxItem("2", "过人"));
            list.Add(new KeyValueComboBoxItem("3", "防守"));
            list.Add(new KeyValueComboBoxItem("4", "组织"));
            list.Add(new KeyValueComboBoxItem("5", "守门"));
            cmbType.ItemsSource = list;
            cmbType.SelectedIndex = 0;

            var list2 = new List<KeyValueComboBoxItem>(5);
            list2.Add(new KeyValueComboBoxItem("0", "所有颜色"));
            list2.Add(new KeyValueComboBoxItem("4", "橙"));
            list2.Add(new KeyValueComboBoxItem("3", "紫"));
            list2.Add(new KeyValueComboBoxItem("2", "蓝"));
            list2.Add(new KeyValueComboBoxItem("1", "绿"));
            cmbColor.ItemsSource = list2;
            cmbColor.SelectedIndex = 0;

            var list3 = new List<KeyValueComboBoxItem>(6);
            list3.Add(new KeyValueComboBoxItem("0", "所有等级"));
            list3.Add(new KeyValueComboBoxItem("1", "1"));
            list3.Add(new KeyValueComboBoxItem("2", "2"));
            list3.Add(new KeyValueComboBoxItem("3", "3"));
            list3.Add(new KeyValueComboBoxItem("4", "4"));
            list3.Add(new KeyValueComboBoxItem("5", "5"));
            cmbLevel.ItemsSource = list3;
            cmbLevel.SelectedIndex = 0;
        }

        void BindSkill()
        {
            int type = ComboBoxHelper.GetSelectValueInt(cmbType);
            int color = ComboBoxHelper.GetSelectValueInt(cmbColor);
            int level = ComboBoxHelper.GetSelectValueInt(cmbLevel);
            BindPlayerSkill(cmbSkill, type, level, color);
        }

        private void CmbType_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isInit)
            {
                BindSkill();
            }
        }
    }
}
