using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class Area : MapElement // свободная зона
    {
        public Area(RCT rct, int floor) : base(rct, floor)
        {
            
        }
        public override void HookAddToChildElements()
        {
            surface = "Ground";
            AddLayer(new NodeLayer(getPrefabNuber(), "Main", "ControllQuad"));
        }
        public Building CreateBuilding(RCT rct)
        {
            Building building = new Building(rct, floorNumber);
            addNewElement(building);
            building.Init();
            return building;
        }

    }
   
}

