using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokED
{
    public enum ShaderParamaterType
    {
        Float,
        Vec2,
        Vec3,
        Vec4,
    }

    public struct ShaderParameter
    {
        public ShaderParamaterType Type;
        public string Name;
        public string Desc;
        public object Value;
    }

    public class Shader
    {
        private List<ShaderParameter> _parameters = new List<ShaderParameter>();

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

        public void AddParameter(ShaderParameter param)
        {
            _parameters.Add(param);
        }

        public IEnumerable<ShaderParameter> Parameters
        {
            get { return _parameters; }
        }
    }
}
