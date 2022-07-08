using UnityEngine;

public class GameTile : MonoBehaviour
{
    [SerializeField] private Transform _arrow;
    [SerializeField] private GameObject _dangerSign;
    [SerializeField] private GameObject[] _diceFaces;
    private GameTileContent _content;
    
    public int Index { get; set; }
    
    public uint MineCounter { get; set; }

    public GameTileContent Content
    {
        get => _content;
        set
        {
            if (_content != null)
            {
                _content.Recycle();
            }
            _content = value;
            _content.transform.localPosition = transform.localPosition;
        }
    }

    public void HideMine()
    {
        _content.HideOrShow();
    }

    public void ShowCounter()
    {
        if (_content.Type != GameTileContent.GameTileContentType.Mine)
        {
            if (MineCounter > 0)
            {
                //_dangerSign.SetActive(true);
                ClearTitleFace();
                _arrow.gameObject.SetActive(false);
                _diceFaces[MineCounter - 1].SetActive(true);

            }
            else
            {
                ClearTitleFace();
                _arrow.gameObject.SetActive(true);
            }
        }
        else
        {
            ClearTitleFace();
            _arrow.gameObject.SetActive(true);
        }
    }

    private void ClearTitleFace()
    {
        foreach (var face in _diceFaces)
        {
            face.SetActive(false);
        }
    }

    public void ClearTitle()
    {
        MineCounter = 0;
    }
}
