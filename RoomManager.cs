using Raylib_cs;
using System.Numerics;

public static class RoomManager {
    public enum GridID {
        Limits = -1,
        Empty,
        Wall,
        Wall1,
        Wall2,
        Wall3,
        Door,
        Hazard1,
        Hazard2,
        Hazard3,
        Hazard4,
        Hazard5,
        EnemyMelee,
        EnemySpellCaster,
        EnemyTeleporter,
        EnemyBouncer,
        EnemyCharge,
        MoreCharges,
        FasterDash,
        DoubleDash,
        HomingAttack, 
        BiggerSpells,
    }
    public static List<Room> rooms = new List<Room>();
    public static Room currentRoom;
    public static Vector2 roomScreenPos = Vector2.Zero;
    public static Vector2 roomMaxScreenPos = Vector2.Zero;

    public static int rows = 20;
    public static int cols = 34;

    public static void Init() {
        roomScreenPos.X = (Raylib.GetScreenWidth() - 32 * cols)/2;   
        roomScreenPos.Y = (Raylib.GetScreenHeight() - 32 * rows)/2;    
        roomMaxScreenPos.X = (Raylib.GetScreenWidth() - 32 * cols)/2 + 32 * cols;   
        roomMaxScreenPos.Y = (Raylib.GetScreenHeight() - 32 * rows)/2 + 32 * rows;    
    }

    public static void SetCurrentRoom(Room room) {
        currentRoom = room; 
    }

