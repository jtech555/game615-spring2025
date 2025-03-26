using UnityEngine;

public class CellState
{
    public int x;
    public int y;
    public float height;

    // Boolean to indicate if the cell has a tree
    public bool hasTree;

    // Constructor to initialize the cell state
    public CellState()
    {
        // Randomly decide if the cell should have a tree (50% chance)
        hasTree = Random.value > 0.5f; // Random.value returns a float between 0 and 1
    }

    public CellState Clone()
    {
        return new CellState
        {
            x = this.x,
            y = this.y,
            height = this.height,
            hasTree = this.hasTree // Copy tree status when cloning
        };
    }
}
