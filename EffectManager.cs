using Raylib_cs;
using System.Net;
using System.Numerics;

public static class EffectManager {
    public static List<Effect> enemyEffects = []; 
    public static List<Effect> playerEffects = []; 
    public static List<Effect> worldEffects = [];

    static bool asd(Effect effect) {
        if (effect is EffectBurn && effect.ticks > 2) {
//          Console.WriteLine("qqqqqq");
            return true;
        }
        if (effect is EffectWater && effect.ticks > 4) {
            return true;
        }
        if (effect is EffectSlow && effect.ticks > 5) {
//          Console.WriteLine("SexooOooOOoo");
            return true;
        }
        if (effect is EffectLighting && effect.ticks > 1) {
//          Console.WriteLine("MiAU");
            return true;
        }

        return false;
    }

    public static void UpdatePlayerEffects() {
        for (int i = playerEffects.Count-1; i >= 0; i--) {
            playerEffects[i].UpdatePlayer(playerEffects[i].player);
            if (asd(playerEffects[i])) {
                playerEffects.RemoveAt(i);
                continue;
            }
        }
    }

    public static void UpdateEnemyEffects() {
        for (int i = enemyEffects.Count-1; i >= 0; i--) {
            enemyEffects[i].UpdateEnemy(enemyEffects[i].enemy);
            if (asd(enemyEffects[i])) {
                enemyEffects[i].enemy.effects--;
                enemyEffects.RemoveAt(i);
                continue;
            }
        }
    }

    public static void UpdateWorldEffects() {
        for (int i = worldEffects.Count-1; i >= 0; i--) {
            worldEffects[i].UpdateWall();
            if (asd(worldEffects[i])) {
                worldEffects.RemoveAt(i);
                continue;
            }
        }
    }



    public static void DrawPlayerEffects() {
        foreach (Effect effect in playerEffects) {
            effect.Draw();
        }
    }

    public static void DrawEnemyEffects() {
        foreach (Effect effect in enemyEffects) {
            effect.Draw();
        }
    }

    public static void DrawWorldEffects() {
        foreach (Effect effect in worldEffects) {
            effect.Draw();
        }
    }
}
