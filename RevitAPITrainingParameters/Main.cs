using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITrainingParameters
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            var selectedRef = uidoc.Selection.PickObject(ObjectType.Element, "Выберите элемент");
            var selectedElement = doc.GetElement(selectedRef);
            if(selectedElement is FamilyInstance)
            {
                using (Transaction ts = new Transaction(doc, "Set parameters"))
                {
                    ts.Start();
                    var familyInstance = selectedElement as FamilyInstance;
                    Parameter commentParameter = familyInstance.LookupParameter("Комментарии");
                    commentParameter.Set("TestComment");

                    Parameter typeCommentsParameter = familyInstance.Symbol.LookupParameter("Комментарии к типоразмеру");
                    typeCommentsParameter.Set("TestTypeComments");
                    ts.Commit();
                }
            }

            return Result.Succeeded;
        }
    }
}
