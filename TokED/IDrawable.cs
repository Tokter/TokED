using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokGL;

namespace TokED
{
    public interface IDrawable
    {
        void Draw(LineBatch lineBatch, SpriteBatch spriteBatch);
    }
}
