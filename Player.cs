using Raylib_cs;
using System.Numerics;

public class Player {
    public Vector2 pos;
    public Vector2 initialPos;
    public Rectangle rect;
    public Rectangle hitbox;

    public Vector2 pointerPos = Vector2.Zero;
    public Vector2 pointerSize = new Vector2(16*2, 16*2);

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

    public bool canMove = true;
    public bool isDashing = false;
    public bool canDash = true;
    float dashPower = 10; // Adjust as needed
    float dashDuration = 0.2f; // Adjust as needed
    float dashCooldown = 1f; // Adjust as needed
    float dashTimer = 0.0f;
    float cooldownTimer = 0.0f;

    public bool invencibility = false;
    public int invencibilityFrames = 40;
    public int invencibilityFramesCounter = 40;

    public bool isIdle = false;
    public bool isMoving = false;
    public bool isFacingRight = true;
    public bool isFacingLeft = false;
    public bool isFacingDown = false;
    public bool isFacingUp = false;
    public bool isAttacking = false;

    public int animationFrameCounter = 0; 
    public int currentSprite = 0;
    public int sideSpriteCount = 6;
    public int upSpriteCount = 6;
    public int downSpriteCount = 6;
    public int spriteCount = 6;

    public Player(Vector2 pos) {
        this.pos = pos;
        this.initialPos = pos; 
        this.rect = new Rectangle(pos.X, pos.Y, 32, 32);
        this.hitbox = this.rect;
        this.spells = [];
        this.speed = 5;
    }

    public void ResetFacing() {
        isFacingRight = false;
        isFacingLeft = false;
        isFacingDown = false;
        isFacingUp = false;
    }

    public void Update(float deltaTime) {
        pointerPos = Raylib.GetMousePosition();
        if (pointerPos.X < 0) {
            pointerPos.X = 0;
        }
        if (pointerPos.X > Raylib.GetScreenWidth()) {
            pointerPos.X = Raylib.GetScreenWidth();
        }
        if (pointerPos.Y < 0) {
            pointerPos.Y = 0;
        }
        if (pointerPos.Y > Raylib.GetScreenHeight()) {
            pointerPos.Y = Raylib.GetScreenHeight();
        }
        Raylib.SetMousePosition((int)pointerPos.X, (int)pointerPos.Y);

        velocity = Vector2.Zero;
        if (Raylib.IsKeyDown(KeyboardKey.A)) {
            velocity.X = -1;
//          isFacingLeft = true;
        }
        if (Raylib.IsKeyDown(KeyboardKey.D)) {
            velocity.X = 1;
//          isFacingRight = true;
        }
        if (Raylib.IsKeyDown(KeyboardKey.W)) {
            velocity.Y = -1;
        }
        if (Raylib.IsKeyDown(KeyboardKey.S)) {
            velocity.Y = 1;
        }

        if (Raylib.IsKeyPressed(KeyboardKey.Space) && canDash) {
            isDashing = true;
            canDash = false;
            dashTimer = dashDuration;
            cooldownTimer = dashCooldown;
        }

        if (isDashing) {
            if (velocity.X == 0 && velocity.Y == 0) {
                velocity.X = 1;
            }
            velocity *= dashPower;

            dashTimer -= Raylib.GetFrameTime();

            if (dashTimer <= 0.0f) {
                isDashing = false;

            }
        } else {
            velocity *= speed;
            canMove = true;
        }

        if (cooldownTimer > 0.0f) {
            cooldownTimer -= Raylib.GetFrameTime();
            canDash = false;
        } else {
            canDash = true;
        }

        if (velocity.X != 0 && velocity.Y != 0) {
            float x = (float)Math.Floor((velocity.X*velocity.X) / velocity.Length()); 
            float y = (float)Math.Floor((velocity.Y*velocity.Y) / velocity.Length()); 
            velocity.X = velocity.X < 0 ? -x : x;
            velocity.Y = velocity.Y < 0 ? -y : y;
        } 

        if (velocity != Vector2.Zero) {
            isMoving = true;
        } else {
            isMoving = false;
        }

        pos += velocity;
        rect.Position = pos;
        hitbox.Position = pos;

        float opposite = pointerPos.Y - Util.GetRectCenter(rect).Y;
        float adjacent = pointerPos.X - Util.GetRectCenter(rect).X;
        angle = (float)Math.Atan2(opposite, adjacent);
        float angleInDeg = float.RadiansToDegrees(angle); 

        if (angleInDeg < 45 && angleInDeg > -45) {
            ResetFacing();
            isFacingRight = true;
        }         
        if ((angleInDeg > 135 && angleInDeg < 180) || (angleInDeg < -135 && angleInDeg > -180)) {
            ResetFacing();
            isFacingLeft = true;
        }
        if (angleInDeg < 135 && angleInDeg > 45) {
            ResetFacing();
            isFacingDown = true;
        }

        if (angleInDeg > -135 && angleInDeg < -45) {
            ResetFacing();
            isFacingUp = true;
        }

//      Console.WriteLine("angle: " + angleInDeg);

//      if (angleInDeg < 90 && angleInDeg > -90) {
//          ResetFacing();

//      }

        if (spellCount < maxSpells) {
            if (spellFrames % spellCooldown == 0) {
                spellCount++;
                spellFrames = 1;
            }

            spellFrames++; 
        }
 
        if (Raylib.IsMouseButtonPressed(MouseButton.Left)) {
            if (spellCount <= maxSpells && spellCount > 0) {
                SpellManager.playerSpells.Add(new SpellIceshard(Util.GetRectCenter(rect), spellSpeed, angle, Color.White));
                spellCount--;
                isAttacking = true;
                currentSprite = 3;
                animationFrameCounter = 0;
            }
        }

        if (invencibility && invencibilityFramesCounter >= 0) {
            invencibilityFramesCounter--;
        } else {
            invencibilityFramesCounter = invencibilityFrames;
            invencibility = false;
        }

        if (isAttacking) {
            if (animationFrameCounter > 0 && animationFrameCounter % 10 == 0) {
                currentSprite++;
            }
            if (currentSprite > 5) {
                isAttacking = false;
                currentSprite = 0;
            } else {
                animationFrameCounter++;
            }
        } else if (isMoving) {
            if (animationFrameCounter % 10 == 0) {
                currentSprite++;
                if (currentSprite > 2) {
                    currentSprite = 0;
                    animationFrameCounter = 0;
                }
            }
            animationFrameCounter++;
        } else {
            currentSprite = 0;
            animationFrameCounter = 0;
        }
   }

