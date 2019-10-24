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
    class FishEnemy : Enemy
    {
        private Animation animation;

        public FishEnemy(Vector2 position)
            : base("enemy1", position, new Point(64, 64))
        {
            animation = new Animation(Size, 8, 0.1f);
            istitle = false;
        }

        public FishEnemy(FishEnemy other)
            :this(other.Position)
        { }

        public override object Clone()
        {
            return new FishEnemy(this);
        }

        public override void Update(GameTime gameTime)
        {
            if (!IsInScreen()) return;
            animation.Update(gameTime);
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
            drawer.Rectangle = animation.GetRectangle();
            base.Draw(drawer);
        }

        public void ModeTitle()
        {
            istitle = true;
        }
    }
}
