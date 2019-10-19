using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoundyShooter.Actor.Blocks;
using BoundyShooter.Actor.Particles;
using BoundyShooter.Device;
using Microsoft.Xna.Framework;

namespace BoundyShooter.Actor.Entities
{
    internal class PlayerBullet : Entity
    {
        public const float BulletMaxSpeed = 12f;
        private float velocityY;

        public PlayerBullet(Vector2 position, float velocityY)
            : base("blue_ball", position, new Point(16, 16))
        {
            this.velocityY = velocityY;
        }

        public override object Clone()
        {
            return new PlayerBullet(Position, velocityY);
        }

        public override void Update(GameTime gameTime)
        {
            Velocity = new Vector2(0, velocityY * BulletMaxSpeed);

            if (Position.Y < -GameDevice.Instance().DisplayModify.Y)
            {
                Position = new Vector2(Position.X, -GameDevice.Instance().DisplayModify.Y + 1);
                new DestroyParticle(Name, Position, Size, DestroyParticle.DestroyOption.Center);
                IsDead = true;
            }
            base.Update(gameTime);
        }

        public override void Hit(GameObject gameObject)
        {
            if (!(gameObject is Player))
            {
                new DestroyParticle(Name, Position, Size, DestroyParticle.DestroyOption.Center);
                IsDead = true;
            }

            base.Hit(gameObject);
        }
    }
}
