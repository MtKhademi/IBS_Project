using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Web;

namespace Common.Extentions
{
    public static class ObjectExtentions
    {
        public static string ToJsonString<T>(this T o)
            => JsonConvert.SerializeObject(o);


        public static string ToStringProps(this object obj)
        {
            try
            {
                string result = "";
                foreach (var prop in obj.GetType().GetProperties())
                {
                    if (string.IsNullOrEmpty(result))
                        result += $"{prop.Name} : [{prop.GetValue(obj)}]";
                    else
                        result += $" - {prop.Name} : [{prop.GetValue(obj)}]";
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"fatal error in tostringprops", ex);
            }
        }
        public static List<(string nameProperty, string valueProperty)> ToParameters(this object obj)
        {
            try
            {
                var oResult = new List<(string nameProperty, string valueProperty)>();
                foreach (var prop in obj.GetType().GetProperties())
                {
                    oResult.Add((prop.Name, prop.GetValue(obj)?.ToString() ?? null));
                }
                return oResult;
            }
            catch (Exception ex)
            {
                throw new Exception($"fatal error in tostringprops", ex);
            }
        }



        public static string ToQueryString<T>(this T obj)
        {
            string jsonString = JsonConvert.SerializeObject(obj);
            //var jsonObject = JsonConvert.DeserializeObject<JObject>(jsonString);//JObject.Parse(jsonString);
            
            
            JObject jsonObject;
            using (var reader = new JsonTextReader(new StringReader(jsonString)) { DateParseHandling = DateParseHandling.None })
                jsonObject = JObject.Load(reader);

            var properties = jsonObject
                .Properties()
                .Where(p => p.Value.Type != JTokenType.Null)
                .Select(p =>
                    {

                        switch (p.Value.Type)
                        {
                            case JTokenType.Array:
                                StringBuilder result = new();
                                for (int i = 0; i < p.Value.Count(); i++)
                                {
                                    result.Append($"{HttpUtility.UrlEncode(p.Name)}={HttpUtility.UrlEncode(p.Value[i].ToString())}" +
                                        $"{((i < p.Value.Count() - 1) ? "&" : "")}");
                                }
                                return result.ToString();
                            case JTokenType.None:
                            case JTokenType.Object:
                            case JTokenType.Constructor:
                            case JTokenType.Property:
                            case JTokenType.Comment:
                            case JTokenType.Integer:
                            case JTokenType.Float:
                            case JTokenType.String:
                            case JTokenType.Boolean:
                            case JTokenType.Null:
                            case JTokenType.Undefined:
                            case JTokenType.Date:
                            case JTokenType.Raw:
                            case JTokenType.Bytes:
                            case JTokenType.Guid:
                            case JTokenType.Uri:
                            case JTokenType.TimeSpan:
                            default:
                                return $"{HttpUtility.UrlEncode(p.Name)}={HttpUtility.UrlEncode(p.Value.ToString())}";
                        }
                    });
            return string.Join("&", properties);
        }


    }
}
