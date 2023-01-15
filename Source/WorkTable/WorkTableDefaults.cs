using RimWorld;
using System;
using System.Linq.Expressions;
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

        public bool PauseOnComplete = false;
        public int UnpauseCount = 0;
        
        public string CountBuffer = "";
        public string UnpauseBuffer = "";

        public bool ForeverIfUncountable = true;

        public Pawn PawnRestriction;
        public bool SlavesOnly;
        public bool MechsOnly;
        public bool NonMechsOnly;
        internal bool LimitToAllowedStuff;
        public QualityRange QualityRange = QualityRange.All;
        public FloatRange HpRange = new FloatRange(0, 1);

        public void ZeroAll()
        {
            SlavesOnly = false;
            MechsOnly = false;
            NonMechsOnly = false;
            PawnRestriction = null;
        }

        public void SetPawnCatRestriction(ref bool field)
        {
            ZeroAll();

            field = true;
        }

        public void SetPawnRestriction(Pawn pawn)
        {
            ZeroAll();

            PawnRestriction = pawn;
        }

        public string GetRestrictionLabel() => (PawnRestriction != null) ? PawnRestriction.LabelShortCap : ((ModsConfig.IdeologyActive && SlavesOnly) ? ((string)"AnySlave".Translate()) : ((ModsConfig.BiotechActive && MechsOnly) ? ((string)"AnyMech".Translate()) : ((!ModsConfig.BiotechActive || !NonMechsOnly) ? ((string)"AnyWorker".Translate()) : ((string)"AnyNonMech".Translate()))));

        public void ExposeData()
        {
            Scribe_Values.Look(ref SkillRange, "skillRange", Defaultie.Settings.DefaultSkillRange);
            Scribe_Values.Look(ref Range, "range", Defaultie.Settings.DefaultWorktableRange);
            Scribe_Values.Look(ref RepeatCount, "repeatCount", Defaultie.Settings.DefaultRepeatCount);
            Scribe_Defs.Look(ref StoreMode, "storeMode");
            if (StoreMode == null)
            {
                StoreMode = BillStoreModeDefOf.BestStockpile;
            }
            Scribe_Defs.Look(ref RepeatMode, "repeatMode");
            if (RepeatMode == null)
            {
                RepeatMode = BillRepeatModeDefOf.RepeatCount;
            }

            Scribe_Values.Look(ref PauseOnComplete, "pauseOnComplete", false);
            Scribe_Values.Look(ref UnpauseCount, "unpauseCount", 0);
            
            Scribe_Values.Look(ref ForeverIfUncountable, "foreverIfUncountable", true);

            Scribe_Values.Look(ref SlavesOnly, "slavesOnly", false);
            Scribe_Values.Look(ref MechsOnly, "mechsOnly", false);
            Scribe_Values.Look(ref NonMechsOnly, "nonMechsOnly", false);

            Scribe_Values.Look(ref LimitToAllowedStuff, "limitToAllowed", false);
            Scribe_Values.Look(ref QualityRange, "qualityRange", QualityRange.All);
            Scribe_Values.Look(ref HpRange, "hpRange", new FloatRange(0, 1));

            Scribe_References.Look(ref PawnRestriction, "pawnRestriction");

            if (StoreMode == null)
            {
                StoreMode = BillStoreModeDefOf.BestStockpile;
            }

            if (RepeatMode == null)
            {
                RepeatMode = BillRepeatModeDefOf.RepeatCount;
            }
        }

        //public void SetFrom(WorkTableDefaults other)
        //{
        //    SkillRange = other.SkillRange;
        //    Range = other.Range;
        //    StoreMode = other.StoreMode ?? BillStoreModeDefOf.BestStockpile;
        //    RepeatMode = other.RepeatMode ?? BillRepeatModeDefOf.RepeatCount;
        //    RepeatCount = other.RepeatCount;

        //    Log.Message($"Pawn restriction set to {other.PawnRestriction?.LabelShortCap}");
        //    PawnRestriction = other.PawnRestriction;
        //    SlavesOnly = other.SlavesOnly;
        //    MechsOnly = other.MechsOnly;
        //    NonMechsOnly = other.NonMechsOnly;
        //}
    }
}
