using Raylib_cs;
using System.Numerics;

public class Enemy {
    public Vector2 pos;
    public Vector2 initialPos;
    public Rectangle rect;
    public Rectangle hitbox;
    public Vector2 targetPos;
    public int hitRadius = 12;

    public float hp = 0;
    public float speed = 3;
    public Vector2 velocity;
    public float angle = 0;
    public float rotation = 0;
    public int effects = 0;
    public bool isPosEffect = false;

    public Texture2D texture;

    public bool isIdle = false;
    public bool isMoving = false;
    public bool isFacingRight = true;
    public bool isFacingLeft = false;
    public bool isFacingDown = false;
    public bool isFacingUp = false;
    public bool isAttacking = false;

    public int animationFrameCounter = 0; 
    public int currentSprite = 0;
    public int sideSpriteCount = 0;
    public int upSpriteCount = 0;
    public int downSpriteCount = 0;
    public int spriteCount = 0;

    public Rectangle attackHurtbox = new Rectangle();
    public Vector2 attackHurtboxPos = Vector2.Zero;
    public float attackAngle = 0f;
    public Vector2 attackDirVec = Vector2.Zero;
    public bool canAttack = true;
    public int attackCooldown = 70;
    public int attackTimer = 0;
    public int attackDuration = 60;
    public Vector2 playerPos = Vector2.Zero;

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
        Raylib.DrawCircleV(Util.GetRectCenter(rect), hitRadius, Color.Magenta);
        Raylib.DrawText(String.Format("hp: {0}", hp), (int)rect.X, (int)rect.Y-20, 24, Color.Black);
    }

    public void GetDamage(float damage) {
        hp -= damage;
    }

    public void changePos(Vector2 newPos){
        rect = new Rectangle(newPos, rect.Width, rect.Height);
    }

    public void ResetFacing() {
        isFacingRight = false;
        isFacingLeft = false;
        isFacingDown = false;
        isFacingUp = false;
    }
}
