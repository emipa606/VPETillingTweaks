using System.Reflection;
using HarmonyLib;
using UnityEngine;
using Verse;

namespace VPETillingTweaks;

[StaticConstructorOnStartup]
public static class VPETillingTweaksPatcher
{
    public static readonly Texture2D VPETillIcon = ContentFinder<Texture2D>.Get("UI/VCE_Plow", false);

    static VPETillingTweaksPatcher()
    {
        if (VPETillingTweaksDefs.VCE_TilledSoil != null)
        {
            new Harmony("Afropenguinn.Utils.VPETillingTweaks").PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}