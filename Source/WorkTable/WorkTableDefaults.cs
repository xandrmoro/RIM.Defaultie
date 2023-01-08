using RimWorld;
using Verse;

namespace Defaultie
{
    public class WorkTableDefaults: IExposable
    {
        public IntRange SkillRange = Defaultie.Settings.DefaultSkillRange;
        public float Range = Defaultie.Settings.DefaultWorktableRange;
        public BillStoreModeDef StoreMode = BillStoreModeDefOf.BestStockpile;
        public BillRepeatModeDef RepeatMode = BillRepeatModeDefOf.RepeatCount;
        public int RepeatCount = Defaultie.Settings.DefaultRepeatCount;
        public string CountBuffer = "";

        public void ExposeData()
        {
            Scribe_Values.Look(ref SkillRange, "skillRange", Defaultie.Settings.DefaultSkillRange);
            Scribe_Values.Look(ref Range, "range", Defaultie.Settings.DefaultWorktableRange);
            Scribe_Values.Look(ref RepeatCount, "repeatCount", Defaultie.Settings.DefaultRepeatCount);
            Scribe_Defs.Look(ref StoreMode, "storeMode");
            Scribe_Defs.Look(ref RepeatMode, "repeatMode");

            if (StoreMode == null)
            {
                StoreMode = BillStoreModeDefOf.BestStockpile;
            }

            if (RepeatMode == null)
            {
                RepeatMode = BillRepeatModeDefOf.RepeatCount;
            }
        }

        public void SetFrom(WorkTableDefaults other)
        {
            SkillRange = other.SkillRange;
            Range = other.Range;
            StoreMode = other.StoreMode ?? BillStoreModeDefOf.BestStockpile;
            RepeatMode = other.RepeatMode ?? BillRepeatModeDefOf.RepeatCount;
            RepeatCount = other.RepeatCount;
        }
    }
}
