using System;
using System.Net;
using System.Xml.Linq;
using System.Linq;

//неккоректно улицу обрабатывает в <АдресРФ

class Program
{
    static string url = "https://egrul.itsoft.ru/7730588444.xml";

    static void Main()
    {
        string xmlContent = new WebClient().DownloadString(url);
        XDocument doc = XDocument.Parse(xmlContent);

        // Извлечение и вывод информации о компании
        PrintCompanyInfo(doc);

        // Извлечение и вывод адресной информации
        PrintAddressInfo(doc);
    }

    static void PrintAddressInfo(XDocument doc)
    {
        var addressElements = doc.Descendants("СвАдресЮЛ")
                                 .Union(doc.Descendants("СвАдрЮЛФИАС"))
                                 .Union(doc.Descendants("АдресРФ"));

        foreach (var addressElement in addressElements)
        {
            var descendantsAndSelf = addressElement.DescendantsAndSelf().ToList();
            var data = new Dictionary<string, string>();

            // Добавление информации в словарь, если она не равна null
            data.TryAdd("Индекс", descendantsAndSelf.Attributes("Индекс").FirstOrDefault()?.Value);
            data.TryAdd("Код региона", descendantsAndSelf.Attributes("КодРегион").FirstOrDefault()?.Value);
            data.TryAdd("Код адреса по КЛАДР", descendantsAndSelf.Attributes("КодАдрКладр").FirstOrDefault()?.Value);
            data.TryAdd("Дом", descendantsAndSelf.Attributes("Дом").FirstOrDefault()?.Value);
            data.TryAdd("Корпус", descendantsAndSelf.Attributes("Корпус").FirstOrDefault()?.Value);
            data.TryAdd("Квартира", descendantsAndSelf.Attributes("Кварт").FirstOrDefault()?.Value);
            var regionName = descendantsAndSelf.Elements("НаимРегион").FirstOrDefault()?.Value;
            if (string.IsNullOrEmpty(regionName))
            {
                regionName = descendantsAndSelf.Elements("Регион").Select(e => e.Attribute("НаимРегион")?.Value ?? e.Value).FirstOrDefault();
            }
            data.TryAdd("Регион", regionName);
            data.TryAdd("Район", descendantsAndSelf.Elements("МуниципРайон").Select(e => e.Attribute("Наим")?.Value).FirstOrDefault());
            data.TryAdd("Город", descendantsAndSelf.Elements("Город").Select(e => e.Attribute("НаимГород")?.Value).FirstOrDefault());
            data.TryAdd("Населенный пункт", descendantsAndSelf.Elements("НаселПункт").Select(e => e.Attribute("Наим")?.Value).FirstOrDefault());
            data.TryAdd("Улица", descendantsAndSelf.Elements("ЭлУлДорСети").Select(e => e.Attribute("Наим")?.Value).FirstOrDefault());
            data.TryAdd("Дата ГРН", descendantsAndSelf.Attributes("ГРНДата").FirstOrDefault()?.Value);
            data.TryAdd("Дата исправления ГРН", descendantsAndSelf.Attributes("ГРНДатаИспр").FirstOrDefault()?.Value);
            data.TryAdd("Идентификационный номер", descendantsAndSelf.Attributes("ИдНом").FirstOrDefault()?.Value);
            data.TryAdd("Городское/сельское поселение", descendantsAndSelf.Attributes("ГородСелПоселен").FirstOrDefault()?.Value);
            data.TryAdd("Населенный пункт", descendantsAndSelf.Attributes("НаселенПункт").FirstOrDefault()?.Value);
            data.TryAdd("Элемент планировочной структуры", descendantsAndSelf.Attributes("ЭлПланСтруктур").FirstOrDefault()?.Value);
            data.TryAdd("Элемент улично-дорожной сети", descendantsAndSelf.Attributes("ЭлУлДорСети").FirstOrDefault()?.Value);
            var buildings = descendantsAndSelf.Elements("Здание").Select(e => $"Тип: {e.Attribute("Тип")?.Value}, Номер: {e.Attribute("Номер")?.Value}").ToList();
            data.TryAdd("Помещение здания", descendantsAndSelf.Elements("ПомещЗдания").Select(e => e.Attribute("Номер")?.Value).FirstOrDefault());
            data.TryAdd("Помещение квартиры", descendantsAndSelf.Elements("ПомещКвартиры").Select(e => e.Attribute("Номер")?.Value).FirstOrDefault());

            // Вывод словаря в консоль
            foreach (var pair in data)
            {
                if (!string.IsNullOrEmpty(pair.Value))
                {
                    Console.WriteLine($"{pair.Key}: {pair.Value}");
                }
            }

            // Вывод всех зданий
            foreach (var building in buildings)
            {
                Console.WriteLine($"Здание: {building}");
            }
        }







    }

    static void PrintCompanyInfo(XDocument doc)
    {
        var shortNameElement = doc.Descendants("СвНаимЮЛСокр").FirstOrDefault();
        string shortName = shortNameElement != null ? shortNameElement.Attribute("НаимСокр")?.Value : "Краткое название не найдено";

        var fullNameElement = doc.Descendants("СвНаимЮЛ").FirstOrDefault();
        string fullName = fullNameElement != null ? fullNameElement.Attribute("НаимЮЛПолн")?.Value : "Полное наименование не найдено";

        var svYulElement = doc.Descendants("СвЮЛ").FirstOrDefault();
        string inn = svYulElement?.Attribute("ИНН")?.Value ?? "ИНН не найден";
        string ogrn = svYulElement?.Attribute("ОГРН")?.Value ?? "ОГРН не найден";
        string kpp = svYulElement?.Attribute("КПП")?.Value ?? "КПП не найден";

        var directorInfoElement = doc.Descendants("СвФЛ").FirstOrDefault();
        string directorFullName = directorInfoElement != null
            ? $"{directorInfoElement.Attribute("Фамилия")?.Value} {directorInfoElement.Attribute("Имя")?.Value} {directorInfoElement.Attribute("Отчество")?.Value}"
            : "ФИО руководителя не найдено";

        var directorPositionElement = doc.Descendants("СвДолжн").FirstOrDefault();
        string directorPosition = directorPositionElement != null
            ? directorPositionElement.Attribute("НаимДолжн")?.Value
            : "Должность руководителя не найдена";

        Console.WriteLine("ФИО руководителя: " + directorFullName);
        Console.WriteLine("Должность руководителя: " + directorPosition);
        Console.WriteLine("ИНН: " + inn);
        Console.WriteLine("ОГРН: " + ogrn);
        Console.WriteLine("КПП: " + kpp);
        Console.WriteLine("Краткое название: " + shortName);
        Console.WriteLine("Полное наименование: " + fullName);
    }
}
