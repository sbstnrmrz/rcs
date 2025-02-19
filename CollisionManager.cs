using Raylib_cs;
using System.Numerics;

public static class CollisionManager {
    public static void Update(Player player) {
        for (int i = SpellManager.enemySpells.Count-1; i >= 0; i--) {
            Spell spell = SpellManager.enemySpells[i];
            if (Raylib.CheckCollisionCircleRec(spell.pos, spell.hitboxRadius, player.hitbox)) {
                player.hp--;
                SpellManager.enemySpells.RemoveAt(i);
            }
        }
        for (int i = SpellManager.playerSpells.Count-1; i >= 0; i--) {
            Spell spell = SpellManager.playerSpells[i];
            foreach (Enemy enemy in EnemyManager.enemies) {
                if (Raylib.CheckCollisionCircleRec(spell.pos, spell.hitboxRadius, enemy.hitbox)) {
                    if (spell is SpellFireball) {
                        enemy.hp--;
                    }
                    if (spell is SpellWaterball) {
                        enemy.hp -= 1 * spell.currentSprite < 4 ? (spell.currentSprite+1) : 5;
                    }
                    SpellManager.playerSpells.RemoveAt(i);
                }
            }
        }
    }  
    public static void Draw() {

    }  

}
