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
        public float height = 3;
        public float extraheight = 0;
        [SerializeField]
        protected bool defaultIsWalkable;//доступен ли для передвижения по умолчанию
        public Floor(int number, Stage stage)
        {
            this.number = number;
            this.stage = stage;
            this.rct = stage.rct.Clone();
            map = new Node[stage.height, stage.width];
                
        }
        public virtual void Init()
        {
            GenerateNodes();
        }
        protected void GenerateNodes()// генерирует узлы
        {
            for(int x = 0; x < stage.height; x++)
            {
                for(int z = 0; z < stage.width; z++)
                {
                    Node node = new Node(x, z, this, defaultIsWalkable);
                    map[x, z] = node;
                    stage.childNodes.Add(node);
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
                        for(int z = -1; z <= 1; z++)
                        {

                            if ((z == 0 && x == 0) || !rct.isContainCRD(new CRD(fx + x, fz + z)))
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
            for(int x = 0; x < stage.height; x++)
            {
                for(int z = 0; z < stage.width; z++)
                {
                    Node node = GetNode(x, z);
                    if (!node.empty) node.GenerateCell();
                }
            }
        }
        public Node GetNode(int x, int z)//возвращает узел
        {
            if (x < 0 || z < 0 || x >= map.GetLength(0) || z >= map.GetLength(1))
            {
                return new Node();
            }
            else
            {
                return map[x, z];
            }
        }
    }
}
