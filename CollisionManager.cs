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
                EffectManager.playerEffects.Add(new EffectSlow(player, spell.pos, true, spell.angle));
                if (player.GetDamage(0)) {
                    State.StartCameraShake();
                }

                SpellManager.enemySpells.RemoveAt(i);
            }
        }
        for (int i = SpellManager.playerSpells.Count-1; i >= 0; i--) {
            Spell spell = SpellManager.playerSpells[i];
            foreach (Enemy enemy in EnemyManager.enemies) {
                if (Raylib.CheckCollisionCircleRec(spell.pos, spell.hitboxRadius, enemy.hitbox)) {
                    if (spell is SpellFireball && enemy.effects < 2) {
                        EffectManager.enemyEffects.Add(new EffectBurn(enemy, spell.pos, false));
                        enemy.effects++;
                    }
                    else if (spell is SpellFireball && enemy.effects >= 2) {
                        EffectManager.enemyEffects.Add(new EffectBurn(enemy, spell.pos, true));
                    }

                    if (spell is SpellWaterball) {
                        EffectManager.enemyEffects.Add(new EffectWater(enemy, spell.currentSprite < 5 ? spell.currentSprite : 7, spell.dirVec, spell.pos));
                    }

                    if (spell is SpellIceshard && enemy.effects < 2) {
                        EffectManager.enemyEffects.Add(new EffectSlow(enemy, spell.pos, false, spell.angle));
                        enemy.effects++;
                    } 
                    else if (spell is SpellIceshard && enemy.effects >= 2) {
                        EffectManager.enemyEffects.Add(new EffectSlow(enemy, spell.pos, true, spell.angle));
                    }
                    
                    if (spell is SpellLighting) {
                        EffectManager.enemyEffects.Add(new EffectLighting(enemy, spell.pos));
                    }
                    SpellManager.playerSpells.RemoveAt(i);;
                    break;
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
                if (player.GetDamage(1)) {
                    State.StartCameraShake();
                }
            }
        }
    }  
}
