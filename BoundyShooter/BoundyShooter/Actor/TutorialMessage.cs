using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoundyShooter.Actor.Blocks;
using Microsoft.Xna.Framework;

namespace BoundyShooter.Actor
{
    class TutorialMessage : Block
    {
        public TutorialMessage(int messageNum)
            : this("tutorial" + messageNum)
        {
        }

        public TutorialMessage(string name)
            : base(name, false)
        {
        }

        public override object Clone()
        {
            return new TutorialMessage(Name);
        }

        public override void Hit(GameObject gameObject)
        {
        }
    }
}
