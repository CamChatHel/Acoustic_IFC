using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;

namespace Prototype01
{
    class ClearList
    {

        public static void RemoveFromWalls(ref List<IIfcWall> ListToClear, List<IIfcElement> ElementsToRemove)
        {

            foreach (IIfcElement i in ElementsToRemove)
            {
                if (i != null)
                {
                    string ityp = i.GetType().Name;
                    if (ityp == ElementType.IfcWall.ToString() || ityp == ElementType.IfcWallStandardCase.ToString() || ityp == ElementType.IfcWallElementedCase.ToString())
                    {
                        ListToClear.Remove((IIfcWall)i);
                    }
                }
                
            }
            
        }

        public static void RemoveFromSlabs(ref List<IIfcSlab> ListToClear, List<IIfcElement> ElementsToRemove)
        {

            foreach (IIfcElement i in ElementsToRemove)
            {
                if (i != null)
                {
                    string ityp = i.GetType().Name;
                    if (ityp == ElementType.IfcSlab.ToString() || ityp == ElementType.IfcSlabStandardCase.ToString() || ityp == ElementType.IfcSlabElementedCase.ToString())
                    {
                        ListToClear.Remove((IIfcSlab)i);
                    }
                }
                

            }
            
        }


        public static void RemoveFromCurtainWalls(ref List<IIfcCurtainWall> ListToClear, List<IIfcElement> ElementsToRemove)
        {

            foreach (IIfcElement i in ElementsToRemove)
            {
                if (i != null)
                {
                    string ityp = i.GetType().Name;
                    if (ityp == ElementType.IfcCurtainWall.ToString() )
                    {
                        ListToClear.Remove((IIfcCurtainWall)i);
                    }
                }


            }

        }

    }
}
