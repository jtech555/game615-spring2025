# Week 7 

People create platformer prototypes.

## What are you *DOING* when you play a platformer?

- Getting good at controlling the player's movement
    - Does your game have something to get good at?
- Defeating enemies
    - Is it interesting to defeat enemies?
- Collecting things
    - How have you made this satisfying?
- Finding secret locations (exploring)
    - Can you make the player feel clever?

## Progression in Platformers

### Reaching a goal
- Sometimes looking for secrets
- Examples:
    - Mario, Sonic, etc.

### Metroidvania
- Examples:
    - Castlevania
    - Metroid

### Survival
- How far can you get?
- Examples:
    - [Super Crate Box](https://www.youtube.com/watch?v=nnqYRM8yl-g)
    - Smash Bros?
    - Endless runners (e.g. Temple Run)

### Procedural Content
- Example Spelunky
    - [Algorithm](https://tinysubversions.com/spelunkyGen/)

## Activity

- Team up, play someone's game, discuss what it would take to make the game more satisfying. Start doing it! Report back to the group. Two rounds of this.

## Technical

Talk about tools/techniques to help make content for platformers:

- Tilemap
- Prefabs that you can edit when the game isn't running
    ```c#
        [ExecuteInEditMode]
        public class WallScript : MonoBehaviour
        {
            [SerializeField] private Vector3 size = Vector3.one;

            public Vector3 Size
            {
                get => size;
                set
                {
                    size = value;
                    UpdateSize();
                }
            }
    ```
- 

## Next Week

Platformer progression