using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc4.MeasureResource;
using Xbim.Ifc4.UtilityResource;
using Xbim.Ifc4.ProductExtension;
using Prototype01.JSONexport;

namespace Prototype01
{
    public class RelAssociatesPaths
    {
        public RelAssociatesPaths()
        {
            GlobalId = Guid.NewGuid();
        }

        public IfcGloballyUniqueId GlobalId { get; set; }

        public IfcLabel? Name { get; set; }

        public Junction RelatingJunction { get; set; }
        public List<TransmissionPath> RelatedPath { get; } = new List<TransmissionPath>(); //TransmissionPaths (einzelne Wege in Liste eibringen)

        public double[,] Kij = new double[21, 2];
        public double[,] Rij = new double[21, 2];
        public double[,] Lnij = new double[21, 2];

    }

    public class RelAggregatesJunction  //: IfcRelDecomposes, IInstantiableEntity, IPersistEntity, IPersist,  IIfcRelDecomposes, IIfcRelationship, IIfcRoot, IEquatable<RelAggregatesJunction>
    {

        public RelAggregatesJunction()
        {
            GlobalId = Guid.NewGuid();
        }

        public IfcGloballyUniqueId GlobalId { get; set; }

        public IfcLabel? Name { get; set; }

        public Junction RelatingObject { get; set; }

        public List<BuildingElementJSON> RelatedObjects { get; } = new List<BuildingElementJSON>();
        

    }

}
