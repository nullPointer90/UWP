
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Windows.Foundation;
using Windows.Storage;

namespace Crossword.Common
{
    public class Score
    {
        public string NameClassic { get; set; }
        public int ScoreClassic { get; set; }
        public string NameIntelligen { get; set; }
        public int ScoreIntelligen { get; set; }
    }
    class HighScoreMng
    {
        public HighScoreMng()
        {
           
        }

        public static async void CreateFileName(string filename)
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            IStorageItem file = await folder.TryGetItemAsync(filename);
            if(file == null)
            {
                Score score = new Score()
                {
                    NameClassic = "nullPointer",
                    NameIntelligen = "nullPointer"
                };
                await SaveObjectToXml(score, filename);
            }
        }


        public static async Task<T> ReadObjectFromXmlFileAsync<T>(string filename)
        {
            // this reads XML content from a file ("filename") and returns an object  from the XML
            T objectFromXml = default(T);
            var serializer = new XmlSerializer(typeof(T));
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.GetFileAsync(filename);
            using(Stream stream = await file.OpenStreamForReadAsync())
            {
                objectFromXml = (T)serializer.Deserialize(stream);
            }
            return objectFromXml;
        }

        public static async Task SaveObjectToXml<T>(T objectToSave, string filename)
        {
            // stores an object in XML format in file called 'filename'
            var serializer = new XmlSerializer(typeof(T));
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            Stream stream = await file.OpenStreamForWriteAsync();

            using(stream)
            {
                serializer.Serialize(stream, objectToSave);
            }
        }
    }
}
