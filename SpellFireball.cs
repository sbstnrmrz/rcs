using Raylib_cs;
using System.Numerics;

public class SpellFireball : Spell {
    public SpellFireball(Vector2 initialpos, float speed, float angle) : base(initialpos, speed, angle) {
        this.texture = Textures.fireExplosion;
        this.spriteCount = 5; 
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
