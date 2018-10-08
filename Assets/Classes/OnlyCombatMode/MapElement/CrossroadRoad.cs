using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class CrossroadRoad : Road//дорога на перекрестке
    {
        public CrossroadRoad(RCT rct, char axis) : base(rct, axis)
        {

        }
        public override void Upgrade()
        {
            if (rct.Height > 2 && rct.Width > 2)
            {
                RCT crosswalkRct;
                if (axis == 'v')
                {
                    crosswalkRct = new RCT(new CRD(rct.Start.x + 1, rct.Start.z), new CRD(rct.End.x - 1, rct.End.z));
                }
                else
                {
                    crosswalkRct = new RCT(new CRD(rct.Start.x, rct.Start.z + 1), new CRD(rct.End.x, rct.End.z - 1));
                }
                List<RCT> newRCTs = RCT.Cuttind(rct, crosswalkRct);
                List<MapElement> newElements = new List<MapElement>();
                for (int i = 0; i < newRCTs.Count; i++)
                {
                    //newRCTs[i].DebugLog(this.ToString() + " id:" + this.id);
                    if (newRCTs[i].Equals(crosswalkRct))
                    {
                        newElements.Add(new CrossroadCrosswalk(newRCTs[i], axis));
                    }
                    else
                    {
                        newElements.Add(new CrossroadRoad(newRCTs[i], axis));
                    }
                }
                parentElement.moveNodesFromMapElementToThis(this);
                parentElement.addNewElements(newElements);
            }
                base.Upgrade();
        }
    }

}
