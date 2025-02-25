using Raylib_cs;
using System.Numerics;
using System.Runtime.Intrinsics;

public class EffectLighting : Effect {
    List<Enemy> enemies;

    public EffectLighting (Enemy enemy, Vector2 explosionPos, Color color, bool onlyAnimation, bool isPlayer){
        rect = enemy.rect;
        this.enemy = enemy;
        this.explosionPos = explosionPos;
        effectTexture = Textures.lighting;
        effectExplosionTexture = Textures.lightingExplosion;
        this.frames = 1;
        this.color = color;
        this.onlyAnimation = false;
        this.isPlayer = false;
        this.damage = 6;
    }

    public EffectLighting (Player player, Vector2 explosionPos, Color color, bool onlyAnimation, bool isPlayer){
        rect = player.rect;
        this.player = player;
        this.explosionPos = explosionPos;
        effectTexture = Textures.lighting;
        effectExplosionTexture = Textures.lightingExplosion;        
        this.frames = 1;
        this.color = color;
        this.onlyAnimation = true;
        this.isPlayer = isPlayer;
        this.damage = 6;
    }

    public EffectLighting (Vector2 explosionPos, float angle, Color color){
        effectTexture = Textures.lighting;
        effectExplosionTexture = Textures.lightingExplosion;
        this.explosionPos = explosionPos;
        this.angle = angle;
        this.color = color;
        this.onlyAnimation = true;
    }

    public override void UpdateEnemy(Enemy enemy) {
        this.enemies = EnemyManager.enemies;
        int nextHit = State.random.Next(enemies.Count);

        if (isPlayer && onlyAnimation && !onlyHit){
            player.GetDamage(damage);
            onlyHit = true;
        }
        if (!isPlayer) {
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
        }
        base.UpdateEnemy(enemy);
    }

    public override void UpdateWall() {
        base.UpdateWall();
    }

    public override void Draw() {
        base.Draw();
    }

}
