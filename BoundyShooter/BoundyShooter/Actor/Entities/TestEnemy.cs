using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace BoundyShooter.Actor.Entities
{
    class TestEnemy : Enemy
    {
        public TestEnemy(Vector2 position)
            : base("test_enemy", position, new Point(64, 64),3f)
        {
            MaxSpeed = 3;
        }

        public TestEnemy(TestEnemy other)
            :this(other.Position)
        { }
        public override object Clone()
        {
            return new TestEnemy(this);
        }

        public override void Update(GameTime gameTime)
        {
            if (!IsInScreen()) return;
            if(Velocity.Y > MaxSpeed)
            {
                Velocity = new Vector2(0, MaxSpeed);
            }
            base.Update(gameTime);
        }

        public override void Hit(GameObject gameObject)
        {
            base.Hit(gameObject);
        }

    }
}
