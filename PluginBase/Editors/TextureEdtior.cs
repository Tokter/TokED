using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokED.Editors;
using TokGL;

namespace PluginBase.Editors
{
    [Export("Texture", typeof(Editor)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class TextureEdtior : Editor
    {
        private GameObjects.Texture _texture = null;
        private TokGL.Material _mat = null;

        public override void Load()
        {
            _texture = SelectedGameObject.FindParent<GameObjects.Texture>();
            if (_texture != null)
            {
                var bitmap = _texture.GetBitmap();
                if (bitmap != null)
                {
                    _mat = TokGL.Material.CreateTextureColor(TokGL.Texture.FromBitmap(bitmap, true, false));
                    Camera.Up = new Vector3(0, -1, 0);
                    Camera.Position = new Vector3(bitmap.Width / 2.0f, bitmap.Height / 2.0f, -100);
                    Camera.LookAt = new Vector3(bitmap.Width / 2.0f, bitmap.Height / 2.0f, 0);
                    Camera.Fov = 1.0f;
                }
            }
        }

        protected override void OnDispose()
        {
            if (_mat != null)
            {
                _mat.Dispose();
                _mat = null;
            }
        }

        public override void SelectedGameObjectChanged(string propertyName)
        {
            OnDispose();
            Load();
        }

        public override void DrawContent(LineBatch lineBatch, SpriteBatch spriteBatch)
        {
            if (_mat != null)
            {
                spriteBatch.AddSprite(_mat, 0, 0, 0, 0, _mat[TextureUnit.Texture0].Width, _mat[TextureUnit.Texture0].Height);
                lineBatch.AddBox(0, 0, _mat[TextureUnit.Texture0].Width, _mat[TextureUnit.Texture0].Height, Color.Red);
            }
            else
            {
                lineBatch.Add(new Vector2(-100, -100), new Vector2(100, 100), Color.Red);
                lineBatch.Add(new Vector2(100, -100), new Vector2(-100, 100), Color.Red);
            }
        }

    }
}
