using HarmonyLib;
using UnityEngine;
using Verse;

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
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "Defaultie";
        }
    }


}
