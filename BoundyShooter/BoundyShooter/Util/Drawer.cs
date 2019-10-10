using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoundyShooter.Util
{
    /// <summary>
    /// Draw時のプロパティ
    /// </summary>
    class Drawer
    {
        public static Drawer Default
        {
            get
            {
                return new Drawer();
            }
        }

        public Rectangle? Rectangle = null;
        public Color Color = Color.White;
        public float Alpha = 1.0f;
        public float Rotation = 0.0f;
        public Vector2 Origin = Vector2.Zero;
        public Vector2 Scale = Vector2.One;
        public SpriteEffects SpriteEffects = SpriteEffects.None;
        public float LayerDepth = 0.0f;
        public bool DisplayModify = false;
    }
}
