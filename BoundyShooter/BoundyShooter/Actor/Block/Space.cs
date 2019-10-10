using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoundyShooter.Actor.Block
{
    class Space : Block
    {
        public Space()
            : base("", false)
        {
        }

        public override object Clone()
        {
            return new Space();
        }

        public override void Draw()
        {
            //描画しない
        }

        public override void Hit(GameObject gameObject)
        {
        }
    }
}
