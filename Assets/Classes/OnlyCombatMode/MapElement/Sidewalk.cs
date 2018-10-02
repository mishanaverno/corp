using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class Sidewalk : MapElement//тротуар
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
        public override void Upgrade()
        {
            
            int maxParkingSize = 2;
            int placeSize = 4;
            CRD startParking = rct.CloneStart();
            List<MapElement> mapElements = new List<MapElement>();
            int count = 1;
            int parkingSize = count * placeSize;
            int start, ocorner, icorner;
            if (axis == 'v')
            {
                if (rct.Width < 2 || rct.Height < placeSize) return;
                startParking.x++;

                while (parkingSize + placeSize + 1 < rct.Height - 2 && count < maxParkingSize)
                {
                    count++;
                    parkingSize += placeSize + 1;
                }
               
                if(rct.Start != parentElement.rct.Start)
                {
                    start = startParking.z;
                    ocorner = startParking.z;
                    icorner = startParking.z + maxParkingSize - 1;
                }
                else
                {
                    start = rct.End.z - 1;
                    ocorner = rct.End.z;
                    icorner = rct.End.z - maxParkingSize + 1;
                }
                SidewalkParking parking = new SidewalkParking(new RCT(new CRD(startParking.x, start), 2, parkingSize), axis, placeSize);
                // С этим позже могут быть проблемы, т.к. угловая нода не попадает в объект parking
                mapElements.Add(new SidewalkParkingInnerCorner(new RCT(new CRD(parking.rct.Start.x, icorner), 1, 1), ""));
                mapElements.Add(new SidewalkParkingInnerCorner(new RCT(new CRD(parking.rct.End.x, icorner), 1, 1), ""));
                //
                mapElements.Add(parking);
                mapElements.Add(new SidewalkParkingOuterCorner(new RCT(new CRD(parking.rct.Start.x - 1, ocorner), 1, 1)));
                mapElements.Add(new SidewalkParkingOuterCorner(new RCT(new CRD(parking.rct.End.x + 1, ocorner), 1, 1)));
                
            }
            else
            {
                if (rct.Height < 2 || rct.Width < placeSize) return;
                startParking.z++;
                while(parkingSize + placeSize + 1 < rct.Width-2 && count < maxParkingSize)
                {
                    count++;
                    parkingSize += placeSize + 1;
                }
                if(rct.Start != parentElement.rct.Start)
                {
                    start = startParking.x;
                    ocorner = startParking.x;
                    icorner = startParking.x + maxParkingSize - 1;
                }
                else
                {
                    Debug.Log(startParking.x+":"+startParking.z);
                    start = rct.End.x - 1;
                    ocorner = rct.End.x;
                    icorner = rct.End.x - maxParkingSize + 1;
                }
                SidewalkParking parking = new SidewalkParking(new RCT(new CRD(start, startParking.z), parkingSize, 2), axis, placeSize);
                // С этим позже могут быть проблемы, т.к. угловая нода не попадает в объект parking
                mapElements.Add(new SidewalkParkingInnerCorner(new RCT(new CRD(icorner, parking.rct.Start.z ), 1, 1), ""));
                mapElements.Add(new SidewalkParkingInnerCorner(new RCT(new CRD(icorner, parking.rct.End.z), 1, 1), ""));
                //
                mapElements.Add(parking);
                mapElements.Add(new SidewalkParkingOuterCorner(new RCT(new CRD(ocorner, parking.rct.Start.z - 1), 1, 1)));
                mapElements.Add(new SidewalkParkingOuterCorner(new RCT(new CRD(ocorner, parking.rct.End.z + 1), 1, 1)));
            }
            addNewElements(mapElements);
        }
       
    }
}

