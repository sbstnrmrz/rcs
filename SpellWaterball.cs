using Raylib_cs;
using System.Numerics;

public class SpellWaterball : Spell {
    public SpellWaterball(Vector2 initialpos, float speed, float angle, Color color) : base(initialpos, speed, angle, color) {
        this.texture = Textures.waterball;
        this.spriteCount = 8; 
    }

    public override void Update(float deltaTime) {
        base.Update(deltaTime);

        if (animationFrames > 0 && animationFrames % 10 == 0) {
            currentSprite++;
            if (currentSprite > spriteCount) {
                currentSprite = 5;
            }
            animationFrames = 0;
        }
        animationFrames++;
    }

    public override void Draw() {
        base.Draw();
    }
}
