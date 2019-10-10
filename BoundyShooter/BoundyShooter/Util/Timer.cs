using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoundyShooter.Util
{
    class Timer
    {
        public bool Loop
        {
            get;
            private set;
        }

        public bool IsTime
        {
            get;
            private set;
        }

        public float Location
        {
            get
            {
                if (IsTime)
                {
                    return 1.0f;
                }
                else
                {
                    return time / maxTime;
                }
            }
        }

        private float maxTime;
        private float time;

        /// <summary>
        /// タイマー
        /// </summary>
        /// <param name="second">タイマーの時間(秒)</param>
        /// <param name="loop">ループさせる場合はtrue</param>
        public Timer(float second, bool loop = false)
        {
            Initialize(second, loop);
        }

        public void Initialize(float second, bool loop = false)
        {
            maxTime = second * 60;
            time = 0;
            Loop = loop;
        }

        public void Reset()
        {
            time = 0;
        }

        public void Update(GameTime gameTime)
        {
            IsTime = false;
            if (time >= maxTime)
            {
                IsTime = true;
                if (Loop)
                {
                    time = 0;
                }
            }
            else
            {
                time++;
            }
        }
    }
}
