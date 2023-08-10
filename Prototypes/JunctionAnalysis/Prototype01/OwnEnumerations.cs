using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype01
{
    class OwnEnumerations
    {

        

    }

    public enum ElementType
    {
       IfcWall,
        IfcWallStandardCase,
        IfcWallElementedCase,
        IfcSlab,
        IfcSlabStandardCase,
        IfcSlabElementedCase,
        IfcCurtainWall,
        IfcCovering,
        NotDefined
    }

    public enum TypeOfElement
    {
        Wall,
        Slab,
        NotDefined
    }

    public enum DDirections
    {
        Xplus,
        Xminus,
        Yplus,
        Yminus,
        Zplus,
        Zminus,
        None //Null benötigt
    }

    public enum ConnectionZones
    {
        None, //to initialize all elements with "empty"
        Short,
        Middle,
        Border,
        Zero //when connections are evaluated, but no connection zone is there!
    }

    public enum JunctionType //dash = Bindestrich, colon = Doppelpunkt
    {
        Lh1d2,
        Lv1d2,
        Tv1d24,
        Tv2d13,
        Th1d24,
        Th2d13,
        Th2d1d4,
        Tv2d1d4,
        Tv1d2d3,
        Th1d2c4,
        Tv1d2c4,
        Tv2d1c3,
        Xh1d24d3,
        Xv2d13d4,
        Xv1d24d3,
        Xh2d1c3d4,
        Xv2d1c3d4,
        JunctionTypeError,
        ErrorNoCorrectDirection
    }
}
