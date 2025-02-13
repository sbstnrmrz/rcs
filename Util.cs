using Raylib_cs;
using System.Numerics;

public static class Util {
    public static Vector2 GetRectCenter(Rectangle rect) {
        return new Vector2(rect.X + rect.Width/2, rect.Y + rect.Height/2); 
    }
}
