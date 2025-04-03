﻿using EFT;
using EFT.InventoryLogic;
using EFT.UI.DragAndDrop;
using HarmonyLib;
using SPT.Reflection.Patching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace LootInstantSearch.Patches
{
    internal class SearchPatch : ModulePatch
    { 
        protected override MethodBase GetTargetMethod()
        {   
            return AccessTools.Method(typeof(PlayerSearchControllerClass), nameof(PlayerSearchControllerClass.IsItemKnown));
        }

        [PatchPrefix]
        static void Prefix(PlayerSearchControllerClass __instance, Item item)
        {
            //HashSet<Item> hashset_1 = AccessTools.Field(__instance.GetType(), "hashSet_1").GetValue(__instance) as HashSet<Item>;
            //if (!hashset_1.Contains(item))
            //{
                __instance.SetItemAsKnown(item);                    
            //} 
        }
    }

    //optimization for patch2
    /*internal class SearchPatch6 : ModulePatch
    {
        protected override MethodBase GetTargetMethod() 
        {   
            return AccessTools.Method(typeof(PlayerSearchControllerClass), nameof(PlayerSearchControllerClass.SetItemAsKnown));
        }

        [PatchPrefix]
        static bool Prefix(PlayerSearchControllerClass __instance, Item item)
        {
            if (__instance.hashSet_2.Contains(item))
            {
                return false;
            }
            return true;
        }
    }*/

    internal class SearchPatch2 : ModulePatch 
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(SearchableView), nameof(SearchableView.method_2));
        }

        [PatchPrefix]
        static void Prefix(SearchableView __instance, IPlayerSearchController ___iplayerSearchController_0, SearchableItemItemClass ___searchableItemItemClass)
        {
            ___iplayerSearchController_0.SetItemAsKnown(___searchableItemItemClass);
        }
    }

    internal class SearchPatch3 : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(SearchableView), nameof(SearchableView.Start));
        }

        [PatchPrefix]
        static bool Prefix(SearchableView __instance)
        {
            //autoclick dead body containers(backpack, pockets, etc)
            __instance.method_3();
            return false;
        }
    }

    internal class SearchPatch4 : ModulePatch  
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(GClass1966), nameof(GClass1966.ContainsUnknownItems));
        }

        [PatchPrefix]
        static bool Prefix(ref bool __result)
        {
            __result = false;
            return false;
        }

    }

    internal class SearchPatch5 : ModulePatch 
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(GClass1966), nameof(GClass1966.SearchContents));
        }

        [PatchPrefix]
        static void Prefix(GClass1966 __instance, SearchableItemItemClass searchableItem)
        {
            //if (!__instance.hashSet_0.Contains(searchableItem)) 
            //{
                __instance.SetItemAsSearched(searchableItem);
            //}
        }
    }

    internal class AttentionPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(SkillManager), nameof(SkillManager.method_2));
        }

        [PatchPostfix]
        static void Postfix(ref SkillManager __instance)
        {
            __instance.AttentionEliteLuckySearch.Elite(0f).PerLevel(0f);
        }
    }
}
