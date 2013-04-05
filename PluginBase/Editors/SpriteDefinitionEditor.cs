﻿using OpenTK;
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
using TokGL;

namespace PluginBase.Editors
{
    [Export("SpriteDefinition", typeof(Editor)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class SpriteDefinitionEditor : Editor
    {
        private Material _textureMat = null;
        private bool _textureFound = false;

        public override void Load()
        {
            if (_textureMat != null) throw new ArgumentOutOfRangeException("_textureMat should have been null!");
            var textureObj = (SelectedGameObject.Parent as GameObjects.Texture);
            if (textureObj != null && File.Exists(textureObj.FileName))
            {
                var texture = new Texture();
                texture.LoadFromFile(textureObj.FileName, textureObj.PreMultiplyAlpha, false);
                _textureMat = Material.CreateTextureColor(texture);
                _textureMat.Texture0.MinFilter = TextureMinFilter.Nearest;
                _textureMat.Texture0.MagFilter = TextureMinFilter.Nearest;
                _textureMat.DepthTest = true;
                _textureFound = true;
                Camera.Position = new Vector3(texture.Width / 2.0f, texture.Height / 2.0f, Camera.Position.Z);
                Camera.LookAt = new Vector3(texture.Width / 2.0f, texture.Height / 2.0f, Camera.LookAt.Z);
                ActivateTool("Bottom View");
            }
        }

        public override void UnLoad()
        {
            if (_textureMat != null)
            {
                _textureMat.Dispose();
                _textureMat = null;
            }
        }

        public override void DrawContent(LineBatch lineBatch, SpriteBatch spriteBatch)
        {
            if (_textureFound)
            {
                spriteBatch.AddSprite(_textureMat, 0, 0, 0, 0, _textureMat.Texture0.Width, _textureMat.Texture0.Height, Color.White);
                lineBatch.AddBox(0, 0, _textureMat.Texture0.Width, _textureMat.Texture0.Height, Color.Red);
            }
            else
            {
                lineBatch.Add(new Vector2(-100, -100), new Vector2(100, 100), Color.Red);
                lineBatch.Add(new Vector2(100, -100), new Vector2(-100, 100), Color.Red);
            }
            var spriteDef = (SelectedGameObject as GameObjects.SpriteDefinition);
            lineBatch.Add(new Vector2(spriteDef.P1.X, spriteDef.P1.Y), new Vector2(spriteDef.P2.X, spriteDef.P2.Y), Color.Green);
            lineBatch.Add(new Vector2(spriteDef.P3.X, spriteDef.P3.Y));
            lineBatch.Add(new Vector2(spriteDef.P4.X, spriteDef.P4.Y));
            lineBatch.Add(new Vector2(spriteDef.P1.X, spriteDef.P1.Y));
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