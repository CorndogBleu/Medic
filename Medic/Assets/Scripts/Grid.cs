using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.UIElements;


public class Grid 
{
    int[,] grid;
    int gridWidth, gridHeight;
    float cellSize;
    TextMesh[,] debugGrid;

    public Grid(int width, int height, float cellSize, Transform parent)
    {
        gridWidth = width;
        gridHeight = height;

        grid = new int[gridWidth, gridHeight];
        debugGrid = new TextMesh [gridWidth, gridHeight];
        this.cellSize = cellSize;

        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0;y < grid.GetLength(1); y++)
            {
                debugGrid[x,y] = UtilsClass.CreateWorldText(grid[x, y].ToString(), parent,
                    getMidPoint(x,y), 10, Color.white, TextAnchor.MiddleCenter);
                Debug.DrawLine(getWorldPosition(x, y), getWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(getWorldPosition(x, y), getWorldPosition(x + 1, y), Color.white, 100f);
            }
        }

        Debug.DrawLine(getWorldPosition(0, height), getWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(getWorldPosition(width, 0), getWorldPosition(width,height), Color.white, 100f);
    }

    private Vector3 getWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize;
    }

    public void setValue(int x, int y, int value)
    {
        if (x >= 0 && x < gridWidth && y >= 0 && y < gridHeight)
        {
            grid[x, y] = value;
            debugGrid[x, y].text = value.ToString();
        }
    }

    public Vector3 snapToGrid(Transform input)
    {
        
        Vector3 vector =  new Vector3(Mathf.Round(input.position.x), Mathf.Round(input.position.y));
        
        int snap = snapOffset(input, out int xOffset);
        Debug.Log("Offset " + xOffset);

        int scale = getScale(input);
        int modX;

        if (input.localScale.x == input.localScale.y)
        {
            modX = 0;
        }
        else
        {
            modX = 1;
        }
        Debug.Log(modX);
        if (vector.x % 2 == modX)
        {
            vector.x += xOffset;
        }

        if (vector.y % 2 == 0)
            vector.y += 1;

        return vector;
    }

    private int snapOffset(Transform input, out int xOffset)
    {
        int scale = getScale(input);

        switch (scale)
        {
            case 1:
                xOffset = 1;
                return 1;
            case 2:
                xOffset = 1;
                return 3;
        }
        xOffset = -1;
        return -1;
    }

    private Vector3 getMidPoint(int x, int y)
    {
        return getWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f;
    }

    private int getScale (Transform input)
    {
        int scale;
        float x = input.localScale.x;
        float y = input.localScale.y;

        if (x > y)
        {
            //scale = (int)((int)x / y);
            scale = Mathf.RoundToInt(x / y);
            Debug.Log(scale);
        }
        else if (y > x)
        {
            scale = (int)((int)y / x);
            //Debug.Log(y / x);
        }
        else
        {
            scale = 1;
        }

        return scale;
    }
}
