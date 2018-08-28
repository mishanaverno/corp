using UnityEngine;

namespace Map
{
    public class Stage : Object
    {
        public int floorCount, width, height, groundFloor;
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
            }
            this.floors = new Floor[this.floorCount];
            for (int i = 0; i < floorCount; i++)
            {
                this.floors[i] = new Floor(i, this);
            }
        }

    }

}
