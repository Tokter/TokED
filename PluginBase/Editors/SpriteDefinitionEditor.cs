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
    [Export("SpriteDefinition", typeof(Editor)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class SpriteDefinitionEditor : MaterialEditor
    {
        private Handle[] _handles = { new Handle(), new Handle(), new Handle(), new Handle(), new Handle() };
        private GameObjects.SpriteDefinition _spriteDef;
        private BindingManager _bindingManager;

        public override void Load()
        {
            base.Load();
            _spriteDef = SelectedGameObject.FindParent<GameObjects.SpriteDefinition>();
            _bindingManager = new BindingManager();
            _handles[0].Position = new Vector3(_spriteDef.P1);
            _handles[1].Position = new Vector3(_spriteDef.P2);
            _handles[2].Position = new Vector3(_spriteDef.P3);
            _handles[3].Position = new Vector3(_spriteDef.P4);
            _handles[4].Position = new Vector3(_spriteDef.Origin);
            _handles[4].HandleType = HandleType.Cross;
            _handles[4].Snap = SnapType.Half;
            _bindingManager.Bind(_handles[0], "Position", _spriteDef, "P1");
            _bindingManager.Bind(_handles[1], "Position", _spriteDef, "P2");
            _bindingManager.Bind(_handles[2], "Position", _spriteDef, "P3");
            _bindingManager.Bind(_handles[3], "Position", _spriteDef, "P4");
            _bindingManager.Bind(_handles[4], "Position", _spriteDef, "Origin");
            foreach (var handle in _handles) RootControl.AddChild(handle);

            Vector2 middle = (_spriteDef.P1 + _spriteDef.P2 + _spriteDef.P3 + _spriteDef.P4) / 4.0f;

            Camera.Position = new Vector3(middle.X, middle.Y, Camera.Position.Z);
            Camera.LookAt = new Vector3(middle.X, middle.Y, Camera.LookAt.Z);
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            _bindingManager.Dispose();
        }

        public override void DrawContent(LineBatch lineBatch, SpriteBatch spriteBatch)
        {
            base.DrawContent(lineBatch, spriteBatch);
            lineBatch.Add(_spriteDef.P1, _spriteDef.P2, Color.Green);
            lineBatch.Add(_spriteDef.P3);
            lineBatch.Add(_spriteDef.P4);
            lineBatch.Add(_spriteDef.P1);
        }
    }
}
