using RimWorld;
using Verse;

namespace VPETillingTweaks;

[DefOf]
public static class VPETillingTweaksDefs
{
    public static TerrainDef VCE_TilledSoil;

    static VPETillingTweaksDefs()
    {
        DefOfHelper.EnsureInitializedInCtor(typeof(VPETillingTweaksDefs));
    }
}