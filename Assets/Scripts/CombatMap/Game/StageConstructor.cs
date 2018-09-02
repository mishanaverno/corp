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
            }
            else
            {
                rct = new RCT(new CRD(start, 0), stage.width, width + sidewalk * 2);
            }
            stage.CreateStreet(rct, sidewalk);
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
            }
        }

    }
   
}
