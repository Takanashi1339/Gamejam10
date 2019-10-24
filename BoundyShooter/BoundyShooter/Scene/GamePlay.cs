using System;
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
        private bool isEndFlag;
        private Scene next;

        private GameObjectManager gameObjectManager;
        private ParticleManager particleManager;

        private float scroll = 0;

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
            LifeWall.Initialze();
            var lifeWalls = LifeWall.GenerateWall(LifeWall.Count);
            gameObjectManager.AddWall(lifeWalls);
            // csvからマップを読み込む場合

            var reader = GameDevice.Instance().GetCSVReader();
            reader.Read("hard.csv");
            var map = new Map(reader.GetData());
            gameObjectManager.Add(map);
            scroll = map.Height;
        }

        public bool IsEnd()
        {
            return isEndFlag;
        }

        public Scene Next()
        {
            return next; ;
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
                next = Scene.Ending;
            }

            if (gameObjectManager.Map.CheckAllBlockDead())
            {
                //シーン移動
                isEndFlag = true;
                next = Scene.GameOver;
            }

            if(!LifeWall.AllIsDead())
            {
                scroll--;
            }
            if (scroll < Screen.Height)
            {
                scroll = Screen.Height;
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
