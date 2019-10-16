using System;
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
                { "pink_ball",Path },
                { "pink_tail",Path },
                { "test_block", Path },
                { "test_boss", Path },
                { "test_enemy", Path },
                { "life_wall" , Path},

                //必要に応じて自分で追加
            };
    }
}
