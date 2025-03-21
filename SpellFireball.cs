using Raylib_cs;
using System.Numerics;

public class SpellFireball : Spell {
    public SpellFireball(Vector2 initialpos, float speed, float angle, Color color) : base(initialpos, speed, angle, color) {
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
