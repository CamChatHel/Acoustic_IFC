using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.Kernel;
using Xbim.Ifc;
using Xbim.Ifc4.SharedBldgElements;
using Xbim.Ifc4.ProductExtension;

namespace xbimModelHealing01
{
    internal class SpatialRelationHandler
    {
        public void AddSpatialReferenceToElement(IIfcSpatialElement spatialElement, IIfcElement element, IfcStore model)
        {
            IfcRelReferencedInSpatialStructure referencetoelement = model.Instances.New<IfcRelReferencedInSpatialStructure>();
            referencetoelement.RelatedElements.Add((IfcProduct)element);
            referencetoelement.RelatingStructure =(IfcSpatialElement)spatialElement;
        }

        public void AddSpatialReferenceToElement(List<IIfcSpatialElement> spatialElements, IIfcElement element, IfcStore model)
        {
            IfcRelReferencedInSpatialStructure referencetoelement = model.Instances.New<IfcRelReferencedInSpatialStructure>();
            referencetoelement.RelatedElements.Add((IfcProduct)element);
            foreach (var item in spatialElements)
            {
                referencetoelement.RelatingStructure = (IfcSpatialElement)item;
            }
            
        }

        public void AddSpatialContainmentToElement(IIfcSpatialElement spatialElement, IIfcElement element, IfcStore model)
        {
            IfcRelContainedInSpatialStructure referencetoelement = model.Instances.New<IfcRelContainedInSpatialStructure>();
            referencetoelement.RelatedElements.Add((IfcProduct)element);
            referencetoelement.RelatingStructure = (IfcSpatialElement)spatialElement;
        }


        public void DeleteSpatialContainement(IIfcElement element)
        {
            List<IIfcRelContainedInSpatialStructure> relationstoSpatialStructure = element.ContainedInStructure.ToList();
            foreach (IIfcRelContainedInSpatialStructure rel in relationstoSpatialStructure)
            {
                foreach (var item in rel.RelatedElements)
                {
                    if (item == element)
                    {
                        rel.RelatedElements.Remove(item);
                        return;
                    }
                }

            }
        }

        public void DeleteSpatialReferences(IIfcElement element)
        {
            List<IIfcRelReferencedInSpatialStructure> relationstoSpatialStructure = element.ReferencedInStructures.ToList();
            foreach (IIfcRelReferencedInSpatialStructure rel in relationstoSpatialStructure)
            {
                foreach (var item in rel.RelatedElements)
                {
                    if (item == element)
                    {
                        rel.RelatedElements.Remove(item);
                        return;
                    }
                }

            }
        }

        //public static void DeleteSpatialRelationOfCovering(IIfcCovering covering, IfcStore model)
        //{
        //    List<IIfcRelContainedInSpatialStructure> relationsCoveringtoSpatialStructure = covering.ContainedInStructure.ToList();
        //    foreach (IIfcRelContainedInSpatialStructure rel in relationsCoveringtoSpatialStructure)
        //    {
        //        foreach (var item in rel.RelatedElements)
        //        {
        //            if (item == covering)
        //            {
        //                rel.RelatedElements.Remove(item);
        //                return;
        //            }
        //        }


        //    }
        //}
    }
}
