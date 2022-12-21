using HarmonyLib;
using KitchenMods;

namespace KitchenNewCardEveryDay
{
    internal class NewCardEveryDay : IModInitializer
    {
        public void PostActivate(Mod mod)
        {
            var harmony = new Harmony("noxxflame.plateup.newcardeveryday");
            harmony.PatchAll(GetType().Assembly);
        }

        public void PreInject() { }

        public void PostInject() { }
    }
}
