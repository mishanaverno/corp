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
        public NodeLayer(int number, string premitive, string name)
        {
            hasMesh = true;
            direction = "i";
            this.prefabNumber = number;
            this.premitive = premitive;
            this.name = name;
        }
    }
}

