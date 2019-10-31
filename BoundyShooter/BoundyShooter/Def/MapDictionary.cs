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
            Data.Add("2", new WallBlock());
            Data.Add("w", new WhiteBlock());
            Data.Add("P", new Player(Vector2.Zero));
            Data.Add("EB", new EasyBoss(Vector2.Zero));
            Data.Add("NB", new NormalBoss(Vector2.Zero));
            Data.Add("HB", new HardBoss(Vector2.Zero));
            Data.Add("E", new FishEnemy(Vector2.Zero));
            Data.Add("Ej", new JellyEnemy(Vector2.Zero));
            Data.Add("E2", new TestEnemy2(Vector2.Zero));
            Data.Add("t1", new TutorialMessage(1));
            Data.Add("t2", new TutorialMessage(2));
            Data.Add("t3", new TutorialMessage(3));
            Data.Add("t4", new TutorialMessage(4));
            Data.Add("t5", new TutorialMessage(5));
            Data.Add("t6", new TutorialMessage(6));
            Data.Add("H", new HealItem(Vector2.Zero));

            //ここにEntity/Blockを追加
        }
    }
}
