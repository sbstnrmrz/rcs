using Raylib_cs;
using System.Numerics;

class Program {
    public static void Main() {
        int winWidth = 1280;
        int winHeight = 720;
        Raylib.InitWindow(winWidth, winHeight, "rcs");
        Raylib.InitAudioDevice();
        Raylib.SetTargetFPS(60);
        Raylib.DisableCursor();

        UInt64 frames = 0;
        float frameTime = 0f;
        Rectangle rect = new Rectangle(100, 100, 32, 32);
        Player player = new Player(new Vector2(100, 100));
        Random random = new Random();

        State.Init();

        RoomManager.Init();
        RoomManager.StartNewRoom(player, RoomManager.GetRandomRoom());

        Music music = Raylib.LoadMusicStream("assets/run_music2.mp3");
        Raylib.PlayMusicStream(music);
        Raylib.SetMusicVolume(music, 0.0f);

        while (!Raylib.WindowShouldClose()) {
            Raylib.UpdateMusicStream(music);
            frameTime = Raylib.GetFrameTime();
            Vector2 mousePos = Raylib.GetMousePosition();

            // UPDATE
            if (RoomManager.PlayerEnteredPortal(player)) {
                RoomManager.StartNewRoom(player, RoomManager.GetRandomRoom());
            }
            RoomManager.Update(player);
            player.Update(0);
            EnemyManager.Update(player);
            SpellManager.UpdatePlayerSpells();
            SpellManager.UpdateEnemySpells();
            CollisionManager.Update(player);
            EffectManager.UpdatePlayerEffects();
            EffectManager.UpdateEnemyEffects();
            EffectManager.UpdateWorldEffects();
            State.UpdateCamera();

            // DRAW
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.SkyBlue);
            Raylib.BeginMode2D(State.camera);

            RoomManager.DrawCurrentRoom();
            player.Draw();
            EnemyManager.Draw();
            SpellManager.DrawPlayerSpells();
            SpellManager.DrawEnemySpells();
            EffectManager.DrawPlayerEffects();
            EffectManager.DrawEnemyEffects();
            EffectManager.DrawWorldEffects();

            Raylib.DrawFPS(0, 0);
//          Raylib.DrawText(String.Format("FRAME TIME: {0}", frameTime),
//                          0, 20, 24, Color.Black);
//          Raylib.DrawText(String.Format("pos: {0}", Util.GetRectCenter(player.rect)),
//                          0, 40, 24, Color.Black);
//          Raylib.DrawText(String.Format("predicted: {0}", predictedPlayerPos),
//                          0, 60, 24, Color.Black);
//          Raylib.DrawText(String.Format("spell coount: {0}", player.spells.Count),
//                          0, 80, 24, Color.Black);

            SpellManager.DrawDebugInfo();
            Raylib.EndMode2D();
            Raylib.EndDrawing();
            frames++;
        }

        Raylib.CloseWindow();
    }
}
