using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoundyShooter.Actor.Particles;
using BoundyShooter.Device;
using BoundyShooter.Manager;
using BoundyShooter.Util;
using Microsoft.Xna.Framework;

namespace BoundyShooter.Actor.Entities
{
    class KrakenTentacle : Enemy
    {
        private static string ArmName = "kraken_arm";

        private int chain;
        public Vector2 AnchorPosition
        {
            get;
            set;
        }

        public float Rotation
        {
            get;
            set;
        } = 0.0f;

        public float Speed = 1f;

        private Vector2 GetFront(float rotation)
        {
            var radian = MathHelper.ToRadians(Rotation);
            Vector2 vector = new Vector2(
                -(float)Math.Sin(radian),
                (float)Math.Cos(radian)
                );
            return vector;
        }

        public KrakenTentacle(Vector2 anchorPosition, int chain = 8)
            : base("kraken_hand", anchorPosition, new Point(64, 64), 80)
        {
            this.chain = chain;
            this.AnchorPosition = anchorPosition;
        }

        public override object Clone()
        {
            return new KrakenTentacle(Position);
        }

        public override void Update(GameTime gameTime)
        {
            if (!IsInScreen()) return;
            
            Velocity = GetFront(Rotation) * Speed;
            if(Boss.IsDeadFlag)
            {
                IsDead = true;
            }

            base.Update(gameTime);
        }

        public override void Hit(GameObject gameObject)
        {
            if (gameObject is Player player)
            {
                var rotation = Math.Atan2(player.Position.Y - Position.Y, player.Position.X - Position.X);
                if (player.Speed > Player.MaxSpeed / 2)
                {
                    life -= 10;
                    new DestroyParticle(Name, Position, Size, DestroyParticle.DestroyOption.Center);
                    Position = AnchorPosition;
                }
                if (life <= 0)
                {
                    IsDead = true;
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

            if (gameObject is LifeWall)
            {
                Position = AnchorPosition;
            }
        }

        public override void Draw()
        {
            Drawer chainDrawer;
            for (int i = 0; i < chain; i++)
            {
                chainDrawer = Drawer.Default;
                chainDrawer.DisplayModify = true;
                chainDrawer.Origin = new Vector2(Size.X / 2, 0);
                chainDrawer.Rotation = MathHelper.ToRadians(MathHelper.Lerp(0, Rotation,(float)i / chain));
                Renderer.Instance.DrawTexture(ArmName, Vector2.Lerp(AnchorPosition, Position, (float)i / chain), chainDrawer);
            }
            var drawer = Drawer.Default;
            drawer.DisplayModify = true;
            drawer.Rotation = MathHelper.ToRadians(Rotation);
            drawer.Origin = new Vector2(Size.X / 2, 0);
            base.Draw(drawer);
        }
    }
}
