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
        private bool isEndFlag;
        public static readonly int titleBottom = 235;
        private Renderer renderer;
        private ParticleManager particleManager;
        private Animation animation;

        private Player player;


        public Title()
        {
            isEndFlag = false;
            renderer = Renderer.Instance;
            particleManager = new ParticleManager();
            player = new Player(new Vector2(Screen.Width / 2 - 32, Screen.Height / 2 - 32));
            animation = new Animation(new Point(467,235), 4, 0.15f,Animation.AnimationType.Vertical);
        }

        public void Draw()
        {
            GameDevice.Instance().GetGraphicsDevice().Clear(Color.Black);

            renderer.Begin();

            var titledrawer = new Drawer();
            titledrawer.Rectangle = animation.GetRectangle();
            renderer.DrawTexture("title", new Vector2(20, 0), titledrawer);

            player.Draw();
            particleManager.Draw();
            renderer.End();
        }

        public void Initialize()
        {
            isEndFlag = false;
            particleManager.Initialize();
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
            if (Input.GetKeyTrigger(Keys.Enter))
            {
                //シーン移動
                isEndFlag = true;
            }
            animation.Update(gameTime);
            particleManager.Update(gameTime);
            titleTimer.Update(gameTime);
            player.ModeTitle();
            player.Update(gameTime);
        }
    }
}
