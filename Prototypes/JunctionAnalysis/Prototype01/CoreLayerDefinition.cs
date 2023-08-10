using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;
using System.Windows.Media.Media3D;

namespace Prototype01
{
    public class CoreLayerDefinition
    {
        //If "LoadBearing" in MaterialLayer -> use that
        //else: show Form to select the corelayer

        public void GetCoreLayer(IIfcElement element, MyElement MyOwnElement, bool UseCoreLayer, CheckLayers elementTypeLayers)
        {
            if (UseCoreLayer == true)
            {
                //Use CoreLayerInformation from TypeLayer
                // IIfcMaterialLayerSetUsage AllLayerSetsUsage = element.Material as IIfcMaterialLayerSetUsage;
                CoreLayerDefinition SearchCoreLayer = new CoreLayerDefinition();
                SearchCoreLayer.CoreLayer(element, MyOwnElement, elementTypeLayers);
            }
            else
            {
                //Standard is CoreLayer = BoundingBoxValues // if no CoreLayer usage, then THIS is standard
                MyOwnElement.SetCoreMin(MyOwnElement.Min.X, MyOwnElement.Min.Y, MyOwnElement.Min.Z);
                MyOwnElement.SetCoreMax(MyOwnElement.Max.X, MyOwnElement.Max.Y, MyOwnElement.Max.Z);
                elementTypeLayers.dcore = MyOwnElement.Thickness;
                elementTypeLayers.dexterior = 0;
                elementTypeLayers.dinterior = 0;
            }
        }

        public void GetCoreLayer(IIfcElement element, MyElement MyOwnElement, bool UseCoreLayer)
        {
            if (UseCoreLayer == true)
            {
                //Use CoreLayer
               // IIfcMaterialLayerSetUsage AllLayerSetsUsage = element.Material as IIfcMaterialLayerSetUsage;
                CoreLayerDefinition SearchCoreLayer = new CoreLayerDefinition();
                SearchCoreLayer.CoreLayer(element, MyOwnElement);
            }
            else
            {
                //Standard is CoreLayer = BoundingBoxValues // if no CoreLayer usage, then THIS is standard
                MyOwnElement.SetCoreMin(MyOwnElement.Min.X, MyOwnElement.Min.Y, MyOwnElement.Min.Z);
                MyOwnElement.SetCoreMax(MyOwnElement.Max.X, MyOwnElement.Max.Y, MyOwnElement.Max.Z);
            }
        }

        public void CoreLayer(IIfcElement element, MyElement MyOwnElement, CheckLayers elementTypeLayers)
        {

            //TODO: CHANGE!!! Use CoreLayerInformation from LayerType to calculate the Core BoundingBox!

            bool coreLayerExist = false;
            int i = 1;
            double dsum = 0;
            double dsumMinusOne = 0;
            IIfcMaterialLayerSetUsage AllLayerSetsUsage = element.Material as IIfcMaterialLayerSetUsage;

            //TO DO: Fehler abfangen, wenn AllLayerSetsUsage = null, Elemente aus RelAggregates!
            foreach (IIfcMaterialLayer materialLayer in AllLayerSetsUsage.ForLayerSet.MaterialLayers)
            {
                dsumMinusOne = dsum;
                dsum = dsum + (double)materialLayer.LayerThickness.Value;

                if (materialLayer.Category == "LoadBearing") //TODO: Wandtyp Speichern? Dann müsste nicht jedes Element abgefragt werden
                {
                    coreLayerExist = true;
                    //TODO: die Rechnung auslagern -> Fuer LoadBearing nicht vorhanden wird nach dem Ergaenzen auch dasselbe gemacht!
                    dsumMinusOne = Math.Round(dsumMinusOne, 2);
                    dsum = Math.Round(dsum, 2);
                    CalculateCoreMinMax(AllLayerSetsUsage, MyOwnElement, dsumMinusOne, dsum);
                    //Safe this maerial as corelayer material for this project
                    AddCoreLayerMaterials(materialLayer.Name);
                }

            }

            //wennn kein core layer gefunden wurde
            if (coreLayerExist == false) //no MaterialLayer with "LoadBearing" -> //TODO: search for corelayer by hand and add information in IFC 
            {
                //Form anzeigen zur Auswahl des CoreLayers -> Uebergabe Element und seine LayerSet
                new frmAllLayer(element.Name, MyOwnElement, AllLayerSetsUsage).ShowDialog();

                //MaterialLayer mit Core-Informationen ergaenzen -> //TODO: neue IFC?

            }

        }

