using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.MaterialResource;

namespace Prototype01
{
    public partial class frmAllLayer : Form
    {
        public CheckLayers _layerElement { get; set; }
        public double _dinterior { get; set; }
        public double _dcore { get; set; }
        public double _dexterior { get; set; }

        private List<IIfcMaterialLayer> ListofMaterialLayers { get; set; } //MaterialLayer merken

        private IIfcMaterialLayerSetUsage _allMaterialLayers { get; set; } //MaterialLayer merken

        //private MyElement _element { get; set; } //Not Needed ? Layer Thickness independ from real coordinatepoints

        public frmAllLayer(string ElementName, IIfcMaterialLayerSet AllMaterialLayers, double dinterior, double dcore, double dexterior)
        {
                       
            InitializeComponent();

            //this._dinterior = dinterior;
            //this._dcore = dcore;
            //this._dexterior = dexterior;

            lbl_Selectedelement.Text = ElementName;

            List<Label> MaterialLabels = MaterialLabelListe();
            List<Label> ThicknessLabels = ThicknessLabelListe();
            List<GroupBox> GroupFrames = GroupFramesListe();
            List<RadioButton> CoreButtons = CoreButtonListe();

            int i = 0;
            //Write every Layer in a Box in the form
            foreach (IIfcMaterialLayer materialLayer in AllMaterialLayers.MaterialLayers)
            {
                MaterialLabels[i].Text = materialLayer.Material.Name;
                ThicknessLabels[i].Text = materialLayer.LayerThickness.Value.ToString();
                i = i + 1;
            }
            //change ohter elements to not visible
            for (int x = i; x < 9; x++)
            {
                MaterialLabels[x].Visible = false;
                ThicknessLabels[x].Visible = false;
                GroupFrames[x].Visible = false;
            }

        }


        

        public frmAllLayer(string ElementName, MyElement element, IIfcMaterialLayerSetUsage AllMaterialLayers)
        {
            _allMaterialLayers = AllMaterialLayers;
            //_element = element;

            InitializeComponent();

            lbl_Selectedelement.Text = ElementName;

            List<Label> MaterialLabels = MaterialLabelListe();
            List<Label> ThicknessLabels = ThicknessLabelListe();
            List<GroupBox> GroupFrames = GroupFramesListe();
            List<RadioButton> CoreButtons = CoreButtonListe();

            int i = 0;
            //Write every Layer in a Box in the form
            foreach (IIfcMaterialLayer materialLayer in AllMaterialLayers.ForLayerSet.MaterialLayers)
            {
                MaterialLabels[i].Text = materialLayer.Material.Name;
                ThicknessLabels[i].Text = materialLayer.LayerThickness.Value.ToString();
                i = i + 1;
            }
            //change ohter elements to not visible
            for (int x = i; x < 9; x++)
            {
                MaterialLabels[x].Visible = false;
                ThicknessLabels[x].Visible = false;
                GroupFrames[x].Visible = false;
            }

        }


        private List<Label> MaterialLabelListe()
        {
            List<Label> lablist = new List<Label>();
            lablist.Add(lbl_Material1);
            lablist.Add(lbl_Material2);
            lablist.Add(lbl_Material3);
            lablist.Add(lbl_Material4);
            lablist.Add(lbl_Material5);
            lablist.Add(lbl_Material6);
            lablist.Add(lbl_Material7);
            lablist.Add(lbl_Material8);
            lablist.Add(lbl_Material9);
            
            return lablist;
        }

        private List<Label> ThicknessLabelListe()
        {
            List<Label> lablist = new List<Label>();
            lablist.Add(lbl_Thickness1);
            lablist.Add(lbl_Thickness2);
            lablist.Add(lbl_Thickness3);
            lablist.Add(lbl_Thickness4);
            lablist.Add(lbl_Thickness5);
            lablist.Add(lbl_Thickness6);
            lablist.Add(lbl_Thickness7);
            lablist.Add(lbl_Thickness8);
            lablist.Add(lbl_Thickness9);
            
            return lablist;
        }

        private List<RadioButton> CoreButtonListe()
        {
            List<RadioButton> buttonlist = new List<RadioButton>();
            buttonlist.Add(rb_Kern1);
            buttonlist.Add(rb_Kern2);
            buttonlist.Add(rb_Kern3);
            buttonlist.Add(rb_Kern4);
            buttonlist.Add(rb_Kern5);
            buttonlist.Add(rb_Kern6);
            buttonlist.Add(rb_Kern7);
            buttonlist.Add(rb_Kern8);
            buttonlist.Add(rb_Kern9);
            
            return buttonlist;
        }

        private List<GroupBox> GroupFramesListe()
        {
            List<GroupBox> grouplist = new List<GroupBox>();
            grouplist.Add(groupBox1);
            grouplist.Add(groupBox2);
            grouplist.Add(groupBox3);
            grouplist.Add(groupBox4);
            grouplist.Add(groupBox5);
            grouplist.Add(groupBox6);
            grouplist.Add(groupBox7);
            grouplist.Add(groupBox8);
            grouplist.Add(groupBox9);

            return grouplist;
        }



        private void cmd_OK_Click(object sender, EventArgs e)
        {
            //total thickness of layers define the position of corelayer:
            CoreLayerDefinition def = new CoreLayerDefinition();

            //build wall with Layers for acoustic, Set Thickness = 0
            double dblExteriorThickness = 0;
            double dblKernThickness = 0;
            double dblInteriorThickness = 0;
            double dsum = 0;
            
            //look at every group and button
            foreach (Control groupboxes in ActiveForm.Controls.OfType<GroupBox>().OrderBy(x => x.TabIndex)) //OrderBy: make sure you start from the top
            {
                double thickness = 0;
                string materialname = "";

                if (groupboxes.Visible == true)
                {
                    foreach (Label label in groupboxes.Controls.OfType<Label>())
                    {
                        string labelName = label.Name.Substring(0, 12);
                        if (labelName == "lbl_Thicknes")
                        {
                            thickness = Math.Round(Convert.ToDouble(label.Text),2);

                        }

                        if (labelName == "lbl_Material") materialname = Convert.ToString(label.Text);

                    }

                    foreach (RadioButton button in groupboxes.Controls.OfType<RadioButton>())
                    {
                        if (button.Checked == true)
                        {
                            string buttonname = button.Name.Substring(0, 6);
                            if (buttonname == "rb_Ext")
                            {
                                dblExteriorThickness += thickness;
                            }
                            else if (buttonname == "rb_Ker")
                            {
                                
                                dblKernThickness += thickness;
                                //this mateiral should be CoreLayer in the other elements too?

                            }
                            else if (buttonname == "rb_Int")
                            {
                                dblInteriorThickness += thickness;
                            }
                        }

                    }
                }
                

            }

            //All Layers added -> rb_ext is near the baseline, because of MaterialLayerSetUsage Definition
            dsum = dblExteriorThickness + dblKernThickness;
            dblExteriorThickness = Math.Round(dblExteriorThickness, 2);
            dsum = Math.Round(dsum, 2);
            //def.CalculateCoreMinMax(_allMaterialLayers, _element, dblExteriorThickness, dsum);

            this._dinterior = dblInteriorThickness;
            this._dexterior = dblExteriorThickness;
            this._dcore = dblKernThickness;


            this.Close();
        }
    }
}
