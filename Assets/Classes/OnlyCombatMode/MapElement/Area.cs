using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class Area : MapElement
    {
        public Area(RCT rct) : base(rct)
        {

        }
        public Area cutArea(char axis, int width, int start)
        {
            Area newArea;
            if(axis == 'v')
            {
                newArea = new Area(new RCT(new CRD(this.rct.Start.x, start + width), this.rct.Width + this.rct.Start.z - (start  + width), this.rct.Height));
                newArea.moveNodesFromMapElementToThis(this);
                this.rct.Width = start - this.rct.Start.z; 
                
            }
            else
            {
                newArea = new Area(new RCT(new CRD(start + width, this.rct.Start.z), this.rct.Width, this.rct.Height + this.rct.Start.x - (start + width)));
                newArea.moveNodesFromMapElementToThis(this);
                this.rct.Height = start - this.rct.Start.x;
            }
            return newArea;
        }

    }
   
}

