using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoundyShooter.Device;
using BoundyShooter.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BoundyShooter.Scene
{
    class Menu : IScene
    {
        private bool isEndFlag;
        private int checkValue;
        private int MaxValue;
        private Vector2 defaultDrawPos;
        private static int difficultyNumber;
        private string[] difficultyName = {
            "easy",
            "normal",
            "hard",
        };

        public enum Difficulty
        {
            easy,
            normal,
            hard,
        }
        public Menu()
        {
            isEndFlag = false;
            checkValue = 0;
            defaultDrawPos = new Vector2(0, 500);
            MaxValue = 100;
        }
        public void Draw()
        {
            GameDevice.Instance().GetGraphicsDevice().Clear(Color.Black);
            var drawer = Drawer.Default;
            var renderer = Renderer.Instance;

            Renderer.Instance.Begin();
            renderer.DrawTexture("menu_explanation", Vector2.Zero, drawer);
            renderer.DrawTexture(difficultyName[difficultyNumber], defaultDrawPos, drawer);

            Renderer.Instance.End();
        }

        public void Initialize()
        {
            isEndFlag = false;
            checkValue = 0;
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
            if(!(Input.GetKeyState(Keys.Space)))
            {
                checkValue = 0;
            }
            if (Input.GetKeyRelease(Keys.Space))
            {
                difficultyNumber++;
            }
            if(difficultyNumber > (int) Difficulty.hard)
            {
                difficultyNumber = (int)Difficulty.easy;
            }
            if(Input.GetKeyState(Keys.Space))
            {
                checkValue++;
            }
            if(checkValue > MaxValue)
            {
                isEndFlag = true;
            }
        }

        public static Difficulty GetDifficulty()
        {
            return (Difficulty)difficultyNumber;
        }
    }
}
