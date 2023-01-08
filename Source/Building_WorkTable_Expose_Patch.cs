using HarmonyLib;
using RimWorld;
using Verse;

namespace Defaultie
{
    [HarmonyPatch(typeof(Building_WorkTable), "ExposeData")]
    public class Building_WorkTable_Expose_Patch
    {
        public static void Postfix(Building_WorkTable __instance)
        {
            var defaults = __instance.GetDefaults();
            Scribe_Deep.Look(ref defaults, "defaultSettings");
            if (Scribe.mode == LoadSaveMode.LoadingVars && defaults != null)
            {
                __instance.GetDefaults().SetFrom(defaults);
            }
        }
    }
}
