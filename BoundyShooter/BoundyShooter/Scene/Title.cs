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
    class Title : IScene
    {
        private bool isEndFlag;
        private int titlenum;
        private Timer titleTimer;

        private Renderer renderer;

        public Title()
        {
            isEndFlag = false;
            renderer = Renderer.Instance;
            titlenum = 0;
            titleTimer = new Timer(0.15f, true);
        }

        public void Draw()
        {
            GameDevice.Instance().GetGraphicsDevice().Clear(Color.Black);

            renderer.Begin();

            var titledrawer = new Drawer();
            titledrawer.Rectangle = new Rectangle(0, 235 * titlenum, 467, 235);
            renderer.DrawTexture("title", new Vector2(20, 0), titledrawer);

            renderer.End();
        }

        public void Initialize()
        {
            isEndFlag = false;
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
            if (Input.GetKeyTrigger(Keys.Enter))
            {
                //シーン移動
                isEndFlag = true;
            }
            titleTimer.Update(gameTime);
            if (titleTimer.IsTime)
            {
                titlenum += 1;
            }
            if(titlenum == 4)
            {
                titlenum = 0;
            }
        }
    }
}
