using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class Building : MapElement
    {
        public Building(RCT rct) : base(rct)
        {
        }
        public override void OnAddToChildElements()
        {
            base.OnAddToChildElements();
            surface = "Ground";
            AddLayer(new NodeLayer(getPrefabNuber(), "Premetives/Surface", "Building"));
        }
        public void Init()
        {
            CreateRoom(new RCT(rct.Start.Clone().ReturnStepRB(), rct.End.Clone().ReturnStepLT()));
        }
        protected Room CreateRoom(RCT rct)
        {
            Room room = new Room(rct);
            addNewElement(room);
            return room;
        }
        protected ExtansionRoom CreateExtensionRoom(RCT rct)
        {
            ExtansionRoom room = new ExtansionRoom(rct);
            addNewElement(room);
            return room;
        }
        public MapElement AppendRoom(RCT rct)
        {
            RCT growRCT = new RCT(rct.Start.Clone().ReturnStepLT(), rct.End.Clone().ReturnStepRB());
            if (parentElement.rct.isContainRCT(growRCT))
            {
                GrowFor(growRCT);
                ExtansionRoom room = CreateExtensionRoom(rct);
                return room;
            }
            return this;
        } 
        public void GrowFor(RCT rct)
        {
            this.rct.DebugLog("before grow");
            if (parentElement.rct.isContainRCT(rct))
            {
                Area parentArea = parentElement as Area;
                parentArea.RemoveElement(this);
                this.rct = RCT.Addition(this.rct, rct);
                parentArea.addNewElement(this);
            }
            this.rct.DebugLog("after grow");
        }

    }
}
