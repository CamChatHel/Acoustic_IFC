using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc;
using Xbim.ModelGeometry.Scene;
using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.UtilityResource;
using Xbim.Ifc4.Kernel;
using Xbim.Ifc4.ExternalReferenceResource;

using Xbim.Ifc2x3.ExternalReferenceResource;
using Xbim.Ifc2x3.Interfaces;

using System.IO;
using System.Windows.Shell;

namespace Xbim.WinformsSample
{
    public class GetModellInformation
    {
        public List<Ifc4.Interfaces.IIfcElement> GetConnections(IfcGloballyUniqueId elementID, IfcStore model)
        {
            Xbim3DModelContext context = new Xbim3DModelContext(model);
            context.CreateContext();
			Ifc4.Interfaces.IIfcElement selectedElement = model.Instances.FirstOrDefault<Ifc4.Interfaces.IIfcElement>(d => d.GlobalId == elementID.ToString());
            if (selectedElement == null) return  null;
           
            //RelConnection suchen
            List<Ifc4.Interfaces.IIfcRelConnectsElements> connectionFrom = selectedElement.ConnectedFrom.ToList();
            List<Ifc4.Interfaces.IIfcRelConnectsElements> connectionTo = selectedElement.ConnectedTo.ToList();
            List<Ifc4.Interfaces.IIfcElement> templist = new List<Ifc4.Interfaces.IIfcElement>();

            foreach (var element in connectionFrom)
            {
                templist.Add(element.RelatingElement);
            }
            foreach (var element in connectionTo)
            {
                templist.Add(element.RelatedElement);
            }

            return templist;

        }

        public List<string> GetReferencedFiles (string elementID, IfcStore model)
        {
            Xbim3DModelContext context = new Xbim3DModelContext(model);
            context.CreateContext();
            Xbim.Ifc4.Interfaces.IIfcElement selectedElement = model.Instances.FirstOrDefault<Xbim.Ifc4.Interfaces.IIfcElement>(d => d.GlobalId == elementID);
            if (selectedElement == null) return null;

            //Check for IFC Version
            //Check IFC Version before associating document
            if (model.SchemaVersion == Xbim.Common.Step21.XbimSchemaVersion.Ifc4)
            {
                //Search for IfcRelAssociatesDocuments
                List<string> files = new List<string>();
                List<Ifc4.Interfaces.IIfcRelAssociatesDocument> relassociations = selectedElement.HasAssociations.OfType<Ifc4.Interfaces.IIfcRelAssociatesDocument>().ToList();
                foreach (var item in relassociations)
                {
                    var doc = item.RelatingDocument;
                    if (doc is Ifc4.ExternalReferenceResource.IfcDocumentInformation)
                    {
                        var doc2 = (Ifc4.ExternalReferenceResource.IfcDocumentInformation)doc;
                        string docdescription = doc2.Description.ToString();
                        string docpath = doc2.Location.ToString();
                        files.Add(docpath);
                    }
                    if (doc is Ifc4.ExternalReferenceResource.IfcDocumentReference)
                    {
                        var doc2 = (Ifc4.ExternalReferenceResource.IfcDocumentReference)doc;
                        string docdescription = doc2.Description.ToString();
                        string docpath = doc2.ReferencedDocument.Location.ToString();
                        files.Add(docpath);
                    }

                }
                return files;

            }
            else if (model.SchemaVersion == Xbim.Common.Step21.XbimSchemaVersion.Ifc2X3)
            {
                //Search for IfcRelAssociatesDocuments
                List<string> files = new List<string>();
                List<Ifc2x3.Interfaces.IIfcRelAssociatesDocument> relassociations = selectedElement.HasAssociations.OfType<Ifc2x3.Interfaces.IIfcRelAssociatesDocument>().ToList();
                foreach (var item in relassociations)
                {
                    var doc = (Xbim.Ifc2x3.ExternalReferenceResource.IfcDocumentReference)item.RelatingDocument;
                   // string docdescription = "\n Json-File: ";
                    string docpath = doc.Location.ToString();
                    files.Add(docpath);
                }
                return files;
            }
            else { return null; }
            
            
        }
            

    }
}
