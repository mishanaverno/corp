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
        public void CreateStage(int floors,int height,int width, bool enableUnderground, string DesignName)
        {
            this.stage = new Stage(floors, height, width, enableUnderground, DesignName);
            string json = JsonUtility.ToJson(stage);
            Debug.Log(json);
        }
        public void AddStreet(int start, char axis, int width, int sidewalk)
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
                //Debug.Log("RCT " + rct.Start.x + "," + rct.Start.z + ":" + rct.End.x + "," + rct.End.z + " h: " + rct.Height + " w: " + rct.Width);
            }
            else
            {
                rct = new RCT(new CRD(start, 0), stage.width, width + sidewalk * 2);
                //Debug.Log("RCT " + rct.Start.x + "," + rct.Start.z + ":" + rct.End.x + "," + rct.End.z + " h: " + rct.Height + " w: " + rct.Width);
            }
            stage.CreateStreet(rct, axis, sidewalk);
        }
        public void Upgrade()
        {
            stage.UpgradeChildElements();
            
        }
        public void RenderStage()
        {
            for(int i = 0; i < stage.floors.Length; i++)
            {
                stage.floors[i].GenerateCells();
            }
        }

    }
   
}
