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
                    if (spell is SpellBomb) {
                        EffectManager.playerEffects.Add(new EffectStun(player, spell.pos, spell.angle, spell.color, true));
                    }
                    
                    SpellManager.enemySpells.RemoveAt(i);;
                    break;
            }
        }
        
        if (RoomManager.portalActive) {
            if (Raylib.CheckCollisionCircles(player.GetPosition(), player.hitRadius, RoomManager.GetPortalPos(), RoomManager.portalRadius)) {
                Console.WriteLine("player collision with portal");
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
                    if (spell is SpellBomb && enemy.effects < 2) {
                        EffectManager.enemyEffects.Add(new EffectStun(enemy, spell.pos, spell.angle, spell.color, false));
                        enemy.effects++;
                    } 
                    else if (spell is SpellBomb && enemy.effects >= 2) {
                        EffectManager.enemyEffects.Add(new EffectStun(enemy, spell.pos, spell.angle, spell.color, true));
                    }


                    SpellManager.playerSpells.RemoveAt(i);;
                    break;
                }
            }
        }
        foreach (Enemy enemy in EnemyManager.enemies) {

            if (enemy.pos.X < RoomManager.roomScreenPos.X + 32) {
                enemy.pos.X = RoomManager.roomScreenPos.X + 32;
                if (enemy is EnemyBouncer) {
                    enemy.velocity.X *= -1;
                }
                if (enemy is EnemyCharger) {
                    ((EnemyCharger)enemy).isCharging = false;
                }
            }
            if (enemy.pos.X+enemy.rect.Width > RoomManager.roomMaxScreenPos.X - 32) {
                enemy.pos.X = RoomManager.roomMaxScreenPos.X - 32 - enemy.rect.Width;
                if (enemy is EnemyBouncer) {
                    enemy.velocity.X *= -1;
                }
                if (enemy is EnemyCharger) {
                    ((EnemyCharger)enemy).isCharging = false;
                }
            }
            if (enemy.pos.Y < RoomManager.roomScreenPos.Y + 32) {
                enemy.pos.Y = RoomManager.roomScreenPos.Y + 32;
                if (enemy is EnemyBouncer) {
                    enemy.velocity.Y *= -1;
                }
                if (enemy is EnemyCharger) {
                    ((EnemyCharger)enemy).isCharging = false;
                }
            }
            if (enemy.pos.Y+enemy.rect.Height > RoomManager.roomMaxScreenPos.Y - 32) {
                enemy.pos.Y = RoomManager.roomMaxScreenPos.Y - 32 - enemy.rect.Height;
                if (enemy is EnemyBouncer) {
                    enemy.velocity.Y *= -1;
                }
                if (enemy is EnemyCharger) {
                    ((EnemyCharger)enemy).isCharging = false;
                }
            }

            if (Raylib.CheckCollisionRecs(player.hitbox, enemy.hitbox)) {
                if (player.GetDamage(1)) {
                    State.StartCameraShake();
                }
            }
        }
    }  
}
