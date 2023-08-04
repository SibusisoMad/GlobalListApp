using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace GlobalListUtility.Serialization
{
    public static class Serializer
    {
        public static void SerializeToBinary(List<int> list, string fileName)
        {
            using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
            {
                using (BinaryWriter writer = new BinaryWriter(fileStream))
                {
                    foreach (int number in list)
                    {
                        writer.Write(number);
                    }
                }
            }
        }

        public static void SerializeToXml(List<int> list, string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<int>));

                serializer.Serialize(writer, list);
            }
        }
    }
}
