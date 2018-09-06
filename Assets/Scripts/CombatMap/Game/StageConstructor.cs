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
        public void UpgradeStreets()
        {
            for(int i = 0; i < stage.childElements.Count; i++)
            {
                if(stage.childElements[i] is Street)
                {
                    Street street = stage.childElements[i] as Street;
                    street.Upgrade();
                }
                else if(stage.childElements[i] is Crossroad)
                {
                    Crossroad crossroad = stage.childElements[i] as Crossroad;
                    crossroad.Upgrade();
                    for (int k = 0; k < crossroad.childElements.Count; k++)
                    {
                        crossroad.childElements[k].NodesToConsole();
                    }
                }
            }
        }

    }
   
}
