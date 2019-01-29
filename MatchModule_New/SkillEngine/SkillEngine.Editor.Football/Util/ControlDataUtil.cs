using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using SkillEngine.Editor.Football.Data;

namespace SkillEngine.Editor.Football.Util
{
    public enum DataAccessMode
    {
        SetData = 0,
        GetData = 1
    }
    public delegate bool ControlDataHandler<T>(DataAccessMode daMode,Control ctrl,ref T value);
    public class ControlDataUtil
    {
        #region Cache
        static readonly string s_dateFormat = "yyyy-MM-dd";
        static readonly char s_splitMulti = ',';
        static readonly Dictionary<CheckState, string> s_dicCheckStateData = new Dictionary<CheckState, string>();
        static readonly Dictionary<bool, string> s_dicBoolIntData = new Dictionary<bool, string>();
        static readonly Dictionary<bool, string> s_dicBoolStrData = new Dictionary<bool, string>();
        #endregion

        #region .ctor
        static ControlDataUtil()
        {
            s_dicCheckStateData.Add(CheckState.Unchecked, "0");
            s_dicCheckStateData.Add(CheckState.Checked, "1");
            s_dicCheckStateData.Add(CheckState.Indeterminate, "2");
            s_dicBoolIntData.Add(false, "0");
            s_dicBoolIntData.Add(true, "1");
            s_dicBoolStrData.Add(false, "false");
            s_dicBoolStrData.Add(true, "true");
        }
        #endregion

        #region Data
        public static Dictionary<bool, string> DicBoolInt
        {
            get { return s_dicBoolIntData; }
        }
        #endregion

        #region Controls
        public static bool ProcessControlsValue(DataAccessMode daMode, Dictionary<string, Control> dicControls, Dictionary<string, string> dicValues)
        {
            return ProcessControlsValue(daMode, dicControls, dicValues, null, s_dicCheckStateData, s_dicBoolStrData, InnerPropSelector, s_splitMulti);
        }
        public static bool ProcessControlsValue(DataAccessMode daMode, Dictionary<string, Control> dicControls, Dictionary<string, string> dicValues, ControlDataHandler<string> preHandler)
        {
            return ProcessControlsValue(daMode, dicControls, dicValues, preHandler, s_dicCheckStateData, s_dicBoolStrData, InnerPropSelector, s_splitMulti);
        }
        public static bool ProcessControlsValue(DataAccessMode daMode, Dictionary<string, Control> dicControls, Dictionary<string, string> dicValues, ControlDataHandler<string> preHandler, Dictionary<CheckState, string> dicCheckStateData, Dictionary<bool, string> dicBoolData, Func<object, string, string> propSelector, char splitMulti)
        {
            string key = string.Empty;
            string value = string.Empty;
            Control ctrl = null;
            foreach (var kvp in dicControls)
            {
                key = kvp.Key;
                ctrl = kvp.Value;
                if (null == ctrl)
                    continue;
                if (!dicValues.TryGetValue(key, out value))
                    value = string.Empty;
                ProcessControlValue(daMode, ctrl, ref value, preHandler, s_dicCheckStateData, s_dicBoolStrData, InnerPropSelector, s_splitMulti);
                dicValues[key] = value;
            }
            return true;
        }
        public static bool ProcessControlsValue(DataAccessMode daMode, Dictionary<string, Control> dicControls, DataRow dr)
        {
            return ProcessControlsValue(daMode, dicControls, dr, null, s_dicCheckStateData, s_dicBoolStrData, InnerPropSelector, s_splitMulti);
        }
        public static bool ProcessControlsValue(DataAccessMode daMode, Dictionary<string, Control> dicControls, DataRow dr, ControlDataHandler<string> preHandler)
        {
            return ProcessControlsValue(daMode, dicControls, dr, preHandler, s_dicCheckStateData, s_dicBoolStrData, InnerPropSelector, s_splitMulti);
        }
        public static bool ProcessControlsValue(DataAccessMode daMode, Dictionary<string, Control> dicControls, DataRow dr, ControlDataHandler<string> preHandler, Dictionary<CheckState, string> dicCheckStateData, Dictionary<bool, string> dicBoolData, Func<object, string, string> propSelector, char splitMulti)
        {
            string key = string.Empty;
            string value = string.Empty;
            Control ctrl = null;
            DataColumn col = null;
            foreach (var kvp in dicControls)
            {
                key = kvp.Key;
                ctrl = kvp.Value;
                if (null == ctrl || !dr.Table.Columns.Contains(key))
                    continue;
                col = dr.Table.Columns[key];
                value = dr[key].ToString();
                ProcessControlValue(daMode, ctrl, ref value, preHandler, dicCheckStateData, dicBoolData, InnerPropSelector, s_splitMulti);
                if (daMode == DataAccessMode.GetData && !col.ReadOnly)
                {
                    if (string.IsNullOrEmpty(value) && !col.DefaultValue.Equals(DBNull.Value))
                        dr[key] = col.DefaultValue;
                    else
                        dr[key] = value;
                }
            }
            return true;
        }
        public static bool ProcessControlsValueCore<T>(DataAccessMode daMode, Dictionary<string, Control> dicControls, Dictionary<string, T> dicValues, ControlDataHandler<T> handler)
        {
            string key = string.Empty;
            T value = default(T);
            Control ctrl = null;
            foreach (var kvp in dicControls)
            {
                key = kvp.Key;
                ctrl = kvp.Value;
                if (null == ctrl)
                    continue;
                dicValues.TryGetValue(key, out value);
                if (null != handler)
                    handler(daMode, ctrl, ref value);
                dicValues[key] = value;
            }
            return true;
        }
        #endregion

