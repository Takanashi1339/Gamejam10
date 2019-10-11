using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoundyShooter.Manager;
using BoundyShooter.Util;
using Microsoft.Xna.Framework;

namespace BoundyShooter.Actor.Entities
{
    abstract class Enemy : Entity
    {
        private Vector2 bounceVelocity = new Vector2(0, -5f);//跳ね返りの初速
        private float acceleration = 0.1f;
        /// <summary>
        /// 敵のY軸方向の最高速度
        /// </summary>
        public float MaxSpeedY
        {
            get;
            protected set;
        } = 3.0f;
        /// <summary>
        /// 敵のX軸方向の最高速度
        /// </summary>
        public float MaxSpeedX
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
                float speed = 10f;
                var player = GameObjectManager.Instance.Find<Player>().First();
                var rotation = Math.Atan2(player.Position.Y - Position.Y, player.Position.X - Position.X);
                if(Math.Sqrt(
                    Math.Pow(player.Velocity.X,2) + Math.Pow(player.Velocity.Y,2)//2乗した平方根をとる
                    ) > player.Speed / 2)
                {
                    IsDead = true;//プレイヤーの最高速度/2よりも現在の速度が速い場合
                }
                if(rotation < 0)
                {
                    Velocity = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation)) * speed;
                }
                else
                {
                    Velocity = new Vector2((float)Math.Cos(rotation), -(float)Math.Sin(rotation)) * speed;
                }
            }
            base.Hit(gameObject);
        }

        public override void Update(GameTime gameTime)
        {
            var velocity = Velocity;
            if (Velocity.Y < MaxSpeedY)
            {
                velocity.Y += acceleration;
                Velocity = velocity;
            }
            if (Velocity.Y > MaxSpeedY)
            {
                velocity.Y = MaxSpeedY;
            }
            if(Velocity.X > 0)
            {
                velocity.X -= acceleration;
            }
            if(velocity.X <0)
            {
                velocity.X += acceleration;
            }
            if(Velocity.X > MaxSpeedX)
            {
                velocity.X = MaxSpeedX;
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
