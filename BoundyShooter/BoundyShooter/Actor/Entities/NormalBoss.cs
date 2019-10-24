using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoundyShooter.Def;
using Microsoft.Xna.Framework;

namespace BoundyShooter.Actor.Entities
{
    class NormalBoss : Boss
    {
        public NormalBoss(Vector2 position)
            : base("test_boss", position, new Point(256,256), 3f, 10, 1)
        {
        }

        public NormalBoss(NormalBoss other)
            :this(other.Position)
        { }

        public override object Clone()
        {
            return new NormalBoss(this);
        }

        public override Entity Spawn(Map map, Vector2 position)
        {
            position = new Vector2(Screen.Width / 2 - Size.X / 2, position.Y);
            return base.Spawn(map, position);
        }
        protected override void Attack()
        {
        }
    }
}
