using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.Kernel;
using Xbim.Ifc;
using Xbim.Ifc4.SharedBldgElements;
using Xbim.Ifc4.ProductExtension;
 

namespace xbimModelHealing01
{
    public class CoveringHandler
    {
        public static void AddCoveringToElement(IIfcCovering covering, IIfcElement element, IfcStore model)
        {
            IfcRelAggregates relationtoElement = model.Instances.New<IfcRelAggregates>();
            relationtoElement.RelatedObjects.Add((IfcCovering)covering);
            relationtoElement.RelatingObject = (IfcElement)element;


        }

        public static void AddCoveringToElement(IIfcCovering covering, IfcRelAggregates relation)
        {
            relation.RelatedObjects.Add((IfcCovering)covering);

        }

        public static void DeleteSpatialRelationOfCovering(IIfcCovering covering, IfcStore model)
        {
            List<IIfcRelContainedInSpatialStructure> relationsCoveringtoSpatialStructure = covering.ContainedInStructure.ToList();
            foreach (IIfcRelContainedInSpatialStructure rel in relationsCoveringtoSpatialStructure)
            {
                foreach (var item in rel.RelatedElements)
                {
                    if (item == covering)
                    {
                        rel.RelatedElements.Remove(item);
                        return;
                    }
                }
                
                
            }
        }

    }
}
