using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace BoundyShooter.Actor.Entities
{
    abstract class Enemy : Entity
    {
        private Vector2 bounceVelocity = new Vector2(0, -10);
        private float acceleration = 0.1f; //戻るときの加速度
        public float MaxSpeed
        {
            get;
            protected set;
        }
        public Enemy(string name, Vector2 position, Point size,float maxSpeed)
            : base(name, position, size)
        {
            MaxSpeed = maxSpeed;
        }

        public override void Hit(GameObject gameObject)
        {
            if(gameObject is Player)
            {
                Velocity = bounceVelocity;
            }
            base.Hit(gameObject);
        }

        public override void Update(GameTime gameTime)
        {
            if (Velocity.Y < MaxSpeed)
            {
                var velocity = Velocity;
                velocity.Y += acceleration;
                Velocity = velocity;
            }
            base.Update(gameTime);
        }
    }
}
