using System;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    [Serializable]
    public class RCT // кастомный клас прямоугольник используется в каждом элементе карты
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
            this.start = new CRD(start.x, start.z);
            this.width = width;
            this.height = height;
            this.sq = width * height;
            
        }
        public RCT(CRD start, CRD end)
        {
            this.end = new CRD(end.x, end.z);
            this.start = new CRD(start.x, start.z);
            this.width = end.z - start.z + 1;
            this.height = end.x - start.x + 1;
            this.sq = width * height;
        }
        public void CalcSq() //вычисляет площадь прямоугольника
        {
            this.sq = this.width * this.height;
        }
        public bool isContainCRD(CRD crd)// проверяет находится ли координата внутри прямоугольника
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
        public bool isContainRCT(RCT rct)//// проверяет находится ли прямоугольник внутри прямоугольника
        {
            if(this.isContainCRD(rct.start) && this.isContainCRD(rct.end))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool checkCollision(RCT rct)// проверяет пересекается ли данный прямоугольник с указанным прямоугольником
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
        public static RCT getCollision(RCT rct1, RCT rct2) // получает два прямоугольника и возвращает новый равный их пересекающейся части
        {
            return new RCT(new CRD(Mathf.Max(rct1.Start.x, rct2.Start.x), Mathf.Max(rct1.Start.z, rct2.Start.z)), new CRD(Mathf.Min(rct1.End.x, rct2.End.x), Mathf.Min(rct1.End.z, rct2.End.z)));
        }
        public static List<RCT> Cuttind(RCT rct1, RCT rct2) // получает два прямоугольника и получает несколько новых являющихся частями порезанных старых
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
        public bool Equals(RCT rct) // проверяет равны ли прямоугольники
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

        public RCT Clone() // возвращает копию прямоугольника
        {
            return new RCT(new CRD(this.start.x, this.start.z), new CRD(this.end.x, this.end.z));
        }
        public CRD CloneStart() // возвращает копию стартовой точки прямоугольника
        {
            return new CRD(start.x, start.z); 
        }
        public CRD CloneEnd()  // возвращает копию конечной точки прямоугольника
        {
            return new CRD(end.x, end.z);
        }
        public string GetDirectionNoDiagonals(CRD crd)
        {
             string direction, vDirection, hDirection;
            int VD, HD;
            int DSX = crd.x - start.x;
            int DEX = end.x - crd.x;
            int DSZ = crd.z - start.z;
            int DEZ = end.z - crd.z;
            if(DSX < DEX)
            {
                VD = DSX;
                vDirection = "t";
            }
            else
            {
                VD = DEX;
                vDirection = "b";
            }
            if(DSZ < DEZ)
            {
                HD = DSZ;
                hDirection = "l";
            }
            else
            {
                HD = DEZ;
                hDirection = "r";
            }
            if(HD < VD)
                {
                 direction = hDirection;
            }
            else
            {
                 direction = vDirection;
            }
            return direction;
        }
        public string GetDirection(CRD crd)
        {
            string direction, vDirection, hDirection;
            int VD, HD;
            int DSX = crd.x - start.x;
            int DEX = end.x - crd.x;
            int DSZ = crd.z - start.z;
            int DEZ = end.z - crd.z;
            if(DSX < DEX)
            {
                VD = DSX;
                vDirection = "t";
            }
            else
            {
                VD = DEX;
                vDirection = "b";
            }
            if(DSZ < DEZ)
            {
                HD = DSZ;
                hDirection = "l";
            }
            else
            {
                HD = DEZ;
                hDirection = "r";
            }
            if(HD == VD)
            {
                direction = hDirection + vDirection;
            }
            else
            {
                if(HD < VD)
                {
                    direction = hDirection;
                }
                else
                {
                    direction = vDirection;
                }
            }
            return direction;
        }
        public static RCT Addition(RCT rct1,RCT rct2)
        {
            int sx1 = rct1.Start.x, sx2 = rct2.start.x, ex1 = rct1.end.x, ex2 = rct2.end.x, sz1 = rct1.start.z, sz2 = rct2.start.z, ez1 = rct1.end.z, ez2 = rct2.end.z;
            int MINsx = Mathf.Min(sx1, sx2), MINsz = Mathf.Min(sz1, sz2), MAXex = Mathf.Max(ex1, ex2), MAXez = Mathf.Max(ez1, ez2);
            return new RCT(new CRD(MINsx, MINsz), new CRD(MAXex, MAXez));
        }
        public void DebugLog(string text="") // выводит прямоугольник в консоль
        {
            Debug.Log(text+" RCT start: " + Start.x + "," + Start.z + " end: " + End.x + "," + End.z + " w:" + Width + " h:" + Height + " sq:" + sq);
        }
        public void Grow()
        {
            Start = Start.ReturnStepLT();
            End = End.ReturnStepRB();
        }
        public void Thin()
        {
            Start = Start.ReturnStepRB();
            End = End.ReturnStepLT();
        }

    }
}

