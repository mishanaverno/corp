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
            Street street = new Street(rct, axis, sidewalk);
            List<MapElement> newElements = new List<MapElement>();
            for(int i = 0; i< this.childElements.Count; i++)
            {
                if (this.childElements[i].rct.checkCollision(street.rct))
                {
                    if(this.childElements[i] is Area)
                    {
                        street.moveNodesFromMapElementToThis(this.childElements[i]);
                        Area area = this.childElements[i] as Area;
                        //Debug.Log("STREET WIDTH:" + street.width);
                        Area newArea = area.cutArea(street.axis, street.width, street.start);
                        if (!newArea.isEmpty())
                        {
                            newElements.Add(newArea);

                        }
                    
                        
                    }
                    else if(this.childElements[i] is Street)
                    {
                        Street oldstreet = this.childElements[i] as Street;
                        int hsidewalk, vsidewalk;
                        if(oldstreet.axis == 'v')
                        {
                            vsidewalk = oldstreet.sidewalk;
                            hsidewalk = street.sidewalk;
                        }
                        else
                        {
                            vsidewalk = street.sidewalk;
                            hsidewalk = oldstreet.sidewalk;
                        }
                        Crossroad crossroad = new Crossroad(RCT.getCollision(street.rct, oldstreet.rct), vsidewalk, hsidewalk);
                        crossroad.moveNodesFromMapElementToThis(oldstreet);
                        Street newoldstreet = oldstreet.cutStreet(crossroad);
                        newoldstreet.moveNodesFromMapElementToThis(oldstreet);
                        Street newstreet = street.cutStreet(crossroad);
                        newElements.Add(crossroad);
                        if (!newoldstreet.isEmpty())
                        {
                            newElements.Add(newoldstreet);
                        }
                        if (!newstreet.isEmpty())
                        {
                            newElements.Add(street);
                            street = newstreet;
                        }
                        

                    }
                    
                }
            }
            newElements.Add(street);
            for (int i = 0; i < newElements.Count; i++)
            {
                this.childElements.Add(newElements[i]);
            }
            /*for (int i = 0; i < this.childElements.Count; i++)
            {
                this.childElements[i].NodesToConsole();
            }*/
            

        }

    }

}
