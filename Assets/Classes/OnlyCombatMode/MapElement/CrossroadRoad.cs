using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class CrossroadRoad : Road
    {
        private int croswalk;
        private int croswalkwidth = 1;
        public CrossroadRoad(RCT rct, char axis, int croswalk) : base(rct, axis)
        {
            this.croswalk = croswalk;
        }
        public override void Upgrade()
        {
            if (croswalk >= 0)
            {
                RCT crosswalkRct;
                if (axis == 'v')
                {
                    crosswalkRct = new RCT(new CRD(croswalk, rct.Start.z), rct.Width, croswalkwidth);
                }
                else
                {
                    crosswalkRct = new RCT(new CRD(rct.Start.x, croswalk), croswalkwidth, rct.Height);
                }
                List<RCT> newRCTs = RCT.Cuttind(rct, crosswalkRct);
                List<MapElement> newElements = new List<MapElement>();
                for(int i = 0; i < newRCTs.Count; i++)
                {
                    //newRCTs[i].DebugLog(this.ToString() + " id:" + this.id);
                    if (newRCTs[i].Equals(crosswalkRct))
                    {
                        newElements.Add(new CrossroadCrosswalk(newRCTs[i], axis));
                    }
                    else
                    {
                        newElements.Add(new CrossroadRoad(newRCTs[i], axis, -1));
                    }
                }
                parentElement.moveNodesFromMapElementToThis(this);
                parentElement.addNewElements(newElements);
            }
            else
            {
                base.Upgrade();
            }
        }
    }

}
