using Raylib_cs;
using System.Numerics;

public class Enemy {
    public Vector2 pos;
    public Vector2 initialPos;
    public Rectangle rect;
    public Rectangle hitbox;
    public Vector2 targetPos;

    public float hp = 0;
    public float speed = 3;
    public Vector2 velocity;
    public float angle = 0;
    public float rotation = 0;

    public Texture2D texture;

    public Enemy(Vector2 initialPos) {
        this.initialPos = initialPos;
        this.pos = initialPos;
        this.velocity = new Vector2(this.speed);
        this.rect = new Rectangle(initialPos.X, initialPos.Y, 32, 32);
        this.hitbox = this.rect;
    }

    public virtual void Update(Player player, float deltaTime) {
        targetPos = Util.GetRectCenter(player.rect);
        float opposite = targetPos.Y - Util.GetRectCenter(rect).Y;
        float adjacent = targetPos.X - Util.GetRectCenter(rect).X;
        angle = (float)Math.Atan2(opposite, adjacent);
    }

    public virtual void Draw() {
        Raylib.DrawRectanglePro(rect, Vector2.Zero, 0, Color.Lime);
    }
}
