using BoundyShooter.Actor.Entities;
using BoundyShooter.Device;
using BoundyShooter.Util;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoundyShooter.Actor
{
    class HitStop
    {
        private Timer hitStopTimer;
        private static bool hitStop;
        
        public bool isHitStop
        {
            get;
            private set;
        }

        public HitStop()
        {
            Initialize();
        }

        public void Initialize()
        {
            isHitStop = false;
            hitStop = false;
        }

        public void Update(GameTime gameTime)
        {
            if (hitStop)
            {
                hitStopTimer = new Timer(0.05f);
                GameDevice.Instance().DisplayQuake = new Vector2(0, 0.5f);
                isHitStop = true;
                hitStop = false;
            }
            if (hitStopTimer != null)
            {
                hitStopTimer.Update(gameTime);
                if (hitStopTimer.IsTime)
                {
                    isHitStop = false;
                }
            }
        }

        public static void DoHitStop()
        {
            hitStop = true;
        }
    }
}
