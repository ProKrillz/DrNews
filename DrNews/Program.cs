using System.Xml.Linq;
using System.Xml.Serialization;
using Xml2CSharp;

string url = "https://www.dr.dk/nyheder/service/feeds/senestenyt";
XDocument doc = XDocument.Load(url);

List<Item> newz = new();
foreach (Item item in newz.DeserializeXDocumentToList(doc))
    Console.WriteLine($"{item.Title}\nDate: {item.PubDate}\nLink: {item.Link2}\n");

static class Ex
{
    public static IEnumerable<Item> DeserializeXDocumentToList(this IEnumerable<Item> list, XDocument doc)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Item));
        IEnumerable<dynamic> elements = doc.Descendants("item");

        foreach (var element in elements)
        {
            using (var reader = element.CreateReader())
            {
                Item newz = (Item)serializer.Deserialize(reader);
                yield return newz;
            }
        }      
    }
}