        public void CoreLayer(IIfcElement element, MyElement MyOwnElement)
        {
            bool coreLayerExist = false;
            int i = 1;
            double dsum = 0;
            double dsumMinusOne = 0;
            IIfcMaterialLayerSetUsage AllLayerSetsUsage = element.Material as IIfcMaterialLayerSetUsage;

            //TO DO: Fehler abfangen, wenn AllLayerSetsUsage = null, Elemente aus RelAggregates!
            foreach (IIfcMaterialLayer materialLayer in AllLayerSetsUsage.ForLayerSet.MaterialLayers)
            {
                dsumMinusOne = dsum;
                dsum = dsum + (double)materialLayer.LayerThickness.Value;

                if (materialLayer.Category == "LoadBearing") //TODO: Wandtyp Speichern? Dann müsste nicht jedes Element abgefragt werden
                {
                    coreLayerExist = true;
                    //TODO: die Rechnung auslagern -> Fuer LoadBearing nicht vorhanden wird nach dem Ergaenzen auch dasselbe gemacht!
                    dsumMinusOne = Math.Round(dsumMinusOne, 2);
                    dsum = Math.Round(dsum, 2);
                    CalculateCoreMinMax(AllLayerSetsUsage, MyOwnElement, dsumMinusOne, dsum);
                    //Safe this maerial as corelayer material for this project
                    AddCoreLayerMaterials(materialLayer.Name);
                }

            }

            //wennn kein core layer gefunden wurde
            if (coreLayerExist == false) //no MaterialLayer with "LoadBearing" -> //TODO: search for corelayer by hand and add information in IFC 
            {
                //Form anzeigen zur Auswahl des CoreLayers -> Uebergabe Element und seine LayerSet
                new frmAllLayer(element.Name, MyOwnElement, AllLayerSetsUsage).ShowDialog();

                //MaterialLayer mit Core-Informationen ergaenzen -> //TODO: neue IFC?

            }

        }

        public void CalculateCoreMinMax(IIfcMaterialLayerSetUsage AllLayerSetsUsage, MyElement MyOwnElement, double dsumMinusOne, double dsum)
        {
            if (AllLayerSetsUsage.DirectionSense == IfcDirectionSenseEnum.POSITIVE && AllLayerSetsUsage.LayerSetDirection == IfcLayerSetDirectionEnum.AXIS2) //walls
            {
                if (MyOwnElement.AreaVector.NormalVector == new Vector3D(1, 0, 0) || MyOwnElement.AreaVector.NormalVector == new Vector3D(-1, 0, 0))
                {
                    MyOwnElement.SetCoreMin(MyOwnElement.Min.X + dsumMinusOne, MyOwnElement.Min.Y, MyOwnElement.Min.Z);
                    MyOwnElement.SetCoreMax(MyOwnElement.Min.X + dsum, MyOwnElement.Max.Y, MyOwnElement.Max.Z);
                    return;
                }
                else if (MyOwnElement.AreaVector.NormalVector == new Vector3D(0, 1, 0) || MyOwnElement.AreaVector.NormalVector == new Vector3D(0, -1, 0))
                {
                    MyOwnElement.SetCoreMin(MyOwnElement.Min.X, MyOwnElement.Min.Y + dsumMinusOne, MyOwnElement.Min.Z);
                    MyOwnElement.SetCoreMax(MyOwnElement.Max.X, MyOwnElement.Min.Y + dsum, MyOwnElement.Max.Z);
                    return;
                }
            }
            else if (AllLayerSetsUsage.DirectionSense == IfcDirectionSenseEnum.NEGATIVE && AllLayerSetsUsage.LayerSetDirection == IfcLayerSetDirectionEnum.AXIS2) //walls
            {
                if (MyOwnElement.AreaVector.NormalVector == new Vector3D(1, 0, 0) || MyOwnElement.AreaVector.NormalVector == new Vector3D(-1, 0, 0))
                {
                    MyOwnElement.SetCoreMin(MyOwnElement.Max.X - dsum, MyOwnElement.Min.Y, MyOwnElement.Min.Z);
                    MyOwnElement.SetCoreMax(MyOwnElement.Max.X - dsumMinusOne, MyOwnElement.Max.Y, MyOwnElement.Max.Z);
                    return;
                }
                else if (MyOwnElement.AreaVector.NormalVector == new Vector3D(0, 1, 0) || MyOwnElement.AreaVector.NormalVector == new Vector3D(0, -1, 0))
                {
                    MyOwnElement.SetCoreMin(MyOwnElement.Min.X, MyOwnElement.Max.Y - dsum, MyOwnElement.Min.Z);
                    MyOwnElement.SetCoreMax(MyOwnElement.Max.X, MyOwnElement.Max.Y - dsumMinusOne, MyOwnElement.Max.Z);
                    return;
                }
            }
            else if (AllLayerSetsUsage.DirectionSense == IfcDirectionSenseEnum.POSITIVE && AllLayerSetsUsage.LayerSetDirection == IfcLayerSetDirectionEnum.AXIS3) //slabs
            {
                MyOwnElement.SetCoreMin(MyOwnElement.Min.X, MyOwnElement.Min.Y, MyOwnElement.Min.Z + dsumMinusOne);
                MyOwnElement.SetCoreMax(MyOwnElement.Max.X, MyOwnElement.Max.Y, MyOwnElement.Min.Z + dsum);
                return;
            }
            else if (AllLayerSetsUsage.DirectionSense == IfcDirectionSenseEnum.NEGATIVE && AllLayerSetsUsage.LayerSetDirection == IfcLayerSetDirectionEnum.AXIS3) //slabs
            {
                MyOwnElement.SetCoreMin(MyOwnElement.Max.X, MyOwnElement.Min.Y, MyOwnElement.Max.Z - dsumMinusOne);
                MyOwnElement.SetCoreMax(MyOwnElement.Max.X, MyOwnElement.Max.Y, MyOwnElement.Min.Z - dsum);
                return;
            }
        }

        public List<string> CoreLayerMaterials { get; set; }

        public void AddCoreLayerMaterials(string layername)
        {
            CoreLayerMaterials.Add(layername);
        }

    }
}
