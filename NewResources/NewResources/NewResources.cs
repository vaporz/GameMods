using KXRocket;
using NLog;
using System;
using UnityEngine;

namespace NewResources
{
    [ScenePlugin("NewResources")]
    public class NewResources : MonoBehaviour
    {
        private static NLog.Logger logger = LogManager.GetCurrentClassLogger();
        private void Awake()
        {
            ResourceManager.RegisterResourceType(ResourceType.NewRigidResource("液氢", 1));
            ResourceManager.RegisterResourceType(ResourceType.NewRigidResource("液氧", 3));
        }
    }
}
