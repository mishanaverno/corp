using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public struct NodeLayer// слой узла
    {
        public int prefabNumber;
        public string premitive;
        public string name;
        public string direction;
        public bool hasMesh;
        public bool mapping;
        public bool[,] map;
        //public string obstacle;
        public NodeLayer(int number, string premitive, string name)
        {
            map = new bool[0,0];
            mapping = false;
            hasMesh = true;
            direction = "i";
            this.prefabNumber = number;
            this.premitive = premitive;
            this.name = name;
        }
        public void addMap(bool[,] map)
        {
            mapping = true;
            this.map = map;
        }
        public void InvertDirection()
        {
            switch (direction)
            {
                case "l":
                    direction = "r";
                    break;
                case "r":
                    direction = "l";
                    break;
                case "b":
                    direction = "t";
                    break;
                case "t":
                    direction = "b";
                    break;
                default:
                    break;
            }
        }
    }
}

