using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace BoundyShooter.Actor.Entities
{
    class HealItem : Entity
    {
        public HealItem(Vector2 position)
            : base("test_heal", position, new Point(0,0))
        {
        }

        public HealItem(HealItem other)
            :this(other.Position)
        { }
        public override object Clone()
        {
            return new HealItem(this);
        }

        public override void Hit(GameObject gameObject)
        {
            if(gameObject is Player)
            {
                IsDead = true;
            }
            base.Hit(gameObject);
        }
        public override void Draw()
        {
            base.Draw();
        }
    }
}
