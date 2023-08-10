using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc2x3.ProductExtension;
using Xbim.Ifc4.Interfaces;


namespace Prototype01
{
    public class GetAggregationLayers
    {
        //If there ist no meterialLayer or MaterialLayerSetUsage
        //Search for Elements Aggregated 
        //The same if no shape element -> search all other shapes

        public List<IIfcElement> allAggregatedElements = new List<IIfcElement>();

        public List<IIfcElement> SearchAggregates(IIfcElement element)
        {
            
            List<IIfcRelAggregates> relAggregates =  element.IsDecomposedBy.ToList(); //buildingSMART: An object definitions can only be part of a single decomposition
            foreach (IIfcRelAggregates relation in relAggregates)
            {

                List<IIfcObjectDefinition> AggregatesList = relation.RelatedObjects.ToList();
                foreach (IIfcElement aggregate in AggregatesList)
                {
                    List<IIfcRelAggregates> relAggregates2 = aggregate.IsDecomposedBy.ToList();
                    if(relAggregates2.Count() !=0 )
                    {
                        SearchAggregates(aggregate);
                    }
                    else allAggregatedElements.Add(aggregate);
                }
            }

            if (element is IIfcCovering)
            {
                List<IIfcRelAggregates> relAggregates2 = element.Decomposes.ToList();
                foreach (IIfcRelAggregates relation in relAggregates2)
                {
                    IIfcObjectDefinition aggregate = relation.RelatingObject;
                    allAggregatedElements.Add((IIfcElement)aggregate);
                    
                }
            }
            
            return allAggregatedElements;

        }


    }
}
