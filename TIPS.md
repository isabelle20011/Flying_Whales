# Flying_Whales
FOR ENVIRONMENT SETUP:
-look at the environment tab
-look at the models tab
-look at the particles tab

FOR INTERACTABLES:
-look at DamagePlayer: add the script to the object, select the trigger on the collider and then select how much time 
you want between each damage, if you want the player to die or the object to be destroyed.
-look at vPickupItem: player is able to pickup object, can set up child particle system and audioSource to interact, 
look at collectible for an example
-look at EnemyCollision: player is able to destroy object while sprinting or attacking, make sure you have a very 
large collision box, look at destroyable for an example

SETTING UP NEW SCENE:
- add the player prefab, deselect main if this is a linear level
- if linear level add the FreeLookCameraRig
- if it is a main level add the MainCamera

