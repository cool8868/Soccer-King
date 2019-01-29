using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillEngine.Editor.Football.Data
{
    public class BindItemData
    {
        public const string COLCode = "Code";
        public const string COLText= "Text";
        public const string COLPreCode = "PreCode";
        public const string COLFlag = "Flag";
        public const string NACODE = "";
        public const string NATEXT = "<未选择>";
      
        public BindItemData()
        { }
        public BindItemData(string code, string text)
            : this(code, text, string.Empty, string.Empty)
        { }
        public BindItemData(string code, string text, string preCode, string flag)
        {
            this.Code = code;
            this.Text = text;
            this.PreCode = preCode;
            this.Flag = flag;
        }
        public string this[string colName]
        {
            get
            {
                switch (colName)
                {
                    case COLCode:
                        return this.Code;
                    case COLText:
                        return this.Text;
                    default:
                        return string.Empty;
                }
            }
        }
        public string Code
        {
            get;
            set;
        }
        public string Text
        {
            get;
            set;
        }
        public string PreCode
        {
            get;
            set;
        }
        public string Flag
        {
            get;
            set;
        }
    }
    public class SkillItemData
    {
        public const string COLSkillId = "SkillId";
        public const string COLSkillCode = "SkillCode";
        public const string COLSkillName = "SkillName";
        public const string COLSkillSrcType = "SrcType";
        public const string COLModelId = "ModelId";
        public const string COLOpenClipId = "OpenClipId";
        public const string COLClipId = "ClipId";
        public const string COLMemo = "Memo";

        public string SkillId
        {
            get;
            set;
        }
        public string SkillCode
        {
            get;
            set;
        }
        public string SkillName
        {
            get;
            set;
        }
        public string SkillType
        {
            get;
            set;
        }
        public string ModelId
        {
            get;
            set;
        }
        public string OpenClipId
        {
            get;
            set;
        }
        public string ClipId
        {
            get;
            set;
        }
        public string Memo
        {
            get;
            set;
        }

        public object[] Values
        {
            get
            {
                return new[] { SkillId, SkillCode, SkillName, SkillType, ModelId, OpenClipId, ClipId, Memo };
            }
        }
    }
   
}
