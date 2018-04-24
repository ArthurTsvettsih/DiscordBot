using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml;
using System.Xml.Serialization;

namespace DiscordBot.Helpers
{
	class XmlHelpers
	{
		//source: https://stackoverflow.com/questions/6115721/how-to-save-restore-serializable-object-to-from-file
		public void SerializeObject<T>(T serializableObject, string filepath)
		{
			if (serializableObject == null) { return; }

			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				XmlSerializer serializer = new XmlSerializer(serializableObject.GetType());
				using (MemoryStream stream = new MemoryStream())
				{
					serializer.Serialize(stream, serializableObject);
					stream.Position = 0;
					xmlDocument.Load(stream);
					xmlDocument.Save(filepath);
					stream.Close();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public T DeSerializeObject<T>(string filepath)
		{
			if (string.IsNullOrEmpty(filepath)) { return default(T); }

			T objectOut = default(T);

			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(filepath);
				string xmlString = xmlDocument.OuterXml;

				using (StringReader read = new StringReader(xmlString))
				{
					Type outType = typeof(T);

					XmlSerializer serializer = new XmlSerializer(outType);
					using (XmlReader reader = new XmlTextReader(read))
					{
						objectOut = (T)serializer.Deserialize(reader);
						reader.Close();
					}

					read.Close();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			return objectOut;
		}
	}
}
