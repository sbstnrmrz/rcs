using Raylib_cs;
using System.Numerics;

public static class EnemyManager {
    public static List<Enemy> enemies = [];
    public static void Update(Player player) {
        for (int i = enemies.Count-1; i >= 0; i--) {
            Enemy enemy = enemies[i];
            enemy.Update(player, 0);
            if (Raylib.CheckCollisionRecs(enemy.rect, player.rect)) {
                player.invencibility = true;
            }
            if (enemy.hp <= 0) {
                enemies.RemoveAt(i); 
            }
        }
    }

    public static void Draw() {
        foreach (Enemy enemy in enemies) {
            enemy.Draw(); 
        }
    }
}
