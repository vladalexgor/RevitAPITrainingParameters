﻿using Autodesk.Revit.Attributes;
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

            if (selectedElement is Wall)
            {
                Parameter lengthParameter1 = selectedElement.LookupParameter("Длина");
                if(lengthParameter1.StorageType == StorageType.Double)
                {
                    TaskDialog.Show("Длина1", lengthParameter1.AsDouble().ToString());
                }

                Parameter lengthParameter2 = selectedElement.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH);
                if (lengthParameter2.StorageType == StorageType.Double)
                {
                    TaskDialog.Show("Длина2", lengthParameter2.AsDouble().ToString());
                }
            }

            return Result.Succeeded;
        }
    }
}
