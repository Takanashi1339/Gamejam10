using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoundyShooter.Device
{
    class CSVReader
    {
        public const string Path = "./CSV/";
        private List<string[]> stringData;

        public CSVReader()
        {
            stringData = new List<string[]>();
        }

        public List<string[]> GetData()
        {
            return stringData;
        }

        public string[][] GetArrayData()
        {
            return stringData.ToArray();
        }

        public int[][] GetIntData()
        {
            var data = GetArrayData();
            int row = data.Count();
            int[][] intData = new int[row][];
            for (int i = 0; i < row; i++)
            {
                int col = data[i].Count();
                intData[i] = new int[col];
            }
            for (int y = 0; y < row; y++)
            {
                for (int x = 0; x < intData[y].Count(); x++)
                {
                    intData[y][x] = int.Parse(data[y][x]);
                }
            }
            return intData;
        }

        public string[,] GetStringMatrix()
        {
            var data = GetArrayData();
            int row = data.Count();
            int col = data[0].Count();

            string[,] result = new string[row, col];

            for (int y = 0; y < row; y++)
            {
                for (int x = 0; x < col; x++)
                {
                    result[y, x] = data[y][x];
                }
            }

            return result;
        }


        public int[,] GetIntMatrix()
        {
            var data = GetIntData();
            int row = data.Count();
            int col = data[0].Count();

            int[,] result = new int[row, col];

            for (int y = 0; y < row; y++)
            {
                for (int x = 0; x < col; x++)
                {
                    result[y, x] = data[y][x];
                }
            }

            return result;
        }

        public void Clear()
        {
            stringData.Clear();
        }

        public void Read(string filename, string path = Path)
        {
            try
            {
                using (var sr = new System.IO.StreamReader(@"Content/" + path + filename))
                {
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine();

                        var values = line.Split(',');

                        stringData.Add(values);
#if DEBUG
                        foreach (var v in values)
                        {
                            Console.Write("{0}", v);
                        }
                        Console.WriteLine();
#endif
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
