using HarmonyLib;
using RimWorld;

namespace Defaultie
{
    [HarmonyPatch(typeof(BillStack), "AddBill")]
    public class BillStack_AddBill_Patch
    {
        public static void Postfix(BillStack __instance, Bill bill)
        {
            if (__instance.billGiver is Building_WorkTable table && bill is Bill_Production productionBill)
            {
                productionBill.ApplyDefaults(table.GetDefaults(), true);
            }
        }
    }
}
