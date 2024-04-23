using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml.Linq;

class Program
{
    static string url = "https://egrul.itsoft.ru/7736050003.xml";

    static void Main()
    {
        string xmlContent = new WebClient().DownloadString(url);
        XDocument doc = XDocument.Parse(xmlContent);

        // Извлечение и вывод информации о компании
        // PrintCompanyInfo(doc);

        // Извлечение и вывод адресной информации
        PrintAddressInfo(doc);
    }

    static void PrintAddressInfo(XDocument doc)
    {
        var addressElements = doc.Descendants("СвАдресЮЛ")
                              .Union(doc.Descendants("СвАдрЮЛФИАС"))
                              .Union(doc.Descendants("АдресРФ"));

        foreach (var element in addressElements)
        {
            PrintElementAndChildren(element, 0);
        }
    }

    static void PrintElementAndChildren(XElement element, int indentLevel)
    {
        PrintElement(element, indentLevel);
        foreach (var childElement in element.Elements())
        {
            PrintElementAndChildren(childElement, indentLevel + 1);
        }
    }

    static void PrintElement(XElement element, int indentLevel)
    {
        var indent = new string(' ', indentLevel * 2);
        Console.WriteLine($"{indent}{element.Name.LocalName}:");
        foreach (var attr in element.Attributes())
        {
            Console.WriteLine($"{indent}  {attr.Name.LocalName}: {attr.Value}");
        }
        if (!string.IsNullOrEmpty(element.Value))
        {
            Console.WriteLine($"{indent}  Value: {element.Value}");
        }
    }

    // ... оставшаяся часть вашего кода ...
}
