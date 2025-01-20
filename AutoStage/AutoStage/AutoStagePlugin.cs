using KXRocket;
using NLog;
using UnityEngine;

[ScenePlugin("AutoStage")]
public class AutoStagePlugin : MonoBehaviour
{
    private static NLog.Logger logger = LogManager.GetCurrentClassLogger();
    private void Awake()
    {
        logger.Info("AutoStagePlugin is running");
    }
    private void FixedUpdate()
    {
        foreach (var e in GameGlobals.ActiveVesselEngines())
        {
            if (e.IsFlameOut())
            {
                GameManager.Instance.StagingManager.Stage(); // TODO weired, stageManager as Mono, use instance
                return;
            }
        }
    }
}
