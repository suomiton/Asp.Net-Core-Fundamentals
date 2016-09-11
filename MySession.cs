using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace HelloWorld
{
    public class MySession
    {
        public int Count { get; set; }
        public string ToString()
        {
            var json = new DataContractJsonSerializer(this.GetType());
            using (var ms = new MemoryStream())
            {
                json.WriteObject(ms, this);
                ms.Position = 0;
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        public static MySession Parse(string jsonTxt)
        {
            var json = new DataContractJsonSerializer(typeof(MySession));
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonTxt)))
            {
                return (MySession)json.ReadObject(ms);
            }
        }
    }
}