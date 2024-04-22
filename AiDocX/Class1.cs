//var addressElement = doc.Descendants("АдресРФ").FirstOrDefault() ??
//                            doc.Descendants("СвАдрЮЛФИАС").FirstOrDefault();

//if (addressElement != null)
//{
//    var index = addressElement.Attribute("Индекс")?.Value ?? "Индекс не найден";
//    var regionCode = addressElement.Attribute("КодРегион")?.Value ?? "Код региона не найден";
//    var house = addressElement.Attribute("Дом")?.Value ?? "Дом не найден";
//    var building = addressElement.Attribute("Корпус")?.Value ?? "Корпус не найден";
//    var regionName = addressElement.Element("Регион")?.Element("НаимРегион")?.Value ?? "Наименование региона не найдено";
//    var streetType = addressElement.Element("Улица")?.Attribute("ТипУлица")?.Value ?? "";
//    var streetName = addressElement.Element("Улица")?.Attribute("НаимУлица")?.Value ?? "Улица не найдена";

//    Если код региона равен '77', то это Москва
//    if (regionCode == "77")
//    {
//        regionName = "Г.МОСКВА";
//    }

//    Сборка строки адреса
//   var addressParts = new List<string> { index, regionName, $"{streetType} {streetName}", house, building }
//       .Where(part => !string.IsNullOrWhiteSpace(part))
//       .ToArray();

//    var fullAddress = string.Join(", ", addressParts);
//    Console.WriteLine("Адрес: " + fullAddress);
//}
//else
//{
//    Console.WriteLine("Элемент адреса не найден в XML.");
//}


using System;
using System.Net;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;


    //    var elementsToFind = new List<string>
    //    {
    //        "Индекс", "КодРегион", "КодАдрКладр", "Дом", "Корпус", "Кварт", "Регион",
    //        "Район", "Город", "НаселПункт", "Улица", "Района", "НаимРегион", "МуниципРайон",
    //        "ГородСелПоселен", "НаселенПункт", "ЭлПланСтруктур", "ЭлУлДорСети", "Здание",
    //        "ПомещЗдания", "ПомещКвартиры"
    //    };

    //    ParseAndPrintElements(doc.Root, elementsToFind);
    //}

    //static void ParseAndPrintElements(XElement element, List<string> elementsToFind)
    //{
    //    foreach (var elem in element.Elements())
    //    {
    //        if (elementsToFind.Contains(elem.Name.LocalName))
    //        {
    //            Console.WriteLine($"{elem.Name.LocalName}: {elem.Value}");
    //            foreach (var attr in elem.Attributes())
    //            {
    //                Console.WriteLine($"{attr.Name.LocalName}: {attr.Value}");
    //            }
    //        }
    //        ParseAndPrintElements(elem, elementsToFind);
    //    }
    //}
