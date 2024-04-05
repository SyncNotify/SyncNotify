using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SyncNotify
{
    internal class SettingsManager
    {

        public static void SaveSettingsToFile(Settings settings)
        {
            if (!Directory.Exists(App.RootPath))
            {
                Directory.CreateDirectory(App.RootPath);
            }
            string text = JsonConvert.SerializeObject(settings, Formatting.Indented);
            try
            {

                System.IO.File.WriteAllText(App.RootPath + Settings.settingsFileName, text);
            }
            catch { }
        }

        //public static bool GetSettingsByFile(string jsonLocation)
        //{
        //    // 打开文件并创建 StreamReader 对象
        //    StreamReader reader = new StreamReader(jsonLocation);
        //    // 读取文件内容
        //    jsonLocation = reader.ReadToEnd();
        //    JsonConvert.DeserializeObject(jsonLocation);

        //    JsonSerializerSettings jsetting = new JsonSerializerSettings();
        //    jsetting.ContractResolver = new LimitPropsContractResolver(new string[] { "Age", "IsMarry" });
        //    Console.WriteLine(JsonConvert.SerializeObject(p, Formatting.Indented, jsetting));
        //}
    }

    //public class LimitPropsContractResolver : DefaultContractResolver
    //{
    //    string[] props = null;

    //    bool retain;

    //    /// <summary>
    //    /// 构造函数
    //    /// </summary>
    //    /// <param name="props">传入的属性数组</param>
    //    /// <param name="retain">true:表示props是需要保留的字段  false：表示props是要排除的字段</param>
    //    public LimitPropsContractResolver(string[] props, bool retain = true)
    //    {
    //        //指定要序列化属性的清单
    //        this.props = props;

    //        this.retain = retain;
    //    }

    //    protected override IList<JsonProperty> CreateProperties(Type type,

    //    MemberSerialization memberSerialization)
    //    {
    //        IList<JsonProperty> list =
    //        base.CreateProperties(type, memberSerialization);
    //        //只保留清单有列出的属性
    //        return list.Where(p =>
    //        {
    //            if (retain)
    //            {
    //                return props.Contains(p.PropertyName);
    //            }
    //            else
    //            {
    //                return !props.Contains(p.PropertyName);
    //            }
    //        }).ToList();
    //    }
    //}
}
