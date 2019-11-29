# Cubicle

A 2.5D Cube Figure Fighting Game using Unity as a Game Engine.

- Moddeling szenes in 3D for a more realisitc design 
- The game itself will be playable in 2D perspective and feature a classical arena battle system

***
#### Tabele of Contents
[Features to implement]
[Weapon System]
[Character System]

***

#### Features to implement:
* [ ] Character design
* [ ] Stage design
* [x] UI Design
* [ ] Finding Assets
* [x] Input System Design
* [x] Weapon System Design
* [x] Physics System

***
#### Weapon System
All weapons have basic values for 'Firing strength', 'Firing Rate' and 'Damage'
  - Damage = Damage the Weapon will do on an enemy player per projectile hit
  - Firing Strength = The velocity a projectile shot from the weapon possesess
  - Firing Rate = The timeframe which is needed for one projectile to be shot
##### RocketLauncher:
  - Moderate firing rate
  - high damage
  - explosive projectiles (which will hit all the players inside the explosive radius)
##### MiniGun:
  - Very high firing rate
  - Low damage per projectile
##### Reload:
  - will take 5 seconds to reload any weapon to the full amount of ammunition the weapon possesess
  - added Reload bar for further visula cue to show the player(s) the progress of the reload function
 
#### Character System
##### 2 Slot System
  - Each slot can hold one weapon
  - only one slot can be the 'active weapon' at a time
  - the active weapon can be changed
  - when reloading boath slots are deactiavted for any action to perform with the weapons
##### Picking up Items
  - Items can be picked up as long as they are items to pick up
  _(and as long as the character has availabilities to pick up items)_
  - Weapons that are picked up will be automatically equipped to the slot that is empty (if there is one)
  - If there is no weapon slot available the weapon can not be picked up
##### Dropping Weapons
  - Players can drop weapon whenever they have one equipped
  - The weapon that will be equipped will always be the one that is the currently active weapon
  - Dropped weapons will behavve the same way when they are being picked up again (ammunition will stay the smae)
***
Input:
Keyboard/Gamepads -> see in game description of 'how to play'
