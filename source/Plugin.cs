using BepInEx;
using UnityEngine;
using BepInEx.Configuration;
using BepInEx.Logging;

namespace Pause
{
    [BepInPlugin("com.epi.pause", "PAUSE", "1.0.5")]
    public class Plugin : BaseUnityPlugin
    {
        public static GameObject Hook;
        const string KeybindSectionName = "Keybinds";
        internal static ConfigEntry<KeyboardShortcut> TogglePause;
        internal static ManualLogSource Log;

        void Awake()
        {
            Log = base.Logger;
            TogglePause = Config.Bind(KeybindSectionName, "Toggle Pause", new KeyboardShortcut(KeyCode.P));
            Logger.LogInfo($"PAUSE: Loading");
            new WorldTickPatch().Enable();
            new OtherWorldTickPatch().Enable();
            new GameTimerClassPatch().Enable();
            new TimerPanelPatch().Enable();
            Hook = new GameObject("PAUSE");
            Hook.AddComponent<PauseController>();
            DontDestroyOnLoad(Hook);
        }
    }
}