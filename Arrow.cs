using Raylib_cs;
using System.Numerics;

public class Arrow : Projectile {
    public Arrow(Vector2 initialPosition, int speed, float angle) : base(initialPosition, speed, angle) {
        this.texture = Textures.arrow;
    }

    public override void Update(float deltaTime) {
        base.Update(deltaTime);
    }

    public override void Draw() {
        base.Draw();
    }
}
