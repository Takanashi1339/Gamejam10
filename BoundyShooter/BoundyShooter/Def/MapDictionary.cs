using BoundyShooter.Actor;
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

            //ここにEntity/Blockを追加
        }
    }
}
