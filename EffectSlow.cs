using Raylib_cs;
using System.Numerics;
using System.Runtime.Intrinsics;

public class EffectSlow : Effect {
    public EffectSlow (Player player, Vector2 explosionPos, float angle, Color color, bool onlyAnimation){
        rect = player.rect;
        this.player = player;
        this.explosionPos = explosionPos;
        this.angle = angle;
        effectTexture = Textures.iceParticles;
        effectExplosionTexture = Textures.iceshardExplosion;
        this.frames = 18;
        this.color = color;
        this.onlyAnimation = onlyAnimation;
    }

    public EffectSlow (Enemy enemy, Vector2 explosionPos, float angle, Color color, bool onlyAnimation){
        rect = enemy.rect;
        this.enemy = enemy;
        this.explosionPos = explosionPos;
        this.angle = angle;
        effectTexture = Textures.iceParticles;
        effectExplosionTexture = Textures.iceshardExplosion;
        this.frames = 18;
        this.color = color;
        this.onlyAnimation = onlyAnimation;
    }

    public EffectSlow (Vector2 explosionPos, float angle, Color color, bool onlyAnimation){
        effectTexture = Textures.iceParticles;
        effectExplosionTexture = Textures.iceshardExplosion;
        this.frames = 0;
        this.explosionPos = explosionPos;
        this.angle = angle;
        this.color = color;
        this.onlyAnimation = onlyAnimation;
    }

    public override void UpdateEnemy(Enemy enemy) {
        if (onlyAnimation && !onlyHit) {
            enemy.GetDamage(damage);
            onlyHit = true;
        }
        if (!onlyAnimation && frames > 0 && frames % 18 == 0) {
            if (ticks == 0) {
                enemy.GetDamage(damage);
                enemy.speed = 2;
            } if (ticks >= 0 && ticks < 5) {
                enemy.speed = 2;
            } else {
                enemy.speed = 3;
            }
            ticks++;
            frames = 0;
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
