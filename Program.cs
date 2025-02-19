using Raylib_cs;
using System.Numerics;

class Program {
    public static void Main() {
        int winWidth = 800;
        int winHeight = 600;
        Raylib.InitWindow(winWidth, winHeight, "rcs");
        Raylib.SetTargetFPS(60);

        UInt64 frames = 0;
        float frameTime = 0f;
        Rectangle rect = new Rectangle(100, 100, 32, 32);
        Player player = new Player(new Vector2(100, 100));

//      enemies.Add(new EnemyRanger(new Vector2(winWidth-32, 0)));
//      enemies.Add(new EnemyRanger(new Vector2(0, winHeight-32)));
//      enemies.Add(new EnemyRanger(new Vector2(winWidth-32, winHeight-32)));
        EnemyManager.enemies.Add(new EnemyRanger(new Vector2(0, 0)));
        EnemyManager.enemies.Add(new EnemyMelee(new Vector2(500, 500)));

        while (!Raylib.WindowShouldClose()) {
            frameTime = Raylib.GetFrameTime();
            Vector2 mousePos = Raylib.GetMousePosition();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.SkyBlue);

//          float distanceToPlayer = Vector2.Distance(Util.GetRectCenter(player.rect), Util.GetRectCenter(enemy));
//          float timeToTarget = (float)Math.Floor(distanceToPlayer / 10f); 
//          Vector2 predictedPlayerPos = Util.GetRectCenter(player.rect) + player.velocity * timeToTarget;

            // UPDATE
            player.Update(0);
            EnemyManager.Update(player);
            SpellManager.UpdatePlayerSpells();
            SpellManager.UpdateEnemySpells();
            CollisionManager.Update(player);

            // DRAW
            player.Draw();
            EnemyManager.Draw();
            SpellManager.DrawPlayerSpells();
            SpellManager.DrawEnemySpells();

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
            Raylib.EndDrawing();
            frames++;
        }

        Raylib.CloseWindow();
    }
}
