using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class Sidewalk : MapElement
    {
        public char axis;
        public string direction;
        public Sidewalk(RCT rct, char axis, string direction) : base(rct)
        {
            this.axis = axis;
            surface = "Road";
            this.direction = direction;
            AddLayer(new NodeLayer(0,"Surface","Sidewalk"));
        }
        public override void setNodeDirections()
        {
            for(int i = 0; i < childNodes.Count; i++)
            {
                childNodes[i].direction = this.direction;
            }
        }
    }
}

