using RimWorld;

namespace Defaultie
{
    public static class ApplyDefaultsExt
    {
        public static void ApplyDefaults(this Bill_Production bill, WorkTableDefaults defaults, bool newBill = false)
        {
            bill.allowedSkillRange = defaults.SkillRange;
            bill.ingredientSearchRadius = defaults.Range;
            bill.SetStoreMode(defaults.StoreMode);

            bill.pauseWhenSatisfied = defaults.PauseOnComplete;
            bill.unpauseWhenYouHave = defaults.UnpauseCount;

            if (defaults.PawnRestriction != null)
            {
                bill.SetPawnRestriction(defaults.PawnRestriction);
            }
            else if (defaults.SlavesOnly)
            {
                bill.SetAnySlaveRestriction();
            } else if (defaults.MechsOnly)
            {
                bill.SetAnyMechRestriction();
            } else if (defaults.NonMechsOnly)
            {
                bill.SetAnyNonMechRestriction();
            } else
            {
                bill.SetAnyPawnRestriction();
            }

            if (!bill.recipe.WorkerCounter.CanCountProducts(bill))
            {
                if (defaults.ForeverIfUncountable)
                {
                    bill.repeatMode = BillRepeatModeDefOf.Forever;
                }
                else
                {
                    bill.repeatMode = BillRepeatModeDefOf.RepeatCount;
                }
            }
            else
            {
                bill.repeatMode = defaults.RepeatMode;
            }

            if (bill.repeatMode == BillRepeatModeDefOf.RepeatCount && newBill)
            {
                bill.repeatCount = defaults.RepeatCount;
            }

            if (bill.repeatMode == BillRepeatModeDefOf.TargetCount)
            {
                bill.targetCount = defaults.RepeatCount;
                bill.qualityRange = defaults.QualityRange;
                bill.hpRange = defaults.HpRange;
                bill.limitToAllowedStuff = defaults.LimitToAllowedStuff;
            }
        }
    }
}
