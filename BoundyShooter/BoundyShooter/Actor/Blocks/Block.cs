using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoundyShooter.Util;
using Microsoft.Xna.Framework;

namespace BoundyShooter.Actor.Blocks
{
    abstract class Block : GameObject
    {
        public const int BlockSize = 32;
        public Map Map
        {
            get;
            private set;
        }

        //当たり判定を持つかどうか
        public bool IsSolid
        {
            get;
            protected set;
        }

        public Block(string name, bool isSolid)
            : base(name, Vector2.Zero, new Point(BlockSize, BlockSize))
        {
            IsSolid = isSolid;
        }

        public virtual Block Set(Map map, Vector2 position)
        {
            Position = position;
            Map = map;
            return this;
        }

        protected override void Draw(Drawer drawer)
        {
            drawer.DisplayModify = true;
            base.Draw(drawer);
        }
    }
}
