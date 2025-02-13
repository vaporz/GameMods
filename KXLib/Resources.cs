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
            ResourceManager.RegisterResourceType(ResourceType.NewRigidResource("fuel", 1));
            ResourceManager.RegisterResourceType(ResourceType.NewRigidResource("oxidizer", 1));
            ResourceManager.RegisterResourceType(ResourceType.NewRigidResource("液氢", 1));
            ResourceManager.RegisterResourceType(ResourceType.NewRigidResource("液氧", 3));
            ResourceManager.RegisterResourceType(ResourceType.NewRigidResource("偏二甲肼", 1));
            ResourceManager.RegisterResourceType(ResourceType.NewRigidResource("四氧化二氮", 1));
        }
    }
}
