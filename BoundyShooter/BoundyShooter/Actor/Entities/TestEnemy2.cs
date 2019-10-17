using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoundyShooter.Actor.Blocks;
using BoundyShooter.Actor.Particles;
using Microsoft.Xna.Framework;

namespace BoundyShooter.Actor.Entities
{
    class TestEnemy2 : Enemy
    {
        private int reverse = -1;
        private int enemyDir = 0;
        private int xSpeed = 0;
        private Vector2 velocity;
        static Random rand = new Random();
        public TestEnemy2(Vector2 position)
            : base("test_enemy", position, new Point(64,64), 2)
        {
            enemyDir = rand.Next(2);
            xSpeed = rand.Next(2) + 3;
            if (enemyDir == 0)
            {
                velocity = new Vector2(xSpeed, 1);
            }
            else
            {
                velocity = new Vector2(-xSpeed, 1);
            }
        }

        public TestEnemy2(TestEnemy2 other)
            :this(other.Position)
        { }

        public override Entity Spawn(Map map, Vector2 position)
        {
            return base.Spawn(map, position);
        }

        public override object Clone()
        {
            return new TestEnemy2(this);
        }

        public override void Update(GameTime gameTime)
        {
            if (!IsInScreen()) return;
            Velocity = velocity;
            base.Update(gameTime);
        }

        public override void Hit(GameObject gameObject)
        {
            if(gameObject is Block block && block.IsSolid )
            {
                Direction dir = CheckDirection(block);
                CorrectPosition(block);
                if(dir == Direction.Left ||
                    dir == Direction.Right)
                {
                    velocity.X *= reverse;
                }
            }
            base.Hit(gameObject);
            if (IsDead)
            {
                new DestroyParticle(Name, Position, Size, DestroyParticle.DestroyOption.Center);
            }
        }
    }
}
