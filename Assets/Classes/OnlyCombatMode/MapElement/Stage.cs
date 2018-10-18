using System;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
   
    public class Stage : MapElement // родительский элемент карты - singleton
    {
        [SerializeField]
        public int floorCounter = 0, width, height, groundFloor, basementFloor = 0;
        [SerializeField]
        public bool enabledBasement;
        public List<Floor> floors;
        public string DesignName;
        private static Stage instance;
        public Stage(int width, int height, bool enabledBasement, string DesignName) : base(new RCT(new CRD(0, 0), width, height))
        {
            prefabNumber = 0;
            Debug.Log(parentElement);
            this.width = width;
            this.height = height;
            this.enabledBasement = enabledBasement;
            this.DesignName = DesignName;
            floors = new List<Floor>();
            
            if (enabledBasement)
            {
                AddFloor(new UndergroundFloor(floorCounter, this));
                AddFloor(new GroundFloor(floorCounter, this));
                groundFloor = 1;
            }
            else
            {
                AddFloor(new GroundFloor(floorCounter, this));
                groundFloor = 0;
            }
            
            instance = this;
        }
        public void AddFloor(Floor floor)//добавляет этаж
        {
            floor.Init();
            floors.Add(floor);
            floorCounter++;
        }
        public void CreateStreet(RCT rct, char axis, int sidewalk)//добавляет улицу
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
        public static Node GetNode(CRD crd, int floornumber)// возвращает узел
        {
            Stage stage = Stage.GetStage();
            return stage.floors[floornumber].GetNode(crd.x, crd.z);
        }
        public static Node GetNode(CRD crd)
        {
            Stage stage = Stage.GetStage();
            return stage.floors[stage.groundFloor].GetNode(crd.x, crd.z);
        }
        public static Node GetNode(int x, int z, int floornumber)
        {
            return Stage.GetNode(new CRD(x, z), floornumber);
        }
        public static Node GetNode(int x, int z)
        {
            return Stage.GetNode(new CRD(x, z));
        }
        public static Stage GetStage() //возвращает экземпляр сцены 
        {
            return instance;
        }
    }
}
