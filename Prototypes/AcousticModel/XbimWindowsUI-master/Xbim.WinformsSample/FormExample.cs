using HelixToolkit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Shell;
using Xbim.Common;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.Kernel;
using Xbim.Ifc4.ExternalReferenceResource;
using Xbim.ModelGeometry.Scene;
using static Xbim.WinformsSample.JsonJunctionElement;

using Xbim.Ifc2x3.Kernel;
using Xbim.Ifc2x3.ExternalReferenceResource;
using Xbim.Ifc2x3.Interfaces;
using System.Windows.Documents;
using Newtonsoft.Json.Linq;

namespace Xbim.WinformsSample
{
    public partial class FormExample : Form
    {
        private WinformsAccessibleControl _wpfControl;

        int starting = -1;

        protected ILogger Logger { get; private set; }

        private string ifcfilename { get; set; }
        private string jsonfilename { get; set; }
        private string pathname { get; set; }

        public FormExample(ILogger logger = null)
        {
            InitializeComponent();
            Logger = logger ?? XbimLogging.CreateLogger<FormExample>();
            //IfcStore.ModelProviderFactory.UseHeuristicModelProvider();
            _wpfControl = new WinformsAccessibleControl();
            _wpfControl.SelectionChanged += _wpfControl_SelectionChanged;
           
            controlHost.Child = _wpfControl;
        }

        private void _wpfControl_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var ent = e.AddedItems[0] as IPersistEntity;
            if (ent == null)
                txtEntityLabel.Text = "";
            else
                txtEntityLabel.Text = ent.EntityLabel.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "IFC Files|*.ifc;*.ifczip;*.ifcxml|Xbim Files|*.xbim";
            dlg.FileOk += (s, args) =>
            {
                ifcfilename = dlg.FileName.ToString();
                LoadXbimFile(dlg.FileName);
            };
            dlg.ShowDialog(this);
        }

        private void LoadXbimFile(string dlgFileName)
        {
            // TODO: should do the load on a worker thread so as not to lock the UI. 
            // See how we use BackgroundWorker in XbimXplorer

            Clear();

            var model = IfcStore.Open(dlgFileName);
            if (model.GeometryStore.IsEmpty)
            {
                // Create the geometry using the XBIM GeometryEngine
                try
                {
                    var context = new Xbim3DModelContext(model);

                    context.CreateContext();

                    // TODO: SaveAs(xbimFile); // so we don't re-process every time
                }
                catch (Exception geomEx)
                {
                    Logger.LogError(0, geomEx, "Failed to create geometry for {filename}", dlgFileName );
                }
            }
            _wpfControl.ModelProvider.ObjectInstance = model;
        }

        public void Clear()
        {
            var currentIfcStore = _wpfControl.ModelProvider.ObjectInstance as IfcStore;
            currentIfcStore?.Dispose();
            _wpfControl.ModelProvider.ObjectInstance = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var mod = _wpfControl.ModelProvider.ObjectInstance as IfcStore;
            if (mod == null)
                return;
            var found = mod.Instances.OfType<Ifc4.Interfaces.IIfcProduct>().FirstOrDefault(x => x.EntityLabel > starting);
            _wpfControl.SelectedElement = found;
            if (found != null)
                starting = found.EntityLabel;
            else
                starting = -1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _wpfControl.SelectionBehaviour = Presentation.DrawingControl3D.SelectionBehaviours.MultipleSelection;

            var mod = _wpfControl.ModelProvider.ObjectInstance as IfcStore;
            if (mod == null)
                return;
            var found = mod.Instances.OfType<Ifc4.Interfaces.IIfcWallStandardCase>();
            
            var sel = new Presentation.EntitySelection(false);
            sel.AddRange(found);
            _wpfControl.Selection = sel;
        }

		private void buttonSelectElement_Click(object sender, EventArgs e)
		{
            _wpfControl.SelectionBehaviour = Presentation.DrawingControl3D.SelectionBehaviours.SingleSelection;
            var model = _wpfControl.ModelProvider.ObjectInstance as IfcStore;
            var sel = new Presentation.EntitySelection(false);

            string elementID = textBoxSelectElement.Text;
            var selectedElement = model.Instances.FirstOrDefault<Ifc4.Interfaces.IIfcElement>(d => d.GlobalId == elementID);
            sel.Add(selectedElement);
            _wpfControl.Selection = (Presentation.EntitySelection)sel;

		}

