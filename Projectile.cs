using Raylib_cs;
using System.Numerics;

public class Projectile {
    public Vector2 pos;     
    public Vector2 initialPos;     
    public int speed;
    public Vector2 velocity;
    public float angle;
    public Rectangle hitbox;
    public Texture2D texture;

    public Projectile(Vector2 initialPos, int speed, float angle) {
        this.initialPos = initialPos; 
        this.pos = initialPos; 
        this.speed = speed;
        this.angle = angle;
        this.velocity = new Vector2(speed);
        this.hitbox = new Rectangle(pos.X, pos.Y, 28, 28);
    }

    public virtual void Update(float deltaTime) {
        Vector2 dirVec = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        pos += dirVec * velocity;
        hitbox.Position = pos;
    }

    public virtual void Draw() {
//      Raylib.DrawRectanglePro(hitbox, Vector2.Zero, 0, Color.Violet);
        Raylib.DrawTexturePro(texture, new Rectangle(0, 0, 14, 14), hitbox, Vector2.Zero, 0, Color.White);
        Raylib.DrawRectangleLinesEx(hitbox, 1f, Color.Brown);
//      Raylib.DrawCircleV(pos, 8, Color.Violet);
    }
}
