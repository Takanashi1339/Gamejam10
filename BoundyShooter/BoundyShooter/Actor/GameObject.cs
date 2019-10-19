using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BoundyShooter.Device;
using BoundyShooter.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoundyShooter.Def;

namespace BoundyShooter.Actor
{
    enum Direction
    {
        Top,
        Bottom,
        Left,
        Right
    }

    abstract class GameObject : ICloneable
    {
        public Vector2 Position
        {
            get;
            set;
        }

        public string Name
        {
            get;
            protected set;
        }

        public Point Size
        {
            get;
        }

        public Vector2 Velocity
        {
            get;
            protected set;
        }

        public int Width
        {
            get
            {
                return Size.X;
            }
        }
        public int Height
        {
            get
            {
                return Size.Y;
            }
        }

        public bool IsDead
        {
            get;
            protected set;
        }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle(Position.ToPoint(), Size);
            }
        }

        /// <summary>
        /// ゲームオブジェクト
        /// </summary>
        /// <param name="name">アセット名</param>
        /// <param name="position">座標</param>
        /// <param name="size">サイズ</param>
        public GameObject(string name, Vector2 position, Point size)
        {
            Name = name;
            Position = position;
            Size = size;
            Initialize();
        }

        public abstract object Clone();

        public virtual void Initialize()
        {
            Velocity = Vector2.Zero;
        }

        public virtual void Update(GameTime gameTime)
        {
            Position += Velocity;
        }

        public bool IsCollision(GameObject other)
        {
            return Rectangle.Intersects(other.Rectangle);
        }

        public abstract void Hit(GameObject gameObject);

        /// <summary>
        /// 外部から呼び出すのはこちら
        /// </summary>
        public virtual void Draw()
        {
            Draw(Drawer.Default);
        }

        /// <summary>
        /// 継承先から描画する用
        /// 引数無しのDrawメソッドから呼び出す
        /// </summary>
        /// <param name="drawer">Drawer</param>
        protected virtual void Draw(Drawer drawer)
        {
            Renderer.Instance.DrawTexture(Name, Position, drawer);
        }
        public Direction CheckDirection(GameObject otherObj)
        {
            var thisRect = Rectangle;
            var otherRect = otherObj.Rectangle;
            var dic = new Dictionary<Direction, int>();
            dic.Add(Direction.Top, Math.Abs(thisRect.Bottom - otherRect.Top));
            dic.Add(Direction.Bottom, Math.Abs(thisRect.Top - otherRect.Bottom));
            dic.Add(Direction.Right, Math.Abs(thisRect.Left - otherRect.Right));
            dic.Add(Direction.Left, Math.Abs(thisRect.Right - otherRect.Left));
            var dir = dic.OrderBy(pair => pair.Value).First().Key;
            return dir;
        }

        public virtual void CorrectPosition(GameObject other)
        {
            Direction dir = CheckDirection(other);
            var position = Position;

            if (dir == Direction.Top)
            {
                position.Y = other.Rectangle.Top - Size.Y;
            }
            else if (dir == Direction.Right)
            {
                position.X = other.Rectangle.Right;
            }
            else if (dir == Direction.Left)
            {
                position.X = other.Rectangle.Left - Size.X;
            }
            else if (dir == Direction.Bottom)
            {
                position.Y = other.Rectangle.Bottom;
            }
            Position = position;
        }

        public bool IsInScreen()
        {
            var modify = GameDevice.Instance().DisplayModify;
            return Position.X + modify.X + Size.X >= 0
                && Position.X + modify.X <= Screen.Width
                && Position.Y + modify.Y + Size.Y >= 0
                && Position.Y + modify.Y <= Screen.Height;
        }
    }
}
