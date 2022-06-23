using UnityEngine;

public class GameTile : MonoBehaviour
{
    [SerializeField] private Transform _arrow;
    [SerializeField] private GameObject _dangerSign;
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

    public void ShowCounter()
    {
        if (_content.Type != GameTileContent.GameTileContentType.Mine)
        {
            if (MineCounter > 0)
            {
                _dangerSign.SetActive(true);
                _arrow.gameObject.SetActive(false);
            }
            else
            {
                _dangerSign.SetActive(false);
                _arrow.gameObject.SetActive(true);
            }
        }
        else
        {
            _dangerSign.SetActive(false);
            _arrow.gameObject.SetActive(true);
        }
    }

    public void ClearTitle()
    {
        MineCounter = 0;
    }
}
