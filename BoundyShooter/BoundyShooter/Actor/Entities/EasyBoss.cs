using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoundyShooter.Util;
using Microsoft.Xna.Framework;

namespace BoundyShooter.Actor.Entities
{
    class EasyBoss : Boss
    {
        public EasyBoss(Vector2 position) 
            : base("test_boss", position, new Point(256,256), 1f, 10)
        {
        }

        public EasyBoss(EasyBoss other)
            :this(other.Position)
        { }

        public override object Clone()
        {
            return new EasyBoss(this);
        }
    }
}
