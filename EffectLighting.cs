using Raylib_cs;
using System.Numerics;
using System.Runtime.Intrinsics;

public class EffectLighting : Effect {
    List<Enemy> enemies;

    public EffectLighting (Enemy enemy, Vector2 explosionPos){
        rect = enemy.rect;
        effectExplosionTexture = Textures.lightingExplosion;
        this.damage = 6;
        this.enemy = enemy;
        this.frames = 1;
        this.explosionPos = explosionPos;
    }

    public override void UpdateEnemy(Enemy enemy) {
        this.enemies = EnemyManager.enemies;
        int nextHit = State.random.Next(enemies.Count);
        
        if (enemies.Count == 0 && !onlyHit) {
            enemy.GetDamage(damage);
            onlyHit = true;
            nextTarget = Vector2.Zero;
        }

        if (enemies.Count > 1 && !onlyHit) {
            while (enemies[nextHit] == enemy) {
                nextHit = State.random.Next(enemies.Count);
            }
            this.nextTarget = enemies[nextHit].pos;
            enemy.GetDamage(damage);
            enemies[nextHit].GetDamage(damage - 2);
            opposite = nextTarget.Y - enemy.pos.Y;
            adjacent = nextTarget.X - enemy.pos.X;
            hypotenuse = (float)Math.Sqrt(Math.Pow(opposite, 2) + Math.Pow(adjacent, 2));
            angle = (float)Math.Atan2(opposite, adjacent);
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
