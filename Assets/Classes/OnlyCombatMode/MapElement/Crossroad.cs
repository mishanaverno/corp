using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class Crossroad : MapElement//перекресток
    {
        public int hsidewalk, vsidewalk;
        public Crossroad(RCT rct, int vsidewalk, int hsidewalk) : base(rct)
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
                            newMapElements.Add(new CrossroadSidewalk(rcts[i], rcts[i].CloneEnd()));
                            break;
                        case 1:
                            newMapElements.Add(new CrossroadRoad(rcts[i], 'v', rcts[i].Start.x));
                            break;
                        case 2:
                            newMapElements.Add(new CrossroadSidewalk(rcts[i], new CRD(rcts[i].End.x, rcts[i].Start.z)));
                            break;
                        case 3:
                            newMapElements.Add(new CrossroadRoad(rcts[i], 'h', rcts[i].Start.z));
                            break;
                        case 4:
                            newMapElements.Add(new Intersection(rcts[i]));
                            break;
                        case 5:
                            newMapElements.Add(new CrossroadRoad(rcts[i], 'h', rcts[i].End.z));
                            break;
                        case 6:
                            newMapElements.Add(new CrossroadSidewalk(rcts[i], new CRD(rcts[i].Start.x, rcts[i].End.z)));
                            break;
                        case 7:
                            newMapElements.Add(new CrossroadRoad(rcts[i], 'v', rcts[i].End.x));
                            break;
                        case 8:
                            newMapElements.Add(new CrossroadSidewalk(rcts[i], rcts[i].CloneStart()));
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
                Intersection intersection = new Intersection(this.rct);
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
