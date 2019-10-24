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
        private bool isEndFlag,reverse;
        public static readonly int titleBottom = 235;
        private float alpha, maxAlhpa,minAlhpa,plusAlhpa;
        private Renderer renderer;
        private ParticleManager particleManager;
        private Animation animation;

        private Player titlePlayer;
        private List<JellyEnemy> jellyEnemies;


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
            pushdrawer.Alpha = alpha;
            renderer.DrawTexture("push_to_space", new Vector2(39, 700), pushdrawer);

            particleManager.Draw();
            titlePlayer.Draw();
            foreach (var e in jellyEnemies)
            {
                e.Draw();
            }

            renderer.End();
        }

        public void Initialize()
        {
            isEndFlag = false;
            reverse = false;
            particleManager = new ParticleManager();
            particleManager.Initialize();
            GameDevice.Instance().DisplayModify = Vector2.Zero;
            titlePlayer = new Player(new Vector2(Screen.Width / 2 - 32, Screen.Height / 2 - 32));
            animation = new Animation(new Point(467, 235), 4, 0.15f, Animation.AnimationType.Vertical);
            alpha = 0;
            maxAlhpa = 1;
            minAlhpa = 0;
            plusAlhpa = 0.05f;
            jellyEnemies = new List<JellyEnemy>();
            for(int i = 0; i < 4;i++)
            {
                jellyEnemies.Add(new JellyEnemy(new Vector2((GameDevice.Instance().GetRandom().Next(7) + 1) * 64,
                    (GameDevice.Instance().GetRandom().Next(6) + 1) * 64 + 235)));
            }
        }

        public bool IsEnd()
        {
            return isEndFlag;
        }

        public Scene Next()
        {
            return Scene.Menu;
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
            if(alpha <= minAlhpa ||
                alpha >= maxAlhpa)
            {
                reverse = !reverse;
            }
            if(reverse)
            {
                alpha += plusAlhpa;
            }
            else if (!reverse)
            {
                alpha -= plusAlhpa;
            }
            particleManager.Update(gameTime);
            animation.Update(gameTime);            
            titlePlayer.ModeTitle();
            titlePlayer.Update(gameTime);
            foreach (var e in jellyEnemies)
            {
                e.ModeTitle();
                e.Update(gameTime);
            }
        }
    }
}
