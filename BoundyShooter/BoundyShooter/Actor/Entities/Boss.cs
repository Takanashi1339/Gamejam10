﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using BoundyShooter.Util;
using BoundyShooter.Manager;
using BoundyShooter.Device;

namespace BoundyShooter.Actor.Entities
{
    abstract class Boss : Entity
    {
        private Timer attackTimer;
        static Random rand = new Random();
        private Vector2 knockBack = new Vector2(0, -8);
        //プレイヤーに当たった数、プレイヤーに何回当たったら死ぬか、召喚するエネミーの位置
        private int maxCount, enemynum;
        protected float hitCount;
        protected List<KrakenTentacle> tentacles = new List<KrakenTentacle>();

        public bool IsDeadFlag
        {
            get;
            private set;
        } = false;

        protected static readonly List<Vector2> TentaclePositions = new List<Vector2>()
        {
            new Vector2(16, 96),
            new Vector2(48, 176),
            new Vector2(144, 176),
            new Vector2(176, 96)
        };

        abstract protected void Attack();

        public Boss(string name,Vector2 position,Point size,float summon,int deathCount,int enemynum) 
            : base(name,position,size)
        {
            //エネミー召喚する間隔の設定
            attackTimer = new Timer(summon,true);
            hitCount = 0;
            maxCount = deathCount;
            this.enemynum = enemynum;
        }

        public override void Update(GameTime gameTime)
        {
            //Console.WriteLine(hitCount);
            if (!IsInScreen()) return;
            attackTimer.Update(gameTime);
            if (attackTimer.IsTime)
            {
                Attack();

                ////bosssize / blocksize
                //summonPos = rand.Next(Size.Y / enemySize);
                //if (enemynum == 0)
                //{
                //    GameObjectManager.Instance.Add(new TestEnemy(new Vector2(Position.X + enemySize * summonPos, Position.Y + Size.Y)));
                //}
                //else if (enemynum == 1)
                //{
                //    GameObjectManager.Instance.Add(new TestEnemy2(new Vector2(Position.X + enemySize * summonPos, Position.Y + Size.Y)));
                //}
                //else if (enemynum == 2)
                //{
                //    GameObjectManager.Instance.Add(new TestEnemy(new Vector2(Position.X + enemySize * summonPos, Position.Y + Size.Y)));
                //    GameObjectManager.Instance.Add(new TestEnemy2(new Vector2(Position.X + enemySize * summonPos, Position.Y + Size.Y)));
                //}
            }
            if(maxCount <= hitCount)
            {
                IsDead = true;
                IsDeadFlag = true;
            }
            base.Update(gameTime);
            Velocity = Vector2.Zero;
        }

        public override void Hit(GameObject gameObject)
        {
            Direction dir = CheckDirection(gameObject);
            if (gameObject is Player player)
            {
                if (player.Speed > Player.MaxSpeed / 2)
                {
                    if (dir == Direction.Top)
                    {
                        Velocity = knockBack;
                        tentacles.ForEach(tentacle => tentacle.AnchorPosition += knockBack);
                        hitCount++;
                        GameDevice.Instance().DisplayQuake = new Vector2(0, 0.25f);
                    }
                }

            }
            if (gameObject is PlayerBullet)
            {
                if (dir == Direction.Top)
                {
                    Velocity = knockBack / 10;
                    tentacles.ForEach(tentacle => tentacle.AnchorPosition += knockBack / 10);
                    hitCount += 0.1f;
                }
            }
            base.Hit(gameObject);
        }

        public override void Draw()
        {
            var players = GameObjectManager.Instance.Find<Player>();
            if (players.Count == 0)
            {
                return;
            }
            var drawer = Drawer.Default;
            drawer.DisplayModify = true;
            base.Draw(drawer);
        }
    }
}
