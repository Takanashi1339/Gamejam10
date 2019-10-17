using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoundyShooter.Actor.Blocks;
using BoundyShooter.Actor.Particles;
using BoundyShooter.Def;
using BoundyShooter.Device;
using BoundyShooter.Manager;
using BoundyShooter.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        public float BladeRotation
        {
            set;
            get;
        }

        public float Speed
        {
            get;
            private set;
        }
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

        public bool IsCharging
        {
            get;
            private set;
        }

        public Timer gunTimer;

        public const int BladeAmount = 3;
        public const float Deceleration = 0.025f; //減速度
        public const float RotaionSpeed = 8f; //減速度
        public const float MaxSpeed = 15f;
        public const float ChargeSpeed = 0.3f;
        public const float BulletRate = 0.15f;

        public override void Hit(GameObject gameObject)
        {
            if(gameObject is LifeWall wall&& !wall.IsDead)
            {
                var rotation = Rotation;
                Rotation = 180 - rotation;
                new DestroyParticle("pink_ball", Position, new Point(16, 16), DestroyParticle.DestroyOption.Up);
            }
            if (gameObject is Block block && block.IsSolid)
            {
                Direction dir = CheckDirection(block);
                CorrectPosition(block);
                
                if (dir == Direction.Left || dir == Direction.Right)
                {
                    var rotation = Rotation;
                    Rotation = 360 - rotation;
                    if (dir == Direction.Left)
                    {
                        new DestroyParticle("pink_ball", Position, new Point(16, 16), DestroyParticle.DestroyOption.Left);
                    }
                    else
                    {
                        new DestroyParticle("pink_ball", Position, new Point(16, 16), DestroyParticle.DestroyOption.Right);
                    }

                }else if (dir == Direction.Top || dir == Direction.Bottom)
                {
                    var rotation = Rotation;
                    Rotation = 180 - rotation;
                    if (dir == Direction.Top)
                    {
                        new DestroyParticle("pink_ball", Position, new Point(16, 16), DestroyParticle.DestroyOption.Up);
                    }
                    else
                    {
                        new DestroyParticle("pink_ball", Position, new Point(16, 16), DestroyParticle.DestroyOption.Down);
                    }
                }
            }
            if (gameObject is EasyBoss)
            {
                Direction dir = CheckDirection(gameObject);
                CorrectPosition(gameObject);

                if (dir == Direction.Left || dir == Direction.Right)
                {
                    var rotation = Rotation;
                    Rotation = 360 - rotation;
                    if (dir == Direction.Left)
                    {
                        new DestroyParticle("pink_ball", Position, new Point(16, 16), DestroyParticle.DestroyOption.Left);
                    }
                    else
                    {
                        new DestroyParticle("pink_ball", Position, new Point(16, 16), DestroyParticle.DestroyOption.Right);
                    }

                }
                else if (dir == Direction.Top || dir == Direction.Bottom)
                {
                    var rotation = Rotation;
                    Rotation = 180 - rotation;
                    if (dir == Direction.Top)
                    {
                        new DestroyParticle("pink_ball", Position, new Point(16, 16), DestroyParticle.DestroyOption.Up);
                    }
                    else
                    {
                        new DestroyParticle("pink_ball", Position, new Point(16, 16), DestroyParticle.DestroyOption.Down);
                    }
                }
            }
        }

        public Player(Vector2 position) 
            : base("player", position, new Point(64, 64))
        {
            Rotation = GameDevice.Instance().GetRandom().Next(360);
            gunTimer = new Timer(1f, false); //発射時に手動でリセット
        }

        public override object Clone()
        {
            return new Player(Position);
        }

        public override void Update(GameTime gameTime)
        {
            gunTimer.Update(gameTime);
            if (!IsCharging && Position.Y < -GameDevice.Instance().DisplayModify.Y)
            {
                var rotation = Rotation;
                Rotation = 180 - rotation;
                new DestroyParticle("pink_ball", Position, new Point(16, 16), DestroyParticle.DestroyOption.Down);
            }
            IsCharging = false;
            Velocity = Front * Speed;
            Speed -= Deceleration;
            if (Speed < 0)
            {
                Speed = 0;
            }

            if (Input.GetKeyTrigger(Keys.Space))
            {
                Speed = 0;
            }
            if (Input.GetKeyState(Keys.Space))
            {
                Velocity = Vector2.Zero;
                Rotation += RotaionSpeed;
                Speed += ChargeSpeed;
                if (Speed > MaxSpeed)
                {
                    Speed = MaxSpeed;
                }
                IsCharging = true;
            }
            BladeRotation -= (Speed - MaxSpeed / 2) * 2;
            if (Speed <= MaxSpeed / 2 && gunTimer.Location >= (BulletRate + Speed / (MaxSpeed / 2) / (1 + BulletRate)))
            {
                gunTimer.Reset();
                GameObjectManager.Instance.Add(new PlayerBullet(Position + Size.ToVector2() / 2 - new Vector2(8, 8), -Math.Sign(Front.Y)));
            }
            new TailParticle(Position + new Vector2(16, 16));
            base.Update(gameTime);
        }

        public override void Draw()
        {
            DrawBlade();
            DrawGun();
            if (IsCharging)
            {
                DrawGage();
            }
            var drawer = Drawer.Default;
            drawer.Rotation = MathHelper.ToRadians(Rotation);
            drawer.Origin = Size.ToVector2() / 2;
            base.Draw(drawer);
        }

        private void DrawBlade()
        {
            if (Speed > MaxSpeed / 2)
            {
                var spin = (Speed - MaxSpeed / 2) / MaxSpeed * (3f/2f);
                var blade = Drawer.Default;
                for (int i = 0; i < BladeAmount; i++)
                {
                    blade.Rotation = MathHelper.ToRadians(360 / BladeAmount * i + BladeRotation);
                    blade.Origin = new Vector2(Size.ToVector2().X / 4, Size.ToVector2().Y / 4 + spin * Size.ToVector2().Y);
                    //blade.Origin = Size.ToVector2() / 2 + (spin * Size.ToVector2() / 2);
                    blade.DisplayModify = true;
                    var bladePosition = Position + new Vector2(Size.X / 4, Size.Y / 4 - Size.Y * spin);
                    Renderer.Instance.DrawTexture("blade", bladePosition, blade);
                }

            }
        }

        private void DrawGun()
        {
            var gunSign = Math.Sign(Front.Y); //上なら-1, 下なら1
            var gun = Drawer.Default;
            if (gunSign == -1)
            {
                gun.SpriteEffects = SpriteEffects.FlipVertically;
            }
            gun.DisplayModify = true;
            var gunSpeed = -(MaxSpeed / 2) + Speed;
            if (gunSpeed > 0)
            {
                return;
            }
            var gunPosition = Position + new Vector2(0, gunSpeed * gunSign * 6);
            Renderer.Instance.DrawTexture("gun", gunPosition, gun);
        }

        private void DrawGage()
        {
            var gagePosition = Position + new Vector2(-16, Size.Y);
            var empty = Drawer.Default;
            empty.DisplayModify = true;
            Renderer.Instance.DrawTexture("test_gage_empty", gagePosition, empty);
            var gage = Drawer.Default;
            gage.DisplayModify = true;
            gage.Rectangle = new Rectangle(0, 0, (int) (96 * (Speed / MaxSpeed)), 32);
            Renderer.Instance.DrawTexture("test_gage", gagePosition, gage);
        }
    }
}
