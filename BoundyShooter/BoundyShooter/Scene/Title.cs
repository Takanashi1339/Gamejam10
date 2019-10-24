using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using BoundyShooter.Device;
using BoundyShooter.Util;
using BoundyShooter.Def;
using BoundyShooter.Actor.Particles;
using BoundyShooter.Actor.Entities;
using BoundyShooter.Manager;
using BoundyShooter.Actor;
using BoundyShooter.Actor.Blocks;

namespace BoundyShooter.Scene
{
    class Title : IScene
    {
        private bool isEndFlag,reverce;
        public static readonly int titleBottom = 235;
        private float alph;
        private Renderer renderer;
        private ParticleManager particleManager;
        private Animation animation;

        private Player titlePlayer;


        public Title()
        {
            isEndFlag = false;
            renderer = Renderer.Instance;
        }

        public void Draw()
        {
            GameDevice.Instance().GetGraphicsDevice().Clear(Color.Black);

            renderer.Begin();

            var titledrawer = new Drawer();
            titledrawer.Rectangle = animation.GetRectangle();
            renderer.DrawTexture("title", new Vector2(20, 0), titledrawer);

            var pushdrawer = new Drawer();
            pushdrawer.Alpha = alph;
            renderer.DrawTexture("push_to_space", new Vector2(39, 700), pushdrawer);

            titlePlayer.Draw();
            particleManager.Draw();

            renderer.End();
        }

        public void Initialize()
        {
            isEndFlag = false;
            particleManager = new ParticleManager();
            particleManager.Initialize();
            GameDevice.Instance().DisplayModify = Vector2.Zero;
            titlePlayer = new Player(new Vector2(Screen.Width / 2 - 32, Screen.Height / 2 - 32));
            animation = new Animation(new Point(467, 235), 4, 0.15f, Animation.AnimationType.Vertical);
            reverce = false;
            alph = 0;
        }

        public bool IsEnd()
        {
            return isEndFlag;
        }

        public Scene Next()
        {
            return Scene.GamePlay;
        }

        public void Shutdown()
        {
        }

        public void Update(GameTime gameTime)
        {
            if (Input.GetKeyTrigger(Keys.Space))
            {
                //シーン移動
                isEndFlag = true;
            }
            if(alph <= 0 ||
                alph >= 1)
            {
                reverce = !reverce;
            }
            if(reverce)
            {
                alph += 0.05f;
            }
            else if (!reverce)
            {
                alph -= 0.05f;
            }
            animation.Update(gameTime);
            particleManager.Update(gameTime);
            titlePlayer.ModeTitle();
            titlePlayer.Update(gameTime);
            Console.WriteLine(titlePlayer.Position);
        }
    }
}
