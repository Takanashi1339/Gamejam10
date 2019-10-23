using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoundyShooter.Actor.Blocks
{
    class WallBlock : Block
    {
        private string[] blockNames = new string[]
        {
            "block_1",
            "block_2",
            "block_3",
            "block_4",
        };

        public WallBlock()
            : base("block_1", false)
        {
        }

        public override void Update(GameTime gameTime)
        {
            Name = blockNames[LifeWall.nowCount];
            base.Update(gameTime);
        }
        public override object Clone()
        {
            return new WallBlock();
        }

        public override void Hit(GameObject gameObject)
        {
        }
    }
}
