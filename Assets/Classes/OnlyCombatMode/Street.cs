using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class Street : MapElement
    {
        public int start, width, sidewalk;
        public char axis;

        public Street(RCT rct, int sidewalk) : base(rct)
        {
            this.sidewalk = sidewalk;
            if (rct.Height > rct.Width)
            {
                this.axis = 'v';
                this.width = rct.Width;
                this.start = rct.start.z;
            }
            else
            {
                this.axis = 'h';
                this.width = rct.Height;
                this.start = rct.start.x;
            }          
        }
        public Street cutStreet(Crossroad crossroad)
        {
            Street newStreet;
            if(this.axis == 'v')
            {
                newStreet = new Street(new RCT(new CRD(crossroad.rct.end.x + 1, this.rct.start.z), this.rct.end), this.sidewalk);
                newStreet.moveNodesFromMapElementToThis(this);
                this.rct.Height = crossroad.rct.start.x - this.rct.start.x;
            }
            else
            {
                newStreet = new Street(new RCT(new CRD(this.rct.start.x, crossroad.rct.end.z + 1), this.rct.end), this.sidewalk);
                newStreet.moveNodesFromMapElementToThis(this);
                this.rct.Width = crossroad.rct.start.z - this.rct.start.z;
            }
            return newStreet;
        }
        public void Upgrade()
        {
            if (sidewalk > 0)
            {
                if(this.axis == 'v')
                {
                    Sidewalk sidewalk1 = new Sidewalk(new RCT(this.rct.start, this.sidewalk, this.rct.Height), this.axis);
                    sidewalk1.moveNodesFromMapElementToThis(this);
                    this.childElements.Add(sidewalk1);
                    Sidewalk sidewalk2 = new Sidewalk(new RCT(new CRD(this.rct.end.z - this.sidewalk + 1, this.rct.start.x ), this.sidewalk, this.rct.Height), this.axis);
                    sidewalk2.moveNodesFromMapElementToThis(this);
                    this.childElements.Add(sidewalk2);
                    Road road = new Road(new RCT(new CRD(this.rct.start.x, this.rct.start.z + this.sidewalk),this.width, this.rct.Width), this.axis);
                    road.moveNodesFromMapElementToThis(this);
                    this.childElements.Add(road);
                }
                else
                {
                    Sidewalk sidewalk1 = new Sidewalk(new RCT(this.rct.start, this.rct.Width, this.sidewalk), this.axis);
                    sidewalk1.moveNodesFromMapElementToThis(this);
                    this.childElements.Add(sidewalk1);
                    Sidewalk sidewalk2 = new Sidewalk(new RCT(new CRD(this.rct.end.x - this.sidewalk + 1, this.rct.start.z), this.rct.Width, this.sidewalk), this.axis);
                    sidewalk2.moveNodesFromMapElementToThis(this);
                    this.childElements.Add(sidewalk2);
                    Road road = new Road(new RCT(new CRD(this.rct.start.x + this.sidewalk, this.rct.start.z), this.rct.Width, this.width), this.axis);
                    road.moveNodesFromMapElementToThis(this);
                    this.childElements.Add(road);
                }
                
            }
            else
            {
                Road road = new Road(this.rct, this.axis);
                road.moveNodesFromMapElementToThis(this);
                this.childElements.Add(road);
            }
            for (int i = 0; i < this.childElements.Count; i++)
            {
                this.childElements[i].NodesToConsole();
            }
        }
    }
}

