using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using SkillEngine.Editor.Football.Util;
using SkillEngine.Editor.Football.Data;
using SkillEngine.Editor.Football.UI.ControlLib;


namespace SkillEngine.Editor.Football.UI.ControlBase
{
    public partial class FlatCtrl : UserControl, IFlatControl
    {
        public FlatCtrl()
        {
            InitializeComponent();
        }

        #region Cache
        public const char KEYSplit = ':';
        protected readonly Dictionary<string, Control> _dicAControls = new Dictionary<string, Control>();
        protected readonly Dictionary<string, Control> _dicNControls = new Dictionary<string, Control>();
        protected readonly Dictionary<string, Control> _dicNAControls = new Dictionary<string, Control>();
        protected readonly Dictionary<string, string> _dicAValues = new Dictionary<string, string>();
        protected readonly Dictionary<string, XElement> _dicNValues = new Dictionary<string, XElement>();
        protected readonly Dictionary<string, XElement> _dicNAValues = new Dictionary<string, XElement>();
        protected readonly XElement _rootXe = new XElement("root");
        #endregion

        #region Data
        public EnumClassRank ClassRank
        {
            get;
            set;
        }
        public EnumClassFlag ClassFlag
        {
            get;
            set;
        }
        #endregion

        #region IFlatControl
        public virtual XElement GetValue()
        {
            lock (_rootXe)
            {
                _dicAValues.Clear();
                _dicNValues.Clear();
                _dicNAValues.Clear();
                ProcessControlsValue(DataAccessMode.GetData, _dicAControls, _dicAValues);
                ProcessControlsValue(DataAccessMode.GetData, _dicNControls, _dicNValues);
                ProcessControlsValue(DataAccessMode.GetData, _dicNAControls, _dicNAValues);
                _rootXe.RemoveAll();
                foreach (var kvp in _dicAValues)
                {
                    _rootXe.Add(new XAttribute(kvp.Key, kvp.Value));
                }
                foreach (var kvp in _dicNValues)
                {
                    if (null == kvp.Value)
                        continue;
                    IncludeXElement(_rootXe, kvp.Value, kvp.Key.Split(KEYSplit)[0]);
                }
                foreach (var kvp in _dicNAValues)
                {
                    if (null == kvp.Value)
                        continue;
                    CombineXElement(_rootXe, kvp.Value);
                }
                return _rootXe;
            }
        }
        public virtual bool SetValue(XElement xe)
        {
            if (null == xe)
                return true;
            lock (_rootXe)
            {
                _dicAValues.Clear();
                _dicNValues.Clear();
                _dicNAValues.Clear();
                string key = string.Empty;
                foreach (var xa in xe.Attributes())
                {
                    key = xa.Name.LocalName;
                    if (_dicAControls.ContainsKey(key))
                        _dicAValues[key] = xa.Value;
                }
                int idx = 0;
                foreach (var xe2 in xe.Elements())
                {
                    key = xe2.Name.LocalName;
                    if (_dicNValues.ContainsKey(key))
                        key = string.Concat(key, KEYSplit, idx);
                    if (_dicNControls.ContainsKey(key))
                        _dicNValues[key] = xe2;
                    ++idx;
                }
                foreach (var kvp in _dicNAControls)
                {
                    _dicNAValues[kvp.Key] = xe;
                }
                ProcessControlsValue(DataAccessMode.SetData, _dicAControls, _dicAValues);
                ProcessControlsValue(DataAccessMode.SetData, _dicNControls, _dicNValues);
                ProcessControlsValue(DataAccessMode.SetData, _dicNAControls, _dicNAValues);
                return true;
            }
        }
        #endregion

        #region Init
        public virtual void InitData()
        {

        }
        #endregion

        #region ProcessXmlValue
        protected bool ProcessControlsValue(DataAccessMode daMode, Dictionary<string, Control> dicControls, Dictionary<string, XElement> dicValues)
        {
            if (dicControls.Count == 0)
                return true;
            return ControlDataUtil.ProcessControlsValueCore<XElement>(daMode, dicControls, dicValues, this.ProcessControlValue);
        }
        protected bool ProcessControlValue(DataAccessMode daMode, Control control, ref XElement xe)
        {
            if (this.PreProcessControlValue(daMode, control, ref xe))
                return true;
            var flatCtrl = control as FlatCtrl;
            if (null == flatCtrl)
                return true;
            return ProcessFlatControlValue(daMode, flatCtrl, ref xe);
        }
        protected virtual bool PreProcessControlValue(DataAccessMode daMode, Control control, ref XElement xe)
        {
            return false;
        }
        protected bool ProcessFlatControlValue(DataAccessMode daMode, FlatCtrl flatCtrl, ref XElement xe)
        {
            if (daMode == DataAccessMode.GetData)
                xe = flatCtrl.GetValue();
            else if (daMode == DataAccessMode.SetData)
                flatCtrl.SetValue(xe);
            return true;
        }
        #endregion

