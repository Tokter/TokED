using OpenTK;
using OpenTK.Graphics.OpenGL;
using PluginBase.Components;
using PluginBase.GameObjects;
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
    [Export("Scene", typeof(Editor)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class SceneEditor : Editor
    {
        private Scene _scene;
        private LineBatch _lineBatch;
        private SpriteBatch _spriteBatch;

        public override void Load()
        {
            _scene = SelectedGameObject.FindParent<GameObjects.Scene>();
            _scene.Load();
        }

        public override void DrawContent(LineBatch lineBatch, SpriteBatch spriteBatch)
        {
            _lineBatch = lineBatch;
            _spriteBatch = spriteBatch;
            if (_scene != null) RenderGameObject(_scene, Matrix4.Identity);
        }

        private void RenderGameObject(GameObject gameObject, Matrix4 parentTransformation)
        {
            if (gameObject.Visible)
            {
                var transComponent = gameObject.Component<Transformation>();
                Matrix4 transformation;

                if (transComponent != null)
                {
                    transformation = transComponent.Transform;
                    transformation *= parentTransformation;

                    bool drawn = false;
                    foreach (var c in gameObject.Components)
                    {
                        var drawable = c as IDrawable;
                        if (drawable != null)
                        {
                            drawable.Draw(_lineBatch, _spriteBatch);
                            _lineBatch.Flush(transformation);
                            _spriteBatch.Flush(transformation);
                            drawn = true;
                        }
                    }

                    if (!drawn)
                    {
                        _lineBatch.Add(new Vector3(-10, 0, 0), new Vector3(10, 0, 0), Color.Red);
                        _lineBatch.Add(new Vector3(0, -10, 0), new Vector3(0, 10, 0), Color.Green);
                        _lineBatch.Add(new Vector3(0, 0, -10), new Vector3(0, 0, 10), Color.Blue);
                        _lineBatch.Flush(transformation);
                    }
                }
                else
                {
                    transformation = parentTransformation;
                }

                foreach (var go in gameObject.Children)
                {
                    RenderGameObject(go, transformation);
                }
            }
        }
    }
}
