using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Logging;
using HarmonyLib;
using Template.Patches;
using Template.Config;

namespace Template;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
[BepInDependency("ainavt.lc.lethalconfig", BepInDependency.DependencyFlags.SoftDependency)]
public class Template : BaseUnityPlugin
{
	internal static ManualLogSource HarmonyLog;
	internal static ManualLogSource TranspilerLog;

	private void Awake()
	{
		Logger.LogDebug($"Loading {PluginInfo.PLUGIN_NAME}...");

		// I prepended spaces because I hate having it right next to the colon in logs lol
		HarmonyLog = BepInEx.Logging.Logger.CreateLogSource($" {PluginInfo.PLUGIN_NAME}(Harmony})");
		TranspilerLog = BepInEx.Logging.Logger.CreateLogSource($" {PluginInfo.PLUGIN_NAME}(Transpiler)");

		Logger.LogDebug("Loading Configs...");
		TemplateConfig.BindAllTo(Config);

		Logger.LogDebug("Patching Methods...");
		Harmony.CreateAndPatchAll(typeof(PatchTemplates));

		OptionalModPatcher();

		Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_NAME} is loaded!");
	}

	private void OptionalModPatcher() {
		var pluginNames = Chainloader.PluginInfos.Keys;
		
		foreach (var name in pluginNames) {
			switch(name) {
				case "ainavt.lc.lethalconfig":
					TemplateDynamicConfig.RegisterDynamicConfig();
					break;
			}
		}

		Logger.LogInfo($"{PluginInfo.PLUGIN_NAME} modded compat is loaded!");
	}

}

