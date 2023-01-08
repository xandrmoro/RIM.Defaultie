using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Verse;

namespace Defaultie
{
    public class DefaultieWindow : Window
    {
        private readonly Action<Rect> drawContent;
        private readonly Action onUpdate;
        private readonly Rect initialRect;

        public DefaultieWindow(Rect initialRect, Action<Rect> drawContent, Action onUpdate)
        {
            layer = WindowLayer.GameUI;
            doCloseX = true;
            absorbInputAroundWindow = false;
            preventCameraMotion = false;
            this.initialRect = initialRect;
            this.drawContent = drawContent;
            this.onUpdate = onUpdate;
        }

        protected override void SetInitialSizeAndPosition()
        {
            base.SetInitialSizeAndPosition();

            windowRect = initialRect;
        }

        public override void DoWindowContents(Rect inRect)
        {
            GameFont font = Text.Font;
            Text.Font = GameFont.Small;

            drawContent(inRect);

            Text.Font = font;
        }

        public override void WindowUpdate()
        {
            base.WindowUpdate();

            onUpdate();
        }
    }

    [HarmonyPatch]
    public class CloseWindow_Patch
    {
        public static IEnumerable<MethodBase> TargetMethods()
        {
            yield return AccessTools.Method(typeof(ITab_Bills), "CloseTab");
            yield return AccessTools.Method(typeof(MainTabWindow_Inspect), "CloseOpenTab");
        }

        static void Postfix()
        {
            Find.WindowStack.WindowOfType<DefaultieWindow>()?.Close(false);
        }
    }
}
