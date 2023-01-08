using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace Defaultie
{
    [HarmonyPatch]
    public class ITab_Bills_FillTab_Patch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(ITab_Bills).GetMethod("FillTab", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        private static Lazy<Texture2D> icon = new Lazy<Texture2D>(() => ContentFinder<Texture2D>.Get("UI/Icons/Options/OptionsGeneral"));
        public static Texture2D Icon => icon.Value;

        static void Postfix(ITab_Bills __instance, float ___PasteX, float ___PasteY, float ___PasteSize, Vector2 ___WinSize)
        {
            var iconRect = new Rect(___WinSize.x - ___PasteX + ___PasteSize, ___PasteY, ___PasteSize, ___PasteSize);

            if (Widgets.ButtonImageFitted(iconRect, Icon, Color.white))
            {
                var tabRect = Traverse.Create(__instance).Property<Rect>("TabRect").Value;

                var windowRect = new Rect(tabRect.x + tabRect.width, tabRect.y, 250f, tabRect.height);
                var innerRect = windowRect.ContractedBy(10f).AtZero();

                Find.WindowStack.Add(new DefaultieWindow(windowRect, (inRect) =>
                {
                    var selTable = Traverse.Create(__instance).Property<Building_WorkTable>("SelTable").Value;
                    var defaults = selTable.GetDefaults();

                    var listing = new Listing_Standard();

                    listing.Begin(inRect);
                    {
                        IntRange skillRange = new IntRange(0, 20);
                        listing.Label("Skill level");
                        listing.IntRange(ref defaults.SkillRange, 0, 20);

                        listing.Gap();

                        string text = "IngredientSearchRadius".Translate();
                        string text2 = ((defaults.Range == 999f) ? "Unlimited".TranslateSimple() : defaults.Range.ToString("F0"));
                        listing.Label(text + ": " + text2);
                        defaults.Range = listing.Slider((defaults.Range > 100f) ? 100f : defaults.Range, 3f, 100f);
                        if (defaults.Range >= 100f)
                        {
                            defaults.Range = 999f;
                        }

                        listing.Gap();

                        Log.Message(defaults.StoreMode.defName);

                        if (listing.RadioButton("On floor", defaults.StoreMode == BillStoreModeDefOf.DropOnFloor))
                        {
                            defaults.StoreMode = BillStoreModeDefOf.DropOnFloor;
                        }

                        if (listing.RadioButton("Best stockpile", defaults.StoreMode == BillStoreModeDefOf.BestStockpile))
                        {
                            defaults.StoreMode = BillStoreModeDefOf.BestStockpile;
                        }

                        listing.Gap();

                        if (listing.ButtonText(defaults.RepeatMode.LabelCap))
                        {
                            List<FloatMenuOption> list = new List<FloatMenuOption>
                            {
                                new FloatMenuOption(BillRepeatModeDefOf.RepeatCount.LabelCap, delegate
                                {
                                    defaults.RepeatMode = BillRepeatModeDefOf.RepeatCount;
                                }),
                                new FloatMenuOption(BillRepeatModeDefOf.TargetCount.LabelCap, delegate
                                {
                                    defaults.RepeatMode = BillRepeatModeDefOf.TargetCount;
                                }),
                                new FloatMenuOption(BillRepeatModeDefOf.Forever.LabelCap, delegate
                                {
                                    defaults.RepeatMode = BillRepeatModeDefOf.Forever;
                                })
                            };
                            Find.WindowStack.Add(new FloatMenu(list));
                        }
                        
                        defaults.CountBuffer = defaults.RepeatCount.ToString();
                        listing.IntEntry(ref defaults.RepeatCount, ref defaults.CountBuffer);
                    }
                    listing.End();

                    foreach (var bill in selTable.billStack.Bills.OfType<Bill_Production>().Where(b => b.GetAddedFields().Linked))
                    {
                        bill.ApplyDefaults(defaults);
                    }
                }, () =>
                {
                    var selTable = Traverse.Create(__instance).Property<Building_WorkTable>("SelTable").Value;
                    var defaults = selTable.GetDefaults();

                    if (defaults.Range < GenRadial.MaxRadialPatternRadius)
                    {
                        GenDraw.DrawRadiusRing(selTable.Position, defaults.Range);
                    }
                }));

                SoundDefOf.Tick_Low.PlayOneShotOnCamera();
            }
        }
    }
}
