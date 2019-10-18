﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoundyShooter.Def
{
    static class Textures
    {
        //テクスチャディレクトリのデフォルトパス
        private static readonly string Path = "./Texture/";


        //読み込み対象データ
        public static readonly string[,] Data = new string[,]
            {
                //{ "texture_name", Path },
                { "player", Path },
                { "blade", Path },
                { "gun", Path },
                { "blue_particle",Path },
                { "pink_ball",Path },
                { "blue_ball",Path },
                { "pink_tail",Path },
                { "blue_tail",Path },
                { "test_block", Path },
                { "test_boss", Path },
                { "test_enemy", Path },
                { "test_gage", Path },
                { "test_gage_empty", Path },
                { "life_wall_1" , Path},
                { "life_wall_2" ,Path },
                { "life_wall_3" ,Path },
                { "life_wall_4" ,Path },
                { "block_1" ,Path },
                { "block_2" ,Path },
                { "block_3" ,Path },
                { "block_4" ,Path },
                { "white_block" ,Path },

                //必要に応じて自分で追加
            };
    }
}
