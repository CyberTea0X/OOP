using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton
{
    public class SettingsSingleton
    {
        private static SettingsSingleton? instance;
        private string? playerName;
        private int? maxFps;
        private int? minFps;

        public string? PlayerName { get { return instance?.playerName; } }
        public int? MaxFps { get { return instance?.maxFps; } }
        public int? MinFps { get { return instance?.minFps; } }

        public SettingsSingleton()
        {
            if (instance == null)
            {
                throw new InvalidOperationException("Попытка доступа к синглтону, хотя экземпляра не существует");
            }
        }

        public SettingsSingleton(string playerName, int maxFps, int minFps)
        {
            var ptr = (instance == null) ? this : instance;
            ptr.playerName = playerName;
            ptr.maxFps = maxFps;
            ptr.minFps = minFps;
            if (instance == null) {
                instance = this;
            }
        }
    }
}