		private void buttonGetGUID_Click(object sender, EventArgs e)
		{
            _wpfControl.SelectionBehaviour = Presentation.DrawingControl3D.SelectionBehaviours.SingleSelection;
            var model = _wpfControl.ModelProvider.ObjectInstance as IfcStore;
            var sel = new Presentation.EntitySelection(false);
            sel = _wpfControl.Selection;

            var element = (Xbim.Ifc4.Interfaces.IIfcElement)sel.FirstOrDefault();
            if (element == null) return;
            textboxGetGUID.Text = element.GlobalId.ToString();

            string ausgabe = "In IFC:\n";
            //Get connections from IFC model
            GetModellInformation auslesen = new GetModellInformation();
            List<Xbim.Ifc4.Interfaces.IIfcElement> ConnectedElements = auslesen.GetConnections(element.GlobalId, model);
            if (ConnectedElements == null)
            {
                textBoxConnections.Text = "Kein IfcRelConnect";
            }
            else
            {
                
                foreach (var el in ConnectedElements)
                {
                    ausgabe = ausgabe + "\n " + el.GlobalId.ToString();
                }
            }
            
            //Get Related Documents from IFC model
            List<string> jsonfiles = auslesen.GetReferencedFiles(element.GlobalId.ToString(), model);
            if (jsonfiles == null)
            {
                textBoxConnections.Text = "Keine JSON hinterlegt.";
            }
            else
            {
                foreach (var item in jsonfiles)
                {
                    ausgabe = ausgabe + item + ", \n";
                }
                textBoxConnections.Text = ausgabe;
            }
            

           


        }

		private void buttonJsonInformation_Click(object sender, EventArgs e) //search in external file VBAcoustic Results
		{
            //Get Junctions from corresponding json file
            string jsonfilename = "";
            var dlg = new OpenFileDialog();
            dlg.Filter = "JSON Files|*.json";
            dlg.FileOk += (s, args) =>
            {
                jsonfilename = dlg.FileName.ToString();

            };
            dlg.ShowDialog(this);

            if (jsonfilename == null) return;
            
            //string jsonfilename = @"C:\Users\chca426\Documents\GitHub\Dissertation\Modelle\Referenzmodelle\MiniReferenzmodell\Referenzmodell_4RaumModel_junctions.json";
            StreamReader myfile = new StreamReader(jsonfilename);
            string jsonString = myfile.ReadToEnd();

            string guidelement = textboxGetGUID.Text;

            //Chek which type of json file : input data or calculation results
            JObject jobject = JObject.Parse(jsonString);
            JToken inputdata = jobject.SelectToken("Name");
            JToken insituresults = jobject.SelectToken("InSituResults");

            if (insituresults != null)
            {
                JsonJunctionElementCalculationResults convertedJsonJunction = JsonConvert.DeserializeObject<JsonJunctionElementCalculationResults>(jsonString);
                GetJsonInformation neueAusgabe = new GetJsonInformation();
                textBoxjJsonInfo.Text = neueAusgabe.AusgabeCalculationResults(convertedJsonJunction);
            }
            if (inputdata != null)
            {
                JsonJunctionElementInputData convertedJsonJunction = JsonConvert.DeserializeObject<JsonJunctionElementInputData>(jsonString);
                GetJsonInformation neueAusgabe = new GetJsonInformation();
                textBoxjJsonInfo.Text = neueAusgabe.AusgabeInpuData(convertedJsonJunction);

            }


        }



