using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Buurlanden
{
    class Program
    {
        private static TextReader stdin = System.Console.In;
        private static TextWriter stdout = System.Console.Out;

        static void Main(string[] args)
        {
            int amount = int.Parse(stdin.ReadLine());

            List<char[,]> mapList = new List<char[,]>();

            for (int i = 0; i < amount; i++)
            {
                string[] values = stdin.ReadLine().Split(' ');

                int[] intValues = new int[] { int.Parse(values[0]), int.Parse(values[1]) };

                if (intValues[0] >= 1 && intValues[0] <= 20 && intValues[1] >= 1 && intValues[1] <= 20)
                {
                    int width = intValues[0];
                    int height = intValues[1];

                    mapList.Add(new char[width, height]);

                    for (int j = 0; j < height; j++)
                    {
                        char[] letters = stdin.ReadLine().ToCharArray();

                        for (int k = 0; k < letters.Length; k++)
                        {
                            mapList[i][k, j] = letters[k];
                        }
                    }
                }
            }

            List<int[,]> neighbourCountList = new List<int[,]>();

            for (int i = 0; i < mapList.Count; i++)
            {
                int[,] neighbourCount = new int[mapList[i].GetLength(0), mapList[i].GetLength(1)];

                Dictionary<char, HashSet<char>> neighbourCountOfCharacter = new Dictionary<char, HashSet<char>>();

                for (int posX = 0; posX < mapList[i].GetLength(0); posX++)
                {
                    for (int posY = 0; posY < mapList[i].GetLength(1); posY++)
                    {
                        char currentCharacter = mapList[i][posX, posY];

                        if (!neighbourCountOfCharacter.ContainsKey(currentCharacter)) neighbourCountOfCharacter.Add(currentCharacter, new HashSet<char>());

                        if (posX != 0)
                        {
                            AddNeighbour(mapList[i], posX - 1, posY, currentCharacter, neighbourCountOfCharacter[currentCharacter]);
                        }

                        if (posX != mapList[i].GetLength(0) - 1)
                        {
                            AddNeighbour(mapList[i], posX + 1, posY, currentCharacter, neighbourCountOfCharacter[currentCharacter]);
                        }

                        if (posY != 0)
                        {
                            AddNeighbour(mapList[i], posX, posY - 1, currentCharacter, neighbourCountOfCharacter[currentCharacter]);
                        }

                        if (posY != mapList[i].GetLength(1) - 1)
                        {
                            AddNeighbour(mapList[i], posX, posY + 1, currentCharacter, neighbourCountOfCharacter[currentCharacter]);
                        }
                    }
                }

                for (int posX = 0; posX < mapList[i].GetLength(0); posX++)
                {
                    for (int posY = 0; posY < mapList[i].GetLength(1); posY++)
                    {
                        neighbourCount[posX, posY] = neighbourCountOfCharacter[mapList[i][posX, posY]].Count;
                    }
                }

                neighbourCountList.Add(neighbourCount);
            }

            for (int listNumber = 0; listNumber < neighbourCountList.Count; listNumber++)
            {
                for (int i = 0; i < neighbourCountList[listNumber].GetLength(1); i++)
                {
                    stdout.Write(listNumber + 1);

                    for (int j = 0; j < neighbourCountList[listNumber].GetLength(0); j++)
                    {
                        stdout.Write(" " + neighbourCountList[listNumber][j, i]);
                    }

                    stdout.WriteLine();
                }
            }
        }

        static private void AddNeighbour(char[,] map, int posX, int posY, char character, HashSet<char> neighbourList)
        {
            if (character != map[posX, posY])
            {
                neighbourList.Add(map[posX, posY]);
            }
        }
    }
}
