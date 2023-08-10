using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Prototype01
{
    class CheckAngle
    {
        //public static double GetAngle(Vector3D normal1, Vector3D normal2)
        //{
        //    //Skalarprodukt
        //    double skalar = normal1.X * normal2.X + normal1.Y * normal2.Y + normal1.Z * normal2.Z;

        //    //Laenge 
        //    double laenge1 = Math.Sqrt(Math.Pow(normal1.X, 2) + Math.Pow(normal1.Y, 2) + Math.Pow(normal1.Z, 2));
        //    double laenge2 = Math.Sqrt(Math.Pow(normal2.X, 2) + Math.Pow(normal2.Y, 2) + Math.Pow(normal2.Z, 2));

        //    // alpha in radiant
        //    double Alpha = Math.Acos(skalar / (laenge1 * laenge2));
        //    // ... in degree
        //    return  Alpha * (180 / Math.PI);

            
        //}

        public static double? GetAngle(Vector3D normal1, Vector3D normal2)
        {
            // Mathematical corrected calculation 180 = parallel = 0
            //cos(x) = (u * v) /(betrag u * betrag v) -> x = cos-1(..)
            var erg = (normal1.X * normal2.X + normal1.Y * normal2.Y + normal1.Z * normal2.Z) / (normal1.Length * normal2.Length);
            erg = Math.Acos(erg) * (180 / Math.PI);
            if (erg == 180) erg = 0;
            if (erg == 0 || erg == 90)
            {
                return erg;
            }
            else
            { return null;  }
            
         }
        public static double? GetAngle(Vector3D normal1, DDirections direction2)
        {

            //Alter Ansatz -> Mathematische Berechnung sollte aber auch gehen.
            if (normal1 == new Vector3D(1, 0, 0) || normal1 == new Vector3D(-1, 0, 0))
            {
                if (direction2 == DDirections.Yminus || direction2 == DDirections.Yplus ||
                    direction2 == DDirections.Zminus || direction2 == DDirections.Zplus)
                {
                    return 90;
                }

                if (direction2 == DDirections.Xminus || direction2 == DDirections.Xplus)
                {
                    return 0;
                }
            }

            if (normal1 == new Vector3D(0, 1, 0) || normal1 == new Vector3D(0, -1, 0))
            {
                if (direction2 == DDirections.Xminus || direction2 == DDirections.Xplus ||
                    direction2 == DDirections.Zminus || direction2 == DDirections.Zplus)
                {
                    return 90;
                }

                if (direction2 == DDirections.Yminus || direction2 == DDirections.Yplus)
                {
                    return 0;
                }
            }

            if (normal1 == new Vector3D(0, 0, 1) || normal1 == new Vector3D(0, 0, -1))
            {
                if (direction2 == DDirections.Yminus || direction2 == DDirections.Yplus ||
                    direction2 == DDirections.Xminus || direction2 == DDirections.Xplus)
                {
                    return 90;
                }

                if (direction2 == DDirections.Zminus || direction2 == DDirections.Zplus)
                {
                    return 0;
                }
            }

            //TODO: Andere Vektoren wie X, Y, Z sind nicht beruecksichtigt
            return null;

        }


    }
}
