using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoundyShooter.Device;
using BoundyShooter.Util;
using Microsoft.Xna.Framework;

namespace BoundyShooter.Actor.Particles
{
    class DestroyParticle : Particle
    {
        public enum DestroyOption
        {
            Left,
            Right,
            Up,
            Down,
            Center
        }

        private int location = 0;
        private DestroyOption option;
        private bool isDisPlayModify;

        public DestroyParticle(string name, Vector2 position, Point size, DestroyOption option, int particleSize = 10, bool displayModify = true) 
            : base(name, position, size, particleSize, 0.05f)
        {
            this.option = option;
            isDisPlayModify = displayModify;
        }

        public override object Clone()
        {
            return new DestroyParticle(Name, Position, Size, option);
        }

        public override void Hit(GameObject gameObject)
        {
        }

        public override void Update(GameTime gameTime)
        {
            location++;
            base.Update(gameTime);
        }

        public override void Draw()
        {
            var drawer = Drawer.Default;
            if(isDisPlayModify)
            {
                drawer.DisplayModify = true;
            }
            var pixSize = 2;
            for (int x = 0; x < Size.X; x += pixSize)
            {
                for (int y = 0; y < Size.Y; y += pixSize)
                {
                    drawer.Rectangle = new Rectangle(x, y, pixSize, pixSize);
                    switch (option)
                    {
                        case DestroyOption.Center:
                            Renderer.Instance.DrawTexture(Name, Position + new Vector2(
                                ((Size.X - pixSize / 2) / 2 - x) * location + Size.X / 2, 
                                ((Size.Y - pixSize / 2) / 2 - y) * location), drawer);
                            break;
                        case DestroyOption.Up:
                            Renderer.Instance.DrawTexture(Name, Position + new Vector2(
                                ((Size.X - pixSize / 2) / 2 - x) * location + Size.X * 2,
                                ((Size.Y - pixSize / 2) - pixSize / 2) * pixSize * 2 - (y * location) - Size.Y), drawer);
                            break;
                        case DestroyOption.Down:
                            Renderer.Instance.DrawTexture(Name, Position + new Vector2(
                                ((Size.X - pixSize / 2) / 2 - x) * location + Size.X * 2,
                                y * location), drawer);
                            break;
                        case DestroyOption.Left:
                            Renderer.Instance.DrawTexture(Name, Position + new Vector2(
                                (Size.X - pixSize / 2) * pixSize * 2 - (x * location),
                                ((Size.Y - pixSize / 2) / 2 - y) * location), drawer);
                            break;
                        case DestroyOption.Right:
                            Renderer.Instance.DrawTexture(Name, Position + new Vector2(
                                x * location,
                                ((Size.Y - pixSize / 2) / 2 - y) * location), drawer);
                            break;
                    }
                }
            }
            //base.Draw(drawer);
        }
    }
}
