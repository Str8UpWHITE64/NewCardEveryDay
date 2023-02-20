using HarmonyLib;
using KitchenData;
using System.Collections.Generic;

namespace KitchenNewCardEveryDay
{
    [HarmonyPatch(typeof(UnlockConditionRegular))]
    public class UnlockConditionRegular_Patch
    {
        [HarmonyPatch("ShouldProvide")]
        [HarmonyPrefix]
        public static bool ShouldProvide_PrefixPatch(UnlockConditionRegular __instance, ref bool __result, List<Unlock> candidates, HashSet<int> current_cards, UnlockRequest request)
        {
            //set dayInterval to how often you want cards
            int dayInterval = 1;
            int dayOffset = __instance.DayOffset;

            //if day <= DayMax or DayMax <= 0
            bool maxDay = request.Day <= __instance.DayMax || __instance.DayMax <= 0;
            //if day >= DayMin or DayMin <= 0
            bool minDay = request.Day >= __instance.DayMin || __instance.DayMin <= 0;
            //if (day - DayOffset) % DayInterval != 0
            bool dayInt = (request.Day - dayOffset) % dayInterval != 0;
            //if tier == Tier or DayMax <= 0
            bool reqTier = request.Tier == __instance.TierRequired || __instance.TierRequired <= 0;
            __result = maxDay && minDay && dayInt && reqTier;
            return false;
        }
    }
}
