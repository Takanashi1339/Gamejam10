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
        private float selectAlpha;
        private float maxSelectAlpha;
        private float minSelectAlpha;
        private float alphaIncrease;
        private Vector2 defaultDrawPos;
        private Vector2 difficultySpace;
        private Vector2 nowPos;
        private static int checkSelectvalue;
        private static int maxSelectValue;
        private static int difficultyNumber;
        private string[] difficultyName = {
            "easy",
            "normal",
            "hard",
        };

        private Player player;
        private List<FishEnemy> fishEnemies;

        public enum Difficulty
        {
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
            selectAlpha = 1.0f;
            maxSelectAlpha = 1.0f;
            minSelectAlpha = 0.4f;
            alphaIncrease = 0.03f;
            defaultDrawPos = new Vector2(0, 400);
            difficultySpace = new Vector2(0, 100);
            player = new Player(defaultDrawPos);
        }
        public void Draw()
        {
            GameDevice.Instance().GetGraphicsDevice().Clear(Color.Black);
            var easyDrawer = Drawer.Default;
            var normalDrawer = Drawer.Default;
            var hardDrawer = Drawer.Default;
            var drawer = Drawer.Default;
            var renderer = Renderer.Instance;
            if(difficultyNumber == (int)Difficulty.easy)
            {
                easyDrawer.Alpha = selectAlpha;
                normalDrawer.Alpha = notSelectAlpha;
                hardDrawer.Alpha = notSelectAlpha;
                nowPos = defaultDrawPos;
            }
            if(difficultyNumber == (int)Difficulty.normal)
            {
                easyDrawer.Alpha = notSelectAlpha;
                normalDrawer.Alpha = selectAlpha;
                hardDrawer.Alpha = notSelectAlpha;
                nowPos = defaultDrawPos + difficultySpace;
            }
            if(difficultyNumber == (int)Difficulty.hard)
            {
                easyDrawer.Alpha = notSelectAlpha;
                normalDrawer.Alpha = notSelectAlpha;
                hardDrawer.Alpha = selectAlpha;
                nowPos = defaultDrawPos + difficultySpace * 2;
            }

            Renderer.Instance.Begin();
            fishEnemies.ForEach(f => f.Draw());
            renderer.DrawTexture("menu_explanation", Vector2.Zero, drawer);
            renderer.DrawTexture(difficultyName[(int)Difficulty.easy], defaultDrawPos, easyDrawer);
            renderer.DrawTexture(difficultyName[(int)Difficulty.normal], defaultDrawPos + difficultySpace, normalDrawer);
            renderer.DrawTexture(difficultyName[(int)Difficulty.hard], defaultDrawPos + difficultySpace * 2, hardDrawer);
            player.Draw();
            Renderer.Instance.End();
        }

        public void Initialize()
        {
            isEndFlag = false;
            checkMoveScene = true;
            checkSelectvalue = 0;
            fishEnemies = new List<FishEnemy>();
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
                selectAlpha = 1.0f;
            }
            if(difficultyNumber > (int) Difficulty.hard)
            {
                difficultyNumber = (int)Difficulty.easy;
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
            if(selectAlpha > maxSelectAlpha || selectAlpha < minSelectAlpha)
            {
                alphaIncrease *= -1;
            }
            selectAlpha += alphaIncrease;
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
