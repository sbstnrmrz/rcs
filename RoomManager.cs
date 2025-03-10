using Raylib_cs;
using System.Numerics;
using System.Text.RegularExpressions;

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
    public static Vector2 roomScreenSize = Vector2.Zero;
    public static bool currentRoomHasEnemies = false;
    public static int currentRoomEnemyCount = 0;
    public static bool roomCleaned;
    public static bool doorsOpened = false;
    public static bool doorsLeft = false;
    public static bool doorsRight = false;
    public static bool doorsTop = false;
    public static bool doorsBot = false;
    public static Rectangle topWall1;
    public static Rectangle topWall2;
    public static Rectangle botWall1;
    public static Rectangle botWall2;
    public static Rectangle leftWall1;
    public static Rectangle leftWall2;
    public static Rectangle rightWall1;
    public static Rectangle rightWall2;
    public static float portalRadius = 50;
    public static bool portalActive = false;
    public static int portalAnimationFrames = 0;
    public static int portalCurrentSprite = 0;
    public static Rectangle portalRect = new Rectangle(0, 0, 61*3, 60*2);
    public static Vector2 portalRectSize = new Vector2(61*2, 60*2);
    public static int portalYOff = 0;

    public static int rows = 20;
    public static int cols = 34;

    public static Rectangle topDoor = new Rectangle(roomScreenPos.X + (cols/2-1) * 32, roomScreenPos.Y, 32*2, 32);
    public static Rectangle botDoor = new Rectangle(roomScreenPos.X + (cols/2-1) * 32, roomScreenPos.Y + (rows-1) * 32, 32*2, 32);
    public static Rectangle leftDoor = new Rectangle(roomScreenPos.X, roomScreenPos.Y + (rows/2-1) * 32, 32, 32*2);
    public static Rectangle rightDoor = new Rectangle(roomScreenPos.X + (cols-1) * 32, roomScreenPos.Y + (rows/2-1) * 32, 32, 32*2);


    public static void Init() {
//      string pattern = @"[^/]+$";
        string pattern = @"[^/]+\.room$";
        string roomDirectory = "./";
        string[] files = Directory.GetFiles(roomDirectory);

        foreach (string file in files) {
            if (Regex.IsMatch(file, pattern)) {
                rooms.Add(LoadRoomFile(file));
            }
        }

        roomScreenPos.X = (Raylib.GetScreenWidth() - 32 * cols)/2;   
        roomScreenPos.Y = (Raylib.GetScreenHeight() - 32 * rows)/2;    
        roomMaxScreenPos.X = (Raylib.GetScreenWidth() - 32 * cols)/2 + 32 * cols;   
        roomMaxScreenPos.Y = (Raylib.GetScreenHeight() - 32 * rows)/2 + 32 * rows;    
        roomScreenSize.X = 32 * cols; 
        roomScreenSize.Y = 32 * rows; 
    }

    public static void OpenDoors() {
        doorsOpened = true;
        doorsLeft = true;
        doorsRight = true;
        doorsTop = true;
        doorsBot = true;

        for (int i = rows/2; i < rows/2+3; i++) {
            if (doorsLeft) {
               currentRoom.mat[i, 0] = (int)GridID.Door; 
            }
            if (doorsRight) {
               currentRoom.mat[i, cols-1] = (int)GridID.Door; 
            }
        }

        for (int i = cols/2; i < cols/2+3; i++) {
            if (doorsTop) {
               currentRoom.mat[0, i] = (int)GridID.Door; 
            }
            if (doorsBot) {
               currentRoom.mat[rows-1, i] = (int)GridID.Door; 
            }
        }
        
    }

    public static void StartNewRoom(Player player, Room room) {
        player.pos = GetWorldPos(rows/2, cols/2);
        currentRoom = GetRandomRoom(); 
        currentRoom.Init();
        currentRoomEnemyCount = room.enemyCount;
        Console.WriteLine("current room info:");
        Console.WriteLine("  enemy count: " + currentRoomEnemyCount);

        roomCleaned = false;
        portalActive = false;
        portalCurrentSprite = 0;
        portalAnimationFrames = 0;
    }

    public static void SetCurrentRoom(Room room) {
        currentRoom = room; 
        currentRoom.Init();
        currentRoomEnemyCount = room.enemyCount;
        Console.WriteLine("current room info:");
        Console.WriteLine("  enemy count: " + currentRoomEnemyCount);
    }

    public static void Update(Player player) {
        currentRoomEnemyCount = EnemyManager.enemies.Count;
        if (currentRoomEnemyCount < 1 && !roomCleaned) {
            roomCleaned = true;
            portalActive = true;
            Console.WriteLine("room cleaned");
        } 

 //     Console.WriteLine("portal: " + portalActive);
 //
        if (portalActive) {
            if (portalAnimationFrames > 0 && portalAnimationFrames % 5 == 0) {
                portalCurrentSprite++;
            }

            portalAnimationFrames++;
            if (portalCurrentSprite > 4) {
                portalCurrentSprite = 0;
                portalYOff++;
                if (portalYOff > 2) {
                    portalYOff = 0;
                }
            }
        }
//      if (portalActive) {
//          if (portalAnimationFrames > 0 && portalAnimationFrames % 15 == 0) {
//              portalCurrentSprite++;
//          }

//          portalAnimationFrames++;
//          if (portalCurrentSprite > 7) {
//              portalCurrentSprite = 4;
//          }
//      }

//      if (roomCleaned && !portalActive) {
//          if (portalAnimationFrames > 0 && portalAnimationFrames % 15 == 0) {
//              portalCurrentSprite++;
//          }

//          portalAnimationFrames++;
//          if (portalCurrentSprite > 3) {
//              portalActive = true;
//          }
//      }
    }

    public static void DrawCurrentRoom() {
        int counter = 0;


        Raylib.DrawTexturePro(Textures.floor,
                              new Rectangle(0, 0, 1280, 720),
                              new Rectangle(0, 0, 1280*2, 720*2),
                              Vector2.Zero,
                              0,
                              Color.White);

        portalRect.X = GetPortalPos().X - portalRect.Width/2;
        portalRect.Y = GetPortalPos().Y - portalRect.Height/2;
        if (portalActive) {
            Raylib.DrawCircleV(new Vector2((roomScreenPos.X + roomScreenSize.X)/2, (roomScreenPos.Y + roomScreenSize.Y)/2), portalRadius, Color.Purple);
            Raylib.DrawTexturePro(Textures.portal, 
                                  new Rectangle(portalCurrentSprite * 62, 61 * portalYOff, 61, 60),
                                  portalRect,
                                  Vector2.Zero,
                                  0,
                                  Color.White);
            Raylib.DrawRectangleLinesEx(portalRect, 1f, Color.Black);
        }

        Raylib.DrawLineEx(new Vector2(0, GetPortalPos().Y), new Vector2(Raylib.GetScreenWidth(), GetPortalPos().Y), 1f, Color.Black);
        Raylib.DrawLineEx(new Vector2(GetPortalPos().X, 0), new Vector2(GetPortalPos().X, Raylib.GetScreenHeight()), 1f, Color.Black);


        for (int i = 0; i < 15; i++) {
            Raylib.DrawTexturePro(Textures.tree,
                                  new Rectangle(0, 0, 82, 97),
                                  new Rectangle(i * 80, -90, 82*2, 97*2),
                                  Vector2.Zero,
                                  0,
                                  Color.White);
            Raylib.DrawTexturePro(Textures.tree,
                                  new Rectangle(0, 0, 82, 97),
                                  new Rectangle(i * 80, 50 + 5 * 120, 82*2, 97*2),
                                  Vector2.Zero,
                                  0,
                                  Color.White);
        }
        for (int i = 0; i < 6; i++) {
            Raylib.DrawTexturePro(Textures.tree,
                                  new Rectangle(0, 0, 82, 97),
                                  new Rectangle(-30, 30 + i * 120, 82*2, 97*2),
                                  Vector2.Zero,
                                  0,
                                  Color.White);

            Raylib.DrawTexturePro(Textures.tree,
                                  new Rectangle(0, 0, 82, 97),
                                  new Rectangle(20 + 14 * 80, 30 + i * 120, 82*2, 97*2),
                                  Vector2.Zero,
                                  0,
                                  Color.White);
        }



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

    public static Room GetRandomRoom() {
        return rooms[State.random.Next(rooms.Count)];
    }

    public static Vector2 GetWorldPos(int i, int j) {
        return new Vector2(roomScreenPos.X + j * 32, roomScreenPos.Y + i * 32);  
    }

    public static Vector2 GetPortalPos() {
        return new Vector2(roomScreenPos.X + roomScreenSize.X/2, roomScreenPos.Y + roomScreenSize.Y/2);
    }

    public static bool PlayerEnteredPortal(Player player) {
        if (RoomManager.portalActive) {
            if (Raylib.CheckCollisionRecs(player.rect, portalRect)) {
//          if (Raylib.CheckCollisionCircles(player.GetPosition(), player.hitRadius, RoomManager.GetPortalPos(), RoomManager.portalRadius)) {
                Console.WriteLine("player collision with portal");
                return true;
            }
        }

        return false;
    }

}
