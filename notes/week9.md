# Grid-based Simulation

## What do you need
- Generate a grid full of cells
- Cells shoud have state
    - start with indeces
- State is updated based on two things:
    - Neighborhood (bottom up)
    - Global resources/equations (top down)
    - Ideas:
        - Water flow (check if neighbors are water, make me water, lower water level of neighbor)
        - Random walks and seeing how many end up at a destination that affects a resource determining the extent to which that cell is affected by the destination.
            - Also consider, distance "as the crow flies", or optimized distance, or is there a path?
        
- Timed update (ability to control the rate)
    - Simulation steps
        - Cellular Automata store F(n+1) in a separate grid, and copy over into F when complete
    - Realtime state (monsters destroying things, various other experimental stuff like Physics balls bouncing around)
        - This can influence the automata too of course
    - Achieve simulation step timing with basic floats and if statements or coroutines
- Input, you must be able to change the state of the cells somehow
    - Detect where the mouse is on the grid, and affect that cell
- Visualize the state of the cells
    - Display text in a window?
    - Get stuff from poly.pizza? (remember to size it right!)
    - height? Perlin noise?
        ```c#
        float sampleX = (xIndex + offsetX) / scale;
        float sampleY = (yIntex + offsetY) / scale;
        float noiseValue = Mathf.PerlinNoise(sampleX, sampleY);
        ```
    - Genereate buildings?
        - For loops and cubes?
        - Recursively?
    ```c#
        void GenerateTower(int height, Vector3 scale, Vector3 position)
        {
            if (height <= 0) return;

            // Create a cube at the specified position
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.SetParent(transform);
            cube.transform.position = position;
            scale *= 0.8f;
            cube.transform.localScale = scale;

            // Recursively create the next cube above this one
            GenerateTower(height - 1, scale, position + new Vector3(0, scale.y, 0));
        }
    ```
- Roads and other connectors
    - Pathfinding

## Requirements for the Assignment

- 
