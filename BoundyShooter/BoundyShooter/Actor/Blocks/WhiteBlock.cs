using BoundyShooter.Actor.Entities;
using BoundyShooter.Actor.Particles;
using BoundyShooter.Manager;
using BoundyShooter.Util;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoundyShooter.Actor.Blocks
{
    class WhiteBlock : Block
    {
        public bool Destroyed
        {
            get;
            private set;
        }

        private const float DestroyTime = 0.0675f;

        private Timer destroyTimer; 

        public WhiteBlock() 
            : base("white_block", true)
        {
        }

        public override object Clone()
        {
            return new WhiteBlock();
        }

        public override void Hit(GameObject gameObject)
        {
            if (!Destroyed && gameObject is PlayerBullet)
            {
                Destroy();
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (Destroyed)
            {
                destroyTimer.Update(gameTime);
                if (destroyTimer.IsTime)
                {
                    new DestroyParticle(Name, Position, Size, DestroyParticle.DestroyOption.Center);
                    Map.ReplaceBlock(this, new Space());

                    if (Map.GetBlock(Position + new Vector2(Size.X, 0)) is WhiteBlock other1 && !other1.Destroyed)
                    {
                        other1.Destroy();
                    }
                    if (Map.GetBlock(Position + new Vector2(-Size.X, 0)) is WhiteBlock other2 && !other2.Destroyed)
                    {
                        other2.Destroy();
                    }
                    if (Map.GetBlock(Position + new Vector2(0, Size.Y)) is WhiteBlock other3 && !other3.Destroyed)
                    {
                        other3.Destroy();
                    }
                    if (Map.GetBlock(Position + new Vector2(0, -Size.Y)) is WhiteBlock other4 && !other4.Destroyed)
                    {
                        other4.Destroy();
                    }
                }
            }
            base.Update(gameTime);
        }

        public void Destroy()
        {
            Destroyed = true;
            destroyTimer = new Timer(DestroyTime);
        }
    }
}
