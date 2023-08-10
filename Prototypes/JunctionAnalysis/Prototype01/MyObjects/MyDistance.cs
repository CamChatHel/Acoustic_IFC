using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Prototype01
{
    public class MyDistance
    {
        public double? Distance { get; set; } //calculated distance

        public DDirections Direction { get; set; } //direction of the distance X, Y, Z (siehe OwnEnumeration)

        public Point3D pointMaxE1 { get; set; }
        public Point3D pointMinE1 { get; set; }
        public Point3D pointMaxE2 { get; set; }
        public Point3D pointMinE2 { get; set; }


        //Aufteilung in normal und CoreLayer 
        public void CalculateDistance(MyElement SelectedElement, MyElement element2, bool coreYes)
        {
            
            if (coreYes)
            {
                pointMaxE1 = SelectedElement.CoreMax;
                pointMinE1 = SelectedElement.CoreMin;
                pointMaxE2 = element2.CoreMax;
                pointMinE2 = element2.CoreMin;
            }
            else
            {
                pointMaxE1 = SelectedElement.Max;
                pointMinE1 = SelectedElement.Min;
                pointMaxE2 = element2.Max;
                pointMinE2 = element2.Min;
            }
            

            GetDistance(SelectedElement, element2, pointMaxE1, pointMinE1, pointMaxE2, pointMinE2);

        }


        private void GetDistance(MyElement SelectedElement, MyElement element2, Point3D pointMaxE1, Point3D pointMinE1, Point3D pointMaxE2, Point3D pointMinE2)
        {
            // TODO: Werte fuer Abstand runden

            // Calculate the distance between 2 MyElement
            // MyDistance erg = new MyDistance();
            //Are the elements NOT in the same Z-Level? above or below each other
            if (pointMaxE1.Z <= pointMinE2.Z || pointMinE1.Z >= pointMaxE2.Z)
            {
                //above /below
                //not relevant if wall-elements are not parallel -> false flanking element
                if (SelectedElement.Type == TypeOfElement.Wall && element2.Type == TypeOfElement.Wall)
                {
                    if ((SelectedElement.AreaVector.NormalVector.X == 1 || SelectedElement.AreaVector.NormalVector.X == -1) && (element2.AreaVector.NormalVector.Y == 1 || element2.AreaVector.NormalVector.Y == -1))
                    {
                        Direction = DDirections.None;
                        Distance = null;
                        return;
                    }
                    if ((element2.AreaVector.NormalVector.X == 1 || element2.AreaVector.NormalVector.X == -1) && (SelectedElement.AreaVector.NormalVector.Y == 1 || SelectedElement.AreaVector.NormalVector.Y == -1))
                    {
                        Direction = DDirections.None;
                        Distance = null;
                        return;
                    }

                }


                XYDistanceAbove( pointMaxE1, pointMinE1, pointMaxE2, pointMinE2);

            }
            else // elements considered same level
            {
                XYDistance(pointMaxE1, pointMinE1, pointMaxE2, pointMinE2);
            }

        }

        private void XYDistance( Point3D pointMaxE1, Point3D pointMinE1, Point3D pointMaxE2, Point3D pointMinE2) 
            ///////TODO: ############### HIER WEITER MACHEN ### FEHLER BEI FASSADE
        {

            if (pointMaxE1.X <= pointMinE2.X || pointMinE1.X >= pointMaxE2.X) //Nr1
            {
                //beneath or over the corner? 
                if (pointMaxE1.Y <= pointMinE2.Y || pointMinE1.Y >= pointMaxE2.Y)
                {   //corner
                    Distance = null;
                    Direction = DDirections.None;
                }
                else
                {
                    // beneath
                    if (pointMaxE1.X <= pointMinE2.X)
                    {
                        Direction = DDirections.Xplus;
                        Distance = pointMinE2.X - pointMaxE1.X;
                    }
                    else
                    {
                        Direction = DDirections.Xminus;
                        Distance = pointMinE1.X - pointMaxE2.X;
                    }
                }

            }
            else if (pointMaxE1.Y <= pointMinE2.Y || pointMinE1.Y >= pointMaxE2.Y) //Nr2
            {
                //beneath or over the corner?
                if (pointMaxE1.X <= pointMinE2.X || pointMinE1.X >= pointMaxE2.X)
                {
                    //over the corner not relevant
                    Direction = DDirections.None;
                    Distance = null;

                }
                else
                {
                    // beneath
                    if (pointMaxE1.Y <= pointMinE2.Y)
                    {
                        Direction = DDirections.Yplus;
                        Distance = pointMinE2.Y - pointMaxE1.Y;
                    }
                    else
                    {
                        Direction = DDirections.Yminus;
                        Distance = pointMinE1.Y - pointMaxE2.Y;
                    }

                }
            }
            else if (pointMaxE1.X > pointMinE2.X && pointMinE1.X <= pointMinE2.X) //Nr3
            {
                if (!(pointMaxE1.Y <= pointMinE2.Y && pointMinE1.Y >= pointMaxE2.Y))
                {
                    //Overlap! in X-Direction
                    Distance = 0;
                    Direction = DDirections.Xminus;
                }


            }
            else if (pointMinE1.X < pointMaxE2.X && pointMaxE1.X >= pointMaxE2.X) //Nr4
            {
                if (!(pointMaxE1.Y <= pointMinE2.Y && pointMinE1.Y >= pointMaxE2.Y))
                {
                    //Overlap! in X-Direction
                    Distance = 0;
                    Direction = DDirections.Xplus;
                }

            }
            else if (pointMinE1.Y < pointMaxE2.Y && pointMaxE1.Y >= pointMaxE2.Y) //Nr5
            {
                if (!(pointMaxE1.X <= pointMinE2.X && pointMinE1.X >= pointMaxE2.X))
                {
                    //Overlap! in Y-Direction
                    Distance = 0;
                    Direction = DDirections.Yminus;
                }

            }
            else if (pointMinE1.Y < pointMaxE2.Y && pointMaxE1.Y >= pointMaxE2.Y) //Nr6
            {
                if (!(pointMaxE1.X <= pointMinE2.X && pointMinE1.X >= pointMaxE2.X))
                {
                    //Overlap! in Y-Direction
                    Distance = 0;
                    Direction = DDirections.Yplus;
                }

            }
            else                        //Nr7
            {
                Direction = DDirections.None;
                Distance = null;
            }



        }

        private void XYDistanceAbove( Point3D pointMaxE1, Point3D pointMinE1, Point3D pointMaxE2, Point3D pointMinE2)
        {
            //element2 is potential  flanking element
            //Element lies above or below. It must be exactly under or above each other to be flanking elements.
            //The relevant distance is in Z-direction.
            if (pointMaxE1.X <= pointMinE2.X || pointMinE1.X >= pointMaxE2.X)
            {
                Direction = DDirections.None;
                Distance = null;
            }
            else if (pointMaxE1.Y <= pointMinE2.Y || pointMinE1.Y >= pointMaxE2.Y)
            {
                Direction = DDirections.None;
                Distance = null;
            }
            else
            {
                if ((pointMinE1.Z - pointMaxE2.Z) < (pointMinE2.Z - pointMaxE1.Z))
                {
                    Direction = DDirections.Zplus;
                    Distance = pointMinE2.Z - pointMaxE1.Z;
                }
                else
                {
                    Direction = DDirections.Zminus;
                    Distance = pointMinE1.Z - pointMaxE2.Z;
                }
            }



        }




      
    }
}
