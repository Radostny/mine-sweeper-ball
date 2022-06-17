using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Vector2Int _boardSize;
    [SerializeField] private GameBoard _board;
    [SerializeField] private Camera _camera;

    private Ray _touchRay => _camera.ScreenPointToRay(Input.mousePosition);
    //visualize mf to see how it actually works on screen

    void Start()
    {
        _board.Initialize(_boardSize);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleTouch();
        }
        
    }

    private void HandleTouch()
    {
        GameTile tile = _board.GetTile(_touchRay);
    }
}