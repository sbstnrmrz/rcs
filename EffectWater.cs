using Raylib_cs;
using System.Numerics;

public class EffectWater : Effect {
    public EffectWater (Enemy enemy, int damage){
        rect = enemy.rect;
        texture = Textures.waterball;
        this.damage = damage;
        this.enemy = enemy;
    }

    public override void UpdateEnemy(Enemy enemy) {
        enemy.GetDamage(damage);
        ticks++;
    }

    public override void Draw() {
        base.Draw();
    }

}
