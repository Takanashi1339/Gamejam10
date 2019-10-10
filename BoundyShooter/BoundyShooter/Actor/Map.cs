using Microsoft.Xna.Framework;
using BoundyShooter.Def;
using BoundyShooter.Device;
using BoundyShooter.Manager.Interface;
using BoundyShooter.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoundyShooter.Actor;
using BoundyShooter.Actor.Blocks;
using BoundyShooter.Actor.Entities;
using BoundyShooter.Actor.Particles;

namespace BoundyShooter.Actor
{
    class Map
    {
        public string Name
        {
            get;
            private set;
        }

        public int Width
        {
            get;
            private set;
        }
        public int Height
        {
            get;
            private set;
        }

        private List<List<Block>> mapList;
        private GameDevice gameDevice;
        private List<Block[]> replaceList;

        public Map(List<string[]> data)
        {
            mapList = new List<List<Block>>();
            gameDevice = GameDevice.Instance();
            replaceList = new List<Block[]>();

            for (int linCnt = 0; linCnt < data.Count(); linCnt++)
            {
                mapList.Add(addBlock(linCnt, data[linCnt]));
            }
        }


        private List<Block> addBlock(int lineCnt, string[] line)
        {
            var objectDict = MapDictionary.Data;

            var workList = new List<Block>();
            int colCnt = 0;
            foreach (var s in line)
            {
                try
                {
                    var work = objectDict[s].Clone() as GameObject;
                    var position =
                        new Vector2(
                            colCnt * Block.BlockSize,
                            lineCnt * Block.BlockSize);
                    if (work is Block block)
                    {
                        work = block.Set(this, position);
                    }
                    else if (work is Entity entity)
                    {
                        entity.Spawn(this, position - new Vector2(0, entity.Height - Block.BlockSize));
                        work = new Space().Set(this, position);
                    }

                    if ((lineCnt + 1) * work.Height > Height)
                    {
                        Height = (lineCnt + 1) * work.Height;
                    }
                    if ((colCnt + 1) * work.Width > Width)
                    {
                        Width = (colCnt + 1) * work.Width;
                    }

                    workList.Add(work as Block);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                colCnt++;
            }

            return workList;
        }

        private void replace()
        {
            foreach (var replace in replaceList)
            {
                var before = replace[0];
                var after = replace[1];
                for (int y = 0; y < mapList.Count; y++)
                {
                    var x = mapList[y].IndexOf(before);
                    if (x >= 0)
                    {
                        mapList[y][x] = after.Set(this, after.Position);
                    }
                }
            }
        }

        public void ReplaceBlock(Block before, Block after)
        {
            replaceList.Add(new Block[] { before, after });
        }

        public Block GetBlock(Vector2 position)
        {
            var x = (int)position.X / Block.BlockSize;
            var y = (int)position.Y / Block.BlockSize;
            return mapList[y][x];
        }

        public void Update(GameTime gameTime)
        {
            foreach (var list in mapList)
            {
                foreach (var obj in list)
                {
                    if (obj is Space)
                    {
                        continue;
                    }
                    obj.Update(gameTime);
                }
            }
            replace();
        }

        public void Draw()
        {
            foreach (var list in mapList)
            {
                foreach (var obj in list)
                {
                    obj.Draw();
                }
            }
        }

        public void Hit(GameObject gameObject)
        {
            var otherRect = gameObject.Rectangle;
            Point workMax = otherRect.Location + otherRect.Size;
            Point workMin = otherRect.Location;
            int minX = workMin.X / Block.BlockSize;
            int minY = workMin.Y / Block.BlockSize;
            int maxX = workMax.X / Block.BlockSize;
            int maxY = workMax.Y / Block.BlockSize;
            if (minX < 1)
            {
                minX = 1;
            }
            if (minY < 1)
            {
                minY = 1;
            }
            if (maxX < minX)
            {
                maxX = minX;
            }
            if (maxY < minY)
            {
                maxY = minY;
            }

            Range yRange = new Range(0, mapList.Count() - 1);
            Range xRange = new Range(0, mapList[0].Count() - 1);
            for (int row = minY - 1; row <= (maxY + 1); row++)
            {
                for (int col = minX - 1; col <= (maxX + 1); col++)
                {
                    if (xRange.IsOutOfRange(col) ||
                        yRange.IsOutOfRange(row))
                    {
                        continue;
                    }

                    GameObject obj = mapList[row][col];

                    if (obj is Space)
                    {
                        continue;
                    }

                    if (obj.IsCollision(gameObject))
                    {
                        gameObject.Hit(obj);
                        obj.Hit(gameObject);
                    }
                }
            }
        }
    }
}
