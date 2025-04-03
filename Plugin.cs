using BepInEx;
using BepInEx.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LootInstantSearch.Patches;

namespace LootInstantSearch
{
    // first string below is your plugin's GUID, it MUST be unique to any other mod. Read more about it in BepInEx docs. Be sure to update it if you copy this project.
    [BepInPlugin("wizard.LootInstantSearch", "LootInstantSearch", "1.0.3")]
    public class Plugin : BaseUnityPlugin
    {
        public static ManualLogSource LogSource;

        // BaseUnityPlugin inherits MonoBehaviour, so you can use base unity functions like Awake() and Update()
        private void Awake()
        {
            // save the Logger to variable so we can use it elsewhere in the project
            LogSource = Logger;
            LogSource.LogInfo("LootInstantSearch loaded!");

            // uncomment line(s) below to enable desired example patch, then press F6 to build the project:
            new AttentionPatch().Enable();
            new SearchPatch().Enable();
            new SearchPatch2().Enable();
            new SearchPatch3().Enable();
            new SearchPatch4().Enable();
            new SearchPatch5().Enable();
            new SearchPatch6().Enable();
            
        }

        private static HashSet<string> AlreadyThrownPatches = new HashSet<string>();
        public static Exception ShowErrorNotif(Exception ex)
        {
            if (!AlreadyThrownPatches.Add(ex.Source))
            {
                return ex;
            }

            NotificationManagerClass.DisplayWarningNotification(
                $"Seion.Iof thew an exception. Perhaps version incompatibility? Exception: {ex.Message}",
                duration: EFT.Communications.ENotificationDurationType.Infinite
                );

            return ex;
        }
    }
}
