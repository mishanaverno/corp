using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class RoadStripe : MapElement //полоса движения(напр. встречная)
    {
        public char axis;
        public RoadStripe(RCT rct, int floor, char axis) : base(rct,floor)
        {
            this.axis = axis;
            surface = "Road";
        }
        public override void Upgrade()
        {
            List<MapElement> mapElements = new List<MapElement>();
            if (axis == 'v')
            {
                int linecount = this.rct.Width / 2;
                for (int i = this.rct.Start.z; i <= this.rct.Start.z + linecount * 2; i += 2)
                {
                    RCT rct = new RCT(new CRD(this.rct.Start.x, i), 2, this.rct.Height);
                    mapElements.Add(new RoadLine(rct,floorNumber, axis));
                }

            }
            else
            {
                int linecount = this.rct.Height / 2;
                for(int i = this.rct.Start.x; i <= this.rct.Start.x + linecount * 2; i += 2)
                {
                    RCT rct = new RCT(new CRD(i, this.rct.Start.z), this.rct.Width, 2);
                    mapElements.Add(new RoadLine(rct,floorNumber, axis));
                }
            }
            this.addNewElements(mapElements);
        }
        public override void HookAddToChildElements()
        {
           
        }
    }
}
