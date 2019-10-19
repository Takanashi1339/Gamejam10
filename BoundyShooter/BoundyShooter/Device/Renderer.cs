using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using BoundyShooter.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoundyShooter.Device
{
    class Renderer
    {
        public static Renderer Instance
        {
            get;
            private set;
        }

        private Dictionary<string, Texture2D> textures;
        private SpriteBatch spriteBatch;
        private GraphicsDevice graphicsDevice;
        private ContentManager contentManager;

        private Renderer(ContentManager content, GraphicsDevice graphicsDevice)
        {
            Instance = this;
            spriteBatch = new SpriteBatch(graphicsDevice);
            this.graphicsDevice = graphicsDevice;
            contentManager = content;
        }

        public static void Initialize(ContentManager content, GraphicsDevice graphicsDevice)
        {
            var renderer = new Renderer(content, graphicsDevice);
            renderer.textures = new Dictionary<string, Texture2D>();
        }

        public void LoadContent(string assetName, string filePath = "./")
        {
            if (!textures.ContainsKey(assetName))
            {
                LoadContent(assetName, contentManager.Load<Texture2D>(filePath + assetName));
            }
        }

        public void LoadContent(string assetName, Texture2D texture)
        {
            if (!textures.ContainsKey(assetName))
            {
                textures[assetName] = texture;
            }
        }

        /// <summary>
        /// 描画開始
        /// </summary>
        public void Begin()
        {
            spriteBatch.Begin();
        }

        /// <summary>
        /// 描画開始
        /// </summary>
        /// <param name="sortMode">ソートモード</param>
        /// <param name="blendState">合成状態</param>
        public void Begin(SpriteSortMode sortMode, BlendState blendState)
        {
            spriteBatch.Begin(sortMode, blendState);
        }

        /// <summary>
        /// 描画終了
        /// </summary>
        public void End()
        {
            spriteBatch.End();
        }

        public void DrawTexture(string assetName, Vector2 position, Drawer drawer)
        {
            if (textures.ContainsKey(assetName))
            {
                spriteBatch.Draw(
                    textures[assetName], //テクスチャ
                    position + GameDevice.Instance().DisplayQuake + drawer.Origin
                        + ((drawer.DisplayModify)
                            ? GameDevice.Instance().DisplayModify
                            : Vector2.Zero),
                    drawer.Rectangle,
                    drawer.Color * drawer.Alpha,
                    drawer.Rotation,
                    drawer.Origin,
                    drawer.Scale,
                    drawer.SpriteEffects,
                    drawer.LayerDepth
                    );
            }
        }
    }
}
