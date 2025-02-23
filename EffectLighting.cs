using Raylib_cs;
using System.Numerics;
using System.Runtime.Intrinsics;

public class EffectLighting : Effect {
    List<Enemy> enemies;

    public EffectLighting (Enemy enemy, Vector2 explosionPos){
        rect = enemy.rect;
        effectExplosionTexture = Textures.lightingExplosion;
        this.damage = 4;
        this.enemy = enemy;
        this.frames = 1;
        this.explosionPos = explosionPos;
        this.enemies = EnemyManager.enemies;
    }

    public override void UpdateEnemy(Enemy enemy) {
        int nextHit = State.random.Next(enemies.Count);
        Console.WriteLine(enemies.Count);
        if (enemies.Count > 0 && !onlyHit) {
            while (enemies[nextHit] == enemy) {
                nextHit = State.random.Next(enemies.Count);
            }
            this.nextTarget = enemies[nextHit].pos;
            enemy.GetDamage(damage);
            enemies[nextHit].GetDamage(damage/2);
            onlyHit = true;
        }

        if (enemies.Count == 0 && !onlyHit) {
            enemy.GetDamage(damage);
            enemies[nextHit].GetDamage(damage/2);
            onlyHit = true;
        }
        if (frames > 0 && frames % 35 == 0) {
            ticks++;
        }
        
        base.UpdateEnemy(enemy);
    }

    public override void Draw() {
        base.Draw();
    }

}
