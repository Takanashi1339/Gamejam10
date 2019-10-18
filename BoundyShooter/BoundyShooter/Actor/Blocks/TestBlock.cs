using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace BoundyShooter.Actor.Blocks
{

    class TestBlock : Block
    {
        private string[] blockNames = new string[]
        {
            "block_1",
            "block_2",
            "block_3",
            "block_4",
        };

        public TestBlock() 
            : base("block_1", true)
        {
        }

        public override void Update(GameTime gameTime)
        {
            Name = blockNames[LifeWall.nowCount];
            base.Update(gameTime);
        }
        public override object Clone()
        {
            return new TestBlock();
        }

        public override void Hit(GameObject gameObject)
        {
        }
    }
}
