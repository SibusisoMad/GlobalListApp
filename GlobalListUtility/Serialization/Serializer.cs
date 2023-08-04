using Serilog;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace GlobalListUtility.Serialization
{
    public class Serializer
    {
        private static readonly ILogger Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("GlobalListLog.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
        public static void SerializeToBinary(List<int> list, string filePath)
        {

            try
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

                Logger.Information("Binary serialization completed successfully. File path: {FilePath}", filePath);

            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error occured during binary serialization");
            }
            
        }

        public static void SerializeToXml<T>(T data, string filePath)
        {

            try
            {

                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (TextWriter writer = new StreamWriter(filePath))
                {
                    serializer.Serialize(writer, data);
                }

                Logger.Information("Serialized object of type {Type} to XML: {FilePath}", typeof(T).Name, filePath);

            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error occured during XML serialization");
            }
        }
    }
}
