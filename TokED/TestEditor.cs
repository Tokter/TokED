using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokGL;

namespace TokED
{
    public class TestEditor : IDisposable
    {
        private RenderManager _manager;
        private SpriteBatch _batch;
        private Material _tileMat;
        private const int MAP_HEIGHT = 4;
        private const int MAP_WIDTH = 8;
        private const int MAP_LENGTH = 8;
        private int[, ,] _map = new int[MAP_HEIGHT, MAP_WIDTH, MAP_LENGTH]
        {
            {
                {1,1,1,1,1,1,1,1},
                {1,1,1,1,1,1,1,1},
                {1,1,1,1,1,1,1,1},
                {1,1,1,1,1,1,1,1},
                {1,1,1,1,1,1,1,1},
                {1,1,1,1,1,1,1,1},
                {1,1,1,1,1,1,1,1},
                {1,1,1,1,1,1,1,1},
            },
            {
                {1,1,1,1,1,1,1,1},
                {1,1,1,1,1,1,1,1},
                {1,1,1,1,1,1,1,1},
                {1,1,1,0,0,1,1,1},
                {1,1,1,0,0,1,1,1},
                {1,1,1,1,1,1,1,1},
                {1,1,1,1,1,1,1,1},
                {1,1,1,1,1,1,1,1},
            },
            {
                {1,1,1,1,1,1,1,1},
                {1,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,1},
                {1,1,1,1,1,1,1,1},
            },
            {
                {1,1,1,1,1,1,1,1},
                {0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0},
                {1,0,0,0,0,0,0,1},
            }

        };

        public TestEditor()
        {
            _manager = new RenderManager();
            _manager.Camera = new Camera();
            _manager.Camera.CameraType = CameraType.Orthogonal;
            _manager.Camera.Position = new Vector3(0, 0, -10);
            _manager.Camera.LookAt = new Vector3(0, 0, 0);
            _manager.Camera.ZNear = 0;
            _manager.Camera.ZFar = 100;
            _manager.Camera.Up = new Vector3(0, -1, 0);
            _manager.Camera.Fov = 1.0f;
            
            _batch = new SpriteBatch();

            _tileMat = Material.CreateTextureColor(Texture.CreateFromResource("GrassDirtTile64.png", true, false));
            _tileMat.Texture0.MagFilter = TextureMinFilter.Nearest;
            _tileMat.Texture0.MinFilter = TextureMinFilter.Nearest;
            _tileMat.AlphaBlend = true;
            _tileMat.DepthTest = false;
        }

        public void Resize(int width, int height)
        {
            _manager.Camera.Width = width;
            _manager.Camera.Height = height;
        }

        public void Draw()
        {
            _manager.Begin();
            _batch.Begin(_manager);

            float px = 0;
            float py = 0;
            for (int height = 0; height < MAP_HEIGHT; height++)
            {
                int  gray = 255 - (MAP_HEIGHT - height - 1)*50;
                for (int x = 0; x < MAP_WIDTH; x++)
                {
                    for (int y = 0; y < MAP_LENGTH; y++)
                    {
                        if (_map[height, x, y] > 0)
                        {
                            _batch.AddSprite(_tileMat, px + x * 32 - y * 32, py + x * 16 + y * 16 - height * 37, 0, 0, 64, 71, Color.FromArgb(255, gray, gray, gray));
                        }
                    }
                }
            }

            _batch.End();
            _manager.End();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
