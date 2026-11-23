# Folders Structure

Here is presented a suggestion of folders structure for Unity projects.

## Pattern

```
Assets
+--- Art
| +--- Materials
| +--- Meshes
| +--- Textures
| +--- Fonts
| +--- Animations
| +--- TerrainLayers
+--- Audio
| +--- Musics
| +--- SoundEffects
| +--- VoiceOvers
+--- Code
| +--- Scripts
| +--- Shaders
+--- Docs
+--- Level
| +--- Inputs
| +--- Physics
| +--- Prefabs
| +--- Scenes
| +--- Timelines
| +--- ScriptableObjects
+--- Settings
| +--- AudioMixer
| +--- Lighting
| +--- RenderPipeline
+--- ThirdParty
```

## Rules

1. The folder structure described in the [**Pattern**](#pattern) section MUST be used, but is not neccessary create folders that will not be used in the project. New folders or subfolders can be created as neccessary.
1. Files MUST be placed inside subfolders, respecting the allowed files types of each subfolder.
1. A few third party packages, like `Oculus Integration`, and some special folders from Unity can generate problems if placed inside another folder, so no problem leaving theses folders directly within the Assets folder.
1. Folders naming MUST follow the rules described in [Folders And Files Naming](file-naming.md)
