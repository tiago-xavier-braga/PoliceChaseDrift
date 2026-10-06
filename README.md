# Police Chase: Drift

An arcade chase game: the player drives through a city block while police cars
spawn at the farthest point and hunt them down. Every hit costs health; the run
ends when health reaches zero. A day/night cycle runs during the match, turning
on street and car lights at night. Built in Unity for the web.

**Status: shipped.** Playable in the browser on
[itch.io](https://xavigames.itch.io/), no install.

## Requirements

Unity **6000.0.60f1** — Universal Render Pipeline, Input System, WebGL build target.

## Running

Open the project in Unity and press Play from `Assets/Level/Scenes/Start.unity`.
It loads the intro cutscene, then the game; both are built from additive scenes
(`StaticEnvironment` + `DynamicEnvironment` + `Cutscene` or `Game`).

## Controls

| Action | Keys |
|---|---|
| Accelerate / brake and reverse | `W` `S` / `↑` `↓` |
| Steer | `A` `D` / `←` `→` |

## Layout

```
Assets/
  Art/          meshes, materials, textures, animations, fonts
  Audio/        music, sound effects, voice-overs
  Code/         gameplay scripts and shader graphs
  Level/        scenes, prefabs, timelines, inputs, ScriptableObjects
  Settings/     render pipeline and build profiles
  XaviGames/    shared tools: event channels, scene bundles, UI core
  ThirdParty/   LeanTween, Kenney kits, skybox, water shader
```

Organised by resource type. Systems talk through ScriptableObject event
channels (`OnGameStateChanged`, `OnCarSelected`, `OnNightChanged`, `OnReloadGame`).

## What's implemented

- Wheel-collider car physics with per-car parameters (speed, torque, steering, drift friction)
- Police bots that chase the player and back off after a collision
- Timed bot spawner, always spawning at the point farthest from the player
- Health, damage on collision, smoke when damaged, game over and restart
- Day/night cycle driving street lamps and car lights
- Destructible lamp posts with a dissolve shader
- Intro cutscene on Timeline
- Loading screen and additive scene loading
