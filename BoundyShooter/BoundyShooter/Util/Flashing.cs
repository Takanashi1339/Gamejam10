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
        public Flashing(float maxAlpha, float minAlpha, float second, bool loop = true)
        {
            Initialize(maxAlpha, minAlpha, second);
        }

        public void Initialize(float maxAlpha , float minAlpha, float second, bool loop = true)
        {
            this.maxAlpha = maxAlpha;
            this.minAlpha = minAlpha;
            this.loop = loop;
            nowAlpha = maxAlpha;
            finish = false;
            range = (maxAlpha - minAlpha) * 2 / (second * 60);
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
