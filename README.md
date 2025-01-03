# CS 425/426 Senior Project

### Find final build at https://github.com/carterawebb/CS425SeniorProject/releases/tag/v1.4.31
### Carter Webb, Andrew Kalb, and Gavin Casper
### Instructors: Dave Feil-Seifer, Devrin Lee, and Sara Davis with the Department of CSE
### External Advisor: Eelke Folmer with the Department of CSE
### CS 426 Senior Project in Computer Science, Spring 2024, at UNR, CSE Department

## Project Description
Those Who Soar is an innovative top-down roguelike game, combining the immersive elements of adventure and strategy within our 2D art style. Our game is developed for our Capstone Project for CS 426 at UNR. This game represents a well-balanced blend of technical skill, creative design, and engaging gameplay.

Those Who Soar is set in a world where players navigate through levels, each presenting unique challenges and opportunities, say, a new enemy or a new weapon. The game's core mechanics revolve around player movement, and smart combat. Players can choose from a selection of weapons, each with distinct characteristics, to combat enemies. This variety ensures that each playthrough offers a new and challenging experience.

Some key features of our game include an intuitive UI, robust tile and level-building tools, and breakable objects that add depth to the game environment. The inclusion of a vendor system also allows players to upgrade their abilities and equipment, providing a sense of progression and personalization. The game's health and damage system are finely tuned to offer a balanced level of difficulty, keeping players engaged but not overwhelmed.

Significant post-concept additions have enhanced the game's appeal. These include the development of a complex enemy AI, the creation of main and pause menus for a seamless user experience, and an audio manager to enrich the game's auditory environment. Planned future enhancements include the addition of diverse levels, an expanded arsenal of weapons, varied enemy types, at least one formidable boss, immersive sound effects, and lore to deepen the narrative experience.

Those Who Soar is not just a game for us, it's a testament to the team's learning journey in Unity game development. It embodies our commitment to creating an engaging, entertaining, and challenging experience for players as newcomers to game programming. As we continue to refine and expand our project, we aim to push the boundaries of what can be achieved by a team of emerging game developers.

## Project Related Resources
S. Rogers, Level up : the guide to great video game design, 2nd ed. Chichester: Wiley, 2014.

