using BoundyShooter.Device;
using BoundyShooter.Util;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoundyShooter.Actor.Particles
{
    class HealParticle : Particle
    {

        private int location = 0;

        private int pixSize = 2;

        public HealParticle(string name, Vector2 position, Point size,int particleSize = 10)
            : base(name, position, size, particleSize, 0.05f)
        {
            location = particleSize * pixSize * 2;
        }

        public override object Clone()
        {
            return new HealParticle(Name, Position, Size);
        }

        public override void Hit(GameObject gameObject)
        {
        }

        public override void Update(GameTime gameTime)
        {
            location--;
            base.Update(gameTime);
        }

        public override void Draw()
        {
            var drawer = Drawer.Default;
            drawer.DisplayModify = true;
            for (int x = 0; x < Size.X; x += pixSize)
            {
                for (int y = 0; y < Size.Y; y += pixSize)
                {
                    drawer.Rectangle = new Rectangle(x, y, pixSize, pixSize);
                    Renderer.Instance.DrawTexture(Name, Position + new Vector2(
                                ((Size.X - pixSize / 2) / 2 - x) * location + Size.X / 2,
                                ((Size.Y - pixSize / 2) / 2 - y) * location), drawer);
                }
            }
            //base.Draw(drawer);
        }
    }
}
