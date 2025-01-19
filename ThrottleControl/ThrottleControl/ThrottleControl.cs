using KXRocket;
using Newtonsoft.Json.Linq;
using NLog;
using UnityEngine;

namespace ThrottleControlPlugin
{

    public class Config
    {
        public float interval;
    }

    [PartPlugin("ThrottleControl")]
    public class ThrottleControlPlugin : PartPluginBase
    {
        private static NLog.Logger logger = LogManager.GetCurrentClassLogger();
        private Config config;
        private bool keyPressed;
        private float throttle;
        private float direction = 1;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
                keyPressed = !keyPressed;
        }
        private void FixedUpdate()
        {
            if (!keyPressed)
                return;
            BaseEnginePlugin engine = gameObject.GetComponent<BaseEnginePlugin>();
            engine.SetThrottle(throttle);

            throttle += 1f / config.interval * Time.fixedDeltaTime * direction;
            if (throttle > 1 || throttle < 0)
                direction = direction == 1 ? -1 : 1;

            throttle = throttle > 1 ? 1 : throttle;
            throttle = throttle < 0 ? 0 : throttle;
        }

        public override void LoadPlayerPluginConfig(JObject config)
        {
            return;
        }

        public override void LoadPartPluginConfig(JObject config)
        {
            this.config = config.ToObject<Config>();
        }
    }
}
