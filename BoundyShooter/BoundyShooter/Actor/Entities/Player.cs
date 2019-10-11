using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoundyShooter.Actor.Blocks;
using BoundyShooter.Def;
using BoundyShooter.Device;
using BoundyShooter.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BoundyShooter.Actor.Entities
{
    class Player : Entity
    {
        public float Rotation
        {
            get;
            private set;
        } = 0.0f;

        public float Speed
        {
            get;
            private set;
        }

        public const float Deceleration = 0.05f; //減速度
        public const float RotaionSpeed = 7f; //減速度
        public const float MaxSpeed = 15f;

        public Vector2 Front
        {
            get
            {
                var radian = MathHelper.ToRadians(Rotation);
                Vector2 vector = new Vector2(
                    (float)Math.Sin(radian),
                    -(float)Math.Cos(radian)
                    );
                return vector;
            }
        }
        public override void Hit(GameObject gameObject)
        {

            if (gameObject is Block block && block.IsSolid)
            {
                Direction dir = CheckDirection(block);
                CorrectPosition(block);
                
                if (dir == Direction.Left || dir == Direction.Right)
                {
                    var rotation = Rotation;
                    Rotation = 360 - rotation;
                }else if (dir == Direction.Top || dir == Direction.Bottom)
                {
                    var rotation = Rotation;
                    Rotation = 180 - rotation;
                }
            }
        }

        public Player(Vector2 position) 
            : base("player", position, new Point(64, 64))
        {
            Rotation = GameDevice.Instance().GetRandom().Next(360);
        }

        public override object Clone()
        {
            return new Player(Position);
        }

        public override void Update(GameTime gameTime)
        {
            Velocity = Front * Speed;
            Speed -= Deceleration;
            if (Speed < 0)
            {
                Speed = 0;
            }

            if (Input.GetKeyState(Keys.Space))
            {
                Velocity = Vector2.Zero;
                Rotation += RotaionSpeed;
                Speed = MaxSpeed;
            }
            base.Update(gameTime);
        }

        public override void Draw()
        {
            var drawer = Drawer.Default;
            drawer.Rotation = MathHelper.ToRadians(Rotation);
            drawer.Origin = Size.ToVector2() / 2;
            base.Draw(drawer);
        }
    }
}
