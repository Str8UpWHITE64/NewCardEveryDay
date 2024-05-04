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
            bool isThemeDay = request.Day / 5 == 1 && request.Day % 5 == 0;
            FileLog.Log("isThemeDay: " + isThemeDay);
            bool isFranchiseDay = request.Day / 15 == 1 && request.Day % 15 == 0;
            FileLog.Log("isFranchiseDay: " + isFranchiseDay);
            bool isStartingDay = request.Day == 0;
            bool isSpecialDay = isThemeDay || isFranchiseDay || isStartingDay;
            bool isRegularDay = (request.Day - __instance.DayOffset) % 1 == 0 && !isSpecialDay;
            FileLog.Log("isRegularDay: " + isRegularDay);

            __result = isRegularDay;
            return false;
        }
    }
}
