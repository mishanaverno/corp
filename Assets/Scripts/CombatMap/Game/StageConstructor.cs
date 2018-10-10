using System.Collections.Generic;
using UnityEngine;

namespace Map
{
      //Клас конструирующий уровень(карту)
    public class StageConstructor
    {
        public Stage stage;
        public StageConstructor()
        {

        }
        public void CreateStage(int width, int height, bool enableUnderground, string DesignName)
        {
            this.stage = new Stage(width, height, enableUnderground, DesignName);
            string json = JsonUtility.ToJson(stage);
            Debug.Log(json);
        }
        public void AddStreet(int start, char axis, int width, int sidewalk) //добавляет улицу
        {
            RCT rct;
            if (width < 2)
            {
                width = 2;
            }
            if (width == 3)
            {
                width++;
            }
            if (axis == 'v')
            {
                rct = new RCT(new CRD(0, start), width + sidewalk * 2, stage.height);
            }
            else
            {
                rct = new RCT(new CRD(start, 0), stage.width, width + sidewalk * 2);

            }
            stage.CreateStreet(rct, axis, sidewalk);
        }
        public void Upgrade()
        {
            stage.UpgradeChildElements();
        }
        public void RenderStage() // рендерит игровые объекты
        {
            stage.ProcessLayersChildElements(new List<NodeLayer>());
            for(int i = 0; i < stage.floors.Count; i++)
            {
                stage.floors[i].GenerateCells();
            }
            
        }
        public void RenderBackground()//Добавление фона карты
        {
            Node[,] map = stage.floors[stage.groundFloor].map;
            // left side
            int sbg = 0, ebg = 0;
            for (int x = 0; x < map.GetLength(0); x++)
            {
                Node node = Stage.GetNode(x, stage.rct.Start.z);
                MapElement mapElement = node.mapElement.GetParentByClass(typeof(Street));
                
                if (mapElement.GetType() == typeof(Street))
                {
                    Street street = mapElement as Street;
                    if (street.axis == 'h')
                    {
                        createQuads(new CRD(sbg, stage.rct.Start.z - 1), new Vector3(0, 0, 0), ebg - sbg + 1);
                        int sx = x;
                        int ex = x + mapElement.rct.Height - 1;
                        x = ex + 1;
                        sbg = x;
                        BackgroundStreet bgStreet = new BackgroundStreet(new RCT(new CRD(sx, stage.rct.Start.z - 25), new CRD(ex, stage.rct.Start.z - 1)), street.axis, street.sidewalk);
                    }
                }
                else{
                    ebg = x;
                }
            }
            createQuads(new CRD(sbg, stage.rct.Start.z - 1), new Vector3(0, 0, 0), ebg - sbg + 1);
            //right side
            sbg = 0; ebg = 0;
            for (int x = 0; x < map.GetLength(0); x++)
            {
                Node node = Stage.GetNode(x, stage.rct.End.z);
                MapElement mapElement = node.mapElement.GetParentByClass(typeof(Street));
                if (mapElement.GetType() == typeof(Street))
                {
                    Street street = mapElement as Street;
                    if (street.axis == 'h')
                    {
                        createQuads(new CRD(sbg, stage.rct.End.z + 1), new Vector3(0, 180, 0), ebg - sbg + 1);
                        int sx = x;
                        int ex = x + mapElement.rct.Height - 1;
                        x = ex + 1;
                        sbg = x;
                        BackgroundStreet bgStreet = new BackgroundStreet(new RCT(new CRD(sx, stage.rct.End.z + 1), new CRD(ex, stage.rct.End.z + 25)), street.axis, street.sidewalk);
                    }
                }
                else
                {
                    ebg = x;
                }
            }
            createQuads(new CRD(sbg, stage.rct.End.z + 1), new Vector3(0, 180, 0), ebg - sbg + 1);
            //top side
            sbg = 0; ebg = 0;
            for (int z = 0; z < map.GetLength(1); z++)
            {
                Node node = Stage.GetNode(stage.rct.Start.x, z);
                MapElement mapElement = node.mapElement.GetParentByClass(typeof(Street));
                if (mapElement.GetType() == typeof(Street))
                {
                    Street street = mapElement as Street;
                    if (street.axis == 'v')
                    {
                        createQuads(new CRD(stage.rct.Start.x - 1, sbg), new Vector3(0, 90, 0), ebg - sbg + 1);
                        int sz = z;
                        int ez = z + mapElement.rct.Width - 1;
                        z = ez + 1;
                        sbg = z;
                        BackgroundStreet bgStreet = new BackgroundStreet(new RCT(new CRD(stage.rct.Start.x - 25, sz), new CRD(stage.rct.Start.x - 1, ez)), street.axis, street.sidewalk);
                    }
                }
                else
                {
                    ebg = z;
                }

            }
            createQuads(new CRD(stage.rct.Start.x - 1, sbg), new Vector3(0, 90, 0), ebg - sbg + 1);
            //bottom side
            sbg = 0; ebg = 0;
            for (int z = 0; z < map.GetLength(1); z++)
            {
                Node node = Stage.GetNode(stage.rct.End.x, z);
                MapElement mapElement = node.mapElement.GetParentByClass(typeof(Street));
                if (mapElement.GetType() == typeof(Street))
                {
                    Street street = mapElement as Street;
                    if (street.axis == 'v')
                    {
                        createQuads(new CRD(stage.rct.End.x + 1, sbg), new Vector3(0, 270, 0), ebg - sbg + 1);
                        int sz = z;
                        int ez = z + mapElement.rct.Width - 1;
                        z = ez + 1;
                        sbg = z;
                        BackgroundStreet bgStreet = new BackgroundStreet(new RCT(new CRD(stage.rct.End.x + 1, sz), new CRD(stage.rct.End.x + 25, ez)), street.axis, street.sidewalk);
                    }
                }
                else
                {
                    ebg = z;
                }
            }
            createQuads(new CRD(stage.rct.End.x + 1, sbg), new Vector3(0, 270, 0), ebg - sbg + 1);
            createQuad(new CRD(-1, -1), new Vector3(0, 45, 0), -1);
            createQuad(new CRD(-1, stage.width), new Vector3(0, 135, 0), -1);
            createQuad(new CRD(stage.height, -1), new Vector3(0, -45, 0), -1);
            createQuad(new CRD(stage.height, stage.width), new Vector3(0, -135, 0), -1);
        }
        private void createQuads(CRD crd, Vector3 rotation, int width)
        {
            Debug.Log("bgsss crd: " + crd.x + ":" + crd.z + " w:" + width);
            CRD ncrd = new CRD(crd.x, crd.z);
            int quadWidt = 1;
            while (width > 0)
            {
                if (width >= 20)
                {
                    quadWidt = 20;
                }else if (width >= 10)
                {
                    quadWidt = 10;
                }else if (width >= 5)
                {
                    quadWidt = 5;
                }else if (width >= 3)
                {
                    quadWidt = 3;
                }else if (width >= 1)
                {
                    quadWidt = 1;
                }
                
                if (ncrd.z < 0)
                {
                    createQuad(new CRD(ncrd.x, ncrd.z), rotation, quadWidt);
                    ncrd.x += quadWidt;
                }
                else if(ncrd.z >= stage.width)
                {
                    createQuad(new CRD(ncrd.x+quadWidt-1, ncrd.z), rotation, quadWidt);
                    ncrd.x += quadWidt;
                }
                else if(ncrd.x < 0)
                {
                    createQuad(new CRD(ncrd.x, ncrd.z+quadWidt-1), rotation, quadWidt);
                    ncrd.z += quadWidt;
                }else if(ncrd.x >= stage.height){
                    createQuad(new CRD(ncrd.x, ncrd.z), rotation, quadWidt);
                    ncrd.z += quadWidt;
                }
                width -= quadWidt;
            }
        }
        private void createQuad(CRD crd, Vector3 rotation, int width)
        {
            Debug.Log("bg crd: " + crd.x + ":" + crd.z + " w:" + width);
            Vector3 position = new Vector3(crd.x, stage.groundFloor, crd.z);
            int prefabNumber = 0;
            string size;
            if (width == -1)
                size = "Corner";
            else
                size = width.ToString();
            GameObject cellInstance = GameObject.Instantiate (Resources.Load("Stage/" + stage.DesignName + "/Background/" + stage.order + "/"+ size +"/" + prefabNumber), position, Quaternion.Euler(rotation)) as GameObject;
            cellInstance.transform.name = "bg-[" + position.x + "," + position.z + "]";
        }

    }
   
}
