using Raylib_cs;
using System.Numerics;

public static class CollisionManager {
    public static void Update(Player player) {
        if (player.pos.X < RoomManager.roomScreenPos.X + 32) {
            player.pos.X = RoomManager.roomScreenPos.X + 32;
        }
        if (player.pos.X+player.rect.Width > RoomManager.roomMaxScreenPos.X - 32) {
            player.pos.X = RoomManager.roomMaxScreenPos.X - 32 - player.rect.Width;
        }
        if (player.pos.Y < RoomManager.roomScreenPos.Y + 32) {
            player.pos.Y = RoomManager.roomScreenPos.Y + 32;
        }
        if (player.pos.Y+player.rect.Height > RoomManager.roomMaxScreenPos.Y - 32) {
            player.pos.Y = RoomManager.roomMaxScreenPos.Y - 32 - player.rect.Height;
        }
        player.rect.Position = player.hitbox.Position = player.pos; 

        for (int i = SpellManager.enemySpells.Count-1; i >= 0; i--) {
            Spell spell = SpellManager.enemySpells[i];
            if (Raylib.CheckCollisionCircleRec(spell.pos, spell.hitboxRadius, player.hitbox)) {

                if (player.GetDamage(0)) {
                    State.StartCameraShake();
                }

                if (spell is SpellFireball) {
                        EffectManager.playerEffects.Add(new EffectBurn(player, spell.pos, spell.angle, spell.color, true));
                    }

                    if (spell is SpellWaterball) {
                        EffectManager.playerEffects.Add(new EffectWater(player, spell.currentSprite < 5 ? spell.currentSprite : 7, spell.dirVec, spell.pos, spell.color));
                    }

                    if (spell is SpellIceshard) {
                        EffectManager.playerEffects.Add(new EffectSlow(player, spell.pos, spell.angle, spell.color, true));
                    } 

                    if (spell is SpellLighting) {
                        EffectManager.playerEffects.Add(new EffectLighting(player, spell.pos, spell.color, false, true));
                    }
                    
                    SpellManager.enemySpells.RemoveAt(i);;
                    break;
            }
        }

        for (int i = SpellManager.playerSpells.Count-1; i >= 0; i--) {
            Spell spell = SpellManager.playerSpells[i];
            foreach (Enemy enemy in EnemyManager.enemies) {
                if (Raylib.CheckCollisionCircleRec(spell.pos, spell.hitboxRadius, enemy.hitbox)) {
                    if (spell is SpellFireball && enemy.effects < 2) {
                        EffectManager.enemyEffects.Add(new EffectBurn(enemy, spell.pos, spell.angle, spell.color, false));
                        enemy.effects++;
                    }
                    else if (spell is SpellFireball && enemy.effects >= 2) {
                        EffectManager.enemyEffects.Add(new EffectBurn(enemy, spell.pos, spell.angle, spell.color, true));
                    }

                    if (spell is SpellWaterball) {
                        EffectManager.enemyEffects.Add(new EffectWater(enemy, spell.currentSprite < 5 ? spell.currentSprite : 7, spell.dirVec, spell.pos, spell.color));
                    }

                    if (spell is SpellIceshard && enemy.effects < 2) {
                        EffectManager.enemyEffects.Add(new EffectSlow(enemy, spell.pos, spell.angle, spell.color, false));
                        enemy.effects++;
                    } 
                    else if (spell is SpellIceshard && enemy.effects >= 2) {
                        EffectManager.enemyEffects.Add(new EffectSlow(enemy, spell.pos, spell.angle, spell.color, true));
                    }
                    
                    if (spell is SpellLighting) {
                        EffectManager.enemyEffects.Add(new EffectLighting(enemy, spell.pos, spell.color, false, false));
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
