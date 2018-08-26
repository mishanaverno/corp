using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    //Клас конструирующий уровень(карту)
    public class StageConstructor
    {
        public static int[,,] Construct()//основной метод конструирующий карту
        {
            int levelCount = 2;
            Vector2 StageMaxSize = new Vector2(10,10);
            Vector2 StageSize = new Vector2((int)Random.Range(5, StageMaxSize.x), (int)Random.Range(5,StageMaxSize.y));
            bool basement = false;
            List<Road> roads = GenerateRoads(2, 2, 1, 3, StageSize);
            int[,,] outputMap = new int[levelCount, (int) StageSize.x, (int) StageSize.y];
            outputMap = PaintRoads(roads, outputMap);
            ShowToConsole(outputMap);
            return outputMap;
        }
        public static void ShowToConsole(int[,,] array)// метод для дебага карты в консоль 
        {
            for (int y = 1; y < array.GetLength(0); y++)
            {
                Debug.Log("floor:" + y);
                for (int x = 0; x < array.GetLength(1); x++)
                {
                    string line = "";
                    for (int z=0; z < array.GetLength(2); z++)
                    {
                        line += array[y, x, z] + "-";
                    }
                    Debug.Log("line " + x + ":" + line);
                }
            }
        }
        public static List<Road> GenerateRoads(int count, int MaxRoadWide, int maxRoadPadding, int parkingMaxL, Vector2 stageSize)//метод генерирующий дороги
        {
            List<Road> roads = new List<Road>();
            for(int i = 1; i <= count; i++){
                roads.Add(new Road(MaxRoadWide, maxRoadPadding, parkingMaxL, stageSize));
                Debug.Log("add road");
            }
            return roads; 
        }
        public static int[,,] PaintRoads(List<Road> roads, int[,,] map)//метод отрисовывающий дороги на карте
        {
            foreach (Road road in roads)
            {
                map = road.Paint(map);
            }
            return map;
        } 
    }
    public class Road//класс описывает объект дорога
    {
        public int wide;
        public int padding;
        public int axis; //0 - паралельно X, 1 - паралельно Z
        public int startPos;
        public int parkingMaxLength;
        public Road(int MaxRoadWide, int maxRoadPadding, int parkingMaxL, Vector2 stageSize)// метод конструктор, генерирует ширину, ось, тротуар
        {
            wide = (int)Random.Range(1, MaxRoadWide);
            padding = (int)Random.Range(0, maxRoadPadding);
            padding =  2;
            parkingMaxLength = parkingMaxL;
            axis = (int) Random.Range(0, 100);
            if(axis > 50)
            {
                axis = 1;
            }
            else
            {
                axis = 0;
            }

            if (axis == 0)
            {
                startPos = (int)Random.Range(0, stageSize.y-1);
               
            }
            else
            {
                startPos = (int)Random.Range(0, stageSize.x-1);
            }
            Debug.Log("Road start" + startPos + " -> " + (stageSize.y));
        }
        public int[,,] Paint(int[,,] map)// метод рисующий дорогу на карте
        {
            if (axis == 1)
            {
                return map = PaintZ(map);
            }
            else
            {
                return map = PaintX(map);
            }
            
        }
        protected int[,,] PaintX(int[,,] map)// метод рисующий дорогу на оси X
        {   
            for (int i = 0; i<map.GetLength(1); i++)
            {
                map[1, i, startPos] = 1;
                for (int p = (int)Mathf.Clamp(startPos - padding, 0, map.GetLength(2)-1); p <= (int)Mathf.Clamp(startPos + padding, 0, map.GetLength(2)-1); p++)
                {
                   
                    if (map[1, i, p] != 1)
                    {
                        map[1, i, p] = 2;
                    }
                }
                
            }
            return map;
        }
        protected int[,,] PaintZ(int[,,] map)// метод рисующий дорогу на карте по оси Z
        {
            for (int i = 0; i<map.GetLength(2); i++)
            {
                Debug.Log("padding: " + (startPos - padding) + " -> " + (startPos + padding) + " ( "+startPos+" - "+padding+" ) ");
                map[1, startPos, i] = 1;
                for (int p = (int)Mathf.Clamp(startPos - padding, 0, map.GetLength(1)-1); p <= (int)Mathf.Clamp(startPos + padding, 0, map.GetLength(1)-1); p++)
                {
                    if (map[1, p, i] != 1)
                    {
                        map[1, p, i] = 2;
                    }
                }
               
            }
            return map;
        }
    }
}
