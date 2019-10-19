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
    /// <summary>
    /// seald => 継承不可
    /// </summary>
    sealed class GameDevice
    {
        private const int QwakeTime = 6;

        private Sound sound;
        private static Random random;
        private ContentManager content;
        private GraphicsDevice graphics;
        private GameTime gameTime;
        private CSVReader reader;
        private Vector2 qwake;
        private int qwakeCount = 0;

        public Vector2 DisplayModify
        {
            get;
            set;
        }

        public Vector2 DisplayQuake
        {
            get
            {
                var location = (float) qwakeCount * QwakeTime;
                return qwake * location * (int) Math.Pow(-1, qwakeCount);
            }
            set
            {
                qwakeCount = QwakeTime;
                qwake = value;
            }
        }

        private Vector2 displayModify;

        private static GameDevice instance;

        private GameDevice(ContentManager content, GraphicsDevice graphics)
        {
            this.content = content;
            this.graphics = graphics;
            Initialize();
        }

        public static GameDevice Instance(ContentManager content, GraphicsDevice graphics)
        {
            if (instance == null)
            {
                instance = new GameDevice(content, graphics);
            }
            return instance;
        }

        public static GameDevice Instance()
        {
            return instance;
        }

        public void Initialize()
        {
            sound = new Sound(content);
            random = new Random();
            displayModify = Vector2.Zero;
            reader = new CSVReader();
            qwake = Vector2.Zero;
            qwakeCount = 0;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime">ゲーム時間</param>
        public void Update(GameTime gameTime)
        {
            //デバイスで絶対に１回のみ更新が必要なモノ
            Input.Update();
            this.gameTime = gameTime;
            if (qwakeCount > 0)
            {
                qwakeCount--;
            }
        }

        /// <summary>
        /// サウンドオブジェクトの取得
        /// </summary>
        /// <returns>サウンドオブジェクト</returns>
        public Sound GetSound()
        {
            return sound;
        }

        /// <summary>
        /// サウンドオブジェクトの取得
        /// </summary>
        /// <returns>サウンドオブジェクト</returns>
        public CSVReader GetCSVReader()
        {
            return reader;
        }

        /// <summary>
        /// 乱数オブジェクトの取得
        /// </summary>
        /// <returns>乱数オブジェクト</returns>
        public Random GetRandom()
        {
            return random;
        }

        /// <summary>
        /// コンテンツ管理者の取得
        /// </summary>
        /// <returns>コンテンツ管理者オブジェクト</returns>
        public ContentManager GetContentManager()
        {
            return content;
        }

        /// <summary>
        /// グラフィックデバイスの取得
        /// </summary>
        /// <returns>グラフィックデバイスオブジェクト</returns>
        public GraphicsDevice GetGraphicsDevice()
        {
            return graphics;
        }

        /// <summary>
        /// ゲーム時間の取得
        /// </summary>
        /// <returns></returns>
        public GameTime GetGameTime()
        {
            return gameTime;
        }
        
        
    }
}
