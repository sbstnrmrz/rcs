using Raylib_cs;
using System.Numerics;

public static class RoomManager {
    public enum GridID {
        None,
        Wall,
        Door,
        Rock,
        EnemySpellcaster,
        EnemyMelee,
        EnemyBouncer,
    }
    public static List<Room> rooms = new List<Room>();
    public static Room currentRoom;
    public static Vector2 roomScreenPos = Vector2.Zero;

    public static int rows = 20;
    public static int cols = 34;

    public static void Init() {
        roomScreenPos.X = (Raylib.GetScreenWidth() - 32 * cols)/2;   
        roomScreenPos.Y = (Raylib.GetScreenHeight() - 32 * rows)/2;    
    }

    public static void SetCurrentRoom(Room room) {
        currentRoom = room; 
    }

    public static void DrawCurrentRoom() {
        int counter = 0;
        for (int i = 0; i < currentRoom.mat.GetLength(0); i++) {
            for (int j = 0; j < currentRoom.mat.GetLength(1); j++) {
                Color color = Color.Blue;
                if (currentRoom.mat[i, j] == (int)GridID.Wall) {
                    color = Color.Gold;
                }
                if (currentRoom.mat[i, j] == (int)GridID.Door) {
                    color = Color.Gray;
                }
                if (currentRoom.mat[i, j] == (int)GridID.Rock) {
                    color = Color.Black;
                }
                Rectangle grid = new Rectangle(roomScreenPos.X + j * 32,
                                               roomScreenPos.Y + i * 32,
                                               32, 32);
                Raylib.DrawRectangleRec(grid, color);
                Raylib.DrawRectangleLinesEx(grid, 1f, Color.Black);
                Raylib.DrawText(String.Format("{0}", counter), 
                                (int)roomScreenPos.X + j * 32,
                                (int)roomScreenPos.Y + i * 32,
                                15,
                                Color.Black);
                counter++;
            }
        }
    }
}
