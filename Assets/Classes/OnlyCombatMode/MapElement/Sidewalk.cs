using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class Sidewalk : MapElement//тротуар
    {
        public char axis;
        public string direction;
        public Sidewalk(RCT rct, int floor, char axis, string direction) : base(rct, floor)
        {
            this.axis = axis;
            this.direction = direction;
        }
        public override void HookAddToChildElements()
        {
            surface = "Road";
            AddLayer(new NodeLayer(0, "Premetives/Surface", "Sidewalk"));
            if (parentElement.GetType() != typeof(BackgroundStreet))
            {
                AddLayer(new NodeLayer(0, "Main", "ControllQuad"));
            }
            NodeLayer lights = new NodeLayer(getPrefabNuber(), "Additions/Pilars", "NightLight");
            int offset = 2;
            int roadOffset = getRoadOffset(offset);
            lights.addMap(StageConstructor.createPeriodicalMap(rct, axis, roadOffset, 7));
            lights.hasMesh = false;
            lights.nonWalkable = true;
            AddLayer(lights);

        }
        private int getRoadOffset(int offset)
        {
            int roadOffset;
            if (rct.Start == parentElement.rct.Start)
            {
                if (axis == 'v')
                {
                    roadOffset = rct.Width - offset - 1;
                }
                else
                {
                    roadOffset = rct.Height - offset - 1;
                }
            }
            else
            {
                roadOffset = offset;
            }
            return roadOffset;
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
            Street street = parentElement as Street;
            if(street.width - street.sidewalk * 2 > 2 && street.sidewalk > 3)
            {
                createParkingPlaces();
            }
            string benchDirection;
            if (rct.Start == parentElement.rct.Start)
            {
                if (axis == 'v')
                {
                    benchDirection = "b";
                }
                else
                {
                    benchDirection = "l";
                }
            }
            else
            {
                if (axis == 'v')
                {
                    benchDirection = "t";
                }
                else
                {
                    benchDirection = "r";
                }
            }
            //addFurniture("SidewalkBench", rct.Start, benchDirection);
            AddPeriodicalFurniture("SidewalkBench", benchDirection, axis, getRoadOffset(1), 10);
            furnitureList.Add("StreetTerminal");
            furnitureList.Add("GarbageBasket");
            furnitureList.Add("BoxBasket");
            furnitureList.Add("StreetStand");

            AddRandomFurniture(direction, 6);
        }
        public void createParkingPlaces()
        {
            if (parentElement.GetType() == typeof(BackgroundStreet)) return;
            string outerStartDirection, outerEndDirection;
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
                    outerStartDirection = "lb";
                    outerEndDirection = "lt";
                }
                else
                {
                    start = rct.End.z - 1;
                    ocorner = rct.End.z;
                    icorner = rct.End.z - maxParkingSize + 1;
                    outerStartDirection = "rb";
                    outerEndDirection = "rt";
                }
                SidewalkParking parking = new SidewalkParking(new RCT(new CRD(startParking.x, start), 2, parkingSize), floorNumber, axis, placeSize);
                // С этим позже могут быть проблемы, т.к. угловая нода не попадает в объект parking
                CRD cornerCRD = new CRD(parking.rct.Start.x, icorner);
                mapElements.Add(new SidewalkParkingInnerCorner(new RCT(cornerCRD, 1, 1),floorNumber, parking.rct.GetDirection(cornerCRD)));
                cornerCRD = new CRD(parking.rct.End.x, icorner);
                mapElements.Add(new SidewalkParkingInnerCorner(new RCT(cornerCRD, 1, 1),floorNumber, parking.rct.GetDirection(cornerCRD)));
                //
                mapElements.Add(parking);
                mapElements.Add(new SidewalkParkingOuterCorner(new RCT(new CRD(parking.rct.Start.x - 1, ocorner), 1, 1),floorNumber, outerStartDirection));
                mapElements.Add(new SidewalkParkingOuterCorner(new RCT(new CRD(parking.rct.End.x + 1, ocorner), 1, 1),floorNumber, outerEndDirection));
                
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
                    outerStartDirection = "rt";
                    outerEndDirection = "lt";
                }
                else
                {
                    start = rct.End.x - 1;
                    ocorner = rct.End.x;
                    icorner = rct.End.x - maxParkingSize + 1;
                    outerStartDirection = "rb";
                    outerEndDirection = "lb";
                }
                SidewalkParking parking = new SidewalkParking(new RCT(new CRD(start, startParking.z), parkingSize, 2),floorNumber, axis, placeSize);
                // С этим позже могут быть проблемы, т.к. угловая нода не попадает в объект parking
                CRD cornerCRD = new CRD(icorner, parking.rct.Start.z);
                mapElements.Add(new SidewalkParkingInnerCorner(new RCT(cornerCRD, 1, 1), floorNumber, parking.rct.GetDirection(cornerCRD)));
                cornerCRD = new CRD(icorner, parking.rct.End.z);
                mapElements.Add(new SidewalkParkingInnerCorner(new RCT(cornerCRD, 1, 1), floorNumber, parking.rct.GetDirection(cornerCRD)));
                //
                mapElements.Add(parking);
                mapElements.Add(new SidewalkParkingOuterCorner(new RCT(new CRD(ocorner, parking.rct.Start.z - 1), 1, 1),floorNumber, outerStartDirection));
                mapElements.Add(new SidewalkParkingOuterCorner(new RCT(new CRD(ocorner, parking.rct.End.z + 1), 1, 1), floorNumber, outerEndDirection));
            }
            addNewElements(mapElements);
        }
       
    }
}

