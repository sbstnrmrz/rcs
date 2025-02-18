using Raylib_cs;
using System.Numerics;

public class Fireball : Spell {
    public Fireball(Vector2 initialpos, float speed, float angle) : base(initialpos, speed, angle) {
        this.texture = Textures.fireball;
        this.spriteCount = 7; 
    }

    public override void Update(float deltaTime) {
        base.Update(deltaTime);

        if (animationFrames > 0 && animationFrames % 8 == 0) {
            currentSprite++;
            if (currentSprite > spriteCount) {
                currentSprite = 0;
            }
            animationFrames = 0;
        }
        animationFrames++;
    }

    public override void Draw() {
        base.Draw();
    }
}
