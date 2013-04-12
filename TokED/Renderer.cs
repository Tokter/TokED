using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokGL;

namespace TokED
{
    public class Renderer : Component
    {
        public virtual void Draw(LineBatch lineBatch, SpriteBatch spriteBatch)
        {
        }
    }
}
