using BoundyShooter.Actor.Blocks;
using BoundyShooter.Actor.Particles;
using BoundyShooter.Def;
using BoundyShooter.Device;
using BoundyShooter.Util;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoundyShooter.Actor
{
    class BossAlert
    {
        public bool Initialized
        {
            get;
            private set;
        } = false;

        public const int AlertHeight = 42 * Block.BlockSize;

        private string name =  "boss_alert";
        private Point size;
        private Vector2 position;

        private Timer breakTimer;
        private DestroyParticle destroyParticle;
        private HealParticle healParticle;

        public BossAlert()
        {
        }

        public void Initialize()
        {
            size = new Point(320, 160);
            position = new Vector2(Screen.Width / 2 - size.X / 2, Screen.Height / 2 - size.Y / 2);
            healParticle = new HealParticle(name, position, size, 20, false);
            destroyParticle = null;
            breakTimer = new Timer(1f);
            Initialized = true;
        }

        public void Update(GameTime gameTime)
        {
            if(healParticle.IsDead)
            {
                breakTimer.Update(gameTime);
            }
            if(breakTimer.IsTime && destroyParticle == null)
            {
                destroyParticle = new DestroyParticle(name, position, size, DestroyParticle.DestroyOption.Center ,10, false);
                Initialized = false;
            }
        }

        public void Draw()
        {
            var drawer = Drawer.Default;
            if(healParticle.IsDead && !breakTimer.IsTime)
            {
                Renderer.Instance.DrawTexture(name, position, drawer);
            }
        }
    }
}