        #region Control
        public static bool ProcessControlValue(DataAccessMode daMode, Control control, ref string value)
        {
            return ProcessControlValue(daMode, control, ref value, null, s_dicCheckStateData, s_dicBoolStrData, InnerPropSelector, s_splitMulti);
        }
        public static bool ProcessControlValue(DataAccessMode daMode, Control control, ref string value, ControlDataHandler<string> preHandler)
        {
            return ProcessControlValue(daMode, control, ref value, preHandler, s_dicCheckStateData, s_dicBoolStrData, InnerPropSelector, s_splitMulti);
        }
        public static bool ProcessControlValue(DataAccessMode daMode, Control control, ref string value, ControlDataHandler<string> preHandler, Dictionary<CheckState, string> dicCheckStateData, Dictionary<bool, string> dicBoolData, Func<object, string, string> propSelector, char splitMulti)
        {
            if (null != preHandler && preHandler(daMode, control, ref value))
                return true;
            if (control is Label
                || control is TextBox
                || control is MaskedTextBox
                || control is GroupBox)
                return ProcessControlText(daMode, control, ref value);
            if (control is NumericUpDown)
                return ProcessNumericUpDownValue(daMode, (NumericUpDown)control, ref value);
            if (control is DateTimePicker)
                return ProcessDateTimePickerValue(daMode, (DateTimePicker)control, ref value);
            if (control is CheckBox)
                return ProcessCheckBoxValue(daMode, (CheckBox)control, ref value, dicCheckStateData, dicBoolData);
            if (control is ComboBox)
                return ProcessComboBoxCore(daMode, true, (ComboBox)control, ref value, propSelector);
            if (control is CheckedListBox)
                return ProcessCheckedListBoxCore(daMode, true, (CheckedListBox)control, ref value, propSelector, splitMulti);
            return true;
        }
        #endregion

