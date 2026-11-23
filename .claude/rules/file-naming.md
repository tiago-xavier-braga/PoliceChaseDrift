# Folders And Files Naming

This doc presents a guide for naming folders and files.

## Folders Naming

1. The name MUST be written in english.
1. The name MUST NOT contain **spaces** or **special characteres**.
1. The name MUST be written using PascalCase.

## Files Naming

In general the files naming MUST follow the rules described below. Some assets types will have a prefix or suffix, in those case consider the rules below for the part that is not a prefix nor suffix.

1. The name MUST be written in english.
1. The name MUST NOT contain **spaces** or **special characteres**.
1. The name MUST be written using PascalCase.
1. If the file type is one of the one described in the [**Files With Prefix**](#files-with-prefix) section or [**Files With Suffix**](#files-with-suffix) MUST use the required prefix and/or suffix.

### Files With Prefix

* **Texture**: TXT_
* **Material**: MAT_

### Files With Suffix

* **Textures**: For better differentiate types of texture, each type of texture have a specific prefix:
  - Base Map File: _BaseColor
  - Metallic Map: _Metallic
  - Normal Map: _Normal
  - Height Map: _Height
  - Occlusion Map: _Occlusion
  - Emission Map: _Emissive
  - Generic Image: _Img
  
* **Clip**: To better differentiate clip types, each type should have a specific prefix:
  - SFX_: Sound effects, such as machine sounds, button clicks, environmental feedback, and alerts.
  - Music_: Background music, ambient tracks, and looped melodies.
  - Voice_: Voiceovers, including narrations, dialogues.
