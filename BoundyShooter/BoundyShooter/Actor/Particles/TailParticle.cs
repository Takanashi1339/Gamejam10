using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoundyShooter.Util;
using Microsoft.Xna.Framework;

namespace BoundyShooter.Actor.Particles
{
    class TailParticle : Particle
    {
        public TailParticle(Vector2 position) 
            : base("pink_tail", position, new Point(32, 32), 8, 0.075f)
        {
        }

        public override object Clone()
        {
            return new TailParticle(Position);
        }

        public override void Hit(GameObject gameObject)
        {
        }
    }
}
