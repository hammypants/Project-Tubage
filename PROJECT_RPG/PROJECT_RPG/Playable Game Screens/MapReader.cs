﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PROJECT_RPG
{
    class MapReader
    {
        public static MapTile[,] readTileMap(String inputFile, PlayableMainGameScreen owner)
        {
            StreamReader reader = new StreamReader(inputFile);
            int width = Int32.Parse(reader.ReadLine());
            int height = Int32.Parse(reader.ReadLine());
            MapTile[,] mapArr = new MapTile[height,width];
            char[,] tileArr = new char[height,width];
            String temp = reader.ReadLine();
            String[] tempArr;
            int i = 0;
            int j = 0;
            while (temp != null)
            {
                tempArr = temp.Split(' ');
                for (j = 0; j < width; j++)
                {
                    char[] tempChar = tempArr[j].ToCharArray();
                    tileArr[i,j] = tempChar[0];
                }
                i++;
                temp = reader.ReadLine();
            }

            mapTiles(tileArr, mapArr, width, height, owner);

            return mapArr;
        }

        private static void mapTiles(char[,] strArr, MapTile[,] mapArr, int width, int height, PlayableMainGameScreen owner)
        {
            char tempChar;
            MapTile tempTile;
            int fileIndex;
            MapTileCollisionType collision;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    tempChar = strArr[i, j];
                    switch (tempChar)
                    {
                        case '1':
                            fileIndex = 1;
                            collision = MapTileCollisionType.NoCollision;
                            break;
                        case '2':
                            fileIndex = 2;
                            collision = MapTileCollisionType.NoCollision;
                            break;
                        case '3':
                            fileIndex = 3;
                            collision = MapTileCollisionType.HalfCollisionTop;
                            break;
                        case '4':
                            fileIndex = 4;
                            collision = MapTileCollisionType.HalfCollisionBot;
                            break;
                        case '5':
                            fileIndex = 5;
                            collision = MapTileCollisionType.HalfCollisionLeft;
                            break;
                        case '6':
                            fileIndex = 6;
                            collision = MapTileCollisionType.HalfCollisionRight;
                            break;
                        case '7':
                            fileIndex = 7;
                            collision = MapTileCollisionType.HalfCollisionCornerTopLeft;
                            break;
                        case '8':
                            fileIndex = 8;
                            collision = MapTileCollisionType.HalfCollisionCornerTopRight;
                            break;
                        case '9':
                            fileIndex = 10;
                            collision = MapTileCollisionType.HalfCollisionCornerBotRight;
                            break;
                        case 'A':
                            fileIndex = 9;
                            collision = MapTileCollisionType.HalfCollisionCornerBotLeft;
                            break;
                        case '0':
                        default:
                            fileIndex = 0;
                            collision = MapTileCollisionType.FullCollision;
                            break;
                    }
                    tempTile = new MapTile(fileIndex, new Microsoft.Xna.Framework.Vector2(j * 20, i * 20), collision, owner);
                    mapArr[i, j] = tempTile;
                }
            }
        }
    }
}
