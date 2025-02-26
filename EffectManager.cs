using Raylib_cs;
using System.Net;
using System.Numerics;

public static class EffectManager {
    public static List<Effect> enemyEffects = []; 
    public static List<Effect> playerEffects = []; 
    public static List<Effect> worldEffects = [];

    static bool asd(Effect effect, float hp) {
        if (effect is EffectBurn && effect.ticks > 2) {
            return true;
        }
        if (effect is EffectWater && effect.ticks > 4) {
            return true;
        }
        if (effect is EffectSlow && effect.ticks > 5) {
            return true;
        }
        if (effect is EffectLighting && effect.ticks > 1) {

            return true;
        }
        if (effect is EffectStun && effect.ticks > 2) {
          Console.WriteLine(effect.frames);
            return true;
        }

        if (hp < 0) {
            return true;
        }

        return false;
    }

    public static void UpdatePlayerEffects() {
        for (int i = playerEffects.Count-1; i >= 0; i--) {
            playerEffects[i].UpdatePlayer(playerEffects[i].player);
            if (asd(playerEffects[i], 1)) {
                playerEffects.RemoveAt(i);
                continue;
            }
        }
    }

    public static void UpdateEnemyEffects() {
        for (int i = enemyEffects.Count-1; i >= 0; i--) {
            enemyEffects[i].UpdateEnemy(enemyEffects[i].enemy);
            if (asd(enemyEffects[i], enemyEffects[i].enemy.hp)) {
                enemyEffects[i].enemy.effects--;
                enemyEffects.RemoveAt(i);
                continue;
            }
        }
    }

    public static void UpdateWorldEffects() {
        for (int i = worldEffects.Count-1; i >= 0; i--) {
            worldEffects[i].UpdateWorld();
            if (asd(worldEffects[i], 1)) {
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
