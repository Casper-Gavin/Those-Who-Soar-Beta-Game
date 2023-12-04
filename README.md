# CS425SeniorProject

Unity Version: 2022.3.9f1 (LTS)

# Version History

## Version 13

### Version 0.13.4
- Changed CharacterType variable to CharacterTypeEnum to differentiate it from CharacterTypes

### Version 0.13.3
- Got rid of extra reticle attached to the projectile enemy
- refactored some of the reticle code

### Version 0.13.2
- Fix animator hash warning spam
- Remove unused old files
- Fix bug where shield potions only updated health visually, not actually
- Fix bug where new secondary weapon did not replace old secondary weapon
- Fix bug where wrong object pooler was deleted (no enemy bullets)

### Version 0.13.1
- Added Main Menu
- Added Pause Menu
    - Press Escape to pause
    - Pauses character rotation and gun rotation/shooting
- Added Audio Manager

# Version 12

### Version 0.12.3
- Fix enemy bullets colliding with other enemies
- Created new EnemyBullet tag to distinguish player and enemy bullets

### Version 0.12.2
- Refactored Weapon System
- Created WeaponBase abstract base class
- Create ProjectileWeapon and MeleeWeapon implementations
- Now much easier to interchange weapons for players and enemies
- Sword needs to be tested further and made available to player

### Version 0.12.1
- Fixed Component Prefabs bugs - new health script and rewards list

### Version 0.12.0
- Refactored Health System
- Created HealthBase abstract base class
- Created EnemyHealth, PlayerHealth, and ComponentHealth implementations
- Fix weapon and glow remaining on death for player
- System should be open for extension and now more cohesive

# Version 11

### Version 0.11.11
- Fix death + revive, weapon and glow still remain (DO LATER)

### Version 0.11.10
- Disable enemy projectiles from damaging each other or themselves
- Make projectiles damage player

### Version 0.11.9
- Add enemy health and UI

### Version 0.11.8
- Fix flickering gun bug

### Version 0.11.7
- Create FOV for enemy
- Bug with flickering gun direction exists*

### Version 0.11.6
- Create Enemy with gun
- Make enemy AI aim at player

### Version 0.11.5
- Create Enemy Melee Attack system
- Create Enemy Sword and mechanics

### Version 0.11.4
- Create wander state for enemies
- Make wandering enemies avoid obstacles

### Version 0.11.3
- Create patrol state for enemies
- Create follow -> patrol state
- Create path system for defining patrol areas for enemies

### Version 0.11.2
- Create follow state for enemies
- Create idle state for enemies
- Create transition between states

### Version 0.11.1
- Create and Test initial Finite State Machine

### Version 0.11.0
- Create enemy game object and animations
- Create enemy room

# Version 10

### Version 0.10.3
- Added health and shield items to vendor
- Fixed coin bug in which reloading the Unity game reset the amount of coins you had to before you bought something

### Version 0.10.2
- Added the ability for the vendor to actually sell items (only gun is implemented right now)

### Version 0.10.1
- Added logic that opens up UI shop panel dependent on player input and character vicinity to vendor

### Version 0.10.0
- Created a separate vendor room
- Added an animation to a vendor sprite

## Vesrion 9

### Version 0.9.3.1
- Fixed bug in reloading ammo

### Version 0.9.3.0
- Updated each gun's ammo to be stored when switching between guns

### Version 0.9.2
- Changed the Weapon UI to update to the equipped weapon's image

### Vesrion 0.9.1
- Edited the Weapon_Special prefab
- Created functionality to pick up a secondary weapon and switch using 1 and 2 between them

### Version 0.9.0
- Added a scriptable object Item Weapon- a new way to contain data

## Version 8

### Version 0.8.5
- Added a random chance for jars to spawn a collectable when destroyed

### Version 0.8.4
- Added a coin item prefab
- Added a CCoin script to control coin collectables
- Added a CoinManager script to keep track of coins
- Added a Coin UI to display the number of coins collected

### Version 0.8.3
- Made a shield item prefab
- Added a CShield script to control shield collectables
- Added a BonusShield animation for picking up shields

### Version 0.8.2
- Made a health item prefab
- Added a CHealth script to control health collectables
- Added a BonusHealth animation for picking up health

### Version 0.8.1
- Added Collectables base script for future use

## Version 7

### Version 0.7.7
- Added solid bodies to components to prevent walking through them

### Version 0.7.6
- Created a spike prefab that damages the player
- Added a spike animation when rotating spikes

### Version 0.7.4
- Created a chest class with reward trigger functionality
- Added a chest animation upon opening

### Version 0.7.3
- Added the ability to distinguish between damageable and replaceable components

