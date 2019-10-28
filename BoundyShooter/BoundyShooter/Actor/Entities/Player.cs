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
using BoundyShooter.Scene;
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

        public bool IsTitle
        {
            get;
            private set;
        } = false;

        public bool IsMenu
        {
            get;
            private set;
        } = false;


        public Timer gunTimer;
        private Sound sound;

        public const int BladeAmount = 3;
        public const float Deceleration = 0.025f;    //減速度
        public const float RotaionSpeed = 8f;       //回転速度
        public const float MaxSpeed = 15f;          //最高移動速度
        public const float ChargeSpeed = 0.3f;      //ゲージのチャージ速度
        public const float BulletRate = 0.15f;      //弾の最高発射レート
        public const string HitParticle = "blue_particle";

        private bool mapBottomHit = false;
        private bool haveSound = false;
        public override void Hit(GameObject gameObject)
        {
            if(gameObject is LifeWall wall&& !wall.IsDead)
            {
                HitLifeWall(wall);
            }
            if (gameObject is Block block && block.IsSolid)
            {
                HitBlock(block);
            }

            if (gameObject is Boss)
            {
                Direction dir = CheckDirection(gameObject);
                CorrectPosition(gameObject);

                if (dir == Direction.Left || dir == Direction.Right)
                {
                    var rotation = Rotation;
                    Rotation = 360 - rotation;
                }
                else if (dir == Direction.Top || dir == Direction.Bottom)
                {
                    var rotation = Rotation;
                    Rotation = 180 - rotation;
                }
            }
        }

        public void HitWall(bool isLeft)
        {
            var rotation = Rotation;
            Rotation = 360 - rotation;
            if (isLeft)
            {
                Position = new Vector2(Block.BlockSize, Position.Y);
            }
            else
            {
                Position = new Vector2(Screen.Width - Block.BlockSize - Size.X, Position.Y);
            }

            if (isLeft)
            {
                new DestroyParticle(HitParticle, Position, new Point(16, 16), DestroyParticle.DestroyOption.Right);
            }
            else
            {
                new DestroyParticle(HitParticle, Position, new Point(16, 16), DestroyParticle.DestroyOption.Left);
            }
        }

        public void HitLifeWall(LifeWall wall)
        {
            if (Speed > MaxSpeed / 2 && Velocity.Y != 0)
            {
                var rotation = Rotation;
                Rotation = 180 - rotation;
                new DestroyParticle(HitParticle, Position, new Point(16, 16), DestroyParticle.DestroyOption.Up);
            }
            if (mapBottomHit)
            {
                wall.Break();
            }
            CorrectPosition(wall);
        }

        public void HitBlock(Block block)
        {
            Direction dir = CheckDirection(block);
            CorrectPosition(block);

            if (dir == Direction.Left || dir == Direction.Right)
            {
                var rotation = Rotation;
                Rotation = 360 - rotation;
                if (dir == Direction.Left)
                {
                    new DestroyParticle(HitParticle, Position, new Point(16, 16), DestroyParticle.DestroyOption.Left);
                }
                else
                {
                    new DestroyParticle(HitParticle, Position, new Point(16, 16), DestroyParticle.DestroyOption.Right);
                }

            }
            else if (dir == Direction.Top || dir == Direction.Bottom)
            {
                var rotation = Rotation;
                Rotation = 180 - rotation;
                if (dir == Direction.Top)
                {
                    new DestroyParticle(HitParticle, Position, new Point(16, 16), DestroyParticle.DestroyOption.Up);
                }
                else
                {
                    mapBottomHit = true;
                    new DestroyParticle(HitParticle, Position, new Point(16, 16), DestroyParticle.DestroyOption.Down);
                }
            }
        }

        public Player(Vector2 position) 
            : base("player", position, new Point(64, 64))
        {
            Rotation = GameDevice.Instance().GetRandom().Next(360);
            gunTimer = new Timer(1f, false); //発射時に手動でリセット
            sound = GameDevice.Instance().GetSound();
        }

        public override object Clone()
        {
            return new Player(Position);
        }

        public override void Update(GameTime gameTime)
        {
            mapBottomHit = false;
            gunTimer.Update(gameTime);
            if (!IsCharging && Position.Y < -GameDevice.Instance().DisplayModify.Y)
            {
                Position = new Vector2(Position.X, -GameDevice.Instance().DisplayModify.Y + 1);
                var rotation = Rotation;
                Rotation = 180 - rotation;
                new DestroyParticle(HitParticle, Position, new Point(16, 16), DestroyParticle.DestroyOption.Down);
            }
            IsCharging = false;
            Velocity = Front * Speed;
            Speed -= Deceleration;
            if (Speed < 0)
            {
                Speed = 0;
            }

            if (Input.GetKeyTrigger(Keys.Space) && !IsTitle)
            {
                Speed = 0;
                if(!IsMenu)
                {
                    sound.StoppedSE("shoot", 0);
                    sound.RemoveSE("shoot", 0);
                    sound.CreateSEInstance("charge");
                    sound.CreateSEInstance("charging");
                    sound.CreateSEInstance("shoot");
                    sound.PlaySEInstances("charge", 0);
                    haveSound = true;
                }

            }
            if (Input.GetKeyRelease(Keys.Space) && !IsTitle && !IsMenu)
            {
                sound.StoppedSE("charge", 0);
                sound.RemoveSE("charge", 0);
                sound.StoppedSE("charging", 0);
                sound.RemoveSE("charging", 0);
                haveSound = false;
                if (Speed > MaxSpeed / 2)
                {
                    sound.PlaySEInstances("shoot", 0);
                }
            }
            if (Input.GetKeyState(Keys.Space) && !IsTitle)
            {
                if(haveSound)
                {
                    if(sound.IsStoppedSEInstance("charge", 0))
                    {
                        sound.PlaySEInstances("charging", 0, true);
                    }
                }
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
            //if (Speed <= MaxSpeed / 2 && gunTimer.Location >= (BulletRate + Speed / (MaxSpeed / 2) / (1 + BulletRate)))
            if (!Velocity.Equals(Vector2.Zero) && Speed <= MaxSpeed / 2 && gunTimer.Location >= BulletRate && !IsMenu)
            {
                gunTimer.Reset();
                //GameObjectManager.Instance.Add(new PlayerBullet(Position + Size.ToVector2() / 2 - new Vector2(8, 8 + Math.Sign(Front.Y) * Size.Y / 2), -Math.Sign(Front.Y)));
                GameObjectManager.Instance.Add(new PlayerBullet(Position + Size.ToVector2() / 2 - new Vector2(8, 8 + Size.Y / 2), -1));
                sound.PlaySE("gun");
            }
            if (!IsTitle && !IsMenu)
            {
                if (LifeWall.AllIsDead())
                {
                    new DestroyParticle(Name, Position, Size, DestroyParticle.DestroyOption.Center);
                    IsDead = true;
                }
            }
            new TailParticle(Position + new Vector2(16, 16));
            base.Update(gameTime);
            if (Position.X < Block.BlockSize)
            {
                HitWall(true);
            }else if (Position.X > Screen.Width - Block.BlockSize - Size.X)
            {
                HitWall(false);
            }
        }

        public override void Draw()
        {
            DrawBlade();
            var drawer = Drawer.Default;
            if (!IsMenu)
            {
                DrawGun();
                drawer.Rotation = MathHelper.ToRadians(Rotation);
                drawer.Origin = Size.ToVector2() / 2;
            }
            if (IsCharging)
            {
                DrawGage();
            }
            base.Draw(drawer);
        }

        private void DrawBlade()
        {
            if (Speed > MaxSpeed / 2)
            {
                var spin = (float) Math.Sqrt((Speed - MaxSpeed / 2) / MaxSpeed / 2) * 1.5f;
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
            //var gunSign = Math.Sign(Front.Y); //下向き-1, 上向きなら1
            var gunSign = 1;
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
            if(IsMenu)
            {
                gage.Rectangle = new Rectangle(0, 0, (int)(96 * Menu.SelectValueRate()), 16);
            }
            else
            {
                gage.Rectangle = new Rectangle(0, 0, (int)(96 * (Speed / MaxSpeed)), 16);
            }
            Renderer.Instance.DrawTexture("test_gage", gagePosition, gage);
        }

        public void ModeTitle()
        {
            Speed = 13f;
            IsTitle = true;
            if (Title.titleBottom > Position.Y)
            {
                
                var rotation = Rotation;
                Rotation = 180 - rotation;
                new DestroyParticle(HitParticle, Position, new Point(16, 16), DestroyParticle.DestroyOption.Down);
                Position = new Vector2(Position.X, Title.titleBottom);
            }
            if (Screen.Height - Size.Y < Position.Y)
            {  
                var rotation = Rotation;
                Rotation = 180 - rotation;
                new DestroyParticle(HitParticle, Position, new Point(16, 16), DestroyParticle.DestroyOption.Up);
                Position = new Vector2(Position.X, Screen.Height - Size.Y);
            }
        }

        public void ModeMenu(Vector2 position)
        {
            IsMenu = true;
            Position = position;
        }
    }
}
