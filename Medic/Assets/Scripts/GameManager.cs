using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Grid grid;
    int count = 0;

    [SerializeField]
    private int  width, height;
    [SerializeField]
    private float cellSize;

    public GameObject gameObject;

    // Start is called before the first frame update
    void Start()
    {
        grid = new Grid(width, height, cellSize, transform);
        gameObject.transform.position = grid.snapToGrid(gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = grid.snapToGrid(gameObject.transform);
    }
}
