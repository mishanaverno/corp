using System;
using UnityEngine;

namespace Map
{
   
    public class Stage
    {
        [SerializeField]
        public int floorCount, width, height, groundFloor, basementFloor = 0;
        [SerializeField]
        public bool enabledBasement;
        public Floor[] floors;
        public Stage(int floorCount, int width, int height, bool enabledBasement)
        {
            this.floorCount = floorCount;
            this.width = width;
            this.height = height;
            this.enabledBasement = enabledBasement;
            if (enabledBasement)
            {
                this.groundFloor = 1;
                this.floorCount++;
            }
            else
            {
                this.groundFloor = 0;
                this.basementFloor = -1;
            }
            this.floors = new Floor[this.floorCount];
            for (int i = 0; i <= floorCount; i++)
            {
                Floor floor;
                if (i == this.basementFloor)
                {
                    floor = new UndergroundFloor(i, this);
                }                    
                else if(i == this.groundFloor)
                {
                    floor = new GroundFloor(i, this);
                }
                else
                {
                    floor = new AbovegroundFloor(i, this);
                }
                string json = JsonUtility.ToJson(floor);
                Debug.Log(json);
                this.floors[i] = floor;
                floor.Init();
            }
        }

    }

}
