using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Prototype01
{
    public class Plane
    {
        public double Area { get; set; }

        public Vector3D NormalVector { get; set; }


        public void GetPlane(MyElement element)
        {
            
            // Area is the biggest 
            double A1 = (element.Max.X - element.Min.X) * (element.Max.Z - element.Min.Z);
            double A2 = (element.Max.X - element.Min.X) * (element.Max.Y - element.Min.Y);
            double A3 = (element.Max.Z - element.Min.Z) * (element.Max.Y - element.Min.Y);

            //direction of Area
            if (A1 > A2 && A1 > A3)
            {
                Area = A1;
                Vector3D v1 = new Vector3D(element.Max.X - element.Min.X, 0, 0);
                Vector3D v2 = new Vector3D(0, 0, element.Max.Z - element.Min.Z);
                Vector3D v = Vector3D.CrossProduct(v1, v2);
                v = (1 / v.Length) * v;
                int vx = (int)Math.Round(v.X, 0);
                int vy = (int)Math.Round(v.Y, 0);
                int vz = (int)Math.Round(v.Z, 0);
                v.X = vx;
                v.Y = vy;
                v.Z = vz;
                NormalVector = v;
            }
            else if (A2 > A1 && A2 > A3)
            {
                Area = A2;
                Vector3D v1 = new Vector3D(element.Max.X - element.Min.X, 0, 0);
                Vector3D v2 = new Vector3D(0, element.Max.Y - element.Min.Y, 0);
                Vector3D v = Vector3D.CrossProduct(v1, v2);
                v = (1 / v.Length) * v;
                int vx = (int)Math.Round(v.X, 0);
                int vy = (int)Math.Round(v.Y, 0);
                int vz = (int)Math.Round(v.Z, 0);
                v.X = vx;
                v.Y = vy;
                v.Z = vz;
                NormalVector = v;
            }
            else if (A3 > A1 && A3 > A1)
            {
                Area = A3;
                Vector3D v1 = new Vector3D(0, 0, element.Max.Z - element.Min.Z);
                Vector3D v2 = new Vector3D(0, element.Max.Y - element.Min.Y, 0);
                Vector3D v = Vector3D.CrossProduct(v1, v2);
                v = (1 / v.Length) * v;
                int vx = (int)Math.Round(v.X,0);
                int vy = (int)Math.Round(v.Y,0);
                int vz = (int)Math.Round(v.Z,0);
                v.X = vx;
                v.Y = vy;
                v.Z = vz;
                NormalVector = v;
            }



        }


    }

    

}
