using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc4.Interfaces;

namespace Prototype01
{
    public class CheckConnection
    {

        public static List<IIfcElement> GetConnections(List<IIfcRelConnectsElements> connectionFrom, List<IIfcRelConnectsElements> connectionTo)
        {
            
            List <IIfcElement> templist = new List<IIfcElement>();
           
            foreach (var connection in connectionFrom)
            {
                IIfcElement elementwithConnection = connection.RelatingElement;
                //possible flanking element -> put in new list
                templist.Add(elementwithConnection);
            }

            foreach (var connection in connectionTo)
            {
                IIfcElement elementwithConnection = connection.RelatedElement;
                //possible flanking element -> put in new list
                templist.Add(elementwithConnection);

            }
            return templist;
        }

        public static List<IIfcElement> GetConnectionRealizingElements(IIfcElement element)
        {
            //IIfcRelConnectsWithRealizingElements
            List<IIfcElement> templist = new List<IIfcElement>();

            //List<IIfcRelConnectsElements> connectionFrom = element.ConnectedFrom.ToList().Where(s => s is IIfcRelConnectsWithRealizingElements).ToList();
            //List<IIfcRelConnectsElements> connectionTo = element.ConnectedTo.ToList().Where(s => s is IIfcRelConnectsWithRealizingElements).ToList();

            //foreach (IIfcRelConnectsWithRealizingElements item in connectionFrom)
            //{
            //    foreach (IIfcElement realizingitem in item.RealizingElements)
            //    {
            //        templist.Add(realizingitem);
            //    }
            //}
            //foreach (IIfcRelConnectsWithRealizingElements item in connectionTo)
            //{
            //    foreach (IIfcElement realizingitem in item.RealizingElements)
            //    {
            //        templist.Add(realizingitem);
            //    }
                
            //}

            var connectionFrom = element.ConnectedFrom
                            .OfType<IIfcRelConnectsWithRealizingElements>()
                            .SelectMany(rel => rel.RealizingElements);
            var connectionTo = element.ConnectedTo
                            .OfType<IIfcRelConnectsWithRealizingElements>()
                            .SelectMany(rel => rel.RealizingElements);
            foreach (var item in connectionFrom)
            {
                templist.Add(item);
            }
            

            return templist;
        }

        public static List<IIfcElement> GetConnectionRealizingElements(IIfcElement element1, IIfcElement element2)
        {
            //ConnectedFrom  = related Element in Relation
            //ConnectedTo = relating Element in Relation

            //IIfcRelConnectsWithRealizingElements
            List<IIfcElement> templist = new List<IIfcElement>();

            List<IIfcRelConnectsElements> connectionFrom = element1.ConnectedFrom.ToList().Where(s => s is IIfcRelConnectsWithRealizingElements).ToList();
            List<IIfcRelConnectsElements> connectionTo = element1.ConnectedTo.ToList().Where(s => s is IIfcRelConnectsWithRealizingElements).ToList();

            foreach (IIfcRelConnectsWithRealizingElements item in connectionFrom)
            {
                if (item.RelatedElement == element2)
                {
                    foreach (IIfcElement realizingitem in item.RealizingElements)
                    {
                        templist.Add(realizingitem);
                    }
                }

            }
            foreach (IIfcRelConnectsWithRealizingElements item in connectionTo)
            {
                if (item.RelatingElement == element2)
                {
                    foreach (IIfcElement realizingitem in item.RealizingElements)
                    {
                        templist.Add(realizingitem);
                    }
                }
   

            }

            templist = templist.Distinct().ToList();
            return templist;
        }

    }

}
