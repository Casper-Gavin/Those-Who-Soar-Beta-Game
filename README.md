# CS425SeniorProject

Unity Version: 2022.3.9f1 (LTS)

# Version History
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
