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
    }
    
    public override void Draw() {
        base.Draw();
    }
}
