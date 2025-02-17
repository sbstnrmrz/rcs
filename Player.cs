using Raylib_cs;
using System.Numerics;

public class Player {
    public Vector2 pos;
    public Vector2 initialPos;
    public Rectangle rect;
    public Rectangle hitbox;

    public float speed;
    public Vector2 velocity;
    public float angle;
    public float rotation;

    public List<Spell> spells;
    public int spellSpeed = 1;
    public int spellFrames = 1;
    public int spellCooldown = 60;
    public int spellCount = 5;
    public int maxSpells = 5;

    public List<Arrow> arrows;
    public int arrowSpeed = 5;
    public int arrowFrames = 1;
    public int arrowCooldown = 60;
    public int arrowCount = 5;
    public int maxArrows = 5;

    public bool invencibility = false;
    public int invencibilityFrames = 40;
    public int invencibilityFramesCounter = 40;

    public Player(Vector2 pos) {
        this.pos = pos;
        this.initialPos = pos; 
        this.rect = new Rectangle(pos.X, pos.Y, 32, 32);
        this.hitbox = this.rect;
        this.spells = [];
        this.arrows = [];
        this.speed = 5;
    }

    public void Update(float deltaTime) {
        Vector2 mousePos = Raylib.GetMousePosition();
        velocity = Vector2.Zero;

        if (Raylib.IsKeyDown(KeyboardKey.A)) {
            velocity.X -= speed;
        }
        if (Raylib.IsKeyDown(KeyboardKey.D)) {
            velocity.X += speed;
        }
        if (Raylib.IsKeyDown(KeyboardKey.W)) {
            velocity.Y -= speed;
        }
        if (Raylib.IsKeyDown(KeyboardKey.S)) {
            velocity.Y += speed;
        }

        if (velocity.X != 0 && velocity.Y != 0) {
            float x = (float)Math.Floor((velocity.X*velocity.X) / velocity.Length()); 
            float y = (float)Math.Floor((velocity.Y*velocity.Y) / velocity.Length()); 
            velocity.X = velocity.X < 0 ? -x : x;
            velocity.Y = velocity.Y < 0 ? -y : y;
        } 
        pos += velocity;

        float opposite = mousePos.Y - Util.GetRectCenter(rect).Y;
        float adjacent = mousePos.X - Util.GetRectCenter(rect).X;
        angle = (float)Math.Atan2(opposite, adjacent);

        rect.Position = pos;
        hitbox.Position = pos;
        
        if (Raylib.IsMouseButtonPressed(MouseButton.Left)) {
            // cambiar el arraylist por un array fijo para performance?
            spells.Add(new Spell(Util.GetRectCenter(rect), spellSpeed, angle));
//          arrows.Add(new Arrow(rect.Position, spellSpeed, angle));
        }

         if (invencibility && invencibilityFramesCounter >= 0) {
            invencibilityFramesCounter--;
        } else {
            invencibilityFramesCounter = invencibilityFrames;
            invencibility = false;
        }
   }

    public void Draw() {
        Raylib.DrawRectanglePro(rect, Vector2.Zero, 0, invencibility ? Color.DarkPurple : Color.Red);
        Raylib.DrawRectangleLinesEx(hitbox, 1f, Color.Red);
    }
}
