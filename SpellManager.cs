using Raylib_cs;
using System.Buffers;
using System.Numerics;

public static class SpellManager {
    public static List<Spell> enemySpells = []; 
    public static List<Spell> playerSpells = []; 

    static bool CheckSpellOutOfBounds(Spell spell) {
        if (spell.pos.X - spell.hitboxRadius > RoomManager.roomMaxScreenPos.X - 32) {
            IsSpell(spell);
            return true;
        }
        if (spell.pos.X + spell.hitboxRadius < RoomManager.roomScreenPos.X + 32) {
            IsSpell(spell);
            return true;
        }
        if (spell.pos.Y - spell.hitboxRadius > RoomManager.roomMaxScreenPos.Y - 32) {
            IsSpell(spell);
            return true;
        }
        if (spell.pos.Y + spell.hitboxRadius < RoomManager.roomScreenPos.Y + 32) {
            IsSpell(spell);
            return true;
        }

        return false;
    }

    static void IsSpell(Spell spell) {
        if (spell is SpellFireball) {
            EffectManager.worldEffects.Add(new EffectBurn(spell.pos, spell.angle, spell.color, true));
        }
        if (spell is SpellWaterball) {
            EffectManager.worldEffects.Add(new EffectWater(spell.pos, spell.color));
        }
        if (spell is SpellIceshard) {
            EffectManager.worldEffects.Add(new EffectSlow(spell.pos, spell.angle, spell.color, true));
        }
        if (spell is SpellLighting) {
            EffectManager.worldEffects.Add(new EffectLighting(spell.pos, spell.angle, spell.color));
        }
    }

    public static void UpdatePlayerSpells() {
        for (int i = playerSpells.Count-1; i >= 0; i--) {
            Spell spell = playerSpells[i];
            spell.Update(0);
            if (CheckSpellOutOfBounds(spell)) {
                playerSpells.RemoveAt(i);
                continue;
            }
        }
    }

    public static void UpdateEnemySpells() {
        for (int i = enemySpells.Count-1; i >= 0; i--) {
            Spell spell = enemySpells[i];
            spell.Update(0);
            if (CheckSpellOutOfBounds(spell)) {
                enemySpells.RemoveAt(i);
                continue;
            }
        }
    }

    public static void DrawPlayerSpells() {
        foreach (Spell spell in playerSpells) {
            spell.Draw();
        }
    }

    public static void DrawEnemySpells() {
        foreach (Spell spell in enemySpells) {
            spell.Draw();
        }
    }

    public static void DrawDebugInfo() {
//      Raylib.DrawText(String.Format("PlayerSpellCount: {0} | EnemySpellCount {1}", playerSpells.Count, enemySpells.Count), 20, 20, 24, Color.Black);
    }
}
