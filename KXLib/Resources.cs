using KXRocket;
using NLog;
using System;
using UnityEngine;

namespace KXRocket
{
    [ScenePlugin("Resources")]
    public class Resources : MonoBehaviour
    {
        private static NLog.Logger logger = LogManager.GetCurrentClassLogger();
        private void Awake()
        {
            ResourceManager.RegisterResourceType(ResourceType.NewRigidResource("LiquidFuel", 1));
            ResourceManager.RegisterResourceType(ResourceType.NewRigidResource("Oxidizer", 1));
            ResourceManager.RegisterResourceType(ResourceType.NewRigidResource("LqdHydrogen", 1));
            ResourceManager.RegisterResourceType(ResourceType.NewRigidResource("LqdOxygen", 3));
            ResourceManager.RegisterResourceType(ResourceType.NewRigidResource("UDMH", 1)); // 偏二甲肼
            ResourceManager.RegisterResourceType(ResourceType.NewRigidResource("NTO", 1));// 四氧化二氮
        }
    }
}