        #region ProcessStringValue
        protected bool ProcessControlsValue(DataAccessMode daMode, Dictionary<string, Control> dicControls, Dictionary<string, string> dicValues)
        {
            return ControlDataUtil.ProcessControlsValue(daMode, dicControls, dicValues, this.PreProcessControlValue);
        }
        protected virtual bool PreProcessControlValue(DataAccessMode daMode, Control control, ref string value)
        {
            if (control is ValueSetPicker)
            {
                ProcessPickerValue(daMode, (ValueSetPicker)control, ref value);
                return true;
            }
            else if (control is VMaskedTextBox)
            {
                ProcessVMaskedValue(daMode, (VMaskedTextBox)control, ref value);
                return true;
            }
            return false;
        }
        protected bool ProcessPickerValue(DataAccessMode daMode, ValueSetPicker picker, ref string value)
        {
            if (daMode == DataAccessMode.GetData)
            {
                value = picker.Value;
            }
            else if (daMode == DataAccessMode.SetData)
            {
                picker.Value = value;
            }
            return true;
        }
        public static bool ProcessVMaskedValue(DataAccessMode daMode, VMaskedTextBox mask, ref string value)
        {
            bool signFlag = mask.Mask.StartsWith("#");
            int val = 0;
            if (daMode == DataAccessMode.GetData)
            {
                int.TryParse(mask.Text, out val);
                value = val.ToString();
            }
            else if (daMode == DataAccessMode.SetData)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (int.TryParse(value, out val))
                    {
                        int len = 0;
                        foreach (var c in mask.Mask.ToCharArray())
                        {
                            if (c == '0' || c == '9')
                                len++;
                        }
                        value = val.ToString(new string(mask.PromptChar, len));
                        if (signFlag)
                            value = (val < 0 ? "-" : "+") + value;
                        mask.Text = value;
                        return true;
                    }
                }
                mask.Text = signFlag ? "+0" : string.Empty;
            }
            return true;
        }
        #endregion

        #region Tools
        protected void CombineXElement(XElement root, XElement child)
        {
            if (null == root || null == child)
                return;
            foreach (var xa in child.Attributes())
            {
                root.Add(xa);
            }
            child.RemoveAttributes();
            foreach (var xn in child.Nodes())
            {
                root.Add(xn);
            }
        }
        protected void IncludeXElement(XElement root, XElement child, string childName)
        {
            if (null == root || null == child)
                return;
            if (!string.IsNullOrEmpty(childName))
                child.Name = childName;
            root.Add(child);
        }
        protected void BindControl(ComboBox comb, List<BindItemData> bindData)
        {
            comb.DisplayMember = BindItemData.COLText;
            comb.ValueMember = BindItemData.COLCode;
            comb.DataSource = bindData;
        }
        protected void BindControl(ValueSetPicker pick, List<BindItemData> bindData)
        {
            BindControl(pick, bindData, true);
        }
        protected void BindControl(ValueSetPicker pick, List<BindItemData> bindData, bool noneAsFullFlag)
        {
            pick.NoneAsFullFlag = noneAsFullFlag;
            pick.DataSource = bindData;
        }
        protected string GetSelectedValue(ComboBox comb)
        {
            string val = string.Empty;
            var item = comb.SelectedItem as BindItemData;
            if (null != item)
                val = item.Code;
            return val;
        }
        protected string GetClassRankName(EnumClassRank classRank,bool listFlag)
        {
            switch (classRank)
            {
                case EnumClassRank.Trigger:
                    return listFlag?"":"触发条件";
                case EnumClassRank.Effector:
                    return listFlag ? "" : "目标+效果";
                case EnumClassRank.Seeker:
                    return  listFlag?"目标列表":" 目 标 ";
                case EnumClassRank.Filter:
                    return listFlag ? "过滤条件" : "";
                case EnumClassRank.Effect:
                    return listFlag?"效果列表":" 效 果 ";
                default:
                    return string.Empty;
            }
        }
        #endregion

    }
}
