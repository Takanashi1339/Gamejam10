using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoundyShooter.Actor.Particles;
using BoundyShooter.Def;
using BoundyShooter.Device;
using BoundyShooter.Util;
using Microsoft.Xna.Framework;

namespace BoundyShooter.Actor.Blocks
{
    abstract class Block : GameObject
    {
        public const int BlockSize = 32;
        public const int MinLifeTime = 15;
        public const int MaxLifeTime = 45;

        private float fallHeight = Screen.Height - BlockSize;
        private float fallVelocityY = 0.2f;
        private int maxRandom = 5 + 1;
        private int fallAccelerate = 1;
        private Timer fallTimer;
        private int randomLifeTime = -1;
        public Map Map
        {
            get;
            private set;
        }

        //当たり判定を持つかどうか
        public bool IsSolid
        {
            get;
            protected set;
        }

        public Block(string name, bool isSolid)
            : base(name, Vector2.Zero, new Point(BlockSize, BlockSize))
        {
            IsSolid = isSolid;
            fallTimer = new Timer(0.03f, true);
        }

        public virtual Block Set(Map map, Vector2 position)
        {
            Position = position;
            Map = map;
            return this;
        }


        protected override void Draw(Drawer drawer)
        {
            drawer.DisplayModify = true;
            base.Draw(drawer);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (IsInScreen() && LifeWall.AllIsDead())
            {
                fallTimer.Update(gameTime);
                GameOverEffect();
            }
        }
        public void GameOverEffect()
        {
            //初期値なら初期化
            if (randomLifeTime == -1)
            {
                randomLifeTime = GameDevice.Instance().GetRandom().Next(MaxLifeTime - MinLifeTime) + MinLifeTime;
            }
            var modify = GameDevice.Instance().DisplayModify;
            var screenPos = Position + modify + new Vector2(BlockSize, 0);
            var velocity = Velocity;
            if (fallTimer.IsTime)
            {
                fallHeight -= BlockSize;
            }
            if (screenPos.Y > fallHeight)
            {
                velocity.Y = fallVelocityY * fallAccelerate;
                fallAccelerate++;
            }
            if (Velocity.X == 0)
            {
                velocity.X = GameDevice.Instance().GetRandom().Next(maxRandom);
            }
            if (Position.X > Screen.Width - BlockSize || Position.X < 0)
            {
                velocity.X = -velocity.X;
            }
            Velocity = velocity;
            randomLifeTime--;
            if (randomLifeTime == 0)
            {
                new DestroyParticle(Name, Position, Size, DestroyParticle.DestroyOption.Center, 2);
            }
        }
    }
}
