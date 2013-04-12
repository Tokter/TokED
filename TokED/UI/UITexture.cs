using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokGL;

namespace TokED.UI
{

    public struct UITextureInfo
    {
        public string Name;
        public Material Mat;
        public int X;
        public int Y;
        public int Width;
        public int Height;
    }

    public class UITexture
    {
        private static Dictionary<int, UITextureInfo> _textures = new Dictionary<int, UITextureInfo>();
        private static Material _currentMaterial;
        private static int _id = 0;

        public static void RegisterTexture(string resourceName)
        {
            _currentMaterial = Material.CreateTextureColor(Texture.CreateFromStream(Plugins.LoadResourceStream(resourceName), true, false));
            _currentMaterial.Texture0.MagFilter = TextureMagFilter.Nearest;
            _currentMaterial.Texture0.MinFilter = TextureMinFilter.Nearest;
            _currentMaterial.AlphaBlend = true;
            _currentMaterial.DepthTest = false;
        }

        public static void RegisterTextureInfo(string name, int x, int y, int width, int height)
        {
            var info = new UITextureInfo();
            info.Mat = _currentMaterial;
            info.Name = name;
            info.X = x;
            info.Y = y;
            info.Width = width;
            info.Height = height;
            _textures.Add(_id++, info);
        }

        public static UITextureInfo GetTexture(int id)
        {
            return _textures[id];
        }

        public static int GetID(string name)
        {
            foreach (var key in _textures.Keys)
            {
                if (_textures[key].Name == name) return key;
            }
            return -1;
        }
    }

    [Export("EditorTextures", typeof(UITexture)), PartCreationPolicy(CreationPolicy.Shared)]
    public class EditorTextures : UITexture
    {
        public EditorTextures()
        {
            UITexture.RegisterTexture("UI.png");
            UITexture.RegisterTextureInfo("button_default", 0, 0, 15, 15);
            UITexture.RegisterTextureInfo("button_hot", 15, 0, 15, 15);
            UITexture.RegisterTextureInfo("button_down", 30, 0, 15, 15);
            UITexture.RegisterTextureInfo("tooltip", 16, 29, 16, 16);
            UITexture.RegisterTextureInfo("vscroll_track", 0, 73, 13, 16);
            UITexture.RegisterTextureInfo("vscroll_button", 13, 73, 13, 13);
            UITexture.RegisterTextureInfo("vscroll_button_hot", 26, 73, 13, 13);
            UITexture.RegisterTextureInfo("vscroll_button_down", 39, 73, 13, 13);
            UITexture.RegisterTextureInfo("frame", 45, 0, 15, 15);
            UITexture.RegisterTextureInfo("listbox_frame", 32, 29, 16, 16);
            UITexture.RegisterTextureInfo("listbox_item", 27, 45, 9, 9);
            UITexture.RegisterTextureInfo("listbox_item_hot", 36, 45, 9, 9);
            UITexture.RegisterTextureInfo("listbox_item_selected", 18, 45, 9, 9);
            UITexture.RegisterTextureInfo("listbox_item_selected_hot", 45, 45, 9, 9);
            UITexture.RegisterTextureInfo("treeview_button_plus", 9, 45, 9, 9);
            UITexture.RegisterTextureInfo("treeview_button_minus", 0, 45, 9, 9);
            UITexture.RegisterTextureInfo("eye_active", 52, 73, 13, 9);
            UITexture.RegisterTextureInfo("eye_inactive", 65, 73, 13, 9);
            UITexture.RegisterTextureInfo("combo_button", 40, 89, 10, 20);
            UITexture.RegisterTextureInfo("combo_button_hot", 50, 89, 10, 20);
            UITexture.RegisterTextureInfo("combo_button_down", 60, 89, 10, 20);
            UITexture.RegisterTextureInfo("combo", 0, 89, 20, 20);
            UITexture.RegisterTextureInfo("combo_hot", 0, 109, 20, 20);
            UITexture.RegisterTextureInfo("combo_lisbox_frame", 20, 89, 20, 20);
            UITexture.RegisterTextureInfo("flat_combo_button", 70, 89, 10, 20);
            UITexture.RegisterTextureInfo("flat_combo_button_hot", 80, 89, 10, 20);
            UITexture.RegisterTextureInfo("flat_combo_button_down", 90, 89, 10, 20);
            UITexture.RegisterTextureInfo("combo_plus", 100, 89, 20, 20);
            UITexture.RegisterTextureInfo("combo_minus", 120, 89, 20, 20);
            UITexture.RegisterTextureInfo("combo_plus_hot", 20, 109, 20, 20);
            UITexture.RegisterTextureInfo("combo_minus_hot", 40, 109, 20, 20);
            UITexture.RegisterTextureInfo("combo_plus_down", 60, 109, 20, 20);
            UITexture.RegisterTextureInfo("combo_minus_down", 80, 109, 20, 20);

            UITexture.RegisterTextureInfo("vsplitter", 52, 85, 4, 4);
            UITexture.RegisterTextureInfo("vsplitter_hot", 56, 85, 4, 4);
            UITexture.RegisterTextureInfo("vsplitter_down", 60, 85, 4, 4);

            UITexture.RegisterTextureInfo("hsplitter", 64, 85, 4, 4);
            UITexture.RegisterTextureInfo("hsplitter_hot", 68, 85, 4, 4);
            UITexture.RegisterTextureInfo("hsplitter_down", 72, 85, 4, 4);

            UITexture.RegisterTextureInfo("input", 100, 109, 20, 20);
            UITexture.RegisterTextureInfo("input_hot", 120, 109, 20, 20);

            UITexture.RegisterTextureInfo("save", 140, 89, 20, 20);
            UITexture.RegisterTextureInfo("save_hot", 160, 89, 20, 20);
            UITexture.RegisterTextureInfo("save_down", 180, 89, 20, 20);

            UITexture.RegisterTextureInfo("load", 140, 109, 20, 20);
            UITexture.RegisterTextureInfo("load_hot", 160, 109, 20, 20);
            UITexture.RegisterTextureInfo("load_down", 180, 109, 20, 20);

            UITexture.RegisterTextureInfo("checkbox", 0, 15, 14, 14);
            UITexture.RegisterTextureInfo("checkbox_checked", 14, 15, 14, 14);
            UITexture.RegisterTextureInfo("checkbox_hot", 98, 15, 14, 14);
            UITexture.RegisterTextureInfo("checkbox_checked_hot", 112, 15, 14, 14);

            UITexture.RegisterTextureInfo("white", 54, 45, 9, 9);
            UITexture.RegisterTextureInfo("ArrowHead-Right", 126, 15, 14, 14);
            UITexture.RegisterTextureInfo("ArrowHead-Down", 140, 15, 14, 14);
            UITexture.RegisterTextureInfo("Project", 42, 15, 14, 14);
        }
    }
}
