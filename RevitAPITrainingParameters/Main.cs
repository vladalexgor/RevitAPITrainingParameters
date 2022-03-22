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
                var familyInstance = selectedElement as FamilyInstance;
                Parameter widthParameter1 = familyInstance.Symbol.LookupParameter("Ширина");
                TaskDialog.Show("Ширина1", widthParameter1.AsDouble().ToString());

                Parameter widthParameter2 = familyInstance.Symbol.get_Parameter(BuiltInParameter.CASEWORK_WIDTH);
                TaskDialog.Show("Ширина2", widthParameter2.AsDouble().ToString());
            }

            return Result.Succeeded;
        }
    }
}
