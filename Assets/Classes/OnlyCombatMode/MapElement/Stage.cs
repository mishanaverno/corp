using System;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
   
    public class Stage : MapElement
    {
        [SerializeField]
        public int floorCount, width, height, groundFloor, basementFloor = 0;
        [SerializeField]
        public bool enabledBasement;
        public Floor[] floors;
        public string DesignName;
        public Stage(int floorCount, int width, int height, bool enabledBasement, string DesignName) : base(new RCT(new CRD(0, 0), width, height))
        {
            Debug.Log(parentElement);
            this.floorCount = floorCount;
            this.width = width;
            this.height = height;
            this.enabledBasement = enabledBasement;
            this.DesignName = DesignName;
            if (enabledBasement)
            {
                this.groundFloor = 1;
                this.floorCount++;
            }
            else
            {
                this.groundFloor = 0;
                this.basementFloor = -1;
            }
            this.floors = new Floor[this.floorCount];
            for (int i = 0; i < floorCount; i++)
            {
                Floor floor;
                if (i == this.basementFloor)
                {
                    floor = new UndergroundFloor(i, this);
                }                    
                else if(i == this.groundFloor)
                {
                    floor = new GroundFloor(i, this);
                }
                else
                {
                    floor = new AbovegroundFloor(i, this);
                }
                string json = JsonUtility.ToJson(floor);
                Debug.Log(json);
                this.floors[i] = floor;
                floor.Init();
            }
        }
        public void CreateStreet(RCT rct, char axis, int sidewalk)
        {
            List<MapElement> newElements = new List<MapElement>();
            for(int i = 0; i< this.childElements.Count; i++)
            {
                if (this.childElements[i].rct.checkCollision(rct))
                {
        
                    if (this.childElements[i] is Area)
                    {
                        RCT collision = RCT.getCollision(childElements[i].rct, rct);
                        List<RCT> newRcts = RCT.Cuttind(childElements[i].rct, collision);
                        for(int n = 0; n < newRcts.Count; n++)
                        {
                            if (newRcts[n].Equals(collision)){
                                newElements.Add(new Street(collision, axis, sidewalk));
                            }
                            else
                            {
                                newElements.Add(new Area(newRcts[n]));
                            }
                        }
                    }
                    else if(this.childElements[i] is Street)
                    {
                        Street oldstreet = this.childElements[i] as Street;
                        int hsidewalk, vsidewalk;
                        if (oldstreet.axis == 'v')
                        {
                            vsidewalk = oldstreet.sidewalk;
                            hsidewalk = sidewalk;
                        }
                        else
                        {
                            vsidewalk = sidewalk;
                            hsidewalk = oldstreet.sidewalk;
                        }
                        RCT collision = RCT.getCollision(oldstreet.rct, rct);
                        List<RCT> newRcts = RCT.Cuttind(oldstreet.rct, collision);
                        for(int n = 0; n < newRcts.Count; n++)
                        {
                            if (newRcts[n].Equals(collision))
                            {
                                newElements.Add(new Crossroad(collision, vsidewalk, hsidewalk));
                            }
                            else
                            {
                                newElements.Add(new Street(newRcts[n], oldstreet.axis, oldstreet.sidewalk));
                            }
                        }
                        
                    }
                   moveNodesFromMapElementToThis(childElements[i]);
                   childElements.Remove(childElements[i]);
                   i--;
                }
            }
            addNewElements(newElements);
               

        }




    }

}
