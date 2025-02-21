using Raylib_cs;
using System.Numerics;

public class EffectBurn : Effect {
    public EffectBurn (Enemy enemy){
        rect = enemy.rect;
        texture = Textures.fireball;
        this.enemy = enemy;
    }

    public override void UpdateEnemy(Enemy enemy) {
        if (frames > 0 && frames % 30 == 0) {
            if (ticks == 0) {
                enemy.GetDamage(damage);
            } else {
                enemy.hp -= damage;
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
