using System;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    [Serializable]
    public class RCT
    {
        private CRD start = new CRD(0,0);
        private CRD end = new CRD(0,0);
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
                this.end = new CRD(Start.x + height - 1, Start.z + width - 1);
                this.height = end.x - start.x + 1;
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
                this.width = end.z - start.z + 1;
                this.end = new CRD(Start.x + height - 1, Start.z + width - 1);
                this.sq = width * height;
            }
        }

        public CRD End
        {
            get
            {
                return end;
            }

            set
            {
                this.width = end.z - start.z + 1;
                this.height = end.x - start.x + 1;
                this.sq = width * height;
                end = value;
            }
        }

        public CRD Start
        {
            get
            {
                return start;
            }

            set
            {
                this.width = this.end.z - start.z + 1;
                this.height = end.x - start.x + 1;
                this.sq = width * height;
                start = value;
            }
        }

        public RCT(CRD start, int width, int height)
        {
            this.end = new CRD(start.x + height - 1, start.z + width - 1);
            this.start = start;
            this.width = width;
            this.height = height;
            this.sq = width * height;
            
        }
        public RCT(CRD start, CRD end)
        {
            this.end = end;
            this.start = start;
            this.width = end.z - start.z + 1;
            this.height = end.x - start.x + 1;
            this.sq = width * height;
        }
        public bool isContainCRD(CRD crd)
        {
            if ((crd.x < this.Start.x) || (crd.z < this.Start.z) || (crd.x > this.End.x) || (crd.z > this.End.z))
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
            if (rct.End.z < this.Start.z || rct.Start.z > this.End.z || rct.Start.x > this.End.x || rct.End.x < this.Start.x)
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
            return new RCT(new CRD(Mathf.Max(rct1.Start.x, rct2.Start.x), Mathf.Max(rct1.Start.z, rct2.Start.z)), new CRD(Mathf.Min(rct1.End.x, rct2.End.x), Mathf.Min(rct1.End.z, rct2.End.z)));
        }
        public static List<RCT> Cuttind(RCT rct1, RCT rct2)
        {
            rct1.End.StepRB();
            rct2.End.StepRB();
            List<int> xlines = new List<int>();
            List<int> zlines = new List<int>();
            List<RCT> newRCTs = new List<RCT>();

            xlines.Add(rct1.Start.x);
            xlines.Add(rct1.End.x);
            xlines.Add(rct2.Start.x);
            xlines.Add(rct2.End.x);

            zlines.Add(rct1.Start.z);
            zlines.Add(rct1.End.z);
            zlines.Add(rct2.Start.z);
            zlines.Add(rct2.End.z);

            xlines.Sort();
            zlines.Sort();

            rct1.End.StepLT();
            rct2.End.StepLT();
            CRD star, end;
            for (int x = 0; x < xlines.Count - 1; x++)
            {
                for(int z=0; z<zlines.Count-1; z++)
                {
                    star = new CRD(xlines[x], zlines[z]);
                    end = new CRD(xlines[x + 1], zlines[z + 1]);
                    end.StepLT();
                    RCT rct = new RCT(star, end);
                    if (rct1.checkCollision(rct) || rct2.checkCollision(rct))
                    {
                        newRCTs.Add(rct);
                    }
                }
            }
            return newRCTs;
        }
        public bool Equals(RCT rct)
        {
            if(this.Start==rct.Start && this.End == rct.End)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

