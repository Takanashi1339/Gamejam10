using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoundyShooter.Actor.Entities;
using BoundyShooter.Util;
using Microsoft.Xna.Framework;

namespace BoundyShooter.Actor
{
    class LifeWall : GameObject
    {
        private static List<LifeWall> lifeWalls = null;
        private LifeWall(Vector2 position)
            : base("life_wall", position, new Point(448,16))
        {
        }

        public LifeWall(LifeWall other)
            :this(other.Position)
        { }

        public static void Initialze()
        {
            lifeWalls = null;
        }

        public static List<LifeWall> GenerateWall(int wallCount)
        {
            if(lifeWalls == null)
            {
                lifeWalls = new List<LifeWall>();
            }
            for (int i = 0; i < wallCount; i++)
            {
                lifeWalls.Add(new LifeWall(new Vector2(0, 640 + 30 * i))
                    );
            }
            return lifeWalls;
        }
        public override object Clone()
        {
            return new LifeWall(this);
        }

        public override void Update(GameTime gameTime)
        {
            Console.WriteLine(lifeWalls.Count);
            if(lifeWalls.Count <= 0)
            {
                //ゲームオーバの処理をかく
            }
            base.Update(gameTime);
            lifeWalls.RemoveAll(l => l.IsDead);
        }
        public override void Draw()
        {
            var drawer = Drawer.Default;
            base.Draw(drawer);
        }

        public override void Hit(GameObject gameObject)
        {
        //    if(gameObject is Enemy)
        //    {
        //        IsDead = true;
        //    }
        }

    }
}
