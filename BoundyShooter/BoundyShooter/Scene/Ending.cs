﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using BoundyShooter.Device;
using BoundyShooter.Util;
using BoundyShooter.Def;
using BoundyShooter.Manager;
using BoundyShooter.Actor.Particles;

namespace BoundyShooter.Scene
{
    class Ending : IScene
    {
        private bool isEndFlag;
        private Renderer renderer;
        private Flashing flashing;
        private Sound sound;
        private Timer clearParticle;
        private ParticleManager particleManager;

        private float alpha,maxAlpha,plusAlpha;

        public Ending()
        {
            isEndFlag = false;
            renderer = Renderer.Instance;
            flashing = new Flashing(1.0f, 0f, 1f);
            sound = GameDevice.Instance().GetSound();
        }

        public void Draw()
        {
            GameDevice.Instance().GetGraphicsDevice().Clear(Color.Black);
            renderer.Begin();

            var cleardrawer = new Drawer();
            var backDrawer = new Drawer();
            cleardrawer.Alpha = alpha;
            backDrawer.Alpha = flashing.GetAlpha();
            renderer.DrawTexture("clear", new Vector2(Screen.Width / 2 - 342 / 2, Screen.Height / 2 - 234 / 2), cleardrawer);
            renderer.DrawTexture("back_to_title", new Vector2(0, Screen.Height * 7 / 8), backDrawer);
            particleManager.Draw();

            renderer.End();
        }

        public void Initialize()
        {
            isEndFlag = false;
            alpha = 0;
            plusAlpha = 0.05f;
            maxAlpha = 1;
            GameDevice.Instance().DisplayModify = Vector2.Zero;
            clearParticle = new Timer(0.25f, true);
            particleManager = new ParticleManager();
            particleManager.Initialize();
        }

        public bool IsEnd()
        {
            return isEndFlag;
        }

        public Scene Next()
        {
            return Scene.Title;
        }

        public void Shutdown()
        {
        }

        public void Update(GameTime gameTime)
        {
            flashing.Update(gameTime);
            if (Input.GetKeyTrigger(Keys.Space))
            {
                sound.PlaySE("decide");
                //シーン移動
                isEndFlag = true;
            }
            alpha += plusAlpha;
            if(alpha >= maxAlpha)
            {
                alpha = maxAlpha;
            }
            clearParticle.Update(gameTime);
            particleManager.Update(gameTime);
            if(clearParticle.IsTime)
            {
                new DestroyParticle("fireworks", new Vector2((GameDevice.Instance().GetRandom().Next(7) + 1) * 64,(GameDevice.Instance().GetRandom().Next(11) + 1) * 64), new Point(16, 16), DestroyParticle.DestroyOption.Center);
            }
        }
    }
}
