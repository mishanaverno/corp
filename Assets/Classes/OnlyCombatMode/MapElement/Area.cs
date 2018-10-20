using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class Area : MapElement // свободная зона
    {
        public Area(RCT rct) : base(rct)
        {
            
        }
        public override void OnAddToChildElements()
        {
            surface = "Ground";
        }
        public Building CreateBuilding(RCT rct)
        {
            Building building = new Building(rct);
            addNewElement(building);
            building.Init();
            return building;
        }

    }
   
}

