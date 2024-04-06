# LCTemplate
A template for making mods that implements a lot of features that I commonly use

## Config
Template for adding config options, inlcuding advanced usages
Includes a template for LethalConfig compatible configs

## Deps
Includes depndencies that are required (Hard) or optional (Soft)
This repo uses LethalConfig as a soft dependency by default

## Building
Non-release builds will copy the generated DLL into the folder specified by $PROFILE\_PATH
Release builds will copy icon.png, README.md, CHANGELOG.md, and the compiled DLL into a zip file. It will also generate the manifest.json file based on settings in the csproj file. The generated zip can be immediately uploaded to Thunderstore
Builds built with the "Launch" configuration will run a chosen command (expected to be the command to run your modded profile that the mod got copied to) to allow for quickly iterating new mod versions

## Package Additions
Additional files to include in a thunderstore package
