using BepInEx.Configuration;
using LethalConfig;
using LethalConfig.ConfigItems;
using LethalConfig.ConfigItems.Options;
using System.Collections;
using System.Collections.Generic;

namespace Template.Config;

internal static class TemplateDynamicConfig
{
	internal static void RegisterDynamicConfig()
	{
		AddConfigItems(new BaseConfigItem[] {
				new FloatSliderConfigItem(TemplateConfig.ExampleEntry,
						new FloatSliderOptions {
							RequiresRestart = false,
							Min = minValue,
							Max = maxValue})
				});
	}

	internal static void AddConfigItems(IEnumerable<BaseConfigItem> configItems)
	{
		foreach (var item in configItems)
		{
			LethalConfigManager.AddConfigItem(item);
		}
	}
}
