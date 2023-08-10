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
    public class TypeHandler
    {

        public bool CheckforElementType(IIfcElement element)
        {
            
            IIfcTypeObject elementtype = element.IsTypedBy.FirstOrDefault()?.RelatingType;
            if (elementtype != null) { return true; }
            else { return false; }
            
        }

        public bool CheckforMaterialInType(IIfcTypeObject elementtype)
        {
            IIfcRelAssociatesMaterial e = elementtype.HasAssociations?.OfType<IIfcRelAssociatesMaterial>().FirstOrDefault();
            if (elementtype.HasAssociations.OfType<IIfcRelAssociatesMaterial>().FirstOrDefault() != null) return true;
            else return false;

        }

        public bool CheckforMaterialInType(IIfcElement element)
        {
            IIfcTypeObject elementtype = element.IsTypedBy.FirstOrDefault()?.RelatingType;
            
            if (this.CheckforMaterialInType(elementtype) == true) { return true; }
            else { return false; }

        }

        public static void SetupNewTypes(IfcStore model, IIfcElement element, string name)
        {
            if (element is IIfcWall)
            {
                CreateWallType(model, (IIfcWall)element, name);
            }
            if (element is IIfcSlab)
            {
                CreateSlabType(model, (IIfcSlab)element, name);
            }
            if (element is IIfcCurtainWall)
            {
                CreateCurtainType(model, (IIfcCurtainWall)element, name);
            }
        }

        static private void CreateWallType(IfcStore model, IIfcWall element, string name)
        {
            //wall type
            var walltype = model.Instances.New<IfcWallType>(wt =>
            {
                wt.Name = name;
                wt.PredefinedType = IfcWallTypeEnum.STANDARD;
            });

            //get the project: there should only be one and it should exist
            var project = model.Instances.OfType<IfcProject>().FirstOrDefault();
            var relDef = model.Instances.New<IfcRelDefinesByType>();
            relDef.RelatedObjects.Add((IfcWall)element);
            relDef.RelatingType = walltype;

        }

        static private void CreateCurtainType(IfcStore model, IIfcCurtainWall element, string name)
        {
            //wall type
            var walltype = model.Instances.New<IfcCurtainWallType>(wt =>
            {
                wt.Name = name;
                wt.PredefinedType = IfcCurtainWallTypeEnum.NOTDEFINED;
            });

            //get the project: there should only be one and it should exist
            var project = model.Instances.OfType<IfcProject>().FirstOrDefault();
            var relDef = model.Instances.New<IfcRelDefinesByType>();
            relDef.RelatedObjects.Add((IfcCurtainWall)element);
            relDef.RelatingType = walltype;

        }

        static private void CreateSlabType(IfcStore model, IIfcSlab element, string name)
        {
            //floor type
            var slabtype = model.Instances.New<IfcSlabType>(wt =>
            {
                wt.Name = name;
                wt.PredefinedType = IfcSlabTypeEnum.BASESLAB;
            });

            //get the project: there should only be one and it should exist
            var project = model.Instances.OfType<IfcProject>().FirstOrDefault();
            var relDef = model.Instances.New<IfcRelDefinesByType>();
            relDef.RelatedObjects.Add((IfcSlab)element);
            relDef.RelatingType = slabtype;


        }

        public IIfcElementType FindTypeWithMaterialLayerSet(IfcStore model, IIfcMaterialLayerSet materialLayerSet)
        {
            IIfcRelAssociatesMaterial elementtypeRelationToMaterialLayer = materialLayerSet.AssociatedTo.FirstOrDefault();
            if (elementtypeRelationToMaterialLayer != null)
            {
                return elementtypeRelationToMaterialLayer.RelatedObjects.OfType<IIfcElementType>().FirstOrDefault();
            }
            return null;
        }

        public static void SetToExistingType(IIfcElement element, IfcElementType type)
        {
            IfcRelDefinesByType relationtoType = type.Types.FirstOrDefault();
            if (element is IIfcWall)
            {
                relationtoType.RelatedObjects.Add((IfcWall)element);
            }
            if (element is IIfcSlab)
            {
                relationtoType.RelatedObjects.Add((IfcSlab)element);
            }
            if (element is IIfcCurtainWall)
            {
                relationtoType.RelatedObjects.Add((IfcCurtainWall)element);
            }
        }

        //public static void AddWallToExistingType(IIfcWall element, IfcRelDefinesByType relationtoType)
        //{
        //    relationtoType.RelatedObjects.Add((IfcWall)element);
        //}

        //static private void AddWallToExistingType(IfcStore model, IIfcWall element, IfcWallType type)
        //{
        //    var relDef = model.Instances.New<IfcRelDefinesByType>();
        //    relDef.RelatedObjects.Add((IfcWall)element);
        //    relDef.RelatingType = type;

        //}


    }
}
