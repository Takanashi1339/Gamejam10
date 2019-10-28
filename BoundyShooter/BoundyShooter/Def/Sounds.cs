using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoundyShooter.Def
{
    static class Sounds
    {
        //テクスチャディレクトリのデフォルトパス
        public static readonly string Path = "./Sound/";

        //SE(MP3)読み込み対象データ
        public static readonly string[,] SEData = new string[,]
        {
            //{ "se_name", Path },
            { "gun" , Path},
            { "enemy_hit" , Path},
            { "block_destroy" , Path},
            { "charge" , Path},
            { "charging" , Path},
            { "shoot" , Path},
            { "select", Path },
            { "decide", Path },

            //必要に応じて自分で追加
        };


        //BGM(MP3)読み込み対象データ
        public static readonly string[,] BGMData = new string[,]
        {
            //{ "bgm_name", Path },

            //必要に応じて自分で追加
        };
    }
}
