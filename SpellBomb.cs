using Raylib_cs;
using System.Numerics;

public class SpellBomb : Spell {
    public bool isEnemy;
    public SpellBomb(Vector2 initialpos, float speed, float angle, Color color) : base(initialpos, speed, angle, color) {
        this.texture = Textures.bomb;
        this.spriteCount = 2; 
        this.speed = speed;
        this.hitbox = new Rectangle(initialpos, rectSize);
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
