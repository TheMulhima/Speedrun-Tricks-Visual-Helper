# Speedrun-Tricks-Visual-Helper
A mod that changes the color of the knight to indicate when they can do an action which can help learning/doing skips. It shows when fireball, dash, or wall jumping are available and also when the knight is facing left or right (to see turnarounds). You can switch what action causes the color change by pressing the key that was binded to ChangeModeKey in the GlobalSettings file.

# How its meant to be used
It is meant to be used as a indicator for how well you are doing the skips **after** you have finished them. It is not for helping do the skips in real time because: 1) reaction time and 2) input lag.

# How to use
- Download the [Modding API](https://github.com/TheMulhima/Speedrun-Tricks-Visual-Helper/blob/master/README.md#downloading-modding-api). 
- From the releases tab, download the version that corresponds to your game version ([Current Patch](https://github.com/TheMulhima/Speedrun-Tricks-Visual-Helper/releases/download/v1.0.0.1/Speedrun_Tricks_Visual_Helper.dll) or [1.2.2.1](https://github.com/TheMulhima/Speedrun-Tricks-Visual-Helper/releases/download/v1.0.0.1-1.2.2.1/Speedrun_Tricks_Visual_Helper_1221.dll)) and place it in the [Mods folder](https://github.com/TheMulhima/Speedrun-Tricks-Visual-Helper/blob/master/README.md#mods-folder).
- Run the game once with the mod installed and verify that it is installed by seeing if it appears on the top left of the title screen. Then close the game and open the .json file in the [saves folder](https://github.com/TheMulhima/Speedrun-Tricks-Visual-Helper/blob/master/README.md#saves-folder).
- Add a number or lower case letter in `ChangeModeKey` to add a key bind for changing modes. e.g.  `ChangeModeKey = "b"`.
- Open the game and press the bound key to switch between modes.

# Modes Currently Available:
1. Turnaround:
  - It shows a different color when facing right and left
  - e.g. of use -> To help identify how much you're spending drifting in the wrong direction.
2. Fireball
  - It shows when you can fireball.
  - e.g. of use -> Show if you're wasting frames while doing fireball skips.
3. Dash
  - It shows when you can dash.
  - e.g. of use -> practicing timing dashes.
  - Note: if the color doesn't change when you chain dashes, it means you dashed within 1 frame after completing the dash (so that's the optimal dash timing). If you see white flashes, your timing is a bit off.
4. WallJump
  - It shows when you can wall jump
  - e.g. of use -> QGA skip

# Additional Options Explanation
- Current_Mode_Name_AlwaysVisible -> if false, the "Mode : {Mode}" Text disappears after 3 seconds. if true, stays there forever.
- Color_true -> Color of knight when you can't do something.
- Color_false -> Color of knight when you can't do something.
- Turnaround_OnlyinAir -> if true, only changes color in the air.
- Fireball_OnlyinAir -> if true, only changes color in the air.
- Dash_OnlyonGround -> if true, only changes color while on the ground.
- Show_CanFireball_InLogs_While_InTurnAround_Mode-> If in turnaround mode, it writes in the ModLog.txt when you can or can't fireball. Basically allows you to have both modes at the same time.
  - (CP only) To turn on ingame logging go to saves folder and in `ModdingApi.GlobalSettings.json` change the value `ShowDebugLogInGame` to true and then use f10 to toggle it on and off.
        
# Notes (So it doesnt clog up the readme)
## To get to the Hollow knight folder / hollow_knight.app:
- Steam: Go to your Library -> Right Click Hollow Knight -> Click on Properties-> Local Files -> Browse.
- GoG: Select the symbol next to the play button -> Click on Manage Installation -> Show Folder.

## Mods Folder
- Windows: `Hollow Knight\hollow_knight_Data\Managed\Mods`.
- Mac: `hollow_knight.app\contents\Resources\Data\Managed\Mods`.

## Saves Folder
- Windows: `%APPDATA%\..\LocalLow\Team Cherry\Hollow Knight\`.
- Mac: `~/Library/Application Support/unity.Team Cherry.Hollow Knight/`.

## Downloading Modding API
- If on current patch:
  - Use the Modinstaller. 
- if 1.2.2.1:
  - Download the latest Modding API from [here](https://cdn.discordapp.com/attachments/822611561427370054/835911691703156746/Assembly-CSharp.dll)
  - If on windows:
    - Place it in `Hollow Knight\hollow_knight_Data\Managed`.
  - If on mac:
    - Place it in `hollow_knight.app\contents\Resources\Data\Managed\`.
