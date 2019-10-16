using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using BoundyShooter.Util;
using BoundyShooter.Manager;

namespace BoundyShooter.Actor.Entities
{
    abstract class Boss : Entity
    {
        private Timer summonTimer;
        static Random rand = new Random();
        private Vector2 knockBack = new Vector2(0, -8);
        //プレイヤーに当たった数、プレイヤーに何回当たったら死ぬか、召喚するエネミーの位置
        protected int hitCount,maxCount,summonPos,enemySize;

        public Boss(string name,Vector2 position,Point size,float timer,int deathCount) 
            : base(name,position,size)
        {
            //エネミー召喚する間隔の設定
            summonTimer = new Timer(timer,true);
            hitCount = 0;
            maxCount = deathCount;
            enemySize = 64;
        }

        public override void Update(GameTime gameTime)
        {
            Console.WriteLine(hitCount);
            if (!IsInScreen()) return;
            summonTimer.Update(gameTime);
            if (summonTimer.IsTime)
            {
                //bosssize / blocksize
                summonPos = rand.Next(Size.Y / enemySize);
                GameObjectManager.Instance.Add(new TestEnemy(new Vector2(Position.X + enemySize * summonPos, Position.Y + Size.Y)));
            }
            if(maxCount <= hitCount)
            {
                IsDead = true;
            }
            base.Update(gameTime);
            Velocity = Vector2.Zero;
        }

        public override void Hit(GameObject gameObject)
        {
            Direction dir = CheckDirection(gameObject);
            if (gameObject is Player)
            {
                if (dir == Direction.Top)
                {
                    Velocity = knockBack;
                    hitCount++;
                }
            }
            base.Hit(gameObject);
        }
    }
}
