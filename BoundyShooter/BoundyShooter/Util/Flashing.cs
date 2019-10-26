using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoundyShooter.Util
{
    class Flashing
    {
        private float maxAlpha;
        private float minAlpha;
        private float nowAlpha;
        private float range;
        private bool loop;
        private bool finish;
        /// <summary>
        /// 点滅させる
        /// </summary>
        /// <param name="maxAlpha">最大の明るさ</param>
        /// <param name="minAlpha">最小の明るさ</param>
        /// <param name="second">1回点滅するまでの時間</param>
        public Flashing(float maxAlpha, float minAlpha, float second)
        {
            Initialize(maxAlpha, minAlpha, second);
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="maxAlpha">最大の明るさ</param>
        /// <param name="minAlpha">最小の明るさ</param>
        /// <param name="second">変化終了までの所要時間</param>
        /// <param name="loop">ループするかどうか</param>
        /// <param name="reverse">変化を反転させるかどうか</param>
        public Flashing(float maxAlpha, float minAlpha, float second, bool loop, bool reverse = false)
        {
            Initialize(maxAlpha, minAlpha, second, loop, reverse);
        }

        public void Initialize(float maxAlpha , float minAlpha, float second, bool loop = true, bool reverse = false)
        {
            this.maxAlpha = maxAlpha;
            this.minAlpha = minAlpha;
            this.loop = loop;
            nowAlpha = minAlpha;
            if(loop)
            {
                range = (maxAlpha - minAlpha) * 2 / (second * 60);
            }
            else
            {
                range = (maxAlpha - minAlpha) / (second * 60);
            }
            if(reverse)
            {
                nowAlpha = maxAlpha;
                range *= -1;
            }
        }
        public void Update(GameTime gameTime)
        {
            if(!finish)
            {
                nowAlpha += range;
            }
            if(nowAlpha > maxAlpha || nowAlpha < minAlpha)
            {
                if(loop)
                {
                    range *= -1;
                }
                else
                {
                    finish = true;
                }
            }
        }

        public bool EndFlashing()
        {
            return finish;
        }

        public float GetAlpha()
        {
            return nowAlpha;
        }

        public void Reset()
        {
            nowAlpha = maxAlpha;
        }

    }
}
