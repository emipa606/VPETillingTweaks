using System.Collections.Generic;
using RimWorld;
using Verse;

namespace VPETillingTweaks;

public class AutoTillManager(Map map) : MapComponent(map)
{
    private HashSet<Zone_Growing> activeAutoTillZones = [];

    public bool GetAllowAutoTill(Zone_Growing zone)
    {
        return activeAutoTillZones.Contains(zone);
    }

    public void SetAllowAutoTill(Zone_Growing zone, bool autoTill)
    {
        if (!autoTill)
        {
            activeAutoTillZones.Remove(zone);
            return;
        }

        activeAutoTillZones.Add(zone);
        TillZone(zone);
    }

    public override void ExposeData()
    {
        Scribe_Collections.Look(ref activeAutoTillZones, "activeAutoTillZones", LookMode.Reference);
        activeAutoTillZones.Remove(null);
    }

    public override void MapComponentTick()
    {
        if (Find.TickManager.TicksGame % GenTicks.TickLongInterval != 0)
        {
            return;
        }

        foreach (var zone in activeAutoTillZones)
        {
            TillZone(zone);
        }
    }

    private void TillZone(Zone_Growing zone)
    {
        foreach (var center in zone.Cells)
        {
            if (GenConstruct.CanPlaceBlueprintAt(VPETillingTweaksDefs.VCE_TilledSoil, center, Rot4.North, map))
            {
                GenConstruct.PlaceBlueprintForBuild(VPETillingTweaksDefs.VCE_TilledSoil, center, map, Rot4.North,
                    Faction.OfPlayer, null);
            }
        }
    }
}