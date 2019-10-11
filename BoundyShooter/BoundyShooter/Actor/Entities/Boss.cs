using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using BoundyShooter.Util;

namespace BoundyShooter.Actor.Entities
{
    class Boss : Entity
    {
        private Timer sumonTimer;
        private int hitCount;
        public Boss(Vector2 position) 
            : base("test_boss", position, new Point(256,256))
        {
            sumonTimer = new Timer(10f,true);
            hitCount = 0;
        }

        public Boss(Boss other)
            :this(other.Position)
        {
        }

        public override object Clone()
        {
            return new Boss(this);
        }

        public override void Update(GameTime gameTime)
        {
            sumonTimer.Update(gameTime);
            if(sumonTimer.IsTime)
            {

            }
            base.Update(gameTime);
        }

        public override void Hit(GameObject gameObject)
        {
            Direction dir = CheckDirection(gameObject);
            if (gameObject is Player)
            {
                if (dir == Direction.Top)
                {
                    Velocity = new Vector2(0, -8);
                    hitCount += 1;
                }
            }
            base.Hit(gameObject);
        }

        public override void Draw()
        {
            var drawer = Drawer.Default;
            drawer.DisplayModify = true;
            base.Draw(drawer);
        }
    }
}
