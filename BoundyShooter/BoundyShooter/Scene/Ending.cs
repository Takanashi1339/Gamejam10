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
    class Ending : IScene
    {
        private bool isEndFlag;
        private Renderer renderer;

        private float alpha,maxAlpha,plusAlpha;

        public Ending()
        {
            isEndFlag = false;
            renderer = Renderer.Instance;
        }

        public void Draw()
        {
            GameDevice.Instance().GetGraphicsDevice().Clear(Color.Black);
            renderer.Begin();

            var cleardrawer = new Drawer();
            cleardrawer.Alpha = alpha;
            renderer.DrawTexture("clear", new Vector2(Screen.Width / 2 - 342 / 2, Screen.Height / 2 - 234 / 2), cleardrawer);

            renderer.End();
        }

        public void Initialize()
        {
            isEndFlag = false;
            alpha = 0;
            plusAlpha = 0.05f;
            maxAlpha = 1;
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
            if (Input.GetKeyTrigger(Keys.Enter))
            {
                //シーン移動
                isEndFlag = true;
            }
            alpha += plusAlpha;
            if(alpha >= maxAlpha)
            {
                alpha = maxAlpha;
            }
        }
    }
}
