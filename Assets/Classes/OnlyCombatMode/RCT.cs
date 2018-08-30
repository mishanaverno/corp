using System;
using UnityEngine;

namespace Map
{
    [Serializable]
    public class RCT
    {
        CRD start, end;
        int width, height, floor, sq;
        public RCT(CRD start, int width, int height)
        {
            this.start = start;
            this.width = width;
            this.height = height;
            this.sq = width * height;
            this.end = new CRD(start.x + height - 1, start.z + width - 1);
        }
        public bool isContainCRD(CRD crd)
        {
            if ((crd.x < this.start.x) || (crd.z < this.start.z) || (crd.x > this.end.x) || (crd.z > this.end.z))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

