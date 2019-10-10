using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoundyShooter.Util
{
    class Animation
    {
        private Point size;
        private int frame;
        private int maxFrame;
        private Timer timer;
        private AnimationType animationType;

        private bool loop = true;
        private Point startPoint = Point.Zero;

        public bool IsEnd
        {
            get;
            private set;
        }

        /// <summary>
        /// アニメーション方向
        /// </summary>
        public enum AnimationType
        {
            Horizontal, //横
            Vertical,   //縦
        }

        /// <summary>
        /// アニメーションを管理する
        /// </summary>
        /// <param name="size">サイズ</param>
        /// <param name="frame">アニメーションのフレーム数</param>
        /// <param name="time">1フレーム当たりの時間(秒)</param>
        /// <param name="animationType">アニメーション方向</param>
        public Animation(Point size, int frame, float time, AnimationType animationType = AnimationType.Horizontal)
        {
            this.size = size;
            maxFrame = frame;
            timer = new Timer(time, true);
            this.animationType = animationType;
            Initialize();
        }

        /// <summary>
        /// アニメーションを管理する
        /// </summary>
        /// <param name="size">サイズ</param>
        /// <param name="frame">アニメーションのフレーム数</param>
        /// <param name="time">1フレーム当たりの時間(秒)</param>
        /// <param name="startPoint">アニメーションの開始地点</param>
        /// <param name="animationType">アニメーション方向</param>
        public Animation(Point size, int frame, float time, Point startPoint, AnimationType animationType = AnimationType.Horizontal)
            : this(size, frame, time, animationType)
        {
            this.startPoint = startPoint;
        }

        /// <summary>
        /// アニメーションを管理する
        /// </summary>
        /// <param name="size">サイズ</param>
        /// <param name="frame">アニメーションのフレーム数</param>
        /// <param name="time">1フレーム当たりの時間(秒)</param>
        /// <param name="loop">アニメーションをループさせるかどうか</param>
        /// <param name="animationType">アニメーション方向</param>
        public Animation(Point size, int frame, float time, bool loop, AnimationType animationType = AnimationType.Horizontal)
            : this(size, frame, time, animationType)
        {
            this.loop = loop;
        }

        public void Initialize()
        {
            frame = 0;
            timer.Reset();
            IsEnd = false;
        }

        public void Update(GameTime gameTime)
        {
            if (IsEnd) return;
            timer.Update(gameTime);
            if (timer.IsTime)
            {
                frame++;
                if (loop)
                {
                    frame = (frame >= maxFrame)
                        ? 0
                        : frame;
                }
                else
                {
                    if (frame >= maxFrame)
                    {
                        IsEnd = true;
                        frame--;
                    }
                }
            }
        }

        /// <summary>
        /// 描画時の矩形を取得
        /// </summary>
        /// <returns></returns>
        public Rectangle GetRectangle()
        {
            var location = startPoint;

            if (animationType == AnimationType.Horizontal)
            {
                location.X *= frame;
            }
            else
            {
                location.Y *= frame;
            }

            return new Rectangle(location, size);
        }
    }
}
