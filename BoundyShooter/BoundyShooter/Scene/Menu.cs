using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoundyShooter.Actor.Entities;
using BoundyShooter.Def;
using BoundyShooter.Device;
using BoundyShooter.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BoundyShooter.Scene
{
    class Menu : IScene
    {
        private bool isEndFlag;
        private bool checkMoveScene;
        private int maxFishEnemy;
        private float notSelectAlpha;
        private Vector2 defaultDrawPos;
        private Vector2 difficultySpace;
        private Vector2 nowPos;
        private static int checkSelectvalue;
        private static int maxSelectValue;
        private static int difficultyNumber;
        private string[] difficultyName = {
            "tutorial",
            "easy",
            "normal",
            "hard",
        };

        private Player player;
        private List<FishEnemy> fishEnemies;
        private Flashing flashing;

        public enum Difficulty
        {
            tutorial,
            easy,
            normal,
            hard,
        }
        public Menu()
        {
            isEndFlag = false;
            checkMoveScene = true;
            maxFishEnemy = 5;
            checkSelectvalue = 0;
            maxSelectValue = 100;
            notSelectAlpha = 0.2f;
            defaultDrawPos = new Vector2(0, 300);
            difficultySpace = new Vector2(0, 100);
            player = new Player(defaultDrawPos);
            flashing = new Flashing(1.0f, 0.4f, 1f);
        }
        public void Draw()
        {
            GameDevice.Instance().GetGraphicsDevice().Clear(Color.Black);
            var tutorialDrawer = Drawer.Default;
            var easyDrawer = Drawer.Default;
            var normalDrawer = Drawer.Default;
            var hardDrawer = Drawer.Default;
            var drawer = Drawer.Default;
            var renderer = Renderer.Instance;
            if (difficultyNumber == (int)Difficulty.tutorial)
            {
                tutorialDrawer.Alpha = flashing.GetAlpha();
                easyDrawer.Alpha = notSelectAlpha;
                normalDrawer.Alpha = notSelectAlpha;
                hardDrawer.Alpha = notSelectAlpha;
                nowPos = defaultDrawPos;
            }
            if (difficultyNumber == (int)Difficulty.easy)
            {
                tutorialDrawer.Alpha = notSelectAlpha;
                easyDrawer.Alpha = flashing.GetAlpha();
                normalDrawer.Alpha = notSelectAlpha;
                hardDrawer.Alpha = notSelectAlpha;
                nowPos = defaultDrawPos + difficultySpace;
            }
            if(difficultyNumber == (int)Difficulty.normal)
            {
                tutorialDrawer.Alpha = notSelectAlpha;
                easyDrawer.Alpha = notSelectAlpha;
                normalDrawer.Alpha = flashing.GetAlpha();
                hardDrawer.Alpha = notSelectAlpha;
                nowPos = defaultDrawPos + difficultySpace * 2;
            }
            if(difficultyNumber == (int)Difficulty.hard)
            {
                tutorialDrawer.Alpha = notSelectAlpha;
                easyDrawer.Alpha = notSelectAlpha;
                normalDrawer.Alpha = notSelectAlpha;
                hardDrawer.Alpha = flashing.GetAlpha();
                nowPos = defaultDrawPos + difficultySpace * 3;
            }

            Renderer.Instance.Begin();
            fishEnemies.ForEach(f => f.Draw());
            renderer.DrawTexture("menu_explanation", Vector2.Zero, drawer);
            renderer.DrawTexture(difficultyName[(int)Difficulty.tutorial], defaultDrawPos, tutorialDrawer);
            renderer.DrawTexture(difficultyName[(int)Difficulty.easy], defaultDrawPos + difficultySpace, easyDrawer);
            renderer.DrawTexture(difficultyName[(int)Difficulty.normal], defaultDrawPos + difficultySpace * 2, normalDrawer);
            renderer.DrawTexture(difficultyName[(int)Difficulty.hard], defaultDrawPos + difficultySpace * 3, hardDrawer);
            player.Draw();
            Renderer.Instance.End();
        }

        public void Initialize()
        {
            isEndFlag = false;
            checkMoveScene = true;
            checkSelectvalue = 0;
            fishEnemies = new List<FishEnemy>();
            difficultyNumber = 0;
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
            if(fishEnemies.Count < maxFishEnemy)
            {
                for(int i = fishEnemies.Count; i < maxFishEnemy; i++)
                {
                    fishEnemies.Add(
                        new FishEnemy(new Vector2(
                        GameDevice.Instance().GetRandom().Next(Screen.Width),
                        GameDevice.Instance().GetRandom().Next(0, Screen.Height / 2))
                        )
                        );
                }
            }
            fishEnemies.ForEach(f => f.DisplayMode());
            fishEnemies.ForEach(f => f.Update(gameTime));
            fishEnemies.RemoveAll(f => !f.IsInScreen());
            player.ModeMenu(nowPos);
            player.Update(gameTime);
            if (!(Input.GetKeyState(Keys.Space)))
            {
                checkSelectvalue = 0;
            }
            if (Input.GetKeyRelease(Keys.Space) && !checkMoveScene)
            {
                difficultyNumber++;
                flashing.Reset();
            }
            if(difficultyNumber > (int) Difficulty.hard)
            {
                difficultyNumber = (int)Difficulty.tutorial;
            }
            if(Input.GetKeyState(Keys.Space))
            {
                checkSelectvalue++;
            }
            if(checkSelectvalue > maxSelectValue)
            {
                isEndFlag = true;
            }
            if (Input.GetKeyRelease(Keys.Space))
            {
                checkMoveScene = false;
            }
            flashing.Update(gameTime);
        }

        public static Difficulty GetDifficulty()
        {
            return (Difficulty)difficultyNumber;
        }

        public static float SelectValueRate()
        {
            return (float)checkSelectvalue / maxSelectValue;
        }
    }
}