    public static void DrawCurrentRoom() {
        int counter = 0;

        for (int i = 0; i < currentRoom.mat.GetLength(1)-2; i++) {
            Rectangle topGrid = new Rectangle(roomScreenPos.X + 32 + i * 32,
                                           roomScreenPos.Y + 32 - 48,
                                           32, 48);
            Rectangle botGrid = new Rectangle(roomScreenPos.X + 32 + i * 32,
                                           roomMaxScreenPos.Y - 32,
                                           32, 48);
            Raylib.DrawTexturePro(Textures.walls,
                    new Rectangle(0, 0, 32, 48),
                    topGrid,
                    Vector2.Zero,
                    0,
                    Color.White);
            Raylib.DrawTexturePro(Textures.walls,
                    new Rectangle(0, 0, 32, 48),
                    botGrid,
                    new Vector2(32, 48),
                    180,
                    Color.White);
        }

        for (int i = 0; i < currentRoom.mat.GetLength(0)-2; i++) {
            Rectangle leftGrid = new Rectangle(roomScreenPos.X,
                                           roomScreenPos.Y + 32 + i * 32,
                                           32, 48);
            Rectangle rightGrid = new Rectangle(roomMaxScreenPos.X,
                                           roomScreenPos.Y + 32 + i * 32,
                                           32, 48);
            Raylib.DrawTexturePro(Textures.walls,
                    new Rectangle(0, 0, 32, 48),
                    leftGrid,
                    new Vector2(32, 16),
                    -90,
                    Color.White);
            Raylib.DrawTexturePro(Textures.walls,
                    new Rectangle(0, 0, 32, 48),
                    rightGrid,
                    new Vector2(0, 16),
                    90,
                    Color.White);
        }
        
        // borders
        Raylib.DrawTexturePro(Textures.walls,
                new Rectangle(65, 0, 48, 48),
                new Rectangle(roomScreenPos.X - 16, roomScreenPos.Y - 16, 48, 48),
                new Vector2(0, 0),
                0,
                Color.White);

        Raylib.DrawTexturePro(Textures.walls,
                new Rectangle(65, 0, 48, 48),
                new Rectangle(roomMaxScreenPos.X - 32, roomScreenPos.Y - 16, 48, 48),
                new Vector2(0, 48),
                90,
                Color.White);
        
        Raylib.DrawTexturePro(Textures.walls,
                new Rectangle(65, 0, 48, 48),
                new Rectangle(roomScreenPos.X - 16, roomMaxScreenPos.Y - 32, 48, 48),
                new Vector2(48, 0),
                -90,
                Color.White);

        Raylib.DrawTexturePro(Textures.walls,
                new Rectangle(65, 0, 48, 48),
                new Rectangle(roomMaxScreenPos.X - 32, roomMaxScreenPos.Y - 32, 48, 48),
                new Vector2(48, 48),
                180,
                Color.White);

        for (int i = 0; i < currentRoom.mat.GetLength(1)-3; i++) {
            Rectangle topGrid = new Rectangle(roomScreenPos.X + 32 + i * 32,
                                           roomScreenPos.Y + 32,
                                           32, 32);
            Rectangle botGrid = new Rectangle(roomScreenPos.X + 32 + i * 32,
                                           roomMaxScreenPos.Y - 64,
                                           32, 32);
            Raylib.DrawTexturePro(Textures.tiles,
                    new Rectangle(0, 0, 32, 32),
                    topGrid,
                    Vector2.Zero,
                    0,
                    Color.White);
            Raylib.DrawTexturePro(Textures.tiles,
                    new Rectangle(0, 0, 32, 32),
                    botGrid,
                    new Vector2(32, 32),
                    180,
                    Color.White);
        }

        for (int i = 0; i < currentRoom.mat.GetLength(0)-2; i++) {
            Rectangle leftGrid = new Rectangle(roomScreenPos.X + 48,
                                           roomScreenPos.Y + 32 + i * 32,
                                           32, 32);
            Rectangle rightGrid = new Rectangle(roomMaxScreenPos.X - 48,
                                           roomScreenPos.Y + 32 + i * 32,
                                           32, 32);
            Raylib.DrawTexturePro(Textures.tiles,
                    new Rectangle(0, 0, 32, 32),
                    leftGrid,
                    new Vector2(32, 16),
                    -90,
                    Color.White);
            Raylib.DrawTexturePro(Textures.tiles,
                    new Rectangle(0, 0, 32, 32),
                    rightGrid,
                    new Vector2(0, 16),
                    90,
                    Color.White);
        }

        for (int i = 0; i < currentRoom.mat.GetLength(0); i++) {
            for (int j = 0; j < currentRoom.mat.GetLength(1); j++) {
                if (currentRoom.mat[i, j] == 0) {

//                  Vector2 pos = GetWorldPos(i, j);
//                  int ran = 0; //State.random.Next(4);
//                  Raylib.DrawTexturePro(Textures.tiles, 
//                                        new Rectangle(32 * ran, 0, 32, 32),
//                                        new Rectangle(pos.X, pos.Y, 32, 32),
//                                        Vector2.Zero,
//                                        0,
//                                        Color.White);
                }
            }
        }
  

//      for (int i = 0; i < currentRoom.mat.GetLength(0); i++) {
//          for (int j = 0; j < currentRoom.mat.GetLength(1); j++) {
//              Rectangle grid = new Rectangle(roomScreenPos.X + j * 32,
//                                             roomScreenPos.Y + i * 32,
//                                             32, 32);
//              Color color = Color.Blue;

//              if (currentRoom.mat[i, j] == (int)GridID.Limits) {
//                  color = Color.Yellow;
//              }
//              if (currentRoom.mat[i, j] == (int)GridID.Empty) {
//                  color = Color.Blue;
//              }
//              if (currentRoom.mat[i, j] == (int)GridID.Wall) {
//                  color = Color.Red;
//              }
//              if (currentRoom.mat[i, j] == (int)GridID.Wall1) {
//                  color = Color.Red;
//              }
//              if (currentRoom.mat[i, j] == (int)GridID.Wall2) {
//                  color = Color.Red;
//              }
//              if (currentRoom.mat[i, j] == (int)GridID.Wall3) {
//                  color = Color.Red;
//              }
//              if (currentRoom.mat[i, j] == (int)GridID.Door) {
//                  color = Color.Red;
//              }
//              if (currentRoom.mat[i, j] == (int)GridID.Hazard1) {
//                  color = Color.Gold;
//              }
//              if (currentRoom.mat[i, j] == (int)GridID.Hazard2) {
//                  color = Color.Gold;
//              }
//              if (currentRoom.mat[i, j] == (int)GridID.Hazard3) {
//                  color = Color.Gold;
//              }
//              if (currentRoom.mat[i, j] == (int)GridID.Hazard4) {
//                  color = Color.Gold;
//              }
//              if (currentRoom.mat[i, j] == (int)GridID.Hazard5) {
//                  color = Color.Gold;
//              }
//              if (currentRoom.mat[i, j] == (int)GridID.EnemyMelee) {
//                  color = Color.Gray;
//              }
//              if (currentRoom.mat[i, j] == (int)GridID.EnemySpellCaster) {
//                  color = Color.Gray;
//              }
//              if (currentRoom.mat[i, j] == (int)GridID.EnemyTeleporter) {
//                  color = Color.Gray;
//              }
//              if (currentRoom.mat[i, j] == (int)GridID.EnemyBouncer) {
//                  color = Color.Gray;
//              }
//              if (currentRoom.mat[i, j] == (int)GridID.MoreCharges) {
//                  color = Color.Pink;
//              }
//              if (currentRoom.mat[i, j] == (int)GridID.FasterDash) {
//                  color = Color.Pink;
//              }
//              if (currentRoom.mat[i, j] == (int)GridID.DoubleDash) {
//                  color = Color.Pink;
//              }
//              if (currentRoom.mat[i, j] == (int)GridID.HomingAttack) {
//                  color = Color.Pink;
//              }
//              if (currentRoom.mat[i, j] == (int)GridID.BiggerSpells) {
//                  color = Color.Pink;
//              }

//              Raylib.DrawRectangleRec(grid, color);
//              Raylib.DrawRectangleLinesEx(grid, 1f, Color.Black);
//              Raylib.DrawText(String.Format("{0}", counter), 
//                              (int)roomScreenPos.X + j * 32,
//                              (int)roomScreenPos.Y + i * 32,
//                              15,
//                              Color.Black);
//              counter++;
//          }
//      }
//      Util.PrintMatrix(currentRoom.mat);
    }

    public static bool SaveRoomFile(Room room) {
        using (var stream = File.Open("room.room", FileMode.Create))
        using (var writer = new BinaryWriter(stream)) {
            for (int i = 0; i < room.mat.GetLength(0); i++) {
                for (int j = 0; j < room.mat.GetLength(1); j++) {
                    writer.Write(room.mat[i, j]);
                }
            }
        }

        Console.WriteLine("Room saved");

        return true;
    }
    
    public static Room LoadRoomFile(string roomFile) {
       var room = new Room();

        using (var stream = File.Open(roomFile, FileMode.Open))
        using (var reader = new BinaryReader(stream)) {
            for (int i = 0; i < RoomManager.rows; i++) {
                for (int j = 0; j < RoomManager.cols; j++) {
                    room.mat[i, j] = reader.ReadInt32();
                }
            }
        }
        Console.WriteLine(roomFile + " loaded");

        return room;
    }

    public static Vector2 GetWorldPos(int i, int j) {
        return new Vector2(roomScreenPos.X + j * 32, roomScreenPos.Y + i * 32);  
    }
}
