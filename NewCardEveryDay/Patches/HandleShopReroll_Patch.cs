using HarmonyLib;
using Kitchen;
using KitchenData;
using System;
using System.Reflection;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace KitchenNewCardEveryDay
{
    [HarmonyPatch(typeof(HandleShopReroll))]
    public class HandleShopReroll_Patch
    {
        [HarmonyPatch("CreateShop")]
        [HarmonyPrefix]
        public static bool CreateShop_PrefixPatch(HandleShopReroll __instance, bool fixed_location, Vector3 location)
        {
            Entity entity = __instance.EntityManager.CreateEntity(new ComponentType[]
            {
                typeof(CNewShop)
            });
            __instance.EntityManager.AddComponentData<CNewShop>(entity, new CNewShop
            {
                Tags = ShoppingTagsExtensions.DefaultShoppingTag,
                Location = location,
                FixedLocation = fixed_location,
                StartOpen = true
            });
            return false;
        }
    }
}
