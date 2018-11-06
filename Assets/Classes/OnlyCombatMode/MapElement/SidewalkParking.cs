using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class SidewalkParking : MapElement//Парковоочный карман
    {
        public char axis;
        public int placeSize;
        public SidewalkParking(RCT rct, int floor, char axis, int placeSize) : base(rct,floor)
        {
            this.placeSize = placeSize;
            this.axis = axis;
        }
        public override List<NodeLayer> HookProcessLayers(List<NodeLayer> layers)
        {
            return new List<NodeLayer>();
        }
        public override void Upgrade()
        {
            int maxParkingSize = 2;
            int placeCount = 1;
            int width;
            if(axis == 'v')
            {
                width = rct.Height;
            }
            else
            {
                width = rct.Width;
            }
            int i = placeSize;
            while (i < width)
            {
                placeCount++;
                i += 1 + placeSize;
            }
            i = 1;
            int position;
            List<MapElement> mapElements = new List<MapElement>();
            if (axis == 'v') {
                mapElements.Add(new ParkingPlace(new RCT(rct.CloneStart(), maxParkingSize, placeSize),floorNumber, axis));
                position = rct.Start.x + placeSize;
            }
            else
            {
                mapElements.Add(new ParkingPlace(new RCT(rct.CloneStart(), placeSize, maxParkingSize),floorNumber, axis));
                position = rct.Start.z + placeSize;
            }
            while (i < placeCount)
            {
                i++;
                if(axis == 'v')
                {
                    mapElements.Add(new ParkingDelimetr(new RCT(new CRD(position, rct.Start.z), rct.Width, 1),floorNumber, axis));
                    position++;
                    mapElements.Add(new ParkingPlace(new RCT(new CRD(position, rct.Start.z), rct.Width, placeSize),floorNumber, axis));
                }
                else
                {
                    mapElements.Add(new ParkingDelimetr(new RCT(new CRD(rct.Start.x,position), 1, rct.Height),floorNumber, axis));
                    position++;
                    mapElements.Add(new ParkingPlace(new RCT(new CRD(rct.Start.x, position), placeSize, rct.Height),floorNumber, axis));
                    
                }
                position += placeSize;
            }
            addNewElements(mapElements);
        }

    }
}