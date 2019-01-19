using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace AltCodeKneeboard.Models
{
    internal static class ThemeFileManager
    {
        public static async Task<KneeboardTheme> LoadAsync(string filename)
        {
            var theme = new KneeboardTheme();
            theme.Filename = filename;

            var themeData = (await ReadThemeFileAsync(filename)).Values;
            foreach (PropertyDescriptor prop in theme.GetProperties())
            {
                if (themeData.ContainsKey(prop.Name))
                {
                    prop.SetValue(theme, themeData[prop.Name]);
                }
            }

            theme.Persist();
            return theme;
        }

        public static async Task SaveAsync(string filename, KneeboardTheme theme)
        {
            theme.Filename = filename;

            var themeData = new Dictionary<string, object>();
            foreach (PropertyDescriptor prop in theme.GetProperties())
            {
                themeData.Add(prop.Name, prop.GetValue(theme));
            }

            var themeModel = new Theme { Values = themeData };
            await WriteThemeFileAsync(filename, themeModel);

            theme.Persist();
        }

        private static async Task<Theme> ReadThemeFileAsync(string filename)
        {
            return await Task.Factory.StartNew(() =>
            {
                using (var reader = new XmlTextReader(File.OpenRead(filename)))
                {
                    var xml = new XmlSerializer(typeof(Theme), "http://schneenet.com/Kneeboard/Theme.xsd");
                    return (Theme)xml.Deserialize(reader);
                }
            });
        }

        private static async Task WriteThemeFileAsync(string filename, Theme theme)
        {
            await Task.Factory.StartNew(() =>
            {
                using (var stream = File.OpenWrite(filename))
                {
                    var xml = new XmlSerializer(typeof(Theme), "http://schneenet.com/Kneeboard/Theme.xsd");
                    xml.Serialize(stream, theme);
                }
            });
        }
    }
}
