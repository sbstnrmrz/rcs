using Raylib_cs;
using System.Numerics;
using System.Runtime.Intrinsics;

public class EffectSlow : Effect {
    public EffectSlow (Player player, Vector2 explosionPos, float angle, Color color, bool onlyAnimation){
        rect = player.rect;
        effectTexture = Textures.iceParticles;
        effectExplosionTexture = Textures.iceshardExplosion;
        this.player = player;
        this.frames = 18;
        this.explosionPos = explosionPos;
        this.onlyAnimation = onlyAnimation;
        this.angle = angle;
        this.color = color;
    }

    public EffectSlow (Enemy enemy, Vector2 explosionPos, float angle, Color color, bool onlyAnimation){
        rect = enemy.rect;
        this.angle = angle;
        effectTexture = Textures.iceParticles;
        effectExplosionTexture = Textures.iceshardExplosion;
        this.enemy = enemy;
        this.frames = 18;
        this.explosionPos = explosionPos;
        this.onlyAnimation = onlyAnimation;
        this.color = color;
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

    public override void Draw() {
        base.Draw();
    }

}
