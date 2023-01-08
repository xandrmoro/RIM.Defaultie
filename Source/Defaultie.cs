using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.AI;

namespace Defaultie
{
    public class Defaultie : Mod
    {
        public static DefaultieSettings Settings { get; private set; }

        public Defaultie(ModContentPack contentPack) : base(contentPack)
        {
            Settings = GetSettings<DefaultieSettings>();

            new Harmony(Content.PackageIdPlayerFacing).PatchAll();
        }

        public override void WriteSettings()
        {
            base.WriteSettings();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            var listing = new Listing_Standard();

            listing.Begin(inRect);

            listing.ColumnWidth = 170;

            listing.Label("Default skill range");
            listing.Label("Default radius");
            //listing.Label("Default repeat mode");
            listing.Label("Default repeat count");
            //listing.Label("Default store mode");

            //listing.NewColumn();

            //listing.ColumnWidth = 30;

            //listing.Gap();
            //listing.Label(Settings.DefaultWorktableRange.ToString("F0"));
            //listing.Gap();
            ////listing.Gap();
            ////listing.Gap();

            listing.NewColumn();

            listing.ColumnWidth = 200;

            listing.IntRange(ref Settings.DefaultSkillRange, 0, 20);
            Settings.DefaultWorktableRange = listing.SliderLabeled(Settings.DefaultWorktableRange.ToString("F0"), (Settings.DefaultWorktableRange > 100f) ? 100f : Settings.DefaultWorktableRange, 3f, 100f);
            if (Settings.DefaultWorktableRange >= 100f)
            {
                Settings.DefaultWorktableRange = 999f;
            }

            //if (listing.ButtonText(Settings.DefaultRepeatMode.LabelCap))
            //{
            //    List<FloatMenuOption> list = new List<FloatMenuOption>
            //    {
            //        new FloatMenuOption(BillRepeatModeDefOf.RepeatCount.LabelCap, delegate
            //        {
            //            Settings.DefaultRepeatMode = BillRepeatModeDefOf.RepeatCount;
            //        }),
            //        new FloatMenuOption(BillRepeatModeDefOf.TargetCount.LabelCap, delegate
            //        {
            //            Settings.DefaultRepeatMode = BillRepeatModeDefOf.TargetCount;
            //        }),
            //        new FloatMenuOption(BillRepeatModeDefOf.Forever.LabelCap, delegate
            //        {
            //            Settings.DefaultRepeatMode = BillRepeatModeDefOf.Forever;
            //        })
            //    };
            //    Find.WindowStack.Add(new FloatMenu(list));
            //}

            //if (listing.ButtonText(Settings.DefaultBillStore.LabelCap))
            //{
            //    List<FloatMenuOption> list = new List<FloatMenuOption>
            //    {
            //        new FloatMenuOption(BillStoreModeDefOf.DropOnFloor.LabelCap, delegate
            //        {
            //            Settings.DefaultBillStore = BillStoreModeDefOf.DropOnFloor;
            //        }),
            //        new FloatMenuOption(BillStoreModeDefOf.BestStockpile.LabelCap, delegate
            //        {
            //            Settings.DefaultBillStore = BillStoreModeDefOf.BestStockpile;
            //        })
            //    };
            //    Find.WindowStack.Add(new FloatMenu(list));
            //}

            listing.End();

            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return null;// "Defaultie";
        }
    }
}
