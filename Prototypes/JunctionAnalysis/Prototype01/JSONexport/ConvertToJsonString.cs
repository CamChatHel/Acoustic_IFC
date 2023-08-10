using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc4.Kernel;
using Newtonsoft.Json;
using Xbim.Ifc4.Interfaces;

namespace Prototype01.JSONexport
{
    public class ConvertToJSONstring
    {
        public JsonElementwithAllJunctions GetJsonString(List<Junction> stossstellen)
        {

            List<JsonJunction> alle = new List<JsonJunction>();

            foreach (Junction stossstelle in stossstellen)
            {

                JsonJunction junctionforJSON = new JsonJunction()
                {
                    ID = stossstelle.GlobalId,
                    CommonLength = stossstelle.CommonLength,
                    TypeOfJunction = stossstelle.TypeOfJunction.ToString(),
                    TypeOfFastener = stossstelle.TypeOfFastener.ToString(),
                    SeparatingElementID = stossstelle.SeparatingElementID 
                };

                RelAggregatesJunction relationtoelements = stossstelle.IsDecomposedBy;
                if (relationtoelements != null)
                {

                    List<BuildingElementJSON> objects = relationtoelements.RelatedObjects.ToList();
                    if (objects.Count != 0)
                    {
                        foreach (BuildingElementJSON item in objects)
                        {
                            junctionforJSON.BuildingElementsID.Add(item);
                        }
                    }

                }

                List<RelAssociatesPaths> relationtopaths = stossstelle.Transmits;
                if (relationtopaths != null)
                {
                    foreach (RelAssociatesPaths item in relationtopaths)
                    {
                        foreach (var path in item.RelatedPath)
                        {
                            TransmissionPathJSON jsonpath = new TransmissionPathJSON();

                            if (path.GlobalId != null) jsonpath.GlobalId = path.GlobalId.ToString();
                            if (path.Name != null) jsonpath.pathName = path.Name.ToString();
                            if (path.Is_i != null) jsonpath.Is_j = path.Is_j.GlobalId.ToString();
                            if (path.Is_j != null) jsonpath.Is_i = path.Is_i.GlobalId.ToString();

                            junctionforJSON.TransmissionPaths.Add(jsonpath);
                        }

                    }
                }

                alle.Add(junctionforJSON);
            }






            JsonElementwithAllJunctions completejunctionforJSON = new JsonElementwithAllJunctions()
            {
                Name = "Inputdata",
                AllJunctions = alle
            };
            return completejunctionforJSON;

        }

    }
    

    public class JsonJunction
    {

        public string ID { get; set; }

        public string SeparatingElementID { get; set; }
        public double? CommonLength { get; set; }
        public string TypeOfJunction { get; set; }
        public string TypeOfFastener { get; set; }
       
        public List<BuildingElementJSON> BuildingElementsID { get; set; } = new List<BuildingElementJSON>();

        public List<TransmissionPathJSON> TransmissionPaths { get; set; } = new List<TransmissionPathJSON>();

    }

    public class JsonElementwithAllJunctions
    {
        public string Name { get; set; }
        public List<JsonJunction> AllJunctions { get; set; }

    }

    public class TransmissionPathJSON
    {

        public string GlobalId { get; set; }

        public string pathName;

        public string Is_i { get; set; }
        public string Is_j { get; set; }



    }

    public class BuildingElementJSON
    {

        public string ElementID { get; set; }

        public string ElementName { get; set; }
        public string ElementMaterial { get; set; }
        public string Covering1ID { get; set; }
        public string Covering1Material { get; set; }
        public string Covering2ID { get; set; }
        public string Covering2Material { get; set; }


        public void TransformToBuildingElementJSON(IIfcElement element)
        {
            BuildingElementJSON transformedElement = new BuildingElementJSON();
            
            ElementName = element.Name;

            //GUID
            ElementID = element.GlobalId;

            //Material Layers
            CheckLayers layersSearch = new CheckLayers();
            ElementMaterial = layersSearch.GetMaterialLayers(element);

            //Coverings
            List<IIfcRelAggregates> aggretationsRel = new List<IIfcRelAggregates>(); ;
            aggretationsRel = element.IsDecomposedBy.ToList();
            foreach (IIfcRelAggregates relaggr in aggretationsRel)
            {
                foreach (var item in relaggr.RelatedObjects)
                {
                    if (item is IIfcCovering)
                    {
                        if (Covering1ID == "")
                        {
                            Covering1ID = relaggr.GlobalId;
                            Covering1Material = layersSearch.GetMaterialLayers((IIfcElement)item);
                        }
                        else if (Covering2ID == "")
                        {
                            Covering2ID = relaggr.GlobalId;
                            Covering2Material = layersSearch.GetMaterialLayers((IIfcElement)item);
                        }
                        
                    }
                }
                
            }


        }

    }

  

}


