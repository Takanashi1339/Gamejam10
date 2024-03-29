﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoundyShooter.Def;
using BoundyShooter.Device;
using Microsoft.Xna.Framework;

namespace BoundyShooter.Actor.Entities
{
    class NormalBoss : Boss
    {
        float tentacleCount = 0;

        public NormalBoss(Vector2 position)
            : base("kraken_body", position, new Point(256, 256), 4f, 10, 0)
        {
        }

        public NormalBoss(NormalBoss other)
            : this(other.Position)
        { }

        public override object Clone()
        {
            return new NormalBoss(this);
        }

        public override Entity Spawn(Map map, Vector2 position)
        {
            position = new Vector2(Screen.Width / 2 - Size.X / 2, position.Y);
            foreach (var tentaclePos in TentaclePositions)
            {
                tentacles.Add((KrakenTentacle)new KrakenTentacle(position + tentaclePos).Spawn(map, position + tentaclePos));
            }
            return base.Spawn(map, position);
        }

        public override void Update(GameTime gameTime)
        {
            tentacles[0].Rotation = -(float)Math.Sin(tentacleCount / 60) * 60;
            tentacles[1].Rotation = -(float)Math.Sin(tentacleCount / 60) * 30;
            tentacles[2].Rotation = (float)Math.Sin(tentacleCount / 60) * 30;
            tentacles[3].Rotation = (float)Math.Sin(tentacleCount / 60) * 60;
            tentacleCount++;
            base.Update(gameTime);
        }

        protected override void Attack()
        {
            tentacles.ForEach(tentacle => tentacle.Speed = 1f);
            var index = GameDevice.Instance().GetRandom().Next(3);
            tentacles[index].Speed = 2f;
        }
    }
}
