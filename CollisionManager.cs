using Raylib_cs;
using System.Numerics;

public static class CollisionManager {
    public static void Update(Player player) {
        if (player.pos.X < 0) {
            player.pos.X = 0;
        }
        if (player.pos.X+player.rect.Width > Raylib.GetScreenWidth()) {
            player.pos.X = Raylib.GetScreenWidth() - player.rect.Width;
        }
        if (player.pos.Y < 0) {
            player.pos.Y = 0;
        }
        if (player.pos.Y+player.rect.Height > Raylib.GetScreenHeight()) {
            player.pos.Y = Raylib.GetScreenHeight() - player.rect.Height;
        }
        player.rect.Position = player.hitbox.Position = player.pos; 

        for (int i = SpellManager.enemySpells.Count-1; i >= 0; i--) {
            Spell spell = SpellManager.enemySpells[i];
            if (Raylib.CheckCollisionCircleRec(spell.pos, spell.hitboxRadius, player.hitbox)) {
                player.GetDamage(1);

                SpellManager.enemySpells.RemoveAt(i);
            }
        }
        for (int i = SpellManager.playerSpells.Count-1; i >= 0; i--) {
            Spell spell = SpellManager.playerSpells[i];
            foreach (Enemy enemy in EnemyManager.enemies) {
                if (Raylib.CheckCollisionCircleRec(spell.pos, spell.hitboxRadius, enemy.hitbox)) {
                    if (spell is SpellFireball) {
                        enemy.GetDamage(1);
                    }
                    if (spell is SpellWaterball) {
                        enemy.GetDamage(1 * spell.currentSprite < 4 ? (spell.currentSprite+1) : 5);
                    }
                    if (spell is SpellIceshard) {
                        enemy.GetDamage(1);
                    }
                    SpellManager.playerSpells.RemoveAt(i);
                }
            }
        }

        foreach (Enemy enemy in EnemyManager.enemies) {
            if (enemy.pos.X < 0) {
                enemy.pos.X = 0;
            }
            if (enemy.pos.X+player.rect.Width > Raylib.GetScreenWidth()) {
                enemy.pos.X = Raylib.GetScreenWidth() - player.rect.Width;
            }
            if (enemy.pos.Y < 0) {
                enemy.pos.Y = 0;
            }
            if (enemy.pos.Y+player.rect.Height > Raylib.GetScreenHeight()) {
                enemy.pos.Y = Raylib.GetScreenHeight() - player.rect.Height;
            }
            if (Raylib.CheckCollisionRecs(player.hitbox, enemy.hitbox)) {
                player.GetDamage(1);
            }
        }
    }  
}
