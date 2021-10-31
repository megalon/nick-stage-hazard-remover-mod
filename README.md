# Stage Hazard Remover

This mod will remove or disable stage hazards in Nick All-Star Brawl!

## 🚀 Installation

### Slime Mod Manager

Download the latest version of this mod from the [Slime Mod Manager](https://github.com/legoandmars/SlimeModManager/releases/latest)!

## ℹ Usage

This mod adds an option to disable stage hazards in the stage select menu.

![GIF 10-29-2021 9-43-15 PM](https://user-images.githubusercontent.com/27714637/139522945-575806e5-e139-4f16-a048-7c372acb9b70.gif)

**Currently only available in local matches.**

## ℹ Changes

| Stage | Modifications |
|----|----|
| CatDog's House | |
| Ghost Zone | |
| Glove World | Disabled ferris wheel movement, and roller-coaster carts | 
| Harmonic Convergence | | 
| Irken Armada Invasion | |
| Jellyfish Fields | |
| Omashu | Disabled moving delivery crates | 
| Powdered Toast Trouble | Disabled platform movement and covered frying pan |
| Rooftop Rumble | |
| Royal Woods Cemetery | Disabled pop-up tombstone |
| Sewers Slam | Disabled moving sludge |
| Showdown at Teeter Totter Gulch | Disabled see-saw, swing, and horsey movement. Moved bottom blast zone down now that sandtrap is gone. |
| Space Madness |  Disabled platform movement and platforms no longer fall when standing on them |
| Technodrome Takedown | Disabled flying platform movement |
| The Dump | Disabled platform movement and stage stays in starting state |
| The Flying Dutchman's Ship | |
| The Loud House | |
| Traffic Jam | Parked bus and disabled moving cars |
| Western Air Temple | |
| Wild Waterfall | Disable log spawner, and middle and bottom log stay after first spawn |


## 🔧 Developing

This project requires `SlimeModdingUtilities.dll`! 

You can install it with `Slime Modding Utilites` via [Slime Mod Manager](https://github.com/legoandmars/slimemodmanager/releases/latest)

### Setup

Clone the project, then create a file in the root of the project directory named:

`NickStageHazardRemover.csproj.user`

Here you need to set the `GameDir` property to match your install directory.

Example:
```xml
<?xml version="1.0" encoding="utf-8"?>
<Project>
  <PropertyGroup>
    <GameDir>D:\SteamLibrary\steamapps\common\Nickelodeon All-Star Brawl</GameDir>
  </PropertyGroup>
</Project>
```

Now when you build the mod, it should resolve your references automatically, and the build event will copy the plugin into your `BepInEx\plugins` folder!
