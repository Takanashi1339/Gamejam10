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
        private int spawnEnemy;
        private Renderer renderer;
        private ParticleManager particleManager;
        private Animation animation;

        private Player titlePlayer;
        private List<JellyEnemy> jellyEnemies;
        private Flashing flashing;
        private Sound sound;


        public Title()
        {
            isEndFlag = false;
            renderer = Renderer.Instance;
            flashing = new Flashing(1.0f, 0f,0.66f);
            sound = GameDevice.Instance().GetSound();
            spawnEnemy = 4;
        }

        public void Draw()
        {
            GameDevice.Instance().GetGraphicsDevice().Clear(Color.Black);

            renderer.Begin();

            var titledrawer = new Drawer();
            titledrawer.Rectangle = animation.GetRectangle();
            renderer.DrawTexture("title", new Vector2(20, 0), titledrawer);

            var pressdrawer = new Drawer();
            pressdrawer.Alpha = flashing.GetAlpha();
            renderer.DrawTexture("press_space_key", new Vector2(4, 690), pressdrawer);

            particleManager.Draw();
            titlePlayer.Draw();
            jellyEnemies.ForEach(e => e.Draw());

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
            jellyEnemies = new List<JellyEnemy>();
            for(int i = 0; i < spawnEnemy;i++)
            {
                jellyEnemies.Add(new JellyEnemy(new Vector2((GameDevice.Instance().GetRandom().Next(7) + 1) * 64,
                    (GameDevice.Instance().GetRandom().Next(6) + 1) * 64 + titleBottom)));
            }
            sound.PlayBGM("title");
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
                sound.PlaySE("decide");
                //シーン移動
                isEndFlag = true;
            }
            flashing.Update(gameTime);
            particleManager.Update(gameTime);
            animation.Update(gameTime);            
            titlePlayer.ModeTitle();
            titlePlayer.Update(gameTime);
            jellyEnemies.ForEach(e => e.DisplayMode());
            jellyEnemies.ForEach(e => e.Update(gameTime));
        }
    }
}
