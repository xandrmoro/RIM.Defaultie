using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace Defaultie
{
    [HarmonyPatch(typeof(Bill), "DoInterface")]
    public class Bill_DoInterface_Patch
    {
        public static void DrawLinkButton(Bill bill, Rect rect)
        {
            if (bill is Bill_Production productionBill && bill.billStack.billGiver is Building_WorkTable table)
            {
                rect.x -= rect.width;

                var linkedDefaults = productionBill.GetAddedFields();
                var color = linkedDefaults.Linked ? Color.white : new Color(0.2f, 0.2f, 0.2f);

                if (Widgets.ButtonImageFitted(rect, ITab_Bills_FillTab_Patch.Icon, color))
                {
                    if (linkedDefaults.Linked)
                    {
                        linkedDefaults.Linked = false;
                    }
                    else
                    {
                        linkedDefaults.Linked = true;
                        productionBill.ApplyDefaults(table.GetDefaults());
                    }

                    SoundDefOf.Click.PlayOneShotOnCamera();
                }
            }
        }

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (var instruction in instructions)
            {
                if (instruction.Calls(AccessTools.Method(typeof(Widgets), "EndGroup")))
                {
                    yield return new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(instruction);
                    yield return new CodeInstruction(OpCodes.Ldloc_S, 5);
                    yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(Bill_DoInterface_Patch), "DrawLinkButton"));
                }

                yield return instruction;
            }
        }
    }
}
