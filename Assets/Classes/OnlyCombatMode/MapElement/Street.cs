using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class Street : MapElement//улица
    {
        public int start, width, sidewalk;
        public char axis;

        public Street(RCT rct, char axis, int sidewalk) : base(rct)
        {
            //Debug.Log("RCT " + rct.Start.x + "," + rct.Start.z + ":" + rct.End.x + "," + rct.End.z + " h: " + rct.Height + " w: " + rct.Width);
            this.sidewalk = sidewalk;
            this.axis = axis;
            if (axis == 'v')
            {
                
                this.width = rct.Width;
                this.start = rct.Start.z;
            }
            else
            {
                this.width = rct.Height;
                this.start = rct.Start.x;
            }
            //Debug.Log("NEW STREET RCT: h: " + rct.Height + " w: " + rct.Width);
            //Debug.Log("Street RCT " + this.rct.Start.x + "," + this.rct.Start.z + ":" + this.rct.End.x + "," + this.rct.End.z+" h: "+this.rct.Height+" w: "+this.rct.Width);
        }
        public Street cutStreet(Crossroad crossroad)//разрезание улицы перекрестком
        {
            Street newStreet;
            if(this.axis == 'v')
            {
                newStreet = new Street(new RCT(new CRD(crossroad.rct.End.x + 1, this.rct.Start.z), this.rct.End), this.axis, this.sidewalk);
                newStreet.moveNodesFromMapElementToThis(this);

       
                this.rct.Height = crossroad.rct.Start.x - this.rct.Start.x;
            }
            else
            {
                newStreet = new Street(new RCT(new CRD(this.rct.Start.x, crossroad.rct.End.z + 1), this.rct.End), this.axis, this.sidewalk);
                newStreet.moveNodesFromMapElementToThis(this);
                this.rct.Width = crossroad.rct.Start.z - this.rct.Start.z;
            }
            return newStreet;
        }
        public override void Upgrade()
        {
            
            RCT road;
            if (axis == 'v')
            {
                road = new RCT(new CRD(this.rct.Start.x, this.rct.Start.z + this.sidewalk), this.width-sidewalk*2, this.rct.Height);

            }
            else
            {
                road = new RCT(new CRD(this.rct.Start.x+this.sidewalk,this.rct.Start.z),this.rct.Width,this.width-sidewalk*2);
            }
            //Debug.Log("ROAD RCT " + road.Start.x + "," + road.Start.z + ":" + road.End.x + "," + road.End.z + " h: " + road.Height + " w: " + rct.Width);
            List<RCT> rcts = RCT.Cuttind(this.rct, road);
            
            for (int i = 0; i < rcts.Count; i++)
            {
                if (rcts[i].Equals(road))
                {
                    Road newRoad = new Road(rcts[i], this.axis);
                    newRoad.moveNodesFromMapElementToThis(this);
                    newRoad.parentElement = this;
                    this.childElements.Add(newRoad);
                   
                }
                else
                {
                    string direction;
                    if(axis == 'v')
                    {
                        if(rcts[i].Start.z < road.Start.z)
                        {
                            direction = "r";
                        }
                        else
                        {
                            direction = "l";
                        }
                    }
                    else
                    {
                        if (rcts[i].Start.x < road.Start.x)
                        {
                            direction = "b";
                        }
                        else
                        {
                            direction = "t";
                        }
                    }
                    Sidewalk newSidewalk = new Sidewalk(rcts[i], this.axis, direction);
                    newSidewalk.moveNodesFromMapElementToThis(this);
                    newSidewalk.parentElement = this;
                    this.childElements.Add(newSidewalk);
                }
            }
            /*this.NodesToConsole();      
            for(int i = 0; i < this.childElements.Count; i++)
            {
                childElements[i].NodesToConsole();
            }*/

        }
    }
}

