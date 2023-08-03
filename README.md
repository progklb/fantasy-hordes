# Fantasy Hordes

![Image](./Documentation/HEADER.png)

A fantasy themed, horde fighting, hack and slash game.

This project is a work-in-progress and will grow and evolve dynamically. All documented aspects are subject to change. See the [Notion board here](https://aspiring-end-9a9.notion.site/Fantasy-Hordes-838f3c1fb48c49f3933368a95d0d04cf?pvs=4)

## Project Structure

The following folder hierarchy convention is to be used for sub-projects within this repo:

```txt
+-- Assets
|   +-- _PROJECT              // Contains project-specific assets
|   +-- Submodules            // Contains all included submodules
|   +-- [Third-party asset]   // Various third-party assets
```

## Accreditation

- **Toon Fantasy Nature**: Fantasy nature assets used for environments.
  - [Asset Store link](https://assetstore.unity.com/packages/3d/environments/landscapes/toon-fantasy-nature-215197) | [Developer link](https://www.facebook.com/SICSgames)
- **Footsteps Sounds Pack**: Character footsteps sounds asset pack for various terrain types.
  - [Asset Store link](https://assetstore.unity.com/packages/audio/sound-fx/foley/footsteps-sound-pack-165660) | [Developer link](http://www.cafofomusic.com/)

## Systems

![Image](./Documentation/PlayerRig.png)

### Player

The `Assets/_PROJECT/Content/Characters/Prefabs/Player` prefab is a proxy object that holds generic character components. At runtime, the `PlayerCharacter` component spawns a full character prefab.

There are a number of visible gizmos here, as shown in the screenshot above:

- The outermost gizmo is the player's audio source.
- The second-outer most gizmo is the player's navigation mesh agent.
- The third is the player's trigger collider, used for detecting contact with objects in the world (e.g. intersecting with the mesh the player is standing on/in, in order to determine the footstep audio type. Note that this is offset slightly downward, as to intersect with the floor).
- The inner-most collider is a rigidbody collider for affecting objects when colliding with them (e.g. pushing a chair out of the way when running into it).

[Mixamo](https://www.mixamo.com/) has been used for charater animations. All animations are contained in the `Assets/Mixamo` folder, and [this](https://www.youtube.com/watch?v=9H0aJhKSlEQ) tutorial was used to set up the character animations with the Synty Studios characters contained in `Assets/PolygonFantasyCharacters/Prefabs`. Note that these animation contain animation events for triggering audio/effects.

Player movement is handled by Unity's navigation mesh system. When the player clicks on the map, a raycast is generated by the `InputManager` to determine the point on the map that has been clicked. The `InputManager` then raises an event containing this position, and the `PlayerCharacter` listens for this and feeds the position to the `NavigationAgent` to begin navigation.

### Audio

For footstep sounds, characters raise an animation event at the correct time in their animation loop. This notifies the `PlayerCharacter` to play their footstep sound. The correct sound is actively assigned/updated in the `AudioController` based on the surface type that the character is running on - which is handled by `AudioBankTrigger`s in the scene.

> When implementing/change character animations, remember to update the animation events for audio.
