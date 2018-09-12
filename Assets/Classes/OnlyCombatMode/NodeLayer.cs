using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public struct NodeLayer
    {
        public int prefabNumber;
        public string premitive;
        public string name;
        public NodeLayer(int number, string premitive, string name)
        {
            this.prefabNumber = number;
            this.premitive = premitive;
            this.name = name;
        }
    }
}

