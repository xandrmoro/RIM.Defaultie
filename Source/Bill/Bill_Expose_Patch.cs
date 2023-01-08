using HarmonyLib;
using RimWorld;
using Verse;

namespace Defaultie
{
    [HarmonyPatch(typeof(Bill), "ExposeData")]
    public class Bill_Expose_Patch
    {
        public static void Postfix(Bill __instance)
        {
            if (__instance is Bill_Production bill)
            {
                var value = bill.GetAddedFields();
                Scribe_Deep.Look(ref value, "defaultSettings");
                if (Scribe.mode == LoadSaveMode.LoadingVars)
                {
                    if (value != null)
                    {
                        bill.GetAddedFields().Linked = value.Linked;
                    }
                    else
                    {
                        bill.GetAddedFields().Linked = false;
                    }
                }
            }
        }
    }
}
