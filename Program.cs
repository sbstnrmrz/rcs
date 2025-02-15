using Raylib_cs;
using System.Numerics;

class Program {
    public static void Main() {
        int winWidth = 800;
        int winHeight = 600;
        Raylib.InitWindow(winWidth, winHeight, "Hello World");
        Raylib.SetTargetFPS(60);

        UInt64 frames = 0;
        float frameTime = 0f;
        Rectangle rect = new Rectangle(100, 100, 32, 32);
        float f = 0f;
        Player player = new Player(new Vector2(100, 100));
        Rectangle enemy1 = new Rectangle(400, 300, 32, 32);
        List<Enemy> enemies = [];
        enemies.Add(new EnemyRanger(new Vector2(0, 0)));
//      enemies.Add(new EnemyRanger(new Vector2(winWidth-32, 0)));
//      enemies.Add(new EnemyRanger(new Vector2(0, winHeight-32)));
//      enemies.Add(new EnemyRanger(new Vector2(winWidth-32, winHeight-32)));
        enemies.Add(new EnemyMelee(new Vector2(500, 500)));

        while (!Raylib.WindowShouldClose()) {
            frameTime = Raylib.GetFrameTime();
            Vector2 mousePos = Raylib.GetMousePosition();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);
            
            Raylib.DrawRectanglePro(rect, new Vector2(16, 16), f, Color.Red); 

//          float distanceToPlayer = Vector2.Distance(Util.GetRectCenter(player.rect), Util.GetRectCenter(enemy));
//          float timeToTarget = (float)Math.Floor(distanceToPlayer / 10f); 
//          Vector2 predictedPlayerPos = Util.GetRectCenter(player.rect) + player.velocity * timeToTarget;

            f += 1f;

            player.Update(0);
            for (int i = player.spells.Count-1; i >= 0; i--) {
                Spell spell = player.spells[i];
                spell.Update(0);
                // checks out of bounds for object destruction
                if (spell.position.X - spell.radius > winWidth) {
                    player.spells.RemoveAt(i);
                    continue;
                }
                if (spell.position.X + spell.radius < 0) {
                    player.spells.RemoveAt(i);
                    continue;
                }
                if (spell.position.Y - spell.radius > winHeight) {
                    player.spells.RemoveAt(i);
                    continue;
                }
                if (spell.position.Y + spell.radius < 0) {
                    player.spells.RemoveAt(i);
                    continue;
                }
            }
//          foreach (Spell spell in player.spells) {
//              spell.Update(0);
//          }


            player.Draw();
            foreach (Spell spell in player.spells) {
                spell.Draw();
            }

            foreach (Enemy enemy in enemies) {
                enemy.Update(player, 0); 
                if (Raylib.CheckCollisionRecs(enemy.rect, player.rect)) {
                    player.invencibility = true;
                }
            }
        
            foreach (Enemy enemy in enemies) {
                enemy.Draw(); 
            }


            Raylib.DrawLineV(Util.GetRectCenter(player.rect), mousePos, Color.Black);
            Raylib.DrawRectanglePro(rect, new Vector2(16, 16), f, Color.Red); 
            Raylib.DrawCircleV(mousePos, 8, Color.Black);
            Raylib.DrawRectanglePro(enemy1, new Vector2(0, 0), 0, Color.Orange); 
//          Raylib.DrawRectanglePro(new Rectangle(predictedPlayerPos.X, predictedPlayerPos.Y, 16, 16), new Vector2(0, 0), 0, Color.Orange); 
//          Raylib.DrawCircleV(predictedPlayerPos, 8, Color.Magenta);
//          Raylib.DrawLineV(Util.GetRectCenter(enemy1), Util.GetRectCenter(player.rect), Color.Black);

            Raylib.DrawFPS(0, 0);
            Raylib.DrawText(String.Format("FRAME TIME: {0}", frameTime),
                            0, 20, 24, Color.Black);
            Raylib.DrawText(String.Format("pos: {0}", Util.GetRectCenter(player.rect)),
                            0, 40, 24, Color.Black);
//          Raylib.DrawText(String.Format("predicted: {0}", predictedPlayerPos),
//                          0, 60, 24, Color.Black);
            Raylib.DrawText(String.Format("spell coount: {0}", player.spells.Count),
                            0, 80, 24, Color.Black);



            Raylib.EndDrawing();
            frames++;
        }

        Raylib.CloseWindow();
    }
}
