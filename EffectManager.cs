using Raylib_cs;
using System.Numerics;

public static class EffectManager {
    public static List<Effect> enemyEffects = []; 
    public static List<Effect> playerEffects = []; 

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

    public static void DrawEnemyEffects() {
        foreach (Effect effect in enemyEffects) {
            effect.Draw();
        }
    }
}