    public void Draw() {
        Rectangle src = new Rectangle(25*currentSprite, 0, 24, 56);
        Rectangle dst = new Rectangle(rect.X, rect.Y, 24*2, 56*2);
        dst.X = rect.X - (32)/4;
        dst.Y = rect.Y - ((56*2-32))/2;

        if (isAttacking) {
            if (isFacingUp) {
                src.Y = 114;
            }
            if (isFacingDown) {
                src.Y = 57;
            }
            if (isFacingRight) {
                if (currentSprite == 4) {
                    src.Width = 35;
                    dst.Width = 35*2;
                }
                if (currentSprite == 5) {
                    src.X = 136; 
                    src.Width = 29;
                    dst.Width = 29*2;
                }
            }
            if (isFacingLeft) {
                if (currentSprite == 4) {
                    src.Width = 35;
                    dst.Width = 35*2;
                }
                if (currentSprite == 5) {
                    src.X = 136; 
                    src.Width = 29;
                    dst.Width = 29*2;
                }
                src.Width *= -1;
            } 
        } else {
            if (isFacingUp) {
                src.Y = 114;
            }
            if (isFacingDown) {
                src.Y = 57;
            }
            if (isFacingLeft) {
                src.Width *= -1;
            }
        }
        Raylib.DrawTexturePro(Textures.player, 
                src,
                dst,
                Vector2.Zero,
                0,
                Color.White);

//      Raylib.DrawRectanglePro(rect, Vector2.Zero, 0, invencibility ? Color.LightGray : Color.Red);
        Raylib.DrawRectangleLinesEx(hitbox, 1f, Color.Red);
        // pointer
//      Raylib.DrawLineV(Util.GetRectCenter(rect), pointerPos, Color.Black);
//      Raylib.DrawCircleV(pointerPos, 8, Color.Black);
        Raylib.DrawTexturePro(Textures.pointers, 
                              new Rectangle(0, 16, 16, 16),
                              new Rectangle(pointerPos.X, pointerPos.Y, pointerSize.X, pointerSize.Y),
                              new Vector2(pointerSize.X*0.5f, pointerSize.Y*0.5f),
                              0,
                              Color.White);

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
        Raylib.DrawRectangleLinesEx(dst, 1f, Color.Red);

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

    public bool GetDamage(float damage) {
        if (!invencibility) {
            invencibility = true;
            hp -= damage;
            return true;
        }

        return false;
    }
}
