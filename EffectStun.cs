using Raylib_cs;
using System.Numerics;

public class EffectStun : Effect {
    float originalSpeed = 0;
    public EffectStun (Player player, Vector2 explosionPos, float angle, Color color, bool onlyAnimation) {
        rect = player.rect;
        effectTexture = Textures.fireParticles;
        effectExplosionTexture = Textures.fireExplosion;
        this.explosionPos = explosionPos;
        this.player = player;
        this.onlyAnimation = true;
        this.angle = angle;
        this.color = color;
        this.damage = 4;
    }

    public EffectStun (Enemy enemy, Vector2 explosionPos, float angle, Color color, bool onlyAnimation) {
        rect = enemy.rect;
        this.enemy = enemy;
        effectTexture = Textures.fireParticles;
        effectExplosionTexture = Textures.fireExplosion;
        this.explosionPos = explosionPos;
        this.color = color;
        this.onlyAnimation = onlyAnimation;
        this.frames = 25;
        this.damage = 4;
        this.originalSpeed = enemy.speed;
    }

    public EffectStun (Vector2 explosionPos, float angle, Color color, bool onlyAnimation) {
        effectExplosionTexture = Textures.fireExplosion;
        this.explosionPos = explosionPos;
        this.color = color;
        this.onlyAnimation = onlyAnimation;
        this.damage = 4;
    }

    public override void UpdatePlayer(Player player) {
        if (onlyAnimation && !onlyHit) {
            player.GetDamage(damage);
            onlyHit = true;
        } 
        base.UpdateEnemy(enemy);
    }

    public override void UpdateEnemy(Enemy enemy) {
        if (ticks == 0 && !onlyHit) {
            enemy.GetDamage(damage);
            enemy.isPosEffect = true;
            onlyHit = true;
        }

        if (!onlyAnimation && frames > 0 && frames % 25 == 0) {
            ticks++;
            frames = 0;
        }
        if (ticks == 3) {
            ticks++;
            enemy.isPosEffect = false;
        }   
        base.UpdateEnemy(enemy);
    }
    
    public override void UpdateWorld() {
        base.UpdateWorld();
    }

    public override void Draw() {
        base.Draw();
    }

}
