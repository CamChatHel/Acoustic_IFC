using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.UtilityResource;

namespace Prototype01
{
    public class CheckLayers
    {
        //Search for ElementType and their MaterialLayers
        //For ElementType Layer Analysis
        public IfcGloballyUniqueId typeNr { get; set; }
        public int materiallayerSetNr { get; set; }
        // List<int> materiallayerNr { get; set; }  //Vermutlich brauche ich die Info gar nicht odeR??!
        public double dinterior { get; set; }
        public double dcore { get; set; }
        public double dexterior { get; set; }

        public IEnumerable<int> materiallayerSetNrs { get; set; }  //was machen, wenn mehrere IfcMaterialLayerSets zugeordnet wurden?!?

        public static double GetThicknessFromLayers(IIfcElement element) 
        {
            IIfcMaterialLayerSetUsage AllLayerSetsUsage = element.Material as IIfcMaterialLayerSetUsage;
            double ElementThickness = new double();

            if (AllLayerSetsUsage != null)
            {
                var AllElementLayersThickness = AllLayerSetsUsage.ForLayerSet.MaterialLayers.ToList();
                foreach (var Layer in AllElementLayersThickness)
                {
                    double x = (double)Layer.LayerThickness;
                    ElementThickness = ElementThickness + x;
                }
            }
            else
            {
                Console.WriteLine("Error - no IfcMaterialLayerSetUsage for element: " + element.Name);
                IIfcMaterialLayerSet materialLayer = element.Material as IIfcMaterialLayerSet;
                if (materialLayer != null)
                {
                    var AllElementLayersThickness = materialLayer.MaterialLayers.ToList();
                    if (AllElementLayersThickness != null)
                    {
                        foreach (var Layer in AllElementLayersThickness)
                        {
                            double x = (double)Layer.LayerThickness;
                            ElementThickness = ElementThickness + x;
                        }
                    }
                }
                else
                {
                    ElementThickness = 0;
                }
                
                
                //Wenn Bauteil uber RelAggregats aus mehreren Besteht muss hier auf andere zugegriffen werden: FE.Thickness = ShapeBoundingBox.GetThicknessfromShape(FE);

            }
            return ElementThickness;

        }

        //TODO: Find Thickness of CoreLayer from ElementType
        public void GetCoreFromType(IIfcTypeObject ElementType)
        {
            //Find IfcMaterialLayerSet -> For Type
            IEnumerable<IIfcMaterialSelect> materialLayerSelect = ElementType.HasAssociations
                    .OfType<IIfcRelAssociatesMaterial>()
                    .Select(r => r.RelatingMaterial);

            //TODO: Integrate Automatic Search for CoreLayer #############################

            try
            {
                IIfcMaterialSelect mselect = materialLayerSelect.First();
            }
            catch 
            {

                this.dexterior = 0;
                this.dinterior = 0;
                return;
            }


            //Manual Check for CoreLayer with UserForm frmAllLayer
            //Check if LayerSet or LayerSetUsage or Other MaterialSelect
            if (materialLayerSelect.First() is IIfcMaterialLayerSet)
            {
                IIfcMaterialLayerSet materialLayerSets = (IIfcMaterialLayerSet)materialLayerSelect.First();

                using (var form = new frmAllLayer((string)ElementType.Name, materialLayerSets, this.dinterior, this.dcore, this.dexterior))
                {
                    var results = form.ShowDialog();
                    this.dcore = form._dcore;
                    this.dexterior = form._dexterior;
                    this.dinterior = form._dinterior;

                }
            }
            else if (materialLayerSelect.First() is IIfcMaterialLayerSetUsage)
            {
                IIfcMaterialLayerSetUsage materialLayerSetsUsage = (IIfcMaterialLayerSetUsage)materialLayerSelect.First();
                IIfcMaterialLayerSet materialLayerSets = materialLayerSetsUsage.ForLayerSet;

                using (var form = new frmAllLayer((string)ElementType.Name, materialLayerSets, this.dinterior, this.dcore, this.dexterior))
                {
                    var results = form.ShowDialog();
                    this.dcore = form._dcore;
                    this.dexterior = form._dexterior;
                    this.dinterior = form._dinterior;

                }
            }
            else
            {
                //Error, not usable MaterialSelect
                this.dexterior = 0;
                this.dinterior = 0;
            }

        }

        public string GetMaterialLayers(IIfcElement element) // Find Material Layers
        {
            string materialLayers = "";
            IIfcMaterialLayerSetUsage AllLayerSetsUsage = element.Material as IIfcMaterialLayerSetUsage;
            if (AllLayerSetsUsage != null)
            {
                var AllElementLayersThickness = AllLayerSetsUsage.ForLayerSet.MaterialLayers.ToList();
                foreach (var Layer in AllElementLayersThickness)
                {
                    if (materialLayers == "")
                    {
                        materialLayers =  Layer.LayerThickness.ToString() + "_" + Layer.Material.Name.ToString();
                    }
                    else materialLayers = materialLayers + ";" + Layer.LayerThickness.ToString() + "_" + Layer.Material.Name.ToString();
                }
            }
            return materialLayers;
        }
    }
    
}
