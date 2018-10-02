using System;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class Floor //этаж
    {
        [SerializeField]
        public int number;//номер
        public Node[,] map;//узлы
        public Stage stage;//сцена
        protected RCT rct;//прямоугольник
        [SerializeField]
        protected bool defaultIsWalkable;//доступен ли для передвижения по умолчанию
        public Floor(int number, Stage stage)
        {
            this.number = number;
            this.stage = stage;
            this.rct = new RCT(new CRD(0, 0), stage.height, stage.width);
            map = new Node[stage.height,stage.width];
                
        }
        public virtual void Init()
        {
            GenerateNodes();
        }
        protected void GenerateNodes()// генерирует узлы
        {
            Debug.Log("Generate floor " + number);
            for(int x = 0; x < stage.height; x++)
            {
                for(int z = 0; z < stage.width; z++)
                {
                    Node node = new Node(x, z, this, defaultIsWalkable);
                    map[x, z] = node;
                    stage.childNodes.Add(node);
                   // string json = JsonUtility.ToJson(node);
                    //Debug.Log(json);
                }
            }
        }
        public void LinkNodes()//связывает все узлы на этаже между собой
        {
            for (int fx = 0; fx < stage.height; fx++)
            {
                for(int fz = 0; fz < stage.width; fz++)
                {
                    Node currentNode = GetNode(fx, fz);
                    for (int x = -1; x <= 1; x++)
                    {
                        for(int z = -1; z <= +1; z++)
                        {

                            if ((z == 0 && x == 0) || !this.rct.isContainCRD(new CRD(fx + x, fz + z)))
                            {
                                continue;
                            }
                            float w;
                            if (z == 0 || x == 0)
                            {
                               w = 1;
                            }
                            else
                            {
                               w = 1.5f;
                            }
                            Node sibblingnode = GetNode(fx + x, fz + z);
                            currentNode.LinkNode(sibblingnode, w);
                        }
                    }
                }
            }
        }
        public void GenerateCells()//генерирует игровые объекты - клетки
        {
            Debug.Log("Generate cells " + number);
            for(int x = 0; x < stage.height; x++)
            {
                for(int z = 0; z < stage.width; z++)
                {
                    Node node = GetNode(x, z);
                    node.GenerateCell();
                }
            }
        }
        public Node GetNode(int x, int z)//возвращает узел
        {
            return map[x, z];
        }
    }
}
