using Newtonsoft.Json;
using System.IO;

namespace SyncNotify
{
    internal class SettingsManager
    {
        public SettingsManager() {
            if (!Directory.Exists(App.RootPath))
            {
                Directory.CreateDirectory(App.RootPath);
            }
        }
        public void SaveSettingsToFile(Settings settings)
        {
            
            string text = JsonConvert.SerializeObject(settings, Formatting.Indented);
            try
            {
                System.IO.File.WriteAllText(App.RootPath + Settings.settingsFileName, text);
            }
            catch { }
        }

        public Settings GetSettingsByFile(string settingsFileName, Settings settings)
        {
            if (System.IO.File.Exists(App.RootPath + settingsFileName))
            {

                string text = System.IO.File.ReadAllText(App.RootPath + settingsFileName);
                settings = JsonConvert.DeserializeObject<Settings>(text);
                return settings;
            }
            else
            {
                return null;
            }

        }
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
