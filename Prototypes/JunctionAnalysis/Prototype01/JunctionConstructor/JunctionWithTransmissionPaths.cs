using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc4.Interfaces;
using Xbim.Common;
using Xbim.Ifc4.UtilityResource;
using Xbim.Ifc4.MeasureResource;

namespace Prototype01
{
    public class Junction
    {
        /// <summary>
        /// Create junction with attributes to connect BuildingElements in the junction with tranmission paths
        /// </summary>
        /// <param name="CommonLength"></param>
        /// <param name="TypeOfJunction"></param>
        /// /// <param name="TypeOfFastener"></param>
        /// <returns>IfcWall</returns>
        /// 


        public Junction()
        {
            GlobalId = Guid.NewGuid();
        }

        public IfcGloballyUniqueId GlobalId { get; set; }

        public string SeparatingElementID { get; set; }

        public double? CommonLength { get; set; }
        public JunctionType TypeOfJunction { get; set; }
        public RealizingType TypeOfFastener { get; set; }


        public RelAggregatesJunction IsDecomposedBy { get; set; } //zu IfcRelAggregates fuer IfcBuiltElement  //TODO: GEHT DAS HIER?!?!? HIER MUSS RELAGGREGATESJUNCITON HIN

        public List<RelAssociatesPaths> Transmits { get; set; } //zuordnung von TransmissionPath

        //public enum JunctionType //dash = Bindestrich, colon = Doppelpunkt
        //{
        //    Lh1d2,
        //    Lv1d2,
        //    Tv1d24,
        //    Tv2d13,
        //    Th1d24,
        //    Th2d13,
        //    Th2d1d4,
        //    Tv2d1d4,
        //    Th1d2c4,
        //    Tv1d2c4,
        //    Tv2d1c3,
        //    Xh1d24d3,
        //    Xv2d13d4,
        //    Xv1d24d3,
        //    Xh2d1c3d4,
        //    Xv2d1c3d4,
        //    JunctionTypeError,
        //    ErrorNoCorrectDirection
        //}

        public enum RealizingType //Elements in the junction to connect building elements together
        {
            screws, //DE:Schrauben
            anglebrackets, //DE:Winkelverbinder
            bolts, //DE:Bolzen
            plates, //DE:Lochverbinder und Rispenbänder
            hangers, //DE:Ballenschuhe
            others

        }

    }

    public class TransmissionPath //: IEquatable<TransmissionPath>
    {
        [EntityAttribute(8, EntityAttributeState.Optional, EntityAttributeType.None, EntityAttributeType.None, null, null, 20)]
        public IfcIdentifier? Tag { get; set; }
        //public bool Equals(TransmissionPath other);

        public TransmissionPath()
        {
            GlobalId = Guid.NewGuid();
        }

        public IfcGloballyUniqueId GlobalId { get; set; }

        public PathsName? Name;

        public IIfcElement Is_i { get; set; }
        public IIfcElement Is_j { get; set; }

        //public RelAssociatesPaths IsDecomposedBy { get; set; }

        //public IItemSet<IfcRelDefinesByProperties> IsDefinedBy { get; }

        public enum PathsName
        {
            Df,
            Fd,
            Ff,
            DFf,
            Other
        }
    }


}

