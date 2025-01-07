using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace VPETillingTweaks;

[HarmonyPatch(typeof(Zone_Growing), nameof(Zone_Growing.GetGizmos))]
public static class AutoTillGizmo
{
    public static IEnumerable<Gizmo> Postfix(IEnumerable<Gizmo> __result, Zone_Growing __instance)
    {
        foreach (var gizmo in __result)
        {
            yield return gizmo;
        }

        if (!VPETillingTweaksDefs.VCE_TilledSoil.IsResearchFinished)
        {
            yield break;
        }

        var manager = __instance.Map.GetComponent<AutoTillManager>();
        if (manager == null)
        {
            yield break;
        }

        var isActive = manager.GetAllowAutoTill(__instance);
        yield return new Command_Toggle
        {
            defaultLabel = "VTT.AllowTilling".Translate(),
            defaultDesc = "VTT.AllowTillingTT".Translate(),
            icon = VPETillingTweaksPatcher.VPETillIcon,
            isActive = () => isActive,
            toggleAction = delegate
            {
                foreach (var zone in Find.Selector.SelectedObjects.OfType<Zone_Growing>())
                {
                    manager.SetAllowAutoTill(zone, !isActive);
                }
            }
        };
    }
}