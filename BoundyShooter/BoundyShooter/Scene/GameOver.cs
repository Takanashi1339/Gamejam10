using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using BoundyShooter.Device;
using BoundyShooter.Util;

namespace BoundyShooter.Scene
{
    class GameOver : IScene
    {
        private bool isEndFlag;
        private float alpha;
        private float maxAlpha;
        private float alphaRate;

        public GameOver()
        {
            isEndFlag = false;
            alpha = 0f;
            maxAlpha = 1.0f;
            alphaRate = 0.005f;
        }

        public void Draw()
        {
            GameDevice.Instance().GetGraphicsDevice().Clear(Color.Black);

            var drawer = Drawer.Default;
            drawer.Alpha = alpha;

            Renderer.Instance.Begin();
            Renderer.Instance.DrawTexture("testgameover", Vector2.Zero, drawer);
            Renderer.Instance.End();
        }

        public void Initialize()
        {
            isEndFlag = false;
            alpha = 0f;
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
            alpha += alphaRate;
            if(alpha > maxAlpha)
            {
                alpha = maxAlpha;
            }
            if (alpha >= maxAlpha &&Input.GetKeyTrigger(Keys.Space))
            {
                //シーン移動
                isEndFlag = true;
            }
        }
    }
}
