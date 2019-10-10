using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoundyShooter.Device;
using Microsoft.Xna.Framework;

namespace BoundyShooter.Actor.Entities
{
    class Player : Entity
    {
        public Player(Vector2 position) 
            : base("player", position, new Point(64, 64))
        {
        }

        public override object Clone()
        {
            return new Player(Position);
        }

        public override void Update(GameTime gameTime)
        {
            GameDevice.Instance().DisplayModify = new Vector2(0, -Position.Y);

            base.Update(gameTime);
        }
    }
}
