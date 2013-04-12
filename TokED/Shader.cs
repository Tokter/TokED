using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokED
{
    public class Shader
    {
        public string VertexProgram { get; set; }
        public string FragmentProgram { get; set; }

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


    }
}
