using HarmonyLib;
using Kitchen;
using KitchenData;
using System;
using Unity.Collections;
using Unity.Entities;

namespace KitchenNewCardEveryDay
{
    [HarmonyPatch(typeof(CreateShopRequests))]
    public class CreateShopRequests_Patch
    {
        [HarmonyPatch("OnUpdate")]
        [HarmonyPrefix]
        public static bool OnUpdate_PrefixPatch(CreateShopRequests __instance)
        {
            Traverse CSR = Traverse.Create(__instance);
            EntityQuery dayQuery = CSR.Field("_SingletonEntityQuery_SDay_54").GetValue<EntityQuery>();
            EntityQuery shopRemover = CSR.Field("ShopRemover").GetValue<EntityQuery>();

            int num = 0;
            using (NativeArray<CRemovesShopBlueprint> nativeArray = shopRemover.ToComponentDataArray<CRemovesShopBlueprint>(Allocator.Temp))
            {
                foreach (CRemovesShopBlueprint cremovesShopBlueprint in nativeArray)
                {
                    num += cremovesShopBlueprint.Count;
                }
                ShoppingTags defaultShoppingTag = ShoppingTagsExtensions.DefaultShoppingTag;
                int day = dayQuery.GetSingleton<SDay>().Day;

                int shopSize = Math.Max(1, DifficultyHelpers.TotalShopCount(day) - num);
                int staples = Math.Max(0, Math.Min(DifficultyHelpers.StapleCount(day), shopSize));
                int nonStaples = Math.Max(0, shopSize - staples);
                for (int i = 0; i < staples; i++)
                {
                    CSR.Method("AddShop", ShoppingTags.Basic).GetValue(ShoppingTags.Basic);
                }
                for (int j = 0; j < nonStaples; j++)
                {
                    CSR.Method("AddShop", defaultShoppingTag).GetValue(defaultShoppingTag);
                }
                if (day > 0 && day % 5 == 0)
                {
                    for (int k = 0; k < 6; k++)
                    {
                        CSR.Method("AddDecorShop").GetValue();
                    }
                }
            }
            return false;
        }
    }
}
