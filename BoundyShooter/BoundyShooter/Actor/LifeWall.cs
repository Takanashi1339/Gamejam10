using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoundyShooter.Actor.Blocks;
using BoundyShooter.Actor.Entities;
using BoundyShooter.Actor.Particles;
using BoundyShooter.Def;
using BoundyShooter.Device;
using BoundyShooter.Util;
using Microsoft.Xna.Framework;

namespace BoundyShooter.Actor
{
    class LifeWall : GameObject
    {
        private Vector2 displayPos;

        private static List<LifeWall> lifeWalls = null;
        private static int space = 30;
        private static int nowCount = 0;
        private static Point size = new Point(448, 16);

        public static readonly int Count = 4;

        private static string[] wallnames = new string[]
        {
            "life_wall",
            "life_wall_2",
            "life_wall_3",
            "life_wall_4",
        };


        public LifeWall(Vector2 position)
            : base("life_wall",position, size)
        {
            displayPos = position;
            Position = -GameDevice.Instance().DisplayModify + displayPos;
        }

        public LifeWall(LifeWall other)
            :this(other.Position)
        { }

        public static void Initialze()
        {
            lifeWalls = null;
            nowCount = 0;
        }
        public static List<LifeWall> GenerateWall(int wallCount)
        {
            if(lifeWalls == null)
            {
                lifeWalls = new List<LifeWall>();
            }
            for (int i = 0; i < wallCount; i++)
            {
                lifeWalls.Add(new LifeWall(new Vector2(Block.BlockSize, (Screen.Height - size.Y) - space * i)
                    )
                    );
            }
            return lifeWalls;
        }

        public static bool DeathWallIsDead()
        {
            return lifeWalls.First().IsDead;
        }
        public override object Clone()
        {
            return new LifeWall(this);
        }

        public override void Update(GameTime gameTime)
        {
            if(nowCount >= lifeWalls.Count)
            {
                nowCount = lifeWalls.Count - 1;
            }
            Name = wallnames[nowCount];
            Position = -GameDevice.Instance().DisplayModify + displayPos;
            base.Update(gameTime);
        }
        public override void Draw()
        {
            if (IsDead) return;
            var drawer = Drawer.Default;
            drawer.DisplayModify = true;
            base.Draw(drawer);
        }

        public override void Hit(GameObject gameObject)
        {
            if (IsDead) return;
            if (gameObject is Enemy enemy)
            {
                BreakWall();
            }
        }

        public void BreakWall()
        {
            IsDead = true;
            new DestroyParticle(Name, Position, Size, DestroyParticle.DestroyOption.Center);
            nowCount++;
        }
    }
}
