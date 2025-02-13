using Raylib_cs;
using System.Numerics;

class Program {
    public static void Main() {
        Raylib.InitWindow(800, 600, "Hello World");
        Raylib.SetTargetFPS(60);

        UInt64 frames = 0;
        float frameTime = 0f;
        Rectangle rect = new Rectangle(100, 100, 32, 32);
        float f = 0f;
        Player player = new Player(new Vector2(100, 100));
        Rectangle enemy = new Rectangle(400, 300, 32, 32);

        while (!Raylib.WindowShouldClose()) {
            frameTime = Raylib.GetFrameTime();
            Vector2 mousePos = Raylib.GetMousePosition();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);
            
            Raylib.DrawRectanglePro(rect, new Vector2(16, 16), f, Color.Red); 

            float distanceToPlayer = Vector2.Distance(Util.GetRectCenter(player.rect), Util.GetRectCenter(enemy));
            float timeToTarget = (float)Math.Floor(distanceToPlayer / 10f); 
            Vector2 predictedPlayerPos = Util.GetRectCenter(player.rect) + player.velocity * timeToTarget;

            f += 1f;

            player.Update(0);
            foreach (Spell spell in player.spells) {
                spell.Update(0);
            }

            player.Draw();
            foreach (Spell spell in player.spells) {
                spell.Draw();
            }
            Raylib.DrawLineV(Util.GetRectCenter(player.rect), mousePos, Color.Black);
            Raylib.DrawRectanglePro(rect, new Vector2(16, 16), f, Color.Red); 
            Raylib.DrawCircleV(mousePos, 8, Color.Black);
            Raylib.DrawRectanglePro(enemy, new Vector2(0, 0), 0, Color.Orange); 
//          Raylib.DrawRectanglePro(new Rectangle(predictedPlayerPos.X, predictedPlayerPos.Y, 16, 16), new Vector2(0, 0), 0, Color.Orange); 
            Raylib.DrawCircleV(predictedPlayerPos, 8, Color.Magenta);
            Raylib.DrawLineV(Util.GetRectCenter(enemy), Util.GetRectCenter(player.rect), Color.Black);


            Raylib.DrawFPS(0, 0);
            Raylib.DrawText(String.Format("FRAME TIME: {0}", frameTime),
                            0, 20, 24, Color.Black);
            Raylib.DrawText(String.Format("pos: {0}", Util.GetRectCenter(player.rect)),
                            0, 40, 24, Color.Black);
            Raylib.DrawText(String.Format("predicted: {0}", predictedPlayerPos),
                            0, 60, 24, Color.Black);



            Raylib.EndDrawing();
            frames++;
        }

        Raylib.CloseWindow();
    }
}
