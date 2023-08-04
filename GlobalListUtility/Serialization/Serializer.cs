using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace GlobalListUtility.Serialization
{
    public class Serializer
    {
        public static void SerializeToBinary(List<int> list, string filePath)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
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

        public static void SerializeToXml<T>(T data, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (TextWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, data);
            }
        }
    }
}
