using System.Collections.Generic;
using UnityEngine;

namespace Map
{
     public class Road : MapElement//класс описывает объект дорога
     {
        public char axis;  
         public Road(RCT rct, char axis) : base(rct)
         {
            this.axis = axis;
            surface = "Road";
         }
        public override void Upgrade()
        {
            
            RCT roadRCT = this.rct.Clone();
            
            List <MapElement> newElements = new List<MapElement>();   
            if (axis == 'v') {
                int lines = roadRCT.Width / 2;
                int firststripeWidth = lines / 2;

                RCT rct = new RCT(roadRCT.Start, firststripeWidth * 2, this.rct.Height);
                RoadStripe firsrStripe = new RoadStripe(rct, axis);
                newElements.Add(firsrStripe);
                lines -= firststripeWidth;
                roadRCT.Start = new CRD(roadRCT.Start.x, roadRCT.Start.z += firststripeWidth * 2);

                rct = new RCT(new CRD(roadRCT.Start.x, roadRCT.End.z - (lines * 2 - 1)), this.rct.End);
                RoadStripe secontStripe = new RoadStripe(rct, axis);
                newElements.Add(secontStripe);
                roadRCT.End = new CRD(roadRCT.End.x, roadRCT.End.z -= lines * 2);

                if(roadRCT.sq > 0)
                {
                    RoadSafetyZone safetyZone = new RoadSafetyZone(roadRCT, axis);
                    newElements.Add(safetyZone);
                }

            }
            else
            {
                Debug.Log("ROAD NODE Count: " + this.childNodes.Count);
                Debug.Log("ROAD RCT" + this.rct.Start.x + "," + this.rct.Start.z + ":" + this.rct.End.x + ","
                   + this.rct.End.z + " h: " + this.rct.Height + " w: " + this.rct.Width);
                int lines = roadRCT.Height / 2;
                //Debug.Log("ROAD " + this.axis + " LINES "+lines);
                int firststripeWidth = lines / 2;
                //Debug.Log("FIRST STRIPE W:" + firststripeWidth);

                RCT rct = new RCT(roadRCT.Start, this.rct.Width, firststripeWidth * 2);
                RoadStripe firsrStripe = new RoadStripe(rct, axis);
                //Debug.Log("ROADSTRIPE FIRST RCT" + firsrStripe.rct.Start.x + "," + firsrStripe.rct.Start.z + ":" + firsrStripe.rct.End.x + ","
                  //  + firsrStripe.rct.End.z + " h: " + firsrStripe.rct.Height + " w: " + firsrStripe.rct.Width);
                newElements.Add(firsrStripe);
                lines -= firststripeWidth;
                roadRCT.Start = new CRD(roadRCT.Start.x += firststripeWidth * 2, roadRCT.Start.z );

                rct = new RCT(new CRD(roadRCT.End.x - (lines * 2 - 1), roadRCT.Start.z), this.rct.End);
                RoadStripe secontStripe = new RoadStripe(rct, axis);
                newElements.Add(secontStripe);
                roadRCT.End = new CRD(roadRCT.End.x -= lines * 2, roadRCT.End.z);

                if(roadRCT.sq > 0)
                {
                    RoadSafetyZone safetyZone = new RoadSafetyZone(roadRCT, axis);
                    newElements.Add(safetyZone);
                }

                //Debug.Log("ROADSTRIPE SECOND RCT " + rct.Start.x + "," + rct.Start.z + ":" + rct.End.x + "," + rct.End.z + " h: " + rct.Height + " w: " + rct.Width);
                //Debug.Log("SAFETY SECOND RCT " + roadRCT.Start.x + "," + roadRCT.Start.z + ":" + roadRCT.End.x + "," + roadRCT.End.z + " h: " + roadRCT.Height + " w: " + roadRCT.Width + "sq:"+ roadRCT.sq);
            }
            this.addNewElements(newElements);
            newElements.Clear();
            
        }

    }
}
