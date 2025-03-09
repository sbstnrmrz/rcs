using Raylib_cs;
using System.Numerics;

public class EnemyBouncer : Enemy {
    public Vector2 predictedPlayerPos; 
    public float initialSpeed;

    public EnemyBouncer(Vector2 initialPos) : base(initialPos) {
        this.hp = 100;
        this.speed = 2;
        initialSpeed = speed;
        velocity = new Vector2(this.speed);
    }

    public override void Update(Player player, float deltaTime) {
        
        if (!isPosEffect) {
            pos += velocity;
        }
        rect.Position = pos;
        hitbox = rect; 

        if (animationFrameCounter > 0 && animationFrameCounter % 12 == 0) {
            currentSprite++;
            if (currentSprite > 3) {
                currentSprite = 0;
            }
        }
        animationFrameCounter++;
    }
    
    public override void Draw() {
        base.Draw();


        Rectangle src = new Rectangle(currentSprite * 25, 0, 24, 24);
        Rectangle dst = new Rectangle(rect.X - (24*2 - rect.Width)/2, rect.Y - (24*2 - rect.Height)/2, 24*2, 24*2);
        if (velocity.X < 0) {
            src.Y = 25;
        }

        Raylib.DrawTexturePro(Textures.enemyBouncer,
                              src,
                              dst,
                              Vector2.Zero,
                              0,
                              Color.White);
        Raylib.DrawRectangleLinesEx(dst, 1f, Color.Black);
    }
}
