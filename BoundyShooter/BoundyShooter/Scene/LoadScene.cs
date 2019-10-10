using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using BoundyShooter.Def;
using BoundyShooter.Device;
using BoundyShooter.Util;

namespace BoundyShooter.Scene
{
    class LoadScene : IScene
    {
        //読み込み用オブジェクト
        private TextureLoader textureLoader;
        private BGMLoader bgmLoader;
        private SELoader seLoader;
        private Renderer renderer;

        private int totalResouceNum;//全リソース数
        private bool isEndFlag;//終了フラグ
        private Timer timer;//演出用タイマー


        #region テクスチャ用
        /// <summary>
        /// テクスチャ読み込み用２次元配列の取得
        /// </summary>
        /// <returns></returns>
        private string[,] textureMatrix()
        {
            //string path = Textures.Path;

            //読み込み対象データ
            string[,] data = Textures.Data;

            return data;
        }
        #endregion テクスチャ用

        #region BGM用
        /// <summary>
        /// BGM読み込み用２次元配列の取得
        /// </summary>
        /// <returns></returns>
        private string[,] BGMMatrix()
        {
            string[,] data = Sounds.BGMData;

            return data;
        }
        #endregion BGM用

        #region SE用
        /// <summary>
        /// SE読み込み用２次元配列の取得
        /// </summary>
        /// <returns></returns>
        private string[,] SEMatrix()
        {
            //BGM(MP3)読み込み対象データ
            string[,] data = Sounds.SEData;

            return data;
        }
        #endregion SE用

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LoadScene()
        {
            //描画オブジェクトを取得
            renderer = Renderer.Instance;

            //読み込む対象を取得し、実体生成
            textureLoader = new TextureLoader(textureMatrix());
            bgmLoader = new BGMLoader(BGMMatrix());
            seLoader = new SELoader(SEMatrix());
            isEndFlag = false;

            timer = new Timer(1 / 60f, true);
        }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="renderer"></param>
        public void Draw()
        {
            //描画開始
            renderer.Begin();

            renderer.DrawTexture("load", new Vector2(20, 20), Drawer.Default);

            //現在読み込んでいる数を取得
            int currentCount =
                textureLoader.CurrentCount() +
                bgmLoader.CurrentCount() +
                seLoader.CurrentCount();

            //読み込むモノがあれば描画
            if (totalResouceNum != 0)
            {
                //読み込んだ割合
                float rate = (float)currentCount / totalResouceNum;

                /*
                //数字で描画
                renderer.DrawNumber(
                    "number",
                    new Vector2(20, 100),
                    (int)(rate * 100.0f));
                */

                //バーで描画
                var drawer = new Drawer();
                drawer.Scale = new Vector2(rate * Screen.Width, 20);

                renderer.DrawTexture(
                    "white",
                    new Vector2(0, 500),
                    drawer);
            }

            //終了
            //すべてのデータを読み込んだか？
            if (textureLoader.IsEnd() && bgmLoader.IsEnd() && seLoader.IsEnd())
            {
                isEndFlag = true;
            }

            //描画終了
            renderer.End();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            //終了フラグを継続に設定
            isEndFlag = false;
            //テクスチャ読み込みオブジェクトを初期化
            textureLoader.Initialize();
            //BGM読み込みオブジェクトを初期化
            bgmLoader.Initialize();
            //SE読み込みオブジェクトを初期化
            seLoader.Initialize();
            //全リソース数を計算
            totalResouceNum =
                textureLoader.RegistMAXNum() +
                bgmLoader.RegistMAXNum() +
                seLoader.RegistMAXNum();
        }

        /// <summary>
        /// シーン終了か？
        /// </summary>
        /// <returns></returns>
        public bool IsEnd()
        {
            return isEndFlag;
        }

        /// <summary>
        /// 次のシーン
        /// </summary>
        /// <returns></returns>
        public Scene Next()
        {
            return Scene.Title;
        }

        /// <summary>
        /// 終了
        /// </summary>
        public void Shutdown()
        {

        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            //演出確認用（大量のデータがあるときは設定時間を０に）
            //一定時間ごとに読み込み
            timer.Update(gameTime);
            if (timer.IsTime == false)
            {
                return;
            }

            //テクスチャから順々に読み込みを行う
            if (textureLoader.IsEnd() == false)
            {
                textureLoader.Update(gameTime);
            }
            else if (bgmLoader.IsEnd() == false)
            {
                bgmLoader.Update(gameTime);
            }
            else if (seLoader.IsEnd() == false)
            {
                seLoader.Update(gameTime);
            }
        }
    }
}
