using UnityEngine;

namespace Map
{
    public class RCT : Object
    {
        CRD start, end;
        int width, height, floor, sq;
        public RCT(CRD start, int width, int height, int floor)
        {
            this.start = start;
            this.floor = floor;
            this.width = width;
            this.height = height;
            this.sq = width * height;
            this.end = new CRD(start.x + height - 1, start.z + width - 1, floor);
        }
    }
}

