using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Map
{
    public class Furniture : MapElement
    {
        public string furnitureDirection;
        public CRD origin;
        public string name;
        public MonoFurniture monoFurniture;
        public Furniture(string name, CRD start, string furnitureDirection, int prefabNumber) :base(new RCT(start, 0, 0))
        {
            this.name = name;
            this.prefabNumber = prefabNumber;
            Init();
            this.rct = createRCT(start, furnitureDirection, monoFurniture.Width, monoFurniture.Height);
        }
        private RCT createRCT(CRD start, string furnitureDirection, int width, int height) 
        {
            this.furnitureDirection = furnitureDirection;
            RCT rct;
            switch (furnitureDirection)
            {
                case "l":
                    rct = new RCT(start, width, height);
                    this.origin = rct.Start.Clone();
                    break;
                case "r":
                    rct = new RCT(start, width, height);
                    this.origin = rct.End.Clone();
                    break;
                case "t":
                    rct = new RCT(start, height, width);
                    origin = new CRD(rct.Start.x, rct.End.z);
                    break;
                case "b":
                    rct = new RCT(start, height, width);
                    this.origin = new CRD(rct.End.x, rct.Start.z); 
                    break;
                default:
                    rct = new RCT(start, width, height);
                    this.origin = rct.Start.Clone();
                    break;
            }
            return rct;
        }
        public void Init()
        {
            GameObject prefab = Resources.Load("Stage/" + Stage.GetStage().DesignName + "/Furniture/" + name + "/" + order + "/" + prefabNumber) as GameObject;
            MonoFurniture script = prefab.GetComponent<MonoFurniture>();
            monoFurniture = script;
        }
        public override List<NodeLayer> BeforeAddLayersToNode(List<NodeLayer> layers, Node node)
        {
            List<NodeLayer> nodeLayers = new List<NodeLayer>(layers);
            if (node.crd == origin)
            {
                NodeLayer furniture = new NodeLayer(getPrefabNuber(), "Furniture", name);
                furniture.direction = furnitureDirection;
                nodeLayers.Add(furniture);
            }
            return base.BeforeAddLayersToNode(nodeLayers, node);
        }

    }
}
