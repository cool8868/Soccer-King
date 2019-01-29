using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Games.NB.Match.Emulator.WPF.Entity;

namespace Games.NB.Match.Emulator.WPF.Mgrs
{
    public class ComboBoxHelper
    {
        #region ComboBox
        public static void BindComboBox(ComboBox cmb, List<KeyValueComboBoxItem> list)
        {
            cmb.ItemsSource = list;
            //cmb.SelectedIndex = 0;
        }

        public static int GetSelectValueInt(ComboBox cmb)
        {
            string temp = GetSelectValue(cmb);
            if (string.IsNullOrEmpty(temp))
                return 0;
            else
            {
                return Convert.ToInt32(temp);
            }
        }

        public static string GetSelectValue(ComboBox cmb)
        {
            if (cmb.SelectedIndex < 0)
                return "";
            else
            {
                var temp = cmb.SelectedItem as KeyValueComboBoxItem;
                return temp.Value.ToString();
            }
        }

        public static void SetSelectItem(ComboBox cmb, int value)
        {
            SetSelectItem(cmb, value.ToString());
        }

        public static void SetSelectItem(ComboBox cmb, string value)
        {
            var source = cmb.ItemsSource;
            if (source != null)
            {
                var newsource = (List<KeyValueComboBoxItem>)source;
                foreach (var item in newsource)
                {
                    if (item.Value.ToString() == value)
                    {
                        cmb.SelectedItem = item;
                        return;
                    }
                }
            }
        }
        #endregion
    }
}
