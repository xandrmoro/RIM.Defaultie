using HarmonyLib;
using RimWorld;
using System.Linq;
using System.Reflection;
using Verse;

namespace Defaultie
{
    [HarmonyPatch]
    public class ITab_Bills_Ctor_Patch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(ITab_Bills).GetConstructors().First(c => !c.GetParameters().Any());
        }

        static void Postfix(ref float ___PasteX, float ___PasteSize)
        {
            ___PasteX = ___PasteX + ___PasteSize + 4f;
        }
    }
}
