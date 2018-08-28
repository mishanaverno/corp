using UnityEngine;

namespace Map
{
    public class Floor : Object
    {
        public int number;
        public Node[,] map;
        protected Stage stage;
        protected bool isBasement;
        public Floor(int number, Stage stage)
        {
            this.number = number;
            this.stage = stage;
            isBasement = false;
            map = new Node[stage.height,stage.width];
            GenerateNodes();
        }
        protected void GenerateNodes()
        {
            for(int x = 0; x < stage.height; x++)
            {
                for(int z = 0; z < stage.width; z++)
                {
                    map[x, z] = new Node(x, z, number, isBasement);
                }
            }
        }
        protected void LinkNodes()
        {
            for(int x = 0; x < stage.height; x++)
            {
                for(int z =0; z< stage.width; x++)
                {
                    map[x, z] = new Node(x, z, number, isBasement);
                }
            }
        }

    }
}
