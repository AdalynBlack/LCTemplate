// Required for all harmony patches
using HarmonyLib;

// Required for Transpilers
using System.Collections.Generic;
using System.Reflection.Emit;

// Define our namespace for organizational purposes
namespace Template.Patches;

[HarmonyPatch(typeof(ClassToPatch))]
public class PatchTemplates
{
	[HarmonyPatch("MethodToPrefix")] // The method in ClassToPatch that should be patched
	[HarmonyPrefix]
	// Return type may be void or bool. Bool allows you to cancel the original call by returning false
	// __instance allows you to access the "this" variable from the original call
	static bool PrefixTemplate(ClassToPatch __instance)
	{
		__instance.RunImportantFunctionHere();
		return false;
	}

	// Patches may also be written as such to patch a specific class other than the one previously specified
	[HarmonyPatch(typeof(OtherClassToPatch), "MethodToPostfix")]
	[HarmonyPostfix]
	static void PostfixTemplate(OtherClassToPatch __instance)
	{
		__instance.DoSomethingHereAfterThisFinishes();
	}

	[HarmonyPatch("MethodToTranspile")
	[HarmonyTranspiler]
	// ILGenerator only required for creating labels
	static IEnumerable<CodeInstruction> TranspilerTemplate(IEnumerable<CodeInstruction>, ILGenerator generator)
	{
		return new CodeMatcher(instructions, generator)
			// Match from the current position (first instruction by default) until the following instruction(s) are found
			.MatchForward(false,
					new CodeMatch(
						// Call a function, pulling values currently on the evaluation stack and using them as arguments for the function being called.
						OpCodes.Call,

						// AccessTools.Method is a good way to quickly find a specific method using reflection
						AccessTools.Method(typeof(ClassDefiningMethodCall), "MethodName",
							// Parameters must be specified for functions with multiple overloads
							parameters: new type[] {
								typeof(FirstParameterType),
								typeof(SecondParameterType)},

							// Specific generic types must be specified for functions with generics
							generics: new type[] {
								typeof(FirstGenericType),
								typeof(SecondGenericType)
							})),

					// Another instruction for demonstration purposes
					new CodeMatch(OpCodes.Ldlen))

			// Create a label at the current cursor position to jump to later. The cursor will be created before the call function in the previous match section. This call fails if the CodeMatcher wasn't given an ILGenerator during its construction
			.CreateLabel(out var ChosenLabel)

			// Return back to the start of the function
			.Start()
			
			// Insert one or more functions at the current cursor position (before the first instruction in this case)
			.InsertAndAdvance(
					// Branch to the label we created before
					new CodeInstruction(OpCodes.Br_S, ChosenLabel))

			// Convert the CodeMatcher into the IEnumerable<CodeInstruction> required for the return type
			.InstructionEnumeration();
	}
}
