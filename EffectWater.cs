using Raylib_cs;
using System.Numerics;

public class EffectWater : Effect {
    Vector2 dirVec = Vector2.Zero;
    public EffectWater (Enemy enemy, int damage, Vector2 dirVec){
        rect = enemy.rect;
        texture = Textures.waterball;
        this.damage = damage;
        this.enemy = enemy;
        this.dirVec = dirVec;
        this.frames = 10;
    }

    public override void UpdateEnemy(Enemy enemy) {
        
        if (frames > 0 && frames % 10 == 0) {
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
            Console.WriteLine(ticks);
            ticks++;
            frames = 0;
        }
        base.UpdateEnemy(enemy);
    }

    public override void Draw() {
        base.Draw();
    }

}
