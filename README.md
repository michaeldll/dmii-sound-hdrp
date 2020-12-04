# Aria : Gobelins DMII 1 Year Unity Project about Sound

<img src="https://scontent.fcdg2-1.fna.fbcdn.net/v/t1.15752-9/p1080x2048/128904232_381973733021611_6416746486599139642_n.jpg?_nc_cat=106&ccb=2&_nc_sid=ae9488&_nc_ohc=zLPCspBqpgAAX9uHtvV&_nc_ht=scontent.fcdg2-1.fna&tp=6&oh=f3abd1b716422c2998f2eb1a13f5d737&oe=5FEC38A3" data-canonical-src="https://scontent.fcdg2-1.fna.fbcdn.net/v/t1.15752-9/p1080x2048/128904232_381973733021611_6416746486599139642_n.jpg?_nc_cat=106&ccb=2&_nc_sid=ae9488&_nc_ohc=zLPCspBqpgAAX9uHtvV&_nc_ht=scontent.fcdg2-1.fna&tp=6&oh=f3abd1b716422c2998f2eb1a13f5d737&oe=5FEC38A3" width="400" />

## How To Install

We moved to Universal Render Pipeline during the project - Please go to branch URP to see our more recent work

### Creating Worlds

- You can use the World prefab to create you world
- You can move the Doors has you want but they needs to collide with the ground
- If you use splines for the player movement make sure the end points are correctly positionned at the doors for the transitions to be smooth : 
Door Enter : Same position on axis x and z
Door Leave : Collider position on axis x and z

### Worlds Navigation

- Create as many worlds as you want out of the associated Prefab
- Make sure to rename the World with the associated id (exemple: 'World_1' for id = 1)
- Make sure to drag and drop the worlds in Game Manager

### Important details

- Tag Player prefab as "Player".
- Tag Ground GameObject as "Ground" Layer
- Tag Virtual Player prefab as "Virtual Player".
- GetComponentsInChildren is used quite a bit to improve iteration speed, careful with GameObject parenting
- Be sure to disable "double-sided" on skybox materials - this causes issues with the Virtual Player camera
- AudioManager is unstable - if Unity becomes unresponsive and crashes, it might be the culprit. There's no fix yet.
- Be sure to set an OnClick() event in the Canvas start button
- Coding guidelines :
   - Private variables takes underscores (ex: `private bool _isActive = true`)
   - Script order:
      1. Public vars
      2. Private _vars
      3. Public Methods
      4. Private Methods
      5. Unity Hooks (`Update()`...)
