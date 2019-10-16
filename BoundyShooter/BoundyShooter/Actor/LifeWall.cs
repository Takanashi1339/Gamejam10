using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoundyShooter.Actor.Blocks;
using BoundyShooter.Actor.Entities;
using BoundyShooter.Actor.Particles;
using BoundyShooter.Device;
using BoundyShooter.Util;
using Microsoft.Xna.Framework;

namespace BoundyShooter.Actor
{
    class LifeWall : GameObject
    {
        private static List<LifeWall> lifeWalls = null;
        private Vector2 displayPos;

        private LifeWall(Vector2 position)
            : base("life_wall", position, new Point(448,16))
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
        }

        public static List<LifeWall> GenerateWall(int wallCount)
        {
            if(lifeWalls == null)
            {
                lifeWalls = new List<LifeWall>();
            }
            for (int i = 0; i < wallCount; i++)
            {
                lifeWalls.Add(new LifeWall(new Vector2(Block.BlockSize, 640 + 30 * i))
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
            Position = -GameDevice.Instance().DisplayModify + displayPos;
            if(lifeWalls.Count <= 0)
            {
                //ゲームオーバの処理をかく
            }
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
            if (gameObject is Enemy)
            {
                IsDead = true;
                new DestroyParticle(Name, Position, Size, DestroyParticle.DestroyOption.Center);
            }
        }

    }
}
