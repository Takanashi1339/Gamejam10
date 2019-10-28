﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using BoundyShooter.Actor;
using BoundyShooter.Device;
using BoundyShooter.Manager;
using BoundyShooter.Util;
using BoundyShooter.Def;
using BoundyShooter.Actor.Entities;

namespace BoundyShooter.Scene
{
    class GamePlay : IScene
    {
        public static bool ScrollStop = false;

        private bool isEndFlag;
        private Scene next;

        private GameObjectManager gameObjectManager;
        private ParticleManager particleManager;
        private HitStop hitStop;

        private float scroll = 0;
        private string nowMap;
        private string[] mapName =
        {
            "tutorial.csv",
            "easy.csv",
            "normal.csv",
            "hard.csv",
        };

        //チュートリアルでスクロールが止まる高さ
        private int[] stopHeights =
        {
            137 * 32,
            115 * 32,
            91 * 32
        };

        public GamePlay()
        {
            isEndFlag = false;
        }

        public void Draw()
        {
            GameDevice.Instance().GetGraphicsDevice().Clear(Color.Black);

            Renderer.Instance.Begin();
            particleManager.Draw();
            gameObjectManager.Draw();
            Renderer.Instance.End();
        }

        public void Initialize()
        {
            isEndFlag = false;
            next = Scene.Ending;
            gameObjectManager = new GameObjectManager();
            particleManager = new ParticleManager();
            gameObjectManager.Initialize();
            particleManager.Initialize();
            LifeWall.Reset();
            var lifeWalls = LifeWall.GenerateWall(LifeWall.Count);
            gameObjectManager.AddWall(lifeWalls);
            nowMap = mapName[(int)Menu.GetDifficulty()];
            // csvからマップを読み込む場合

            var reader = GameDevice.Instance().GetCSVReader();
            //reader.Read("normal.csv");
            reader.Read(nowMap);
            var map = new Map(reader.GetData());
            gameObjectManager.Add(map);
            scroll = map.Height;
            ScrollStop = false;
            hitStop = new HitStop();

            GameDevice.Instance().GetSound().PlayBGM(
                (Menu.GetDifficulty() == Menu.Difficulty.tutorial)
                ? "tutorial"
                : "stage"
                );
        }

        public bool IsEnd()
        {
            return isEndFlag;
        }

        public Scene Next()
        {
            return next;
        }

        public void Shutdown()
        {
            GameDevice.Instance().GetSound().StopBGM();
        }

        public void Update(GameTime gameTime)
        {

#if DEBUG
            hitStop.Update(gameTime);
            if(hitStop.isHitStop)
            {
                return;
            }
            if (Input.GetKeyTrigger(Keys.Enter))
            {
                //シーン移動
                isEndFlag = true;
                next = Scene.Ending;
            }
#endif

            if (gameObjectManager.Map.CheckAllBlockDead())
            {
                //シーン移動
                isEndFlag = true;
                next = Scene.GameOver;
            }

            if(!LifeWall.AllIsDead() && !ScrollStop)
            {
                scroll--;
            }
            if (scroll < Screen.Height)
            {
                scroll = Screen.Height;
            }

            if (Menu.GetDifficulty() == Menu.Difficulty.tutorial && !ScrollStop)
            {
                foreach (var height in stopHeights)
                {
                    if (scroll == height)
                    {
                        ScrollStop = true;
                    }
                }
            }else if (ScrollStop)
            {
                if (gameObjectManager.Find<Enemy>().FindIndex(enemy => enemy.IsInScreen()) == -1)
                {
                    ScrollStop = false;
                }
            }

            GameDevice.Instance().DisplayModify = new Vector2(0, -scroll + Screen.Height);
            gameObjectManager.Update(gameTime);
            particleManager.Update(gameTime);
        
            if(gameObjectManager.Find<Boss>().Count == 0)
            {
                isEndFlag = true;
                next = Scene.Ending;
            }
        }
    }
}
