using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.ProductExtension;
using Xbim.Ifc4.MaterialResource;
using Xbim.Ifc4.UtilityResource;
using Xbim.Common;
using Xbim.Ifc;
using Xbim.IO;
using Xbim.ModelGeometry.Scene;

namespace xbimModelHealing01
{
    public class StoreyHandler
    {
        public static bool ElementHasBuildingStorey(IIfcElement element) //true = yes, false = no
        {

            List<IIfcRelContainedInSpatialStructure> RelationToSpatialStructures = element.ContainedInStructure.ToList();
            if (RelationToSpatialStructures.Count == 0) { return false; } //no relation, wall has no IfcBuildingStorey
            else
            {
                foreach (IIfcRelContainedInSpatialStructure RelationToSpatialStructure in RelationToSpatialStructures)
                {
                    IIfcSpatialElement SpatialElement = RelationToSpatialStructure.RelatingStructure;
                    if (SpatialElement is IIfcBuildingStorey) //IfcBuildingStorey found, no problem
                    {
                        return true;
                    }
                    else if (RelationToSpatialStructure.Equals(RelationToSpatialStructures.Last())) { return false; }//Last element is still no BuildingStorey
                 
                }
            }
            return false;
        }

        public static string HealingBuildingStoreyToAdd(IIfcElement element, List<BuildingStorey> AllStoreys, Xbim3DModelContext context)
        {
            
            {
                ShapeHandler GeometryOfElement = new ShapeHandler();
                GeometryOfElement.GetShape(element, context);
                string GUIDstorey = StoreyAccordingToElevation(AllStoreys, GeometryOfElement);
                return GUIDstorey;

            }

        }

        public static string StoreyAccordingToElevation(List<BuildingStorey> ListBuildingStoreys, ShapeHandler GeometryOfElement)
        {
            BuildingStorey buildingstoreyBefore = ListBuildingStoreys.First();
            foreach (BuildingStorey buildingstorey in ListBuildingStoreys)
            {
                if (GeometryOfElement.elementBoundingBoxMin.Z <= buildingstorey.Elevation)
                {
                    return buildingstoreyBefore.GUID;
                }
                buildingstoreyBefore = buildingstorey;
            }

            return ListBuildingStoreys.Last().GUID;
        }

        public static IIfcBuildingStorey FindBuildingStorey(IIfcElement Element)
        {
            // Find the IfcBuildingStorey related to the Element

            List<IIfcRelContainedInSpatialStructure> SpatialStructures = Element.ContainedInStructure.ToList();
            foreach (var structure in SpatialStructures)
            {
                IIfcSpatialElement structureElement = structure.RelatingStructure;

                if (structure.RelatingStructure is IIfcBuildingStorey)
                {
                    return (IIfcBuildingStorey)structureElement;
                }
            }
            //if no spatialstructure found return null
            return null;

        }

        public static bool ElementIsReferencedinBuildingStorey(IIfcElement element, IIfcBuildingStorey bs) //true = yes, false = no
        {
            List<IIfcRelReferencedInSpatialStructure> ReferenceToSpatialStructures = element.ReferencedInStructures.ToList();
            if (ReferenceToSpatialStructures.Count == 0) { return false; } //no reference at all, element has no Reference to IfcBuildingStorey
            else
            {
                foreach (IIfcRelReferencedInSpatialStructure ReferenceToSpatialStructure in ReferenceToSpatialStructures)
                {
                    IIfcSpatialElement SpatialElement = ReferenceToSpatialStructure.RelatingStructure;
                    if (SpatialElement is IIfcBuildingStorey) //IfcBuildingStorey found, no problem
                    {
                        if (SpatialElement.GlobalId == bs.GlobalId) return true;
                        
                    }
                }
            }
            return false;
        }

       

    }





    // My Own Class of BuildingSotrey to be able to sort the storey by Elevation
    public class BuildingStorey : IComparable<BuildingStorey>
    {

        public string GUID { get; set; }
        public double Elevation { get; set; }

        //damit binarySearch funktioniert
        public int CompareTo(BuildingStorey other)
        {
            return this.Elevation.CompareTo(other.Elevation);
        }


        public override bool Equals(object obj)
        {
            //definiert wann Objekte gleich sind
            //vergleicht die Stockwerkhoehe

            if (obj is BuildingStorey)
            {
                BuildingStorey other = (BuildingStorey)obj;
                return this.GUID == other.GUID;
            }
            return false;
        }
    }
}
