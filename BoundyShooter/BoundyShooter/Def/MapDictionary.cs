using BoundyShooter.Actor;
using BoundyShooter.Actor.Blocks;
using BoundyShooter.Actor.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoundyShooter.Def
{
    static class MapDictionary
    {
        public static readonly Dictionary<string, GameObject> Data;

        static MapDictionary()
        {
            Data = new Dictionary<string, GameObject>();
            Data.Add("0", new Space());
            Data.Add("1", new TestBlock());
            Data.Add("P", new Player(Vector2.Zero));
            Data.Add("B", new Boss(Vector2.Zero));

            //ここにEntity/Blockを追加
        }
    }
}
