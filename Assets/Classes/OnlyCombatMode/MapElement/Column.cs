using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class Column : MapElement
    {
        public Column(CRD crd, bool asInterior = false):base(new RCT(crd, crd))
        {
           AsInterior = asInterior;
        }

        public bool AsInterior { get; }

        public override void OnAddToChildElements()
        {
            AddLayer(new NodeLayer(getPrefabNuber(), "Premetives/Wall", "Column"));
            base.OnAddToChildElements();
        }
        public override List<NodeLayer> BeforeProcessLayers(List<NodeLayer> layers)
        {
            int oldControllQuad = layers.FindIndex(x => x.name == "ControllQuad");
            if (oldControllQuad >= 0) layers.RemoveAt(oldControllQuad);
            return base.BeforeProcessLayers(layers);
        }
        
    }
}
