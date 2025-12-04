using System.Collections.Generic;

public class FloorItemContainer
{
    public List<FloorItem> floorItems { get; set; }
    public int maxX { get; set; }
    public int maxY { get; set; }

    public FloorItemContainer(List<FloorItem> items)
    {
        floorItems = items;
        foreach (var item in items)
        {
            if (item.x > maxX)
            {
                maxX = item.x;
            }
            if (item.y > maxY)
            {
                maxY = item.y;
            }
        }
    }

    public void RemovePaperRollAt(int y, int x)
    {
        var itemToRemove = floorItems.Find(fi => fi.x == x && fi.y == y && fi.isPaperRoll);
        itemToRemove.isPaperRoll = false;
    }

    public int HowManySurroundingPaperRolls(int y, int x)
    {
        var surroundingItems = 0;
        // what are all the points surround (x,y)
        //North (x, y-1)
        var northX = x;
        var northY = y - 1;
        if (northX > 0 || northY > 0 || northX <= maxX || northY <= maxY)
        {
            var northItem = floorItems.Find(fi => fi.x == northX && fi.y == northY);
            if (northItem != null && northItem.isPaperRoll)
            {
                surroundingItems++;
            }
        }
        //NorthEast (x+1, y-1)
        var northEastX = x + 1;
        var northEastY = y - 1;
        if (northEastX > 0 || northEastY > 0 || northEastX <= maxX || northEastY <= maxY)
        {
            var northEastItem = floorItems.Find(fi => fi.x == northEastX && fi.y == northEastY);
            if (northEastItem != null && northEastItem.isPaperRoll)
            {
                surroundingItems++;
            }
        }
        //East (x+1, y)
        var eastX = x + 1;
        var eastY = y;
        if (eastX > 0 || eastY > 0 || eastX <= maxX || eastY <= maxY)
        {
            var eastItem = floorItems.Find(fi => fi.x == eastX && fi.y == eastY);
            if (eastItem != null && eastItem.isPaperRoll)
            {
                surroundingItems++;
            }
        }
        //SouthEast (x+1, y+1)
        var southEastX = x + 1;
        var southEastY = y + 1;
        if (southEastX > 0 || southEastY > 0 || southEastX <= maxX || southEastY <= maxY)
        {
            var southEastItem = floorItems.Find(fi => fi.x == southEastX && fi.y == southEastY);
            if (southEastItem != null && southEastItem.isPaperRoll)
            {
                surroundingItems++;
            }
        }
        //South (x, y+1)
        var southX = x;
        var southY = y + 1;
        if (southX > 0 || southY > 0 || southX <= maxX || southY <= maxY)
        {
            var southItem = floorItems.Find(fi => fi.x == southX && fi.y == southY);
            if (southItem != null && southItem.isPaperRoll)
            {
                surroundingItems++;
            }
        }
        //SouthWest (x-1, y+1)
        var southWestX = x - 1;
        var southWestY = y + 1;
        if (southWestX > 0 || southWestY > 0 || southWestX <= maxX || southWestY <= maxY)
        {
            var southWestItem = floorItems.Find(fi => fi.x == southWestX && fi.y == southWestY);
            if (southWestItem != null && southWestItem.isPaperRoll)
            {
                surroundingItems++;
            }
        }
        //West (x-1, y)
        var westX = x - 1;
        var westY = y;
        if (westX > 0 || westY > 0 || westX <= maxX || westY <= maxY)
        {
            var westItem = floorItems.Find(fi => fi.x == westX && fi.y == westY);
            if (westItem != null && westItem.isPaperRoll)
            {
                surroundingItems++;
            }
        }
        //NorthWest (x -1, y-1)
        var northWestX = x - 1;
        var northWestY = y - 1;
        if (northWestX > 0 || northWestY > 0 || northWestX <= maxX || northWestY <= maxY)
        {
            var northWestItem = floorItems.Find(fi => fi.x == northWestX && fi.y == northWestY);
            if (northWestItem != null && northWestItem.isPaperRoll)
            {
                surroundingItems++;
            }
        }


        return surroundingItems;
    }
}