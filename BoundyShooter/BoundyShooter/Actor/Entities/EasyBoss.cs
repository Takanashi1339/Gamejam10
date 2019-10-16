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
    class EasyBoss : Boss
    {
        public EasyBoss(Vector2 position) 
            : base("test_boss", position, new Point(256,256), 1f, 4)
        {

        }

        public EasyBoss(EasyBoss other)
            :this(other.Position)
        { }

        public override object Clone()
        {
            return new EasyBoss(this);
        }

        public override Entity Spawn(Map map, Vector2 position)
        {
            position = new Vector2(Screen.Width / 2 - Size.X / 2, position.Y);
            return base.Spawn(map, position);
        }

        public override void Update(GameTime gameTime)
        {
            Console.WriteLine(Position);
            base.Update(gameTime);
        }
    }
}
