using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Media.Media3D;
using Xbim.Ifc4.Interfaces;

namespace Prototype01
{
    public class CheckParallel
    {
        //Check if elements are parallel and can be seen as one element
        //parallel elements -> check if distance is < 0,3 m
        
        public Boolean ParallelToElement(MyElement element1, MyElement element2) //element 1 is reference element
        {
            if ((element1.AreaVector.NormalVector.X == 1 || element1.AreaVector.NormalVector.X == -1)&&(element2.AreaVector.NormalVector.X == 1 || element2.AreaVector.NormalVector.X == -1))
            {
                //element same direction X
                if (element2.DistanceDirection.Direction == DDirections.Xplus || element2.DistanceDirection.Direction == DDirections.Xminus)
                {
                    //element2 is parallel to element 1
                    return true;
                }
            }
            else if ((element1.AreaVector.NormalVector.Y == 1 || element1.AreaVector.NormalVector.Y == -1) && (element2.AreaVector.NormalVector.Y == 1 || element2.AreaVector.NormalVector.Y == -1))
            {
                //element same direction X
                if (element2.DistanceDirection.Direction == DDirections.Yplus || element2.DistanceDirection.Direction == DDirections.Yminus)
                {
                    //element2 is parallel to element 1
                    return true;
                }
            }
            else if ((element1.AreaVector.NormalVector.Z == 1 || element1.AreaVector.NormalVector.Z == -1) && (element2.AreaVector.NormalVector.Z == 1 || element2.AreaVector.NormalVector.Z == -1))
            {
                //element same direction X
                if (element2.DistanceDirection.Direction == DDirections.Zplus || element2.DistanceDirection.Direction == DDirections.Zminus)
                {
                    //element2 is parallel to element 1
                    return true;
                }
            }
            return false;
        }

        public void ElementAnalysis(MyElement element1, List<MyElement> elementList, Boolean coreLayer)
        {
            List<MyElement> elementstomerge = new List<MyElement>();
            for (int i = 0; i<  elementList.Count(); i++)
            {
                var element2 = elementList[i];
                MyDistance abstand = new MyDistance();
                abstand.CalculateDistance(element1, element2, coreLayer);

                if (abstand.Distance <= 0.3) elementstomerge.Add(element2); // Egal ob hintereinander gereiht oder parallel zueinander!
       
            }
            //Merge both elements to one element
            if (elementstomerge.Count()>0) MergeElements(element1, elementstomerge, coreLayer);
            

        }


        private void MergeElements(MyElement element1, List<MyElement> elementList, Boolean coreLayer)
        {
            Point3D newMin = new Point3D();
            Point3D newMax = new Point3D();

            newMin.X = elementList.Min(r => r.Min.X);
            newMin.Y = elementList.Min(r => r.Min.Y);
            newMin.Z = elementList.Min(r => r.Min.Z);
            newMax.X = elementList.Max(r => r.Max.X);
            newMax.Y = elementList.Max(r => r.Max.Y);
            newMax.Z = elementList.Max(r => r.Max.Z);

            element1.Min = newMin;
            element1.Max = newMax;

            if (coreLayer == true)
            {
                //merge corelayer
                newMin.X = elementList.Min(r => r.CoreMin.X);
                newMin.Y = elementList.Min(r => r.CoreMin.Y);
                newMin.Z = elementList.Min(r => r.CoreMin.Z);
                newMax.X = elementList.Max(r => r.CoreMax.X);
                newMax.Y = elementList.Max(r => r.CoreMax.Y);
                newMax.Z = elementList.Max(r => r.CoreMax.Z);

                element1.CoreMin = newMin;
                element1.CoreMax = newMax;
            }
            else
            {
                //use Min/Max for CoreLayer-Values
                 element1.CoreMin = element1.Min ;
                 element1.CoreMax = element1.Max ;
            }

            element1.Merged = true;
            //Combine GUIDs??
            //foreach (MyElement element2 in elementList)
            //{ //TODO: wieso geht das nicht mehr`??!!!
            //    //element1.GUID = element1.GUID  + " " + element2.GUID;
            //}
            
        }

        public void MergeElements(MyElement element1, MyElement element2, Boolean coreLayer)
        {
            Point3D newMin = new Point3D();
            Point3D newMax = new Point3D();
            List<MyElement> elementList = new List<MyElement>();
            elementList.Add(element1);
            elementList.Add(element2);

            newMin.X = elementList.Min(r => r.Min.X);
            newMin.Y = elementList.Min(r => r.Min.Y);
            newMin.Z = elementList.Min(r => r.Min.Z);
            newMax.X = elementList.Max(r => r.Max.X);
            newMax.Y = elementList.Max(r => r.Max.Y);
            newMax.Z = elementList.Max(r => r.Max.Z);

            element1.Min = newMin;
            element1.Max = newMax;

            if (coreLayer == true)
            {
                //merge corelayer
                newMin.X = elementList.Min(r => r.CoreMin.X);
                newMin.Y = elementList.Min(r => r.CoreMin.Y);
                newMin.Z = elementList.Min(r => r.CoreMin.Z);
                newMax.X = elementList.Max(r => r.CoreMax.X);
                newMax.Y = elementList.Max(r => r.CoreMax.Y);
                newMax.Z = elementList.Max(r => r.CoreMax.Z);

                element1.CoreMin = newMin;
                element1.CoreMax = newMax;
            }
            else
            {
                //use Min/Max for CoreLayer-Values
                element1.CoreMin = element1.Min;
                element1.CoreMax = element1.Max;
            }

            element1.Merged = true;
            

        }
    }
}