		private void buttonAddJsonfile_Click(object sender, EventArgs e) //add jsonfile to element
		{
            var guidelement = textboxGetGUID.Text;
            
            //Get Junctions from corresponding json file
            string jsonpathfileName = "";
            var dlg = new OpenFileDialog();
            dlg.Filter = "JSON Files|*.json";
            dlg.FileOk += (s, args) =>
            {
                jsonpathfileName = dlg.FileName.ToString();

            };
            dlg.ShowDialog(this);

            //string pathname = Path.GetDirectoryName(ifcfilename);
            //string jsonpathfileName = @"C:\Users\chca426\Documents\GitHub\Dissertation\Modelle\Referenzmodelle\MiniReferenzmodell\Referenzmodell_4RaumModel_junctionResults.json";
            using (IfcStore model = _wpfControl.ModelProvider.ObjectInstance as IfcStore)
            {
                using (var txn = model.BeginTransaction("Add IfcRelAssociatesDocument"))
                {

                    //Check IFC Version before associating document
                    if (model.SchemaVersion == Xbim.Common.Step21.XbimSchemaVersion.Ifc4)
                    {
						Ifc4.Kernel.IfcRelAssociatesDocument relAssociates = model.Instances.New<Ifc4.Kernel.IfcRelAssociatesDocument>();
						Ifc4.ExternalReferenceResource.IfcDocumentInformation documentinfo = model.Instances.New<Ifc4.ExternalReferenceResource.IfcDocumentInformation>(r => r.Location = jsonpathfileName);
                        documentinfo.Name = "_JunctionResults";
                        documentinfo.Description = "JunctionResults for VBAcoustic";
                        relAssociates.RelatedObjects.Add(model.Instances.FirstOrDefault<Xbim.Ifc4.Interfaces.IIfcElement>(d => d.GlobalId == guidelement));
                        relAssociates.RelatingDocument = documentinfo;
                    }
                    if (model.SchemaVersion == Xbim.Common.Step21.XbimSchemaVersion.Ifc2X3)
                    {
                        Ifc2x3.Kernel.IfcRelAssociatesDocument relAssociates = model.Instances.New<Ifc2x3.Kernel.IfcRelAssociatesDocument>();
                        Ifc2x3.ExternalReferenceResource.IfcDocumentReference documentinfo = model.Instances.New<Ifc2x3.ExternalReferenceResource.IfcDocumentReference>(r => r.Location = jsonpathfileName);
                        documentinfo.Name = "_JunctionResults";
                        //documentinfo.Description = "JunctionResults for VBAcoustic";
                        relAssociates.RelatedObjects.Add((Xbim.Ifc2x3.Kernel.IfcRoot)model.Instances.FirstOrDefault<Xbim.Ifc2x3.Interfaces.IIfcElement>(d => d.GlobalId == guidelement));
                        relAssociates.RelatingDocument = documentinfo;
                    }


                    txn.Commit();
                }
                ////save IFC file
                //string filename3 = Path.GetFileNameWithoutExtension(ifcfilename) + "_mitAnhangJunctionResults.ifc";
                //string PathAndFile = pathname + "\\" + filename3;
                //model.SaveAs(PathAndFile);

                //save IFC file
                string pathname = Path.GetDirectoryName(ifcfilename);

                DateTime now = DateTime.Now;
                string strnow = now.Year.ToString() + now.Month.ToString() + now.Day.ToString() + now.Hour.ToString() + now.Minute.ToString();
                string filename3 = strnow + "_" + Path.GetFileNameWithoutExtension(ifcfilename) + "_mitAnhangJunctionResults.ifc";
                string PathAndFile = pathname + "\\" + filename3;
                model.SaveAs(PathAndFile);
            }

            labelJsonfileAdded.Text = "json file added to element GUID: " + guidelement;

            

        }

		private void buttonSaveIFC_Click(object sender, EventArgs e) //OPEN input Data for VBAcoustic from internal JSON-File to selected Element
		{
            string elementID = textboxGetGUID.Text;
            string ausgabe = "";
            string jsonpath = "";

            using (IfcStore model = _wpfControl.ModelProvider.ObjectInstance as IfcStore)
            {
                GetModellInformation referencetofile = new GetModellInformation();

               List<string> files = referencetofile.GetReferencedFiles(elementID, model);

                if (files != null)
                {
                    //Check JSON-file
                    foreach (var file in files)
                    {
                        jsonpath = file;
                        if (jsonpath == null || jsonpath == "") break;

                        if (jsonpath == null || jsonpath == "") return;
                        StreamReader myfile = new StreamReader(jsonpath);
                        string jsonString = myfile.ReadToEnd();

                        //Chek which type of json file : input data or calculation results
                        JObject jobject = JObject.Parse(jsonString);
                        JToken inputdata = jobject.SelectToken("Name");
                        JToken insituresults = jobject.SelectToken("InSituResults");

                        if (insituresults != null)
                        {
                            JsonJunctionElementCalculationResults convertedJsonJunction = JsonConvert.DeserializeObject<JsonJunctionElementCalculationResults>(jsonString);
                            GetJsonInformation neueAusgabe = new GetJsonInformation();
                            ausgabe = ausgabe + "\r\n" + "Input data from IFC-File:" + neueAusgabe.AusgabeCalculationResults(convertedJsonJunction);
                            neueAusgabe.AusgabeCalculationResults(convertedJsonJunction, DGV01);
                        }
                        if (inputdata != null)
                        {
                            JsonJunctionElementInputData convertedJsonJunction = JsonConvert.DeserializeObject<JsonJunctionElementInputData>(jsonString);
                            GetJsonInformation neueAusgabe = new GetJsonInformation();
                            ausgabe = ausgabe + "\r\n" + "Calculation results from VBAcoustic" + neueAusgabe.AusgabeInpuData(convertedJsonJunction);

                        }

                        //write Input in textbox
                        ausgabe = jsonpath + "\r\n" + ausgabe + "\r\n";


                    }
                }
                

                //write Input in textbox
                textBoxjJsonInfo.Text =  ausgabe;
            }
        }

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}
	}
}
