using UnityEngine;

namespace Map
{
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