[https://forum.unity.com/](https://forum.unity.com/)

[https://www.udemy.com/](https://www.udemy.com/)

[https://assetstore.unity.com/](https://assetstore.unity.com/)

Unity, “Unity Documentation,” docs.unity.com. [https://docs.unity.com/](https://docs.unity.com/) The Unity Documentation website is extremely useful to our group, particularly because the team does not have a lot of experience interacting with Unity. It also has a link to an asset store, where we may consider purchasing professional looking assets at a later time.

A. Hussain, H. Shakeel, F. Hussain, N. Uddin, and T. Latif Ghouri, “(PDF) Unity Game Development Engine: A Technical Survey,” ResearchGate, Jul. 2020. [https://www.researchgate.net/publication/348917348_Unity_Game_Development_Engine_A_Technical_Survey](https://www.researchgate.net/publication/348917348_Unity_Game_Development_Engine_A_Technical_Survey) (accessed Nov. 01, 2023). Unity is one of the most popular game engines used for video game development globally. For that reason, Team 1 has selected Unity to use for development, and chosen this article to act as a reference in case we have questions about the abilities and limitations of Unity as we develop.

S. Aleem, L. F. Capretz, and F. Ahmed, “Game development software engineering process life cycle: a systematic review,” Journal of Software Engineering Research and Development, vol. 4, no. 1, Nov. 2016, doi: [https://doi.org/10.1186/s40411-016-0032-7](https://doi.org/10.1186/s40411-016-0032-7.) This scholarly article contains information about game development, including tips on how to plan, prototype, and move along in the development process. As we move through this project, it will be helpful to apply practices and test practices that are clearly defined in this article.

## More Resources
"Learn to create a 2D Action Roguelike Game in Unity 2022", a Udemy Course by Gianny Dantas.
Since none of us had prior experience in Unity, we followed this Udemy Course to help us get a grip on the fundamental components of game design, and Unity in general. This course served as a foundation for our project, currently and hereafter we are building off of it.

[https://creators.aiva.ai/](https://creators.aiva.ai/)
AIVA is an AI music composer that we used to generate the music for our game (at least for now). We used a base composition (song) and then modified it to fit our game using
AIVA's modification tools.

Unity Version: 2022.3.9f1 (LTS)

# Version History
## Version 1.4
### Version 1.4.31
- Added a death menu (tells you to press "P" when you die to respawn)

### Version 1.4.30
- Fixed? Sliding on death bug as per Instructor feedback

### Version 1.4.29
- Fixed dialogue not properly going away on first pause when a menu UI was open
- Added a Full Reset at the very start of the game just once in case something doesnt get reset

### Version 1.4.28
- new attempt at fixing the vendor menu crash

### Version 1.4.27
- Fixed? DialogueManager crash when the dialogue was displaying and you leave to main menu

### Version 1.4.26
- Disabled enemy gun while teleporting for ghost enemy

### Version 1.4.25
- Fixed crash where the player was dead when the teleporting enemy tried to teleport

### Version 1.4.24
- Fixed not going to Main Menu after beating boss

### Version 1.4.23
- Fixed crash when going to menu with shop panel open

### Version 1.4.22
- Fix bug where torch would become loud occasionally if you went into the game after already having the torch from the main menu

### Version 1.4.21
- Fixed a bug where the player ammo would not be full on picking up a new gun
- Reworked ammo system entirely
- Fixed another bug where bullets would disappear as soon as the player would holster their projectile weapon

### Version 1.4.20
- Fixed bug where dialogue would cause issues when it was open and you paused the game.
- Fixed bug where torch wouldnt get reset when it was bought and the player reset their progress

### Version 1.4.19
- Fixed a bug where you could force re-equip a weapon you already had equipped (minor visual appearance issue)
- Fixed an assert that would pop up when dialogue was finished on the lore scene

### Version 1.4.18
- Changed wording in lore scene dialogue to follow button prompts accurately
- Made reset button (correctly) affect the current scene
- Current Scene is not set back to default on reopening the game anymore

### Version 1.4.17
- Fixed a bug where you could not rebind keys in-game due to time being paused

### Version 1.4.16
- Fixed a bug where the reset progress didn't work if the mainmenu was unloaded at least once
- STILL A BUG: reset progress does not reset the saved level scene
- Fixed a crash where the torch was not found in the lore scene (blacklist lore scene for torch requirements)


### Version 1.4.15
- Fix bugs where the input manager would still be listening for key presses after escaping the input control menu

### Version 1.4.14
- Stopped ammo count from being kept through scenes

### Version 1.4.13
- Fixed gun shooting opposite direction when equipped (equipping weapon resets cooldown)

### Version 1.4.12
- Added a GameManager to send the player to the correct scene and handle player prefs
- Added reset progress button to main menu

### Version 1.4.11
- Fixed shield and health increase logic and matched the skill tree advertisements
- Probably fixed sliding on death and movement on respawn
- Fix cursor invisible bug in the level select menu

### Version 1.4.10
- Fixed a bug where you could not shoot in the boss area on level 5
- Make deagle bullets do more damage (2x normal bullets)
- Fixed a bug where bullets dealt damage to bulletproof enemies due to the skill tree

### Version 1.4.9
- Fixed Level Select crash bug
- Fixed unintended effect of player skipping dialogue and being able to dash in the tutorial (advance sentence has been changed from Space to Enter)

### Version 1.4.8
- Fixed a bug where the torch would cause crashes on the main menu
- Probably fixed a bug where the game would not start (on clicking play) due to skillMenu not being initialized early enough
- Increased fire rate for the BAR weapon, and increased item size on the ground

### Version 1.4.7
- Cleanup walls on tutorial scene
- Add items to the vendor area
- Extend black area to cover entire tutorial
- Blacklist W,A,S,D,Mouse0,1,2 from key bindings
- Tutorial is finished?

### Version 1.4.6
- Added bullet proof enemies to levels 2, 4, and 5

### Version 1.4.5
- Fixed Skill Points not loading when entering a level until you open the skill menu
- Small fix for the level select button not playing a clicking sound when you select it

### Version 1.4.4
- Fixed boss door with no key bug
- Fixed vendor canvas in level one

### Version 1.4.3
- Added Level Select menu to main menu
- Fixed Reset to Defaul button for Controls Menu

### Version 1.4.2
- Fixed tutorial bullet bug

### Version 1.4.1
- Added a door to boss room so you can't leave

### Version 1.4.0
- Added level clear to tutorial
- Fixed tutorial vendor canvas in shadows

## Version 1.3
### Version 1.3.13
- Add special coloring to damage indicators for critical hits

### Version 1.3.12
- Add sprite for bulletproof enemy

### Version 1.3.11
- Add bulletproof melee enemy
- Add regenerating enemy

### Version 1.3.10
- Added the two other guns in levels (bar in 1, special in 5)
- Created Weapon Bar item
- Finished Levels 4 and 5
- Added basic game clear functionality (use OnBossDead event)

### Version 1.3.9
- Skill Levels for each skill now save through quitting

### Version 1.3.8
- Finished up (maybe) input manager
    - Input Manager is now avaliable in any scene (main menu or level)
    - You can set any key to any other key as long as it isnt already being used by another key
    - You can reset all keys to default

### Version 1.3.7
- Begin ghost enemy implementation
- Note: Put ghost enemy in large empty space
- Bug: enemy should flip with weapon
- TODO: make invulnerable during teleport phase

### Version 1.3.6
- Finished most of level 4 and level 5
- Waiting for new enemies to finish level 4
- Still need to check if level 5's boss works

### Version 1.3.5
- Added Input Manager
    - Certain buttons now use the manager
    - Can be set (through the ScriptableObject for now)

### Version 1.3.4
- Fix torch transfer between levels and error spam
- Fix enemy bomber weapon error spam

### Version 1.3.3
- Unfinished fix to the torch
- Changed the tutorial text boxes over to dialogue boxes

### Version 1.3.2
- Fixed traps colliding with bullets

### Version 1.3.1
- Added camping projectile enemies to level 3
- Added ability to flip direction of enemies on prefab placement (change x scale to -1, and it should fix enemy behavior and weapon)

### Version 1.3.0
- Created level three
- Still needs unmoving projectile enemies to replace regular ones

## Version 1.2
### Version 1.2.15
- Fix skill tree damage + critical hit chance (25%)

### Version 1.2.14
- Add initial damage indicators
    - They are ugly, but functional

### Version 1.2.13
- First version of lore scene
    - Added pixel art
    - Changed around story
- Fixed Torch Bug on First Level

### Version 1.2.12
- Lore Scene Functionality

### Version 1.2.11
- Created level two
- Added level clear scene functionality
- Tested adding a key to chests - didn't work (not a big deal)

### Version 1.2.10
- Increase key item size and in UI
- Add red, green, blue keys and doors

### Version 1.2.9.2
- Made own sounds for the Dialogue Audio and Implemented it

### Version 1.2.9
- Restructuring Audio Stuff
- Add Randomized/Predictable Dialogue Audio

### Version 1.2.8.2
- Fixed dialogue box not showing up
- Added LoreScene

### Version 1.2.8
- Added Dialogue System
    - Attached Dialogue System to Vendor
- Changed quality settings

### Version 1.2.7.2
- Changed Main Menu background
- Changed text on Main Menu
    - Updated color gradient
    - Gave outline
- Added background wind sfx to main menu

### Version 1.2.7
- Bunch of torch refining
    - Added a variability to the darkness and length of view distance to make it seem like an actual torch
    - Add a bunch of null object checks to stop from getting errors when transferring scenes
    - Added torch to every scene for future when coming back into the game after quitting
    - Made the torch follow more closely/quickly
- Changed the height of the character so they can fit more places better
- Added fire sound effect for the torch
    - The volume is variable, so when you are close, the torch is louder

### Version 1.2.6
- Updated torch to singleton
    - What happens if we buy the torch, go to another scene, quit (saving the game), then come back? Do we still have the torch?
- Transfered Vendor changed to LevelTwoScene - duplicated original LevelTwo just in case

### Version 1.2.5
- Added torch to VendorCanvas

### Version 1.2.4
- Added effects to torch
- Added torch to buy menu

### Version 1.2.3
- Added torch
- Changed original black mask black-ness
- Changed Vendor to a Prefab
- Added more rays to cast for the FOV effect so there isnt as much rigid movements around corners

### Version 1.2.2
- Put in vendor framework for torch - Torch collectable, scriptable object, and instance in vendor
    - no actual functionality/ doing stuff
- Added LevelTwoScene

### Version 1.2.1
- Made enemies shoot slower
- Changed walking and running sounds
    - Still unsure about how they sound

### Version 1.2.0
- Add key/door system
- Add keys to UI, scale with UI (underneath bars container)
- TODO: test with multiple keys
- TODO: make key asset match game quality + resolution
- TODO: add to map somehow, include in shader

## Version 1.1
### Version 1.1.19
- Added some fake walls to the tutorial
- Adjusted fake wall sprite to be more obvious
- Added a few enemies to level one

### Version 1.1.18
- Fix boss health bar visibility for in and outside the area
- Fix bug where boss would not die

### Version 1.1.17
- Made Skill Tree Damage Additions apply to weapon projectiles (not just applying to the sword as it was before)

### Version 1.1.16
- Fixed reload bugs (could reload at full ammo, could reload during a reload)

### Version 1.1.15
- Fixed menu bugs around skill menu and submenus (pausing, not being able to press escape)


### Version 1.1.14
- Boss health bar disappers after you revive
- Made and edited prefabs to fix bugs across scenes

### Version 1.1.13
- Fixed Tutorial Scene not having correct GameObjects
- Converted many GameObjects that are conencted to a single transferable Prefab
- Added fullscreen button in game

### Version 1.1.12
- Fix sfx on tutorial scene menus
- Unify volume across scenes (fix bug where volume would keep increasing)

### Version 1.1.11
- Clean up boss health code + errors
- Fix projectile enemy gun flickering
- Fix enemy AI to make deep copies instead of sharing decision making scripts
    - This means that we can now have many enemies without their AI messing up
- Fix projectile enemies staring at dead player
- Fix boss music
- Fix menu button click sfx

### Version 1.1.10
- Fix revive bug on level 1

### Version 1.1.9
- Fixed a fake wall bug and edited level one
- Fixed boss intro interfering with pause menu

### Version 1.1.8
- Increase Boss Health
- Fix build issue where boss health wouldnt show

### Version 1.1.7
- Volume Consistency

### Version 1.1.6
- Fixed issue where certain GameObjects werent updated
- Fixed errors
- Fixed skill tree not displaying costs

### Version 1.1.5
- Added unit test for music (current music)

### Version 1.1.4
- Fix Error bug relating to when skill menu is not active
- Added "Test" button to main menu to get to testing scene

### Version 1.1.3
- Add unit test for enemy health (take damage)

### Version 1.1.2
- Add unit test for weapon ammo (consume ammo)

### Version 1.1.1
- Fixed a few level one issues after peer testing

### Version 1.1.0
- Added LevelOneScene to the build
- Created level one
- Added a level clear event - still needs functionality to go to new level (like the pause menu?)

## Version 1.0
### Version 1.0.9
- Fix bug where player projectiles did not work in boss area
- Adjust sword position/size for melee enemies
- Fix bug where projectile enemy would continue shooting after player died
- Shrink projectile weapon for player and bring slightly closer

### Version 1.0.8
- Fix bug where walk/run sfx continued on player death
- Fix bug where player could slide during death animation
- Comments with projectile bug - boss area is enemy layer????

### Version 1.0.7
- Added all other sound effects that I made or gathered
- Walking/Running Logic change - Sound still does not work

### Version 1.0.6.2
- Small Fix on audio:
    - Fixed problem with game music not playing
    - Got walking/running sounds to play but they were very inconsistent and dont work most of the time

### Version 1.0.6.1
- Broke, then - partially - fixed, audio manager:
    - Only Main Menu music and Dash sound effects will play

### Version 1.0.6
- Added new sound effect sounds (most not implemented)
- Bug where you can hear implemented sounds

### Version 1.0.5
- Fix other menu buttons not highlighting when hovered over
- Made volume transfer between scenes
- Added Boss Music for when you enter the boss zone

### Version 1.0.4
- Fix dead state bug from pressing 'esc' on the main menu options submenu
- Fix bug where pause menu options submenu returned to game not pause menu
- Fix bug where cursor was invisible on main menu options submenu in build

### Version 1.0.3
- Added fake wall components
- Started on level one

### Version 1.0.2
- Fixed tutorial-specific bugs

### Version 1.0.1
- Fix bug where revive ('p') during death animation would not revive player
- Add camera snap on player revive ('p')

### Version 1.0.0
- Built project (first release on github)
- Fix cursor visibility in menus
- Fix fullscreen game button

## Version 0.17
### Version 0.17.3
- Fixed damage not transferring bug
- Fixed Crit Chance, Skill Damage bug
- New death sprite

### Version 0.17.2
- Updated sprites for
    - Player
    - Main Enemy
    - Sword
- Shifted WeaponHolder Position

### Version 0.17.1
- Pushed Skill Points saving bug 
- Pushed Options and SkillTree menu fixes for when you exited out of them
- Added Crit logic
- Moved skill tree damage code
- Crit does not work just like skill tree
    - Something is wrong with the TakeDamage function or how we are applying the damage with the weapons. It never takes the damageToEnemy variable, only the damage varaible

## Version 0.16
### Version 0.16.14
- Add weapon bounce

### Version 0.16.13
- Fix invisible player projectiles

### Version 0.16.12
- Fix bomber assert

### Version 0.16.11
- Gave boss less projectiles but increased their speed
- Increased time to start shield regeneration

### Version 0.16.10
- Make weapon image bigger

### Version 0.16.9
- Downscale health potion image
- Fix bug where shield regenerated during death animation
- Fix bug where bomber did not die
- Fix bug where coin, ammo, sp text was invisible (local bug?)

### Version 0.16.8
- Added experience to gain skill points

### Version 0.16.7
- Fixed Enemy UI and Weapons being visible over shader

### Version 0.16.6
- Fixed Skill Tree damage issue

### Version 0.16.6
- finished boss
- addded fade ability to canvas groups

### Version 0.16.5
- added more boss intro functionality

### Version 0.16.4
- added boss camera

### Version 0.16.3
- add shield regen to player

### Version 0.16.2
- cleanup particle systems after bomber explodes

### Version 0.16.1
- fixed Character Skills and SkillTree persistency  issue

## Version 0.15
### Version 0.15.14
- added BossEvent
- needs camera work

### Version 0.15.13
- boss health bar added to UI (currently permanent)
- boss can now take damage/die

### Version 0.15.12
- added collision between player and projectiles
- added boss phase functionality based on player health
- added boss wandering state and detect player decision
- got rid of and adjusted various functions in AI files

### Version 0.15.11
- added boss's spiral shot pattern
- addes boss's random shot pattern

### Version 0.15.10
- added base functionality for boss's circle shot

### Version 0.15.9
- created BossProjectile script

### Version 0.15.8
- started on boss enemy
- explosion animation

### Version 0.15.7
- fixed FOV bug where enemies still show up

### Version 0.15.6
- Add box shake when hit

### Version 0.15.5
- Fix bomber bug temp : need to set skillMenu (null reference exception)

### Version 0.15.4
- Added Field Of View affect
- BUG INTRODUCED - Bomber will not explode after red flashing and stops moving

### Version 0.15.3
- Added Skill Tree openability through "T" button and through the pause menu
- Added Tutorial scene accessibility through the main menu

### Version 0.15.2
- Added Character Skills (for skill tree)
    - Character can get more damage
    - Character can get more health
        - I want to implement different sprites for how much health you have
    - Character can get more shield
        - I want to implement different sprites for how much shield you have
    - Character can now move faster
    - Character can now dash further
- Added initial implementation of skill tree
    - Still need to add a button to pull it up
    - Still need to add a section for it on the pause menu
    - Still need to implement lvl and skill point system to be able to get skill points (SP)
- Updated UI
- Updated Canvas to scale with screen size
    - Updated UI to scale with screen size

### Version 0.15.1
- Add initial death animation
- Need to remove crosshair and weapon?

### Version 0.15.0
- minor tutorial wall fix

## Version 0.14
### Version 0.14.16
 - Changed all current gun weapons to not have automatic reload

### Version 0.14.15
- Added the Tutorial Scene

### Version 0.14.14.2
- Fixed a bug where melee enemies would hit the player more than once on a single swing

### Version 0.14.14.1
- Decrease number of melee enemies spawned
- Remove debug logs around bomber

### Version 0.14.14
- Fix ammo text (was too large, on two lines)
- Increase sword attack rate for player

### Version 0.14.13
- Add bomber enemy functionality
    - Missing explosion particle system asset

### Version 0.14.12.1
- More UI Updates
    - Added background to pause menu and options menus

### Version 0.14.12
- UI Update
    - Changed Health Bar and Background
    - Changed Shield Bar and Background
    - Changed Coin placement
    - Changed In-Game UI Background
    - Changed Weapon Background

### Version 0.14.11
- Add bomber enemy initial implementation

### Version 0.14.10
- Fix sword bug for damaging an enemy multiple times with the same stroke
- Fix double shooting/attacking (not setting cooldown at right spot)
- Don't play attack animation on weapon equip (for sword, left it for guns)
    - ParticleSystem's are ticked to Play on Awake, can't untick without losing the effect completely
    - Attack animation was set to true by default in the unity animator

### Version 0.14.9.5
- Got rid of all recoil

### Version 0.14.9
- Added color change to vendor cost text - yellow for can buy, red for can't buy

### Version 0.14.8
- Player Sword now effects components (jars, boxes)

### Version 0.14.7
- Stopped player from being able to buy health/shield when at full health/shield from vendor
- Added IsFullHealth function for use
- Changed default item cost text to red

### Version 0.14.6
- Fix camera stuck on vendor mode
- Make sword damage enemies, default weapon

### Version 0.14.5
- Add basic sword support for player
- Add support for separate weapon images for the UI equipped weapon visualization.

### Version 0.14.4
- Updated camera smoothing
    - Camera now follows player less smoothly
    - Camera now follows the player more closely when dashing
- Camera now shares the distance between it a specified target to make it easier to see (vendor for now, will prolly add boss when it is added)
- Added a camera zoom out when the player is near a target (vendor for now, will prolly add boss when it is added)

### Version 0.14.3
- Add camera smoothing
    - Can adjust the damping value (lower is smoother, higher is more responsive)
- First version of player to vendor camera transition
    - Need to add a slide to where the vendor is
    - Might want to zoom the camera out a bit

### Version 0.14.2
- Change AudioManager to use a singleton

### Version 0.14.1
- Add primitive damage indicator

### Version 0.14.0
- Create initial spawner prefab for more efficient stage building, particularly involving spawning enemies
- Spawned objects should not overlap
- Spawned objects should remain within the bounds defined in the spawner prefab
- If a spawned object collides 10 times during placement, the spawned object will not be spawned (not enough room inferred)

## Version 0.13
### Version 0.13.6
- Added an option to change the volume of the music in the pause menu

### Version 0.13.5
- Added a dash cooldown timer

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

# Version 0.12
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

# Version 0.11
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

# Version 0.10
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

## Vesrion 0.9
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

## Version 0.8
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

## Version 0.7
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
