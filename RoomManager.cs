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
    public static Rectangle portalRect = new Rectangle(0, 0, 27*3, 40*2);
    public static Vector2 portalRectSize = new Vector2(27*2, 40*2);

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

        Console.WriteLine("portal: " + portalActive);
        if (portalActive) {
            if (portalAnimationFrames > 0 && portalAnimationFrames % 15 == 0) {
                portalCurrentSprite++;
            }

            portalAnimationFrames++;
            if (portalCurrentSprite > 7) {
                portalCurrentSprite = 4;
            }
        }

        if (roomCleaned && !portalActive) {
            if (portalAnimationFrames > 0 && portalAnimationFrames % 15 == 0) {
                portalCurrentSprite++;
            }

            portalAnimationFrames++;
            if (portalCurrentSprite > 3) {
                portalActive = true;
            }
        }
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
            if (i < cols || i > cols+3 && !doorsTop) {
                Raylib.DrawTexturePro(Textures.walls,
                        new Rectangle(0, 0, 32, 48),
                        topGrid,
                        Vector2.Zero,
                        0,
                        Color.White);
            }

            if (i < cols || i > cols+3 && !doorsBot) {
                Raylib.DrawTexturePro(Textures.walls,
                        new Rectangle(0, 0, 32, 48),
                        botGrid,
                        new Vector2(32, 48),
                        180,
                        Color.White);
            }
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

        portalRect.X = GetPortalPos().X - portalRect.Width/2;
        portalRect.Y = GetPortalPos().Y - portalRect.Height/2;
        if (portalActive) {
            Raylib.DrawCircleV(new Vector2((roomScreenPos.X + roomScreenSize.X)/2, (roomScreenPos.Y + roomScreenSize.Y)/2), portalRadius, Color.Purple);
            Raylib.DrawTexturePro(Textures.portal, 
                                  new Rectangle(portalCurrentSprite * 28, 0, 27, 40),
                                  portalRect,
                                  Vector2.Zero,
                                  0,
                                  Color.White);
            Raylib.DrawRectangleLinesEx(portalRect, 1f, Color.Black);
        }

        if (doorsTop) {
            Raylib.DrawRectangleRec(new Rectangle(roomScreenPos.X + (cols/2-1) * 32, roomScreenPos.Y, 32*2, 32), Color.Yellow);
        }
        if (doorsBot) {
            Raylib.DrawRectangleRec(new Rectangle(roomScreenPos.X + (cols/2-1) * 32, roomScreenPos.Y + (rows-1) * 32, 32*2, 32), Color.Yellow);
        }
        if (doorsLeft) {
            Raylib.DrawRectangleRec(new Rectangle(roomScreenPos.X, roomScreenPos.Y + (rows/2-1) * 32, 32, 32*2), Color.Yellow);
        }

        if (doorsRight) {
            Raylib.DrawRectangleRec(new Rectangle(roomScreenPos.X + (cols-1) * 32, roomScreenPos.Y + (rows/2-1) * 32, 32, 32*2), Color.Yellow);

        }

        Raylib.DrawLineEx(new Vector2(0, GetPortalPos().Y), new Vector2(Raylib.GetScreenWidth(), GetPortalPos().Y), 1f, Color.Black);
        Raylib.DrawLineEx(new Vector2(GetPortalPos().X, 0), new Vector2(GetPortalPos().X, Raylib.GetScreenHeight()), 1f, Color.Black);
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
