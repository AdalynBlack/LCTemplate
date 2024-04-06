using BepInEx;
using BepInEx.Configuration;
using System.IO;

namespace Template.Config;

public static class TemplateConfig
{
	public static ConfigFile TemplateFile;

	public static ConfigEntry<type> ExampleEntry;
	
	public static void BindAllTo(ConfigFile config)
	{
		TemplateFile = config;

		// Example
		// 	Category
		ExampleEntry = TemplateFile.Bind<type>(
				"Example.Category",
				"Example Setting Title",
				"ExampleValue",
				new ConfigDescription(
					"Description of usage",
					acceptableValues: new AcceptableValueRange<type>(minValue, maxValue)));
	}
}
