public enum TileType
{
    RED,
    GREEN,
    NONE
}

public class CarpetTile
{
    public bool IsEdge { get; set; }
    public TileType Type { get; set; }

    public CarpetTile(TileType type, bool isEdge)
    {
        Type = type;
        IsEdge = isEdge;
    }

}