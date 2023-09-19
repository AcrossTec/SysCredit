namespace SysCredit.Api.Patchers;

using HarmonyLib;

/// <summary>
///     Introduction
///     https://harmony.pardeike.net/articles/intro.html
/// </summary>
internal static class SysCreditApiPatcher
{
    private static Harmony? Harmony;

    public static void PatchAll(string HarmonyId = "Com.AcrossTec.SysCredit.Api")
    {
        Harmony = new Harmony(HarmonyId);
        Harmony.PatchAll(typeof(SysCreditApiPatcher).Assembly);
    }

    public static void UnpatchAll()
    {
        Harmony?.UnpatchAll(Harmony.Id);
    }
}
