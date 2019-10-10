using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace BoundyShooter.Actor.Block
{
    class TestBlock : Block
    {
        public TestBlock() 
            : base("test_block", true)
        {
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
