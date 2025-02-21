using Raylib_cs;
using System.Numerics;

public class Player {
    public Vector2 pos;
    public Vector2 initialPos;
    public Rectangle rect;
    public Rectangle hitbox;
    public Vector2 pointerPos;

    public float hp = 100;
    public float speed;
    public Vector2 velocity;
    public float angle;
    public float rotation;

    public List<Spell> spells;
    public int spellSpeed = 5;
    public int spellFrames = 1;
    public int spellCooldown = 60;
    public int spellCount = 5;
    public int maxSpells = 5;

    public bool invencibility = false;
    public int invencibilityFrames = 40;
    public int invencibilityFramesCounter = 40;

    public Player(Vector2 pos) {
        this.pos = pos;
        this.initialPos = pos; 
        this.rect = new Rectangle(pos.X, pos.Y, 32, 32);
        this.hitbox = this.rect;
        this.spells = [];
        this.speed = 5;
    }

    public void Update(float deltaTime) {
        pointerPos = Raylib.GetMousePosition();
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

        float opposite = pointerPos.Y - Util.GetRectCenter(rect).Y;
        float adjacent = pointerPos.X - Util.GetRectCenter(rect).X;
        angle = (float)Math.Atan2(opposite, adjacent);

        rect.Position = pos;
        hitbox.Position = pos;

        if (spellCount < maxSpells) {
            if (spellFrames % spellCooldown == 0) {
                spellCount++;
                spellFrames = 1;
            }

            spellFrames++; 
        }
 
        if (Raylib.IsMouseButtonPressed(MouseButton.Left)) {
            if (spellCount <= maxSpells && spellCount > 0) {
                SpellManager.playerSpells.Add(new SpellFireball(Util.GetRectCenter(rect), spellSpeed, angle));
                spellCount--;
            }
        }

        if (invencibility && invencibilityFramesCounter >= 0) {
            invencibilityFramesCounter--;
        } else {
            invencibilityFramesCounter = invencibilityFrames;
            invencibility = false;
        }
   }

    public void Draw() {
        Raylib.DrawRectanglePro(rect, Vector2.Zero, 0, invencibility ? Color.LightGray : Color.Red);
        Raylib.DrawRectangleLinesEx(hitbox, 1f, Color.Red);
        // pointer
//      Raylib.DrawLineV(Util.GetRectCenter(rect), pointerPos, Color.Black);
        Raylib.DrawCircleV(pointerPos, 8, Color.Black);

        Vector2 spellAmmoRectSize = new Vector2(10, 10);
        Raylib.DrawRectanglePro(new Rectangle((pos.X + rect.Width / 2) - (maxSpells * (spellAmmoRectSize.X)) / 2,
                                        pos.Y - 20,
                                        (spellCount * spellAmmoRectSize.X + (spellAmmoRectSize.X / spellCooldown) * spellFrames),
                                        spellAmmoRectSize.Y),
                                new Vector2(0, 0),
                                0,
                                Color.Magenta);

        for (int i = 0; i < maxSpells; i++) {
            Raylib.DrawRectangleLinesEx(new Rectangle((pos.X + rect.Width/2) - ((maxSpells * (spellAmmoRectSize.X)) / 2) + i * (spellAmmoRectSize.X), 
                                        pos.Y - 20, 
                                        spellAmmoRectSize.X, 
                                        spellAmmoRectSize.Y),
                                        1f,
                                        Color.Black);
        }

            // cambiar el arraylist por un array fijo para performance?


//      Raylib.DrawText(String.Format("hp: {0}", hp), (int)rect.X, (int)rect.Y-20, 24, Color.Black);
//      Raylib.DrawTexturePro(Textures.pointers,
//                            new Rectangle((float)Textures.pointersInfo.GetTextureInfo(pointerID).x,
//                                          (float)Textures.pointersInfo.GetTextureInfo(pointerID).y,
//                                          (float)Textures.pointersInfo.GetTextureInfo(pointerID).width,
//                                          (float)Textures.pointersInfo.GetTextureInfo(pointerID).height),
//                            new Rectangle(),
//                                          
//                            );
    }

    public void GetDamage(float damage) {
        if (!invencibility) {
            invencibility = true;
            hp -= damage;
        }
    }
}
