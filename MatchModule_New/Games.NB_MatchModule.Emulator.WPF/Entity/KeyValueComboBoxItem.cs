using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Games.NB.Match.Emulator.WPF.Entity
{
    public class KeyValueComboBoxItem : ComboBoxItem
    {

        public KeyValueComboBoxItem(int key, string content)
        {
            this.Content = content;
            this.Value = key;
        }

        public KeyValueComboBoxItem(string key, string content)
        {
            this.Content = content;
            this.Value = key;
        }

        public KeyValueComboBoxItem(object key, object content)
        {
            this.Content = content;
            this.Value = key;
        }

        [Bindable(true)]
        public object Value { get; set; }
    }
}
