using BoundyShooter.Def;
using BoundyShooter.Device;
using BoundyShooter.Util;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoundyShooter.Actor
{
    class FadeOut
    {
        public static readonly string Name = "black_effect";
        public const int AnimationValue = 4;
        public const int AnimationSpeed = 1;
        public const int AlphaFadeValue = 10;
        public static readonly int MaxAnimationCount = (Screen.Height / Size) * AnimationValue + AlphaFadeValue * AnimationValue;
        public const int Size = 32;
        private int animationCount = 0;
        public bool IsEnd
        {
            get;
            private set;
        } = false;


        public void Update(GameTime gameTime)
        {
            if (animationCount > MaxAnimationCount)
            {
                IsEnd = true;
            }
            animationCount += AnimationSpeed;
        }

        public void Draw()
        {
            for (int y = 0; y < Screen.Height; y += Size)
            {
                for (int x = 0; x < Screen.Width; x += Size)
                {
                    var animationState = animationCount - (y / Size) * AnimationValue;
                    animationState += (x / Size + y / Size) % 2 * AnimationValue * 2;
                    if (animationState <= AnimationValue * AlphaFadeValue)
                    {
                        var drawer = Drawer.Default;
                        drawer.Alpha = (float)animationState / (AnimationValue * AlphaFadeValue);
                        Renderer.Instance.DrawTexture(Name, new Vector2(x, y), drawer);
                    }
                    else
                    {
                        Renderer.Instance.DrawTexture(Name, new Vector2(x, y), Drawer.Default);
                    }
                }
            }
        }
    }
}
