using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokGL;

namespace TokED
{
    public class ShaderDefinition
    {
        private List<ShaderAttribute> _attributes = new List<ShaderAttribute>();
        private List<ShaderParam> _parameters = new List<ShaderParam>();

        public string VertexProgram { get; set; }
        public string FragmentProgram { get; set; }
        public bool Texture0Enabled { get; set; }
        public bool Texture1Enabled { get; set; }
        public bool Texture2Enabled { get; set; }
        public bool Texture3Enabled { get; set; }

        public string ExportName
        {
            get
            {
                var t = this.GetType();
                var atr = t.GetCustomAttributes(typeof(ExportAttribute), false);
                if (atr != null && atr.Length > 0 && atr[0] is ExportAttribute)
                {
                    return (atr[0] as ExportAttribute).ContractName;
                }
                return null;
            }
        }

        public void AddAttribute(ShaderAttribute attrib)
        {
            _attributes.Add(attrib);
        }

        public IEnumerable<ShaderAttribute> Attributes
        {
            get { return _attributes; }
        }

        public void AddParameter(ShaderParam param)
        {
            _parameters.Add(param);
        }

        public IEnumerable<ShaderParam> Parameters
        {
            get { return _parameters; }
        }
    }
}
