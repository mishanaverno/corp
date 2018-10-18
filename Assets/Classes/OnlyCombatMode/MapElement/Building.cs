using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class Building : MapElement
    {
        public Building(RCT rct) : base(rct)
        {
            Debug.Log("B");
            rct.DebugLog();
        }
        public override void OnAddToChildElements()
        {
            surface = "Road";
            //AddLayer(new NodeLayer(getPrefabNuber(), "Premetives/Surface", "Sidewalk"));
            base.OnAddToChildElements();
        }
        public void Init()
        {
            CreateRoom(new RCT(rct.Start.Clone().ReturnStepRB(), rct.End.Clone().ReturnStepLT()));
        }
        public void CreateRoom(RCT rct)
        {
            Room room = new Room(rct);
            addNewElement(room);
        }

    }
}
