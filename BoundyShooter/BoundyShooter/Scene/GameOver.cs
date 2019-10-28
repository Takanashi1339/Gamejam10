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

namespace BoundyShooter.Scene
{
    class GameOver : IScene
    {
        private bool isEndFlag;

        private Flashing flashing;
        private Flashing gameOverFlash;
        private Sound sound;
        public GameOver()
        {
            isEndFlag = false;
            flashing = new Flashing(1.0f, 0.2f, 1f);
            gameOverFlash = new Flashing(1.0f, 0f, 2.5f, false);
            sound = GameDevice.Instance().GetSound();
        }

        public void Draw()
        {
            GameDevice.Instance().GetGraphicsDevice().Clear(Color.Black);

            var gameOverdrawer = Drawer.Default;
            var backDrawer = Drawer.Default;
            gameOverdrawer.Alpha = gameOverFlash.GetAlpha();
            backDrawer.Alpha = flashing.GetAlpha();

            Renderer.Instance.Begin();
            Renderer.Instance.DrawTexture("testgameover", Vector2.Zero, gameOverdrawer);
            if(gameOverFlash.EndFlashing())
            {
                Renderer.Instance.DrawTexture("back_to_title", new Vector2(0, Screen.Height * 3 / 4), backDrawer);
            }
            Renderer.Instance.End();
        }

        public void Initialize()
        {
            isEndFlag = false;
            gameOverFlash.Reset();
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
            gameOverFlash.Update(gameTime);
            if (gameOverFlash.EndFlashing() &&Input.GetKeyTrigger(Keys.Space))
            {
                sound.PlaySE("decide");
                //シーン移動
                isEndFlag = true;
            }
        }
    }
}
