using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoundyShooter.Actor.Particles;
using BoundyShooter.Manager;
using BoundyShooter.Util;
using Microsoft.Xna.Framework;

namespace BoundyShooter.Actor.Entities
{
    abstract class Enemy : Entity
    {
        private Vector2 bounceVelocity = new Vector2(0, -5f);//跳ね返りの初速
        private float acceleration = 0.1f;
        protected int life;
        protected bool istitle;

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

        public Enemy(string name, Vector2 position, Point size, int life = 3)
            : base(name, position, size)
        {
            this.life = life;
            istitle = false;
        }


        public override void Hit(GameObject gameObject)
        {
            if(gameObject is Player)
            {
                float speed = 10f;
                var player = GameObjectManager.Instance.Find<Player>().First();
                var rotation = Math.Atan2(player.Position.Y - Position.Y, player.Position.X - Position.X);
                if(player.Speed > Player.MaxSpeed / 2)
                {
                    IsDead = true;//プレイヤーの最高速度/2よりも現在の速度が速い場合
                }else
                {
                    life--;
                    if (life <= 0)
                    {
                        IsDead = true;
                    }
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
            if (gameObject is PlayerBullet)
            {
                life--;
                if (life <= 0)
                {
                    IsDead = true;
                }
            }

            if(gameObject is LifeWall)
            {
                IsDead = true;
            }
            if(IsDead)
            {
                new DestroyParticle(Name, Position, Size, DestroyParticle.DestroyOption.Center);
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
            if (Velocity.X > 0)
            {
                velocity.X -= acceleration;
            }
            if (velocity.X < 0)
            {
                velocity.X += acceleration;
            }
            if (Velocity.X > MaxSpeedX)
            {
                velocity.X = MaxSpeedX;
            }
            if (!istitle)
            {
                var players = GameObjectManager.Instance.Find<Player>();
                if (players.Count == 0)
                {
                    IsDead = true;
                }
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