### Version 0.7.2
- Added Box prefab

### Version 0.7.1
- Created a ComponentBase class that connects the health logic with component prefabs
- Added Jar prefab

### Version 0.7.0.1
- Checked useablility of the GameObject Brush

### Version 0.7.0.0
- Added chest prefab and Tilemap_Components - halfway through Tile Brush video

## Version 0.6

### Version 0.6.8
- Added gun recoil animation

### Version 0.6.7
- Added collision particle effects for bullets

### Version 0.6.6
- Added muzzle particle effect to gun

### Version 0.6.5
- Removed player rotation
- Made bullets visible above floor
- Add bullet collision with walls

### Version 0.6.4
- Created bigger dungeon room
- Created hitboxes for walls
- Moved reticle to top layer

### Version 0.6.3
- Created tile rule and logic to place tiles correctly

### Version 0.6.2
- Temporary initial dungeon room built

### Version 0.6.1
- Added tile maps for floor/walls design

## Version 0.5
### Version 0.5.3
- Added CameraShake event
    - Attach the CameraShake script to the projectiles being fired

### Version 0.5.2
- Added Camera Shake (Press G button)

### Version 0.5.1
- Added Camera Follow

## Version 0.4
### Version 0.4.7
- Fixed incorrect projectile rotation
- Fixed weapon layering

### Version 0.4.6
- Added ReturnToPool script
    - This script returns the bullet to the pool after a set amount of time

### Version 0.4.5
- Added spread to the bullets

### Version 0.4.4
- Added shooting functionality

### Version 0.4.3
- Added a bullet prefab
- Added Projectile script

### Version 0.4.2
- Added ObjectPooling script
- Game creates a pool of bullets on start for bar gun

### Version 0.4.1
- Added SingleShotWeapon script
- Changed bar gun to use SingleShotWeapon script

## Version 0.3
### Version 0.3.9
 - fixed weapon rotation bug
### Version 0.3.8
 - Created Ammo UI

### Version 0.3.7
 - Made the weapon point in the reticle's direction
 - Changed flip motion to be based off weapon angle

### Version 0.3.6
 - Added a reticle aim feature

### Version 0.3.5
 - Added a recoil function
 - Added some left/right facing checks

### Version 0.3.4
 - Added reloding ammo functionality
 - Added a needed cooldown between shots

### Version 0.3.3
 - Added ammo to gun

### Version 0.3.2
 - Gave the player the current weapon
 - Added shoot and reload functions

### Version 0.3.1
 - Added basic CharacterWeapon class behavior with weapon object

### Version 0.3.0
 - Added gun prefabs

## Version 0.2
### Version 0.2.9
 - Added player highlight
 - Added shield and heart icons
 - Added a player layer

### Version 0.2.8
 - Implemented shield bar and behavior 

### Version 0.2.7
 - Added health number to UI

### Version 0.2.6
 - Connected health bar image to player health
 - Set health bar to update using a lerp for smoothness

### Version 0.2.5
 - Added singleton templated class
 - Made UIManager a singleton

### Version 0.2.4
 - Added initial health bar UI
 - Add UIManager script

### Version 0.2.3
 - Remove movement while dead
 - Reset health to initial health on revive

### Version 0.2.2
 - Added revive functionality after death (press 'P')
 - Added spawn position for revive
 - Created a Manager folder and LevelManager script

### Version 0.2.1
 - Added functionality to take damage (press 'L')
 - Added death, disabled collisions/texture on death

### Version 0.2.0
 - Added a Health folder and script
 - Setup initial and max health

## Version 0.1
### Version 0.1.9
- Added functionality for animations to play when the player is moving and idling
- Added StringToHash for animation variable name

### Version 0.1.8
- Added a CharacterFlip script
- Implemented flipping the character sprite based on movement direction

### Version 0.1.7 
- Added a CharacterDash script
- Implemented dashing

### Version 0.1.6
- Added a CharacterRun script
- Implemented running

### Version 0.1.5
- Added a CharacterMovement script
- Refactored the PlayerController script to use the CharacterMovement script
- Updated PlayerController and CharacterAbility scripts
- Implemented walking

### Version 0.1.4
- Added Core and Components folders
- Added a Character Ability controller

### Version 0.1.3
- Added a Time.FixedDeltaTime to the player movement
- Added a variable to the player movement to control the speed

### Version 0.1.2 
- Added a Character script
- Added RigidBody2d and BoxCollider2D to player
- Added basic movement function to player

### Version 0.1.1
- Added Script, Sprite, and Prefab folders
- Imported ***temporary*** player sprites
- Created a basic scene with a player and a camera
- Created and added a PlayerController script to the player

### Version 0.1.0
- Initial Setup of Project
