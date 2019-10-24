using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoundyShooter.Actor.Particles;
using BoundyShooter.Util;
using Microsoft.Xna.Framework;

namespace BoundyShooter.Actor.Entities
{
    class JellyEnemy : Enemy
    {
        private Animation animation;
        public float Rotation
        {
            get;
            private set;
        }
        private float animationCount = 0;
        private float animationSpeed = 0.025f;
        private const float MaxAnimationAngle = 30;

        public JellyEnemy(Vector2 position) 
            : base("jelly_enemy", position, new Point(64, 64), 2)
        {
            animation = new Animation(Size, 4, 0.3f);
            MaxSpeedX = 0.0f;
            MaxSpeedY = 0.0f;
        }

        public override object Clone()
        {
            return new JellyEnemy(Position);
        }


        public override void Update(GameTime gameTime)
        {
            if (!IsInScreen()) return;
            animation.Update(gameTime);
            animationCount += animationSpeed;
            Rotation = (float)Math.Sin(animationCount) * MaxAnimationAngle;
            base.Update(gameTime);
        }

        public override void Hit(GameObject gameObject)
        {
            base.Hit(gameObject);
            if (IsDead)
            {
                new DestroyParticle(Name, Position, Size, DestroyParticle.DestroyOption.Center);
            }
        }

        public override void Draw()
        {
            var drawer = Drawer.Default;
            drawer.DisplayModify = true;
            drawer.Origin = new Vector2(Size.X / 2, 0);
            drawer.Rotation = MathHelper.ToRadians(Rotation);
            drawer.Rectangle = animation.GetRectangle();
            base.Draw(drawer);
        }

        public void ModeTitle()
        {
            istitle = true;
        }
    }
}
