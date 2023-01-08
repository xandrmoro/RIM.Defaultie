using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Defaultie
{
    public class DefaultieSettings : ModSettings
    {
        public BillStoreModeDef DefaultBillStore = BillStoreModeDefOf.BestStockpile;
        public BillRepeatModeDef DefaultRepeatMode = BillRepeatModeDefOf.RepeatCount;
        public float DefaultWorktableRange = 999f;
        public IntRange DefaultSkillRange = new IntRange(0, 20);
        public int DefaultRepeatCount = 10;

        public override void ExposeData()
        {
            Scribe_Defs.Look(ref DefaultBillStore, "defaultStoreMode");
            Scribe_Defs.Look(ref DefaultRepeatMode, "defaultRepeatMode");
            Scribe_Values.Look(ref DefaultWorktableRange, "defaultWTRange", 999f);
            Scribe_Values.Look(ref DefaultSkillRange, "defaultWTRange", new IntRange(0, 20));
            Scribe_Values.Look(ref DefaultRepeatCount, "defaultRepeatUntil", 10);

            if (DefaultBillStore == null) 
            {
                DefaultBillStore = BillStoreModeDefOf.BestStockpile;
            }

            if (DefaultRepeatMode == null)
            {
                DefaultRepeatMode = BillRepeatModeDefOf.RepeatCount;
            }

            base.ExposeData();
        }
    }
}
