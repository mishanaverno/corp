using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class Column : MapElement
    {
        public Column(CRD crd, int floor, bool asInterior = false):base(new RCT(crd, crd), floor)
        {
           AsInterior = asInterior;
        }

        public bool AsInterior { get; }

        public override void HookAddToChildElements()
        {
            AddLayer(new NodeLayer(getPrefabNuber(), "Premetives/Wall", "Column"));
            base.HookAddToChildElements();
        }
        public override List<NodeLayer> HookProcessLayers(List<NodeLayer> layers)
        {
            int oldControllQuad = layers.FindIndex(x => x.name == "ControllQuad");
            if (oldControllQuad >= 0) layers.RemoveAt(oldControllQuad);
            return base.HookProcessLayers(layers);
        }
        
    }
}
