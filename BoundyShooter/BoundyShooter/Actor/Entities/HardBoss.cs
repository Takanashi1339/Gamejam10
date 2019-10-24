using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoundyShooter.Def;
using BoundyShooter.Util;
using Microsoft.Xna.Framework;

namespace BoundyShooter.Actor.Entities
{
    class HardBoss : Boss
    {
        private Vector2 forword = new Vector2(0, 8);
        private Timer forwordTimer;
        public HardBoss(Vector2 position)
            : base("test_boss", position, new Point(256, 256), 2f, 10, 2)
        {
            forwordTimer = new Timer(5, true);
        }

        public HardBoss(HardBoss other)
            :this(other.Position)
        { }

        public override object Clone()
        {
            return new HardBoss(this);
        }

        public override Entity Spawn(Map map, Vector2 position)
        {
            position = new Vector2(Screen.Width / 2 - Size.X / 2, position.Y);
            return base.Spawn(map, position);
        }

        public override void Update(GameTime gameTime)
        {
            forwordTimer.Update(gameTime);
            if(forwordTimer.IsTime)
            {
                Velocity = forword;
                hitCount--;
            }
            base.Update(gameTime);
        }
        protected override void Attack()
        {
        }
    }
}