        #region ControlLib
        public static bool ProcessControlText(DataAccessMode daMode, Control ctrl, ref string value)
        {
            if (daMode == DataAccessMode.GetData)
            {
                value = ctrl.Text.TrimEnd();
            }
            else if (daMode == DataAccessMode.SetData)
            {
                ctrl.Text = value ?? string.Empty;
            }
            return true;
        }
        public static bool ProcessNumericUpDownValue(DataAccessMode daMode, NumericUpDown num, ref string value)
        {
            if (daMode == DataAccessMode.GetData)
            {
                value = num.Value.ToString();
            }
            else if (daMode == DataAccessMode.SetData)
            {
                decimal dec = 0;
                decimal.TryParse(value, out dec);
                if (dec >= num.Minimum && dec <= num.Maximum)
                    num.Value = dec;
            }
            return true;
        }
        public static bool ProcessDateTimePickerValue(DataAccessMode daMode, DateTimePicker date, ref string value)
        {
            return ProcessDateTimePickerValue(daMode, date, ref value, s_dateFormat);
        }
        public static bool ProcessDateTimePickerValue(DataAccessMode daMode, DateTimePicker date, ref string value, string dateFormat)
        {
            if (daMode == DataAccessMode.GetData)
            {
                value = date.Value.ToString(dateFormat);
            }
            else if (daMode == DataAccessMode.SetData)
            {
                DateTime dt = DateTime.Now;
                DateTime.TryParse(value, out dt);
                if (dt >= date.MinDate && dt <= date.MaxDate)
                    date.Value = dt;
            }
            return true;
        }
        public static bool ProcessCheckBoxValue(DataAccessMode daMode, CheckBox cbx, ref string value)
        {
            return ProcessCheckBoxValue(daMode, cbx, ref value, s_dicCheckStateData, s_dicBoolStrData);
        }
        public static bool ProcessCheckBoxValue(DataAccessMode daMode, CheckBox cbx, ref string value, Dictionary<CheckState, string> dicCheckStateData, Dictionary<bool, string> dicBoolData)
        {
            if (daMode == DataAccessMode.GetData)
            {
                string tmp = value;
                if (cbx.ThreeState)
                {
                    if (dicCheckStateData.TryGetValue(cbx.CheckState, out tmp))
                        value = tmp;
                }
                else
                {
                    if (dicBoolData.TryGetValue(cbx.Checked, out tmp))
                        value = tmp;
                }
            }
            else if (daMode == DataAccessMode.SetData)
            {
                if (cbx.ThreeState)
                {
                    CheckState chkState;
                    if (CollectionsUtil.TryGetDicKeyByValue(dicCheckStateData, value, out chkState))
                        cbx.CheckState = chkState;
                }
                else
                {
                    Dictionary<bool, string> dicBool = null;
                    if (dicBool == null && dicBoolData.ContainsValue(value))
                        dicBool = dicBoolData;
                    if (dicBool == null && s_dicBoolIntData.ContainsValue(value))
                        dicBool = s_dicBoolIntData;
                    if (dicBool == null && s_dicBoolStrData.ContainsValue(value))
                        dicBool = s_dicBoolStrData;
                    bool chkFlag;
                    if (null != dicBool && CollectionsUtil.TryGetDicKeyByValue(dicBool, value, out chkFlag))
                        cbx.Checked = chkFlag;
                }
            }
            return true;
        }
        public static bool ProcessComboBoxValue(DataAccessMode daMode, ComboBox comb, ref string value)
        {
            if (daMode == DataAccessMode.GetData)
            {
                if (comb.DataSource == null)
                    value = comb.Text;
                else
                    value = comb.SelectedValue == null ? string.Empty : comb.SelectedValue.ToString();
            }
            else if (daMode == DataAccessMode.SetData)
            {
                if (comb.DataSource == null)
                    comb.Text = value ?? string.Empty;
                else
                    comb.SelectedValue = value;
            }
            return true;
        }
        public static bool ProcessComboBoxCore(DataAccessMode daMode, bool valueFlag, ComboBox comb, ref string value)
        {
            return ProcessComboBoxCore(daMode, valueFlag, comb, ref value, InnerPropSelector);
        }
        public static bool ProcessComboBoxCore(DataAccessMode daMode, bool valueFlag, ComboBox comb, ref string value, Func<object, string, string> propSelector)
        {
            string colVal = string.Empty;
            if (valueFlag)
                colVal = string.IsNullOrEmpty(comb.ValueMember) ? comb.DisplayMember : comb.ValueMember;
            else
                colVal = string.IsNullOrEmpty(comb.DisplayMember) ? comb.ValueMember : comb.DisplayMember;
            if (daMode == DataAccessMode.GetData)
            {
                var item = comb.SelectedItem;
                if (null == item)
                    value = string.Empty;
                else
                    value = propSelector(item, colVal);
            }
            else if (daMode == DataAccessMode.SetData)
            {
                for (int i = 0; i < comb.Items.Count; i++)
                {
                    if (value == propSelector(comb.Items[i], colVal))
                    {
                        comb.SelectedIndex = i;
                        break;
                    }
                }
            }
            return true;
        }
        public static bool ProcessCheckedListBoxCore(DataAccessMode daMode, bool valueFlag, CheckedListBox clb, ref string value)
        {
            return ProcessCheckedListBoxCore(daMode, valueFlag, clb, ref value, InnerPropSelector, s_splitMulti);
        }
        public static bool ProcessCheckedListBoxCore(DataAccessMode daMode, bool valueFlag, CheckedListBox clb, ref string value, Func<object, string, string> propSelector, char splitMulti)
        {
            string colVal = string.Empty;
            if (valueFlag)
                colVal = string.IsNullOrEmpty(clb.ValueMember) ? clb.DisplayMember : clb.ValueMember;
            else
                colVal = string.IsNullOrEmpty(clb.DisplayMember) ? clb.ValueMember : clb.DisplayMember;
            if (daMode == DataAccessMode.GetData)
            {
                string[] vals = new string[clb.CheckedItems.Count];
                for (int i = 0; i < clb.CheckedItems.Count; i++)
                {
                    vals[i] = propSelector(clb.CheckedItems[i], colVal);
                }
                if (vals.Length > 0)
                    value = string.Join(splitMulti.ToString(), vals);
            }
            else if (daMode == DataAccessMode.SetData)
            {
                string[] vals = value.Split(splitMulti);
                if (vals.Length == 0)
                    return false;
                foreach (int idx in clb.CheckedIndices)
                {
                    clb.SetItemChecked(idx, false);
                }
                var dicVals = vals.ToDictionary(i => i);
                for (int i = 0; i < clb.Items.Count; i++)
                {
                    if (dicVals.ContainsKey(propSelector(clb.Items[i], colVal)))
                        clb.SetItemChecked(i, true);
                }
            }
            return true;
        }
        static string InnerPropSelector(object item, string colName)
        {
            if (null == item || null == colName)
                return string.Empty;
            if (item is DataRow)
                return ((DataRow)item)[colName].ToString();
            if (item is DataRowView)
                return ((DataRowView)item)[colName].ToString();
            if (item is BindItemData)
                return ((BindItemData)item)[colName];
            return item.ToString();
        }
        #endregion

    }
}
