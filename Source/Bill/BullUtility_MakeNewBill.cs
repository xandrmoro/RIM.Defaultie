using HarmonyLib;
using RimWorld;
using Verse;

namespace Defaultie
{
    [HarmonyPatch(typeof(BillUtility), "MakeNewBill")]
    public class BullUtility_MakeNewBill
    {
        public static void Postfix(Bill __result)
        {
            if (Find.Selector.SingleSelectedThing is Building_WorkTable table && __result is Bill_Production productionBill)
            {
                productionBill.ApplyDefaults(table.GetDefaults(), true);
            }
        }
    }
}
