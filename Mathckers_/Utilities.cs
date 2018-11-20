using Mathckers_.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace Mathckers_.Utilities
{
    public static class Utilities
    {
        //public const string MATHLY = "https://math.ly/api/v1/algebra/linear-equations.json?difficulty=";
        public const string MATHLY = "https://math.ly/api/v1/algebra/linear-equations.json";
        //public static T _GetJSONData<T>(string url) where T : new()
        //{
        //    using (var w = new WebClient())
        //    {
        //        var json_data = string.Empty;
        //        // attempt to download JSON data as a string
        //        try
        //        {
        //            json_data = w.DownloadString(url);
        //        }
        //        catch (Exception) { }
        //        // if string with JSON data is not empty, deserialize it to class and return its instance 
        //        return !string.IsNullOrEmpty(json_data) ? JsonConvert.DeserializeObject<T>(json_data) : new T();
        //    }
        //}

        public static ProblemModel _GetJSONData(string url)
        {
            using (var client = new WebClient())
            {
                ProblemModel model = new ProblemModel();

                var data = client.DownloadString(url);
                //var data = "{\"id\":\"pgnrbgbnpgjfc\",\"question\":\"<mfrac><mrow><mrow><mo> - </mo><mn>3</mn></mrow><mi>x</mi><mo> + </mo><mn>1</mn></mrow><mn>5</mn></mfrac><mo> + </mo><mfrac><mrow><mi>x</mi><mo> - </mo><mn>1</mn></mrow><mn>5</mn></mfrac><mo> = </mo><mfrac><mrow><mn>7</mn><mi>x</mi><mo> + </mo><mn>4</mn></mrow><mn>2</mn></mfrac>\",\"choices\":[\"<math><mo>-</mo><mfrac><mn>20</mn><mn>39</mn></mfrac></math>\",\"<math><mn>5</mn><mfrac><mn>2</mn><mn>19</mn></mfrac></math>\",\"<math><mo>-</mo><mn>2</mn><mfrac><mn>8</mn><mn>25</mn></mfrac></math>\",\"<math><mo>-</mo><mfrac><mn>69</mn><mn>416</mn></mfrac></math>\",\"<math><mn>2</mn><mfrac><mn>15</mn><mn>16</mn></mfrac></math>\"],\"correct_choice\":0,\"instruction\":\"Solve for x\",\"category\":\"Algebra\",\"topic\":\"Linear Equations\",\"difficulty\":\"Advanced\"}";
                try
                {
                    //var converted = JsonConvert.DeserializeObject<object>(data);
                    model = JsonConvert.DeserializeObject<ProblemModel>(data);                   
                }
                catch (Exception ex)
                {
                    
                }
                return model;
            }
        }

        public static ProblemModel _GetData(string url)
        {
            
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ProblemModel));
            WebClient syncClient = new WebClient();
            string content = syncClient.DownloadString(url);

            using (MemoryStream memo = new MemoryStream(Encoding.Unicode.GetBytes(content)))
            {
                ProblemModel problem = (ProblemModel)serializer.ReadObject(memo);

                return problem;
            }
        }

    }
}