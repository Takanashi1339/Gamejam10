using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using BoundyShooter.Util;
using BoundyShooter.Manager;
using BoundyShooter.Device;
using BoundyShooter.Actor.Particles;
using BoundyShooter.Actor.Blocks;

namespace BoundyShooter.Actor.Entities
{
    abstract class Boss : Entity
    {
        private Timer attackTimer,deathTimer,endTimer,particleTimer,downTimer;
        private Flashing death;
        static Random rand = new Random();
        private Vector2 knockBack, vibrationVelocity,deathvelocity;
        //プレイヤーに何回当たったら死ぬか、召喚するエネミーの種類
        private int maxCount, enemynum;
        private bool isDeadSound;
        private float hitAlpha;
        protected float hitCount,deathVelY;
        protected List<KrakenTentacle> tentacles = new List<KrakenTentacle>();
        private Sound sound;
        private int hitFlashCount;
        private int maxFlashCount;

        public static bool IsDeadFlag
        {
            get;
            private set;
        }

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
            maxCount = deathCount;
            this.enemynum = enemynum;
            sound = GameDevice.Instance().GetSound();
            attackTimer = new Timer(summon, true);
            deathTimer = new Timer(0.02f, true);
            particleTimer = new Timer(0.5f, true);
            endTimer = new Timer(5.5f, false);
            downTimer = new Timer(2.5f, false);
            death = new Flashing(1f, 0f, 2.8f, false, true);
            knockBack = new Vector2(0, -8);
            vibrationVelocity = new Vector2(2,0);
            deathvelocity = new Vector2(0, 2);
            hitCount = 0;
            deathVelY = 2f;
            IsDeadFlag = false;
            isDeadSound = false;
            hitAlpha = 1f;
            hitFlashCount = 3;
            maxFlashCount = 3;
        }

        public override void Update(GameTime gameTime)
        {
            if (!IsInScreen()) return;
            attackTimer.Update(gameTime);
            if (attackTimer.IsTime)
            {
                Attack();
            }
            if(maxCount <= hitCount)
            {
                IsDeadFlag = true;
                deathTimer.Update(gameTime);
                endTimer.Update(gameTime);
                downTimer.Update(gameTime);
                particleTimer.Update(gameTime);
                if(deathTimer.IsTime)
                {
                    vibrationVelocity.X = -vibrationVelocity.X;
                }
                if(particleTimer.IsTime &&
                    !downTimer.IsTime)
                {
                    sound.PlaySE("boss_dead_short");
                    new DestroyParticle("death_boss", new Vector2(Position.X + Size.X / 2 - 32 , Position.Y + Size.Y / 2- 32), new Point(64,64), DestroyParticle.DestroyOption.Center);
                }
                if (downTimer.IsTime)
                {
                    if(!isDeadSound)
                    {
                        sound.PlaySE("boss_dead_long");
                    }
                    isDeadSound = true;
                    vibrationVelocity.Y = deathVelY;
                    death.Update(gameTime);
                    if (endTimer.IsTime)
                    {
                        IsDead = true;
                    }
                }
                Velocity = vibrationVelocity;
            }
            if (hitAlpha < 1.0f)
            {
                hitAlpha += 0.07f;
            }
            if (hitFlashCount < maxFlashCount && hitAlpha >= 1.0f)
            {
                hitAlpha = 0.2f;
                hitFlashCount++;
            }
            base.Update(gameTime);
            Velocity = Vector2.Zero;
        }

        public override void Hit(GameObject gameObject)
        {
            Direction dir = CheckDirection(gameObject);
            if (gameObject is Player player)
            {
                if (player.Speed > Player.MaxSpeed / 2 &&
                    !IsDeadFlag)
                {
                    hitFlashCount = 0;
                    Velocity = knockBack;
                    tentacles.ForEach(tentacle => tentacle.AnchorPosition += knockBack);
                    hitCount++;
                    GameDevice.Instance().DisplayQuake = new Vector2(0, 0.25f);
                    sound.PlaySE("enemy_hit");
                    HitStop.DoHitStop();
                }

            }
            if (gameObject is PlayerBullet)
            {
                if (!IsDeadFlag)
                {
                    Velocity = knockBack / 4;
                    tentacles.ForEach(tentacle => tentacle.AnchorPosition += knockBack / 4);
                    hitCount += 0.25f;
                }
            }
            if(gameObject is WhiteBlock)
            { }
        }

        public override void Draw()
        {
            var players = GameObjectManager.Instance.Find<Player>();
            if (players.Count == 0)
            {
                return;
            }
            var drawer = Drawer.Default;
            if(downTimer.IsTime)
            {
                drawer.Alpha = death.GetAlpha();
            }
            else
            {
                drawer.Alpha = hitAlpha;
            }
            drawer.DisplayModify = true;
            base.Draw(drawer);
        }

    }
}
