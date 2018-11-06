using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class Crossroad : MapElement//перекресток
    {
        public int hsidewalk, vsidewalk;
        public Crossroad(RCT rct, int floor, int vsidewalk, int hsidewalk) : base(rct, floor)
        {
            this.hsidewalk = hsidewalk;
            this.vsidewalk = vsidewalk;
        }
        public override void Upgrade()
        {
            if(vsidewalk>0 && hsidewalk > 0)
            {
                RCT Irct = new RCT(new CRD(this.rct.Start.x + hsidewalk, this.rct.Start.z + vsidewalk), new CRD(this.rct.End.x - hsidewalk, this.rct.End.z - vsidewalk));
                List<RCT> rcts = RCT.Cuttind(this.rct, Irct);
                List<MapElement> newMapElements = new List<MapElement>();
                for(int i = 0; i < rcts.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            newMapElements.Add(new CrossroadSidewalk(rcts[i], floorNumber, rcts[i].CloneEnd()));
                            break;
                        case 1:
                            newMapElements.Add(new CrossroadRoad(rcts[i],floorNumber, 'v'));
                            break;
                        case 2:
                            newMapElements.Add(new CrossroadSidewalk(rcts[i],floorNumber, new CRD(rcts[i].End.x, rcts[i].Start.z)));
                            break;
                        case 3:
                            newMapElements.Add(new CrossroadRoad(rcts[i],floorNumber, 'h'));
                            break;
                        case 4:
                            newMapElements.Add(new Intersection(rcts[i],floorNumber));
                            break;
                        case 5:
                            newMapElements.Add(new CrossroadRoad(rcts[i],floorNumber, 'h'));
                            break;
                        case 6:
                            newMapElements.Add(new CrossroadSidewalk(rcts[i],floorNumber, new CRD(rcts[i].Start.x, rcts[i].End.z)));
                            break;
                        case 7:
                            newMapElements.Add(new CrossroadRoad(rcts[i],floorNumber, 'v'));
                            break;
                        case 8:
                            newMapElements.Add(new CrossroadSidewalk(rcts[i],floorNumber, rcts[i].CloneStart()));
                            break;
                        default:
                            break;
                    }
                }
                addNewElements(newMapElements);
                newMapElements.Clear();
            }
            else
            {
                Intersection intersection = new Intersection(this.rct, floorNumber);
                childElements.Add(intersection);
                intersection.parentElement = this;
                intersection.moveNodesFromMapElementToThis(this);
            }
            /*for (int i = 0; i < this.childElements.Count; i++)
            {
                childElements[i].NodesToConsole();
            }*/


        }

    }

}
