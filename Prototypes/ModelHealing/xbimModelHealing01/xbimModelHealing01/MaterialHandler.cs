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
using Xbim.Ifc4.MaterialResource;


namespace xbimModelHealing01
{
    public class MaterialHandler
    {
        public IIfcMaterialLayerSet FindMaterialLayerSet(IIfcElement element)
        {
            IIfcMaterialLayerSet materialLayerSet = null;

            IEnumerable<IIfcMaterialSelect> materialLayerSelect = element.HasAssociations
                                .OfType<IIfcRelAssociatesMaterial>()
                                .Select(r => r.RelatingMaterial);
            if (materialLayerSelect.FirstOrDefault() is IIfcMaterialLayerSet)
            {
                materialLayerSet = (IIfcMaterialLayerSet)materialLayerSelect.FirstOrDefault();
            }
            else if (materialLayerSelect.FirstOrDefault() is IIfcMaterialLayerSetUsage)
            {
                IIfcMaterialLayerSetUsage materialLayerSetUsage = (IIfcMaterialLayerSetUsage)materialLayerSelect.FirstOrDefault();
                materialLayerSet = materialLayerSetUsage.ForLayerSet;
            }
            //NOTE: Not considering IfcMaterialConstituentSet or IfcMaterialProfileSet 

            return materialLayerSet;
        }

        public static void SetStandardLayerSetUsageToElement(IfcStore model,  IIfcElement element, IfcElementType type)
        {
            //Create MaterialLayerSetUsage with standard setting
            IfcRelAssociatesMaterial relMaterial = model.Instances.New<IfcRelAssociatesMaterial>();
            IfcMaterialLayerSetUsage matLaySetUsage = model.Instances.New<IfcMaterialLayerSetUsage>();
            matLaySetUsage.DirectionSense = IfcDirectionSenseEnum.POSITIVE;
            if ((element is IIfcWall) || (element is IIfcCurtainWall)) matLaySetUsage.LayerSetDirection = IfcLayerSetDirectionEnum.AXIS2;
            else if ((element is IIfcSlab) || (element is IIfcPlate)) matLaySetUsage.LayerSetDirection = IfcLayerSetDirectionEnum.AXIS3;
            else matLaySetUsage.LayerSetDirection = IfcLayerSetDirectionEnum.AXIS2;
            IEnumerable<IIfcMaterialSelect> materialLayerSelect = element.HasAssociations
                                .OfType<IIfcRelAssociatesMaterial>()
                                .Select(r => r.RelatingMaterial);
            if (materialLayerSelect.FirstOrDefault() is IIfcMaterialLayerSet)
            {
                matLaySetUsage.ForLayerSet = (IfcMaterialLayerSet)materialLayerSelect.FirstOrDefault();
            }
            relMaterial.RelatedObjects.Add(element);
            relMaterial.RelatingMaterial = matLaySetUsage;
        }


    }
}
