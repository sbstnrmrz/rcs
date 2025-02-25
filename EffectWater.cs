using Raylib_cs;
using System.Numerics;

public class EffectWater : Effect {
    Vector2 dirVec = Vector2.Zero;
    public EffectWater (Player player, int damage, Vector2 dirVec, Vector2 explosionPos, Color color){
        rect = player.rect;
        this.player = player;
        effectTexture = Textures.waterball;
        effectExplosionTexture = Textures.waterExplosion;
        this.damage = damage;
        this.dirVec = dirVec;
        this.frames = 10;
        this.explosionPos = explosionPos;
        this.color = color;
        this.onlyAnimation = true;
    }

    public EffectWater (Enemy enemy, int damage, Vector2 dirVec, Vector2 explosionPos, Color color){
        rect = enemy.rect;
        this.enemy = enemy;
        effectTexture = Textures.waterball;
        effectExplosionTexture = Textures.waterExplosion;
        this.damage = damage;
        this.dirVec = dirVec;
        this.frames = 10;
        this.explosionPos = explosionPos;
        this.color = color;
        this.onlyAnimation = true;
    }

    public EffectWater (Vector2 explosionPos, Color color){
        effectExplosionTexture = Textures.waterExplosion;
        this.explosionPos = explosionPos;
        this.color = color;
        this.onlyAnimation = true;
    }

    public override void UpdatePlayer(Player player) {
        if (!onlyHit) {
            player.GetDamage(damage);
            onlyHit = true;
        }
        base.UpdatePlayer(player);
    }

    public override void UpdateEnemy(Enemy enemy) {
        if (damage == 0) {
            damage = 1;
        }
        if (frames > 0 && frames % 10 == 0) {
            if (enemy.speed != 0){ 

                if (ticks == 0) {
                    enemy.GetDamage(damage);
                    enemy.changePos(enemy.pos -= (2*(enemy.velocity)));
                    enemy.isWaterEffect = true;
                } 
                if (ticks == 4) {
                    enemy.isWaterEffect = false;
                }else {
                    enemy.changePos(enemy.pos -= (2*(enemy.velocity)));
                }

            } else {

                if (ticks == 0) {
                    enemy.GetDamage(damage);
                    enemy.changePos(enemy.pos += (3*(dirVec)));
                    enemy.isWaterEffect = true;
                } 
                if (ticks == 4) {
                    enemy.isWaterEffect = false;
                }else {
                    enemy.changePos(enemy.pos += (3*(dirVec)));
                }
            }

            Console.WriteLine(ticks);
            ticks++;
            frames = 0;
        }
        base.UpdateEnemy(enemy);
    }

    public override void UpdateWall() {
        base.UpdatePlayer(player);
    }

    public override void Draw() {
        base.Draw();
    }

}
