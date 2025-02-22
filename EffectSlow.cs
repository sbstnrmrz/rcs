using Raylib_cs;
using System.Numerics;

public class EffectSlow : Effect {
    public EffectSlow (Enemy enemy){
        rect = enemy.rect;
        texture = Textures.iceshard;
        this.enemy = enemy;
        this.frames = 15;
    }

    public override void UpdateEnemy(Enemy enemy) {
        if (frames > 0 && frames % 15 == 0) {
            if (ticks == 0) {
                enemy.GetDamage(damage);
                enemy.speed = 2;
            } if (ticks == 2) {
                enemy.speed = 3;
            } else {
                enemy.speed = 2;
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
