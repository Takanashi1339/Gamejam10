using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoundyShooter.Device;
using BoundyShooter.Util;
using Microsoft.Xna.Framework;

namespace BoundyShooter.Actor.Entities
{
    class HealItem : Entity
    {
        private Animation animation;

        public HealItem(Vector2 position)
            : base("heal_item", position, new Point(32, 32))
        {
            animation = new Animation(Size, 4, 0.1f);
        }

        public HealItem(HealItem other)
            :this(other.Position)
        { }

        public override void Update(GameTime gameTime)
        {
            animation.Update(gameTime);
            base.Update(gameTime);
        }

        public override object Clone()
        {
            return new HealItem(this);
        }

        public override void Hit(GameObject gameObject)
        {
            if(gameObject is Player)
            {
                GameDevice.Instance().GetSound().PlaySE("hit_heal");
                IsDead = true;
            }
            base.Hit(gameObject);
        }
        public override void Draw()
        {
            var drawer = Drawer.Default;
            drawer.DisplayModify = true;
            drawer.Rectangle = animation.GetRectangle();
            base.Draw(drawer);
        }
    }
}
