using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.ModelGeometry.Scene;
using System.Windows.Media.Media3D;
using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.UtilityResource;

namespace Prototype01
{
    public class MyElement
    {
        public int Nr { get; set; } //Entity-Label

        public Point3D Min { get; set; } // Point (X-min / Y-min / Z-min)

        public Point3D Max { get; set; } //Point (X-max / Y-max / Z-Max )

        public Plane AreaVector { get; set; }  //biggest Area and the normalvector for direction

        public TypeOfElement Type { get; set; }  //element type : wall or slab

        public MyDistance DistanceDirection { get; set; } //distance and direction

        public double Thickness { get; set; } //total thickness of the element, all layers

        public Point3D CoreMin { get; set; } // Point (X-min / Y-min / Z-min) from corelayer

        public Point3D CoreMax { get; set; } //Point (X-max / Y-max / Z-Max ) from corelayer

        public IfcGloballyUniqueId GUID { get; set; } //Entity-Label
        //public Guid GUID { get; set; } //Entity-Label

        public Boolean Merged { get; set; } = false; //True if element was geometrically merged with others


        public void GetElementType(string typeinIFC)
        {
            if (typeinIFC == ElementType.IfcWall.ToString() || typeinIFC == ElementType.IfcWallElementedCase.ToString() || typeinIFC == ElementType.IfcWallStandardCase.ToString())
            {
                this.Type = TypeOfElement.Wall;
            }
            else if (typeinIFC == ElementType.IfcSlab.ToString() || typeinIFC == ElementType.IfcSlabElementedCase.ToString() || typeinIFC == ElementType.IfcSlabStandardCase.ToString())
            {
                this.Type = TypeOfElement.Slab;
            }
            else if (typeinIFC == ElementType.IfcCurtainWall.ToString())
            {
                this.Type = TypeOfElement.Wall;
            }
            else
            {
                this.Type = TypeOfElement.NotDefined;
            }
     
        }

        public void SetCoreMinMax(double dinterior, double dexterior, IfcDirectionSenseEnum DirectionSense)  //from ElementType-CoreLayer Check
        {
            Vector3D Elementdirection = this.AreaVector.NormalVector;

            double Xmin = 0;
            double Ymin = 0;
            double Zmin = 0;

            double Xmax = 0;
            double Ymax = 0;
            double Zmax = 0;

            if (Elementdirection.X == 1 || Elementdirection.X == -1)
            {
                if (DirectionSense == IfcDirectionSenseEnum.POSITIVE)
                {
                    Xmin = Math.Round(this.Min.X + dinterior , 2);
                    Ymin = Math.Round(this.Min.Y , 2);
                    Zmin = Math.Round(this.Min.Z , 2);

                    Xmax = Math.Round(this.Max.X - dexterior, 2);
                    Ymax = Math.Round(this.Max.Y , 2);
                    Zmax = Math.Round(this.Max.Z , 2);

                }
                else if (DirectionSense == IfcDirectionSenseEnum.NEGATIVE)
                {
                    Xmin = Math.Round(this.Min.X + dexterior, 2);
                    Ymin = Math.Round(this.Min.Y, 2);
                    Zmin = Math.Round(this.Min.Z, 2);

                    Xmax = Math.Round(this.Max.X - dinterior, 2);
                    Ymax = Math.Round(this.Max.Y, 2);
                    Zmax = Math.Round(this.Max.Z, 2);
                }
            }
            else if (Elementdirection.Y == 1 || Elementdirection.Y == -1)
            {
                if (DirectionSense == IfcDirectionSenseEnum.POSITIVE)
                {
                    Xmin = Math.Round(this.Min.X , 2);
                    Ymin = Math.Round(this.Min.Y + dinterior, 2);
                    Zmin = Math.Round(this.Min.Z, 2);

                    Xmax = Math.Round(this.Max.X , 2);
                    Ymax = Math.Round(this.Max.Y - dexterior, 2);
                    Zmax = Math.Round(this.Max.Z, 2);

                }
                else if (DirectionSense == IfcDirectionSenseEnum.NEGATIVE)
                {
                    Xmin = Math.Round(this.Min.X , 2);
                    Ymin = Math.Round(this.Min.Y + dexterior, 2);
                    Zmin = Math.Round(this.Min.Z, 2);

                    Xmax = Math.Round(this.Max.X , 2);
                    Ymax = Math.Round(this.Max.Y - dinterior, 2);
                    Zmax = Math.Round(this.Max.Z, 2);
                }
            }
            else if (Elementdirection.Z == 1 || Elementdirection.Z == -1)
            {
                if (DirectionSense == IfcDirectionSenseEnum.POSITIVE)
                {
                    Xmin = Math.Round(this.Min.X, 2);
                    Ymin = Math.Round(this.Min.Y , 2);
                    Zmin = Math.Round(this.Min.Z + dinterior, 2);

                    Xmax = Math.Round(this.Max.X, 2);
                    Ymax = Math.Round(this.Max.Y , 2);
                    Zmax = Math.Round(this.Max.Z - dexterior, 2);

                }
                else if (DirectionSense == IfcDirectionSenseEnum.NEGATIVE)
                {
                    Xmin = Math.Round(this.Min.X, 2);
                    Ymin = Math.Round(this.Min.Y , 2);
                    Zmin = Math.Round(this.Min.Z + dexterior, 2);

                    Xmax = Math.Round(this.Max.X, 2);
                    Ymax = Math.Round(this.Max.Y , 2);
                    Zmax = Math.Round(this.Max.Z - dinterior, 2);
                }
            }

            Point3D tempPointMin = new Point3D(Xmin, Ymin, Zmin);
            this.CoreMin = tempPointMin;
            Point3D tempPointMax = new Point3D(Xmax, Ymax, Zmax);
            this.CoreMax = tempPointMax;

        }

        

        public void SetCoreMin(double xvalue, double yvalue, double zvalue)
        {
            xvalue = Math.Round(xvalue, 2);
            yvalue = Math.Round(yvalue, 2);
            zvalue = Math.Round(zvalue, 2);
            Point3D tempPoint = new Point3D(xvalue, yvalue, zvalue);
            this.CoreMin = tempPoint;
  
        }

        public void SetCoreMax(double xvalue, double yvalue, double zvalue)
        {
            xvalue = Math.Round(xvalue, 2);
            yvalue = Math.Round(yvalue, 2);
            zvalue = Math.Round(zvalue, 2);
            Point3D tempPoint = new Point3D(xvalue, yvalue, zvalue);
            this.CoreMax = tempPoint;

        }

    }

    
}
