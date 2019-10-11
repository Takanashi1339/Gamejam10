﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoundyShooter.Def;
using BoundyShooter.Device;
using BoundyShooter.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BoundyShooter.Actor.Entities
{
    class Player : Entity
    {
        public float Rotation
        {
            get;
            private set;
        } = 0.0f;

        public float Speed
        {
            get;
            private set;
        }

        public const float Deceleration = 0.05f; //減速度
        public const float RotaionSpeed = 7f; //減速度

        public Vector2 Front
        {
            set
            {
                if (!value.Equals(Vector2.Zero))
                {
                    value.Normalize();
                    var radian = (float) Math.Atan2(value.X, value.Y);
                    Rotation = MathHelper.ToDegrees(radian);
                }
                else
                {
                    Rotation = 0;
                }
            }
            get
            {
                var radian = MathHelper.ToRadians(Rotation);
                Vector2 vector = new Vector2(
                    (float)Math.Sin(radian),
                    -(float)Math.Cos(radian)
                    );
                return vector;
            }
        }

        public Player(Vector2 position) 
            : base("player", position, new Point(64, 64))
        {
            Rotation = GameDevice.Instance().GetRandom().Next(360);
        }

        public override object Clone()
        {
            return new Player(Position);
        }

        public override void Update(GameTime gameTime)
        {
            Velocity = Front * Speed;
            Speed -= Deceleration;
            if (Speed < 0)
            {
                Speed = 0;
            }

            if (Input.GetKeyState(Keys.Space))
            {
                Velocity = Vector2.Zero;
                Rotation += RotaionSpeed;
                Speed = 10f;
            }
            base.Update(gameTime);
        }

        public override void Draw()
        {
            var drawer = Drawer.Default;
            drawer.Rotation = MathHelper.ToRadians(Rotation);
            drawer.Origin = Size.ToVector2() / 2;
            base.Draw(drawer);
        }
    }
}