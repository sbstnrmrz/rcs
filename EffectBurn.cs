using Raylib_cs;
using System.Numerics;

public class EffectBurn : Effect {
    public EffectBurn (Player player, Vector2 explosionPos, bool onlyAnimation, float angle) {
        rect = player.rect;
        effectTexture = Textures.fireParticles;
        effectExplosionTexture = Textures.fireExplosion;
        this.explosionPos = explosionPos;
        this.player = player;
        this.onlyAnimation = true;
        this.angle = angle;
    }

    public EffectBurn (Enemy enemy, Vector2 explosionPos, bool onlyAnimation) {
        rect = enemy.rect;
        effectTexture = Textures.fireParticles;
        effectExplosionTexture = Textures.fireExplosion;
        this.explosionPos = explosionPos;
        this.enemy = enemy;
        this.onlyAnimation = onlyAnimation;
    }

    public override void UpdatePlayer(Player player) {
        if (onlyAnimation && !onlyHit) {
            player.GetDamage(damage);
            onlyHit = true;
        } 
        base.UpdateEnemy(enemy);
    }

    public override void UpdateEnemy(Enemy enemy) {
        if (onlyAnimation && !onlyHit) {
            enemy.GetDamage(damage);
            onlyHit = true;
        } 
        if (!onlyAnimation && frames > 0 && frames % 30 == 0) {
                enemy.GetDamage(damage);
                ticks++;
                frames = 0;
        }   
        
        base.UpdateEnemy(enemy);
    }

    public override void Draw() {

        base.Draw();
    }

}
