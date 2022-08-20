using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MDevoldere.Local
{
    public static class LocalJson
    {
        public static T? LoadJson<T>(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    string json = File.ReadAllText(path);
                    return JsonSerializer.Deserialize<T>(json);
                }
                return default;
            }
            catch { return default; }
        }

        public async static Task<T?> LoadJsonAsync<T>(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    using FileStream openStream = File.OpenRead(path);
                    return await JsonSerializer.DeserializeAsync<T>(openStream);
                }
                return default;
            }
            catch { return default; }
        }

        public static bool SaveJson(string path, object o)
        {
            try
            {
                string json = JsonSerializer.Serialize(o, new JsonSerializerOptions() { WriteIndented = true, });
                File.WriteAllText(path, json);
                return true;
            }
            catch { return false; }
        }

        public static async Task<bool> SaveJsonAsync(string path, object o)
        {
            try
            {
                using FileStream createStream = File.Create(path);
                await JsonSerializer.SerializeAsync(createStream, o, new JsonSerializerOptions() { WriteIndented = true, });
                await createStream.DisposeAsync();
                return true;
            }
            catch { return false; }
        }
    }
}
