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
        private static List<HealParticle> healParticles;

        private static List<LifeWall> lifeWalls = null;
        private static int space = 30;
        public static int nowCount = 0;
        private static Point size = new Point(448, 16);
        private static int maxWallCount;

        public static readonly int Count = 4;

        private static string[] wallNames = new string[]
        {
            "life_wall_1",
            "life_wall_2",
            "life_wall_3",
            "life_wall_4",
        };


        public LifeWall(Vector2 position)
            : base("life_wall_1",position, size)
        {
            displayPos = position;
            Position = -GameDevice.Instance().DisplayModify + displayPos;
            healParticles = new List<HealParticle>();
        }

        public LifeWall(LifeWall other)
            :this(other.Position)
        { }

        public static void Reset()
        {
            lifeWalls = null;
            nowCount = 0;
        }
        public static List<LifeWall> GenerateWall(int wallCount)
        {
            maxWallCount = wallCount;
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
            //return lifeWalls;
            return new List<LifeWall>(lifeWalls);
        }

        public static bool AllIsDead()
        {
            return lifeWalls.Count <= 0;
        }
        public override object Clone()
        {
            return new LifeWall(this);
        }

        public override void Update(GameTime gameTime)
        {
            nowCount = wallNames.Length - lifeWalls.Count;
            Name = wallNames[nowCount];
            Position = -GameDevice.Instance().DisplayModify + displayPos;
            foreach(var hp in healParticles)
            {
                if(hp.IsDead)
                {
                    lifeWalls.Add(new LifeWall(new Vector2(Block.BlockSize, (Screen.Height - size.Y) - space * lifeWalls.Count)
                        ));
                }
            }
            healParticles.RemoveAll(hp => hp.IsDead);
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
                Break();
            }
        }

        public void Break()
        {
            GameDevice.Instance().GetSound().PlaySE("lifewall_break");
            IsDead = true;
            new DestroyParticle(Name, Position, Size, DestroyParticle.DestroyOption.Center);
            GameDevice.Instance().DisplayQuake = new Vector2(0, 1.5f);
        }

        public static void HealWall()
        {
            if(lifeWalls.Count < maxWallCount)
            {
                healParticles.Add(new HealParticle(wallNames[lifeWalls.Count], lifeWalls.Last().Position, size));
            }
        }
    }
}
