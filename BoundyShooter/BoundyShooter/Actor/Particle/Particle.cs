using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using BoundyShooter.Manager;
using BoundyShooter.Util;

namespace BoundyShooter.Actor.Particle
{
    abstract class Particle : GameObject
    {
        protected Animation animation;

        /// <summary>
        /// パーティクル
        /// </summary>
        /// <param name="name">アセット名</param>
        /// <param name="position">座標</param>
        /// <param name="size">大きさ</param>
        /// <param name="frame">アニメーションのフレーム数</param>
        /// <param name="time">1枚当たりのアニメーション時間</param>
        /// <param name="animationType">アニメーション方向</param>
        public Particle(string name, Vector2 position, Point size, int frame, float time, Animation.AnimationType animationType = Animation.AnimationType.Horizontal)
            : base(name, position, size)
        {
            animation = new Animation(size, frame, time, false, animationType);
            ParticleManager.Instance.Add(this);
        }

        public override void Update(GameTime gameTime)
        {
            animation.Update(gameTime);
            if (animation.IsEnd)
            {
                IsDead = true;
            }
            base.Update(gameTime);
        }

        public override void Draw()
        {
            Drawer drawer = new Drawer();
            drawer.Rectangle = animation.GetRectangle();
            base.Draw(drawer);
        }
    }
}
