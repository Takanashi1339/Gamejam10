using Microsoft.Xna.Framework;
using BoundyShooter.Manager.Interface;
using BoundyShooter.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoundyShooter.Manager
{
    class SceneManager : ISceneMediator
    {
        public static SceneManager Instance
        {
            get;
            private set;
        }
        public IScene CurrentScene
        {
            get;
            private set;
        }

        private Dictionary<Scene.Scene, IScene> scenes;

        public SceneManager()
        {
            Instance = this;
            Initialize();
        }

        public void Initialize()
        {
            if (scenes == null)
            {
                scenes = new Dictionary<Scene.Scene, IScene>();
            }
            else
            {
                scenes.Clear();
            }
        }

        public void Change(Scene.Scene name)
        {
            if (CurrentScene != null)
            {
                CurrentScene.Shutdown();
            }
            CurrentScene = scenes[name];
            CurrentScene.Initialize();
        }

        public void Update(GameTime gameTime)
        {
            if (CurrentScene == null) return;
            CurrentScene.Update(gameTime);

            if (CurrentScene.IsEnd())
            {
                CurrentScene.Shutdown();
                Change(CurrentScene.Next());
            }
        }

        public void Draw()
        {
            CurrentScene.Draw();
        }

        public void Add(Scene.Scene name, IScene scene)
        {
            scenes.Add(name, scene);
        }
    }
}
