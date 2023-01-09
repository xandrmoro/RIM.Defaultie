﻿using RimWorld;

namespace Defaultie
{
    public static class ApplyDefaultsExt
    {
        public static void ApplyDefaults(this Bill_Production bill, WorkTableDefaults defaults, bool newBill = false)
        {
            bill.allowedSkillRange = defaults.SkillRange;
            bill.ingredientSearchRadius = defaults.Range;
            bill.SetStoreMode(defaults.StoreMode);
            bill.repeatMode = defaults.RepeatMode;

            if (defaults.PawnRestriction != null)
            {
                bill.SetPawnRestriction(defaults.PawnRestriction);
            }

            if (defaults.SlavesOnly)
            {
                bill.SetAnySlaveRestriction();
            }

            if (defaults.MechsOnly)
            {
                bill.SetAnyMechRestriction();
            }

            if (defaults.NonMechsOnly)
            {
                bill.SetAnyNonMechRestriction();
            }

            if (bill.repeatMode == BillRepeatModeDefOf.RepeatCount && newBill)
            {
                bill.repeatCount = defaults.RepeatCount;
            }

            if (bill.repeatMode == BillRepeatModeDefOf.TargetCount)
            {
                bill.targetCount = defaults.RepeatCount;
            }
        }
    }
}
