using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc4.ProductExtension;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;
using Xbim.Common.XbimExtensions;
using System.Runtime.Remoting.Contexts;
using Xbim.ModelGeometry.Scene;

namespace Prototype01
{
    public class RoomSelection //Select separating element between 2 rooms
    {
        public string separatingElementID;

        public void GetSeparatingElement(IIfcSpace room1, IIfcSpace room2, Xbim3DModelContext context)
        {
            List<IIfcRelSpaceBoundary> SBroom1 = room1.BoundedBy.ToList();
            List<IIfcRelSpaceBoundary> SBroom2 = room2.BoundedBy.ToList();

            List<IIfcElement> elements1 = new List<IIfcElement>();
            List<IIfcElement> elements2 = new List<IIfcElement>();
            foreach (var SB in SBroom1)
            {
                elements1.Add(SB.RelatedBuildingElement);
            }
            foreach (var SB in SBroom2)
            {
                elements2.Add(SB.RelatedBuildingElement);
            }
            var PossibleSelectedElement = elements1.Intersect(elements2); //Gemeinsame SpaceBoundary can auch für Flanken sein -< Richtung prüfen

            if (PossibleSelectedElement.Count() > 1 )
            {
                Console.WriteLine("More then 1 poissible element. Possible Separating element:");
                
                foreach (var item in PossibleSelectedElement)
                {
                    Console.WriteLine(item.Name.ToString() + ": " + item.GlobalId.ToString());
                }
                Console.WriteLine("Chooose GUID:");
                separatingElementID = Console.ReadLine();
            }
            else if (PossibleSelectedElement.Count() == 1)
            {
                separatingElementID = PossibleSelectedElement.First().GlobalId.ToString();
            }
            else
            {
                Console.WriteLine("No separating element found via room space boundaries");
            }

        }

    }
}
