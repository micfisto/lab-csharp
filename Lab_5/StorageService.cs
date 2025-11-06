using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Lab_5.Models;

namespace Lab_5;

public class StorageService
{
    private const string FilePath = "airports.xml";

    public void SavePlaneXml(List<Airport> airports)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Airport>));

            var settings = new XmlWriterSettings
            {
                Indent = true,
                Encoding = Encoding.UTF8,
                NewLineOnAttributes = false
            };

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");

            using (var writer = XmlWriter.Create(FilePath, settings))
            {
                serializer.Serialize(writer, airports, namespaces);
            }

            Console.WriteLine($"Данные сохранены в \"{FilePath}\"");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сохранении XML: {ex.Message}");
        }
    }

    public List<Airport> LoadPlanesXml()
    {
        if (!File.Exists(FilePath))
        {
            Console.WriteLine("Файл не существует. Загружен пустой список.");
            return new List<Airport>();
        }

        XmlSerializer serializer = new XmlSerializer(typeof(List<Airport>));
        using (FileStream fileStream = new FileStream(FilePath, FileMode.Open))
        {
            try
            {
                var deserialize = serializer.Deserialize(fileStream) as List<Airport>;
                Console.WriteLine($"Данные успешно загружены из \"{FilePath}\"");
                return deserialize ?? new List<Airport>();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка при загрузке XML: {e.Message}");
                return new List<Airport>();
            }
        }
    }
}