using Microsoft.Xna.Framework;
using BoundyShooter.Actor;
using BoundyShooter.Manager.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoundyShooter.Manager
{
    class GameObjectManager : IGameObjectMediator
    {
        private List<GameObject> addGameObjects;
        private List<GameObject> gameObjects;
        private Map nextMap;
        public Map Map
        {
            get;
            private set;
        }

        public List<LifeWall> LifeWalls
        {
            get;
            private set;
        }


        public static IGameObjectMediator Instance
        {
            get;
            private set;
        }

        public GameObjectManager()
        {
            Initialize();
        }

        public void Initialize()
        {
            var instance = this;
            Instance = instance;

            if (instance.gameObjects == null)
            {
                instance.gameObjects = new List<GameObject>();
            }
            else
            {
                instance.gameObjects.Clear();
            }

            if (instance.addGameObjects == null)
            {
                instance.addGameObjects = new List<GameObject>();
            }
            else
            {
                instance.addGameObjects.Clear();
            }
            if (LifeWalls == null)
            {
                LifeWalls = new List<LifeWall>();
            }
        }

        public void Add(GameObject obj)
        {
            addGameObjects.Add(obj);
        }

        public void Add(Map map)
        {
            if (Map == null)
            {
                Map = map;
            }
            else
            {
                nextMap = map;
            }
        }
        public void AddWall(List<LifeWall> lifeWalls)
        {
            LifeWalls = lifeWalls;
        }

        public void Update(GameTime gameTime)
        {
            gameObjects.AddRange(addGameObjects);
            addGameObjects.Clear();
            Map.Update(gameTime);
            LifeWalls.ForEach(l => l.Update(gameTime));
            gameObjects.ForEach(obj => obj.Update(gameTime));
            HitToMap();
            HitToGameObject();
            HitToLifeWall();

            gameObjects.RemoveAll(obj => obj.IsDead);
            if (nextMap != null)
            {
                Map = nextMap;
                nextMap = null;
            }
        }

        public void Draw()
        {
            Map.Draw();
            gameObjects.ForEach(obj => obj.Draw());
            LifeWalls.ForEach(l => l.Draw());
        }

        /// <summary>
        /// T型のGameObjectのListを取得する
        /// </summary>
        /// <typeparam name="T">GameObjectを継承した型引数</typeparam>
        /// <returns>GameObjectManagerに登録されているT型のList</returns>
        public List<T> Find<T>()
            where T : GameObject
        {
            var result = new List<T>();
            foreach (var obj in gameObjects)
            {
                if (obj is T objT)
                {
                    result.Add(objT);
                }
            }
            return result;
        }

        public void HitToMap()
        {
            gameObjects.ForEach(obj => Map.Hit(obj));
        }

        public void HitToGameObject()
        {
            gameObjects.ForEach(obj1 =>
            {
                gameObjects.ForEach(obj2 =>
                {
                    if (obj1.Equals(obj2) ||
                        obj1.IsDead ||
                        obj2.IsDead)
                    {
                        return;
                    }

                    if (obj1.IsCollision(obj2))
                    {
                        obj1.Hit(obj2);
                        obj2.Hit(obj1);
                    }
                });
            });
        }

        public void HitToLifeWall()
        {
            foreach (var wall in LifeWalls)
            {
                foreach(var obj in gameObjects)
                {
                    if(wall.IsDead || obj.IsDead)
                    {
                        return;
                    }
                    if(wall.IsCollision(obj))
                    {
                        wall.Hit(obj);
                        obj.Hit(wall);
                    }
                }
            }
        }

    }
}
