using System;
using UnityEngine;

namespace Map
{
    [Serializable]
    public class RCT
    {
        public CRD start, end;
        public int sq;
        private int width;
        private int height;

        public int Width
        {
            get
            {
                return width;
            }

            set
            {
                width = value;
                this.end = new CRD(start.x + height - 1, start.z + width - 1);
                this.sq = width * height;
            }
        }

        public int Height
        {
            get
            {
                return height;
            }

            set
            {
                height = value;
                this.end = new CRD(start.x + height - 1, start.z + width - 1);
                this.sq = width * height;
            }
        }

        public RCT(CRD start, int width, int height)
        {
            this.start = start;
            this.width = width;
            this.height = height;
            this.sq = width * height;
            this.end = new CRD(start.x + height - 1, start.z + width - 1);
        }
        public RCT(CRD start, CRD end)
        {
            this.start = start;
            this.end = end;
            this.width = end.z - start.z + 1;
            this.height = end.x - start.x + 1;
            this.sq = width * height;
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
        public bool checkCollision(RCT rct)
        {
            if (rct.end.z < this.start.z || rct.start.z > this.end.z || rct.start.x > this.end.x || rct.end.x < this.start.x)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static RCT getCollision(RCT rct1, RCT rct2)
        {
            return new RCT(new CRD(Mathf.Max(rct1.start.x, rct2.start.x), Mathf.Max(rct1.start.z, rct2.start.z)), new CRD(Mathf.Min(rct1.end.x, rct2.end.x), Mathf.Min(rct1.end.z, rct2.end.z)));
        } 
    }
}

