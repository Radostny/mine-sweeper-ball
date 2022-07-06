using DefaultNamespace;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Vector2Int _boardSize;
    [SerializeField] private GameBoard _board;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameTileContentFactory _contentFactory;

    private Ray _touchRay => _camera.ScreenPointToRay(Input.mousePosition);
    
    void Start()
    {
        _board.Initialize(_boardSize, _contentFactory);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleTouch();
        }

        if (Input.GetMouseButtonDown(1))
        {
            _board.LogTileInfo(_touchRay);
        }
        
    }

    private void HandleTouch()
    {
        GameTile tile = _board.GetTile(_touchRay);
        if (tile != null)
        {
            _board.ToggleMine(tile);
            //tile.Content = _contentFactory.Get(GameTileContentType.Mine);
        }
    }
}