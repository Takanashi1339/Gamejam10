using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoundyShooter.Util;
using Microsoft.Xna.Framework;

namespace BoundyShooter.Actor.Entities
{
    abstract class Enemy : Entity
    {
        private Vector2 bounceVelocity = new Vector2(0, -5f);//跳ね返りの初速
        private float acceleration = 0.1f;
        /// <summary>
        /// 敵の最高速度
        /// </summary>
        public float MaxSpeed
        {
            get;
            protected set;
        } = 3.0f;
        public Enemy(string name, Vector2 position, Point size)
            : base(name, position, size)
        {
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

        public override void Draw()
        {
            var drawer = Drawer.Default;
            drawer.DisplayModify = true;
            base.Draw(drawer);
        }
    }
}
