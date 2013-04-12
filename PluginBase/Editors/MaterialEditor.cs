using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokED;
using TokED.Editors;
using TokGL;

namespace PluginBase.Editors
{
    [Export("Material", typeof(Editor)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class MaterialEditor : Editor
    {
        private GameObjects.Material _material = null;
        private bool _textureFound = false;

        public override void Load()
        {
            _material = SelectedGameObject.FindParent<GameObjects.Material>();
            if (_material != null)
            {
                _material.Load();
                if (_material.Mat.Texture0 != null)
                {
                    Camera.Position = new Vector3(_material.Mat.Texture0.Width / 2.0f, _material.Mat.Texture0.Height / 2.0f, Camera.Position.Z);
                    Camera.LookAt = new Vector3(_material.Mat.Texture0.Width / 2.0f, _material.Mat.Texture0.Height / 2.0f, Camera.LookAt.Z);
                }
                ActivateTool("Bottom View");
            }
        }

        public override void SelectedGameObjectChanged(string propertyName)
        {
            _material.UnLoad();
            _material.Load();
        }

        public override void DrawContent(LineBatch lineBatch, SpriteBatch spriteBatch)
        {
            if (_material != null)
            {
                if (_material.Mat.Texture0 != null)
                {
                    spriteBatch.AddSprite(_material.Mat, 0, 0, 0, 0, _material.Mat.Texture0.Width, _material.Mat.Texture0.Height);
                    lineBatch.AddBox(0, 0, _material.Mat.Texture0.Width, _material.Mat.Texture0.Height, Color.Red);
                }
                else
                {
                    spriteBatch.AddSprite(_material.Mat, new Vector2(0, 0), new Vector2(256, 256), 0.0f, 0.0f, 1.0f, 1.0f);
                    lineBatch.AddBox(0, 0, 256, 256, Color.Red);
                }
            }
            else
            {
                lineBatch.Add(new Vector2(-100, -100), new Vector2(100, 100), Color.Red);
                lineBatch.Add(new Vector2(100, -100), new Vector2(-100, 100), Color.Red);
            }
        }

        protected override void OnDispose()
        {
        }

        public override void DrawGui(LineBatch lineBatch, SpriteBatch spriteBatch)
        {
            var ray = Camera.GetWorldRay(MousePos);
            var plane = new Plane(new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(0, 1, 0));
            float? distance;
            ray.Intersects(ref plane, out distance);
            if (distance != null)
            {
                var pos = ray.Position + Vector3.Multiply(ray.Direction, distance.Value);
                spriteBatch.AddText(GuiFont, 250, 0, string.Format("X={0:n2} Y={1:n2}", pos.X, pos.Y), Color.White, HorizontalAlignment.Left, VerticalAlignment.Top);
            }
        }
    }
}
