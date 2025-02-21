using Raylib_cs;
using System.Numerics;

public static class EffectManager {
    public static List<Effect> enemyEffects = []; 
    public static List<Effect> playerEffects = []; 

    static bool asd(Effect effect) {
        if (effect is EffectBurn && effect.ticks > 2) {
            return true;
        }
        if (effect is EffectWater && effect.ticks > 0) {
            return true;
        }

        return false;
    }

    public static void UpdateEnemyEffects() {
        for (int i = enemyEffects.Count-1; i >= 0; i--) {
            enemyEffects[i].UpdateEnemy(enemyEffects[i].enemy);
            if (asd(enemyEffects[i])) {
                enemyEffects.RemoveAt(i);
                continue;
            }
        }
    }

    public static void DrawEnemyEffects() {
        foreach (Effect effect in enemyEffects) {
            effect.Draw();
        }
    }
}
