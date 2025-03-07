using Raylib_cs;
using System.Numerics;

public static class EnemyManager {
    public static List<Enemy> enemies = [];
    public static void Update(Player player) {
        for (int i = 0; i < enemies.Count; i++) {
            Enemy enemy = enemies[i];
            enemy.Update(player, 0);
            if (enemy.hp <= 0) {
                enemies.RemoveAt(i); 
                i--;
            }
        }
//      Console.WriteLine(enemies.Count);
    }

    public static void Draw() {
        foreach (Enemy enemy in enemies) {
            enemy.Draw(); 
        }
    }

    public static void Add(Enemy enemy) {
        enemies.Add(enemy);
    }
}
