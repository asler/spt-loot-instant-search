using EFT;
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
            if (__instance == null || __instance.hashSet_1 == null) return;

            if(!__instance.hashSet_1.Contains(item))
            {
                __instance.SetItemAsKnown(item, true);                    
            } 
        }
    }

    internal class SearchPatch2 : ModulePatch 
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(SearchableView), nameof(SearchableView.method_2));
        }

        [PatchPrefix]
        static void Prefix(SearchableView __instance, IPlayerSearchController ___iplayerSearchController_0, SearchableItemItemClass ___searchableItemItemClass)
        {
            ___iplayerSearchController_0.SetItemAsKnown(___searchableItemItemClass, false);
        }
    }

    internal class SearchPatch3 : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(SearchableView), nameof(SearchableView.Show));
        }

        [PatchPostfix]
        static void Postfix(SearchableView __instance, PlayerSearchControllerClass ___iplayerSearchController_0)
        {
            if (__instance != null && ___iplayerSearchController_0 != null){
                __instance.method_3();
            }
        }
    }

    internal class SearchPatch4 : ModulePatch  
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(GClass2002), nameof(GClass2002.ContainsUnknownItems));
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
            return AccessTools.Method(typeof(GClass2002), nameof(GClass2002.SearchContents));
        }

        [PatchPrefix]
        static void Prefix(GClass2002 __instance, SearchableItemItemClass searchableItem)
        {
            if (!__instance.hashSet_0.Contains(searchableItem)) 
            {
                __instance.SetItemAsSearched(searchableItem);
            }
        }
    }

    //optimization for patch2
    internal class SearchPatch6 : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {

            return AccessTools.Method(typeof(PlayerSearchControllerClass), nameof(PlayerSearchControllerClass.SetItemAsKnown));
        }

        [PatchPrefix]
        static bool Prefix(PlayerSearchControllerClass __instance, Item item)
        {
            /*if (__instance.hashSet_2.Contains(item))
            {
                return false;
            }
            __instance.hashSet_2.Add(item);
            return true;*/

            if (__instance.method_4(item))
            {
                GClass2001 gClass2001 = __instance as GClass2001;
                gClass2001.method_1(item);
            }

            return false;
        }
    }

    internal class AttentionPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {

            return AccessTools.Method(typeof(SkillManager), nameof(SkillManager.method_2));
        }

        [PatchPostfix]
        static void Postfix(SkillManager __instance)
        {
            __instance.AttentionEliteLuckySearch.Elite(0f).PerLevel(0f);
        }
    }
}
