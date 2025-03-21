using Raylib_cs;
using System.Numerics;

public class Player {
    public Vector2 pos;
    public Vector2 initialPos;
    public Rectangle rect;
    public Rectangle hitbox;
    public float hitRadius = 12;
    public float hitOffset = 12;

    public Vector2 pointerPos = Vector2.Zero;
    public Vector2 pointerSize = new Vector2(16*2, 16*2);

    public float hp = 100;
    public float speed;
    public Vector2 velocity;
    public float angle;
    public float rotation;

    public List<Spell> spells;
    public int spellSpeed = 7;
    public int spellFrames = 1;
    public int spellCooldown = 40;
    public int spellCount = 5;
    public int maxSpells = 5;

    public bool canMove = true;
    public bool isDashing = false;
    public bool canDash = true;
    float dashPower = 12; // Adjust as needed
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

    float leftStickX = 0;
    float leftStickY = 0;
    float rightStickX = 0;
    float rightStickY = 0;
    float leftTrigger = 0;
    float rightTrigger = 0;

    const float leftStickDeadzoneX = 0.1f;
    const float leftStickDeadzoneY = 0.1f;
    const float rightStickDeadzoneX = 0.1f;
    const float rightStickDeadzoneY = 0.1f;
    const float leftTriggerDeadzone = -0.9f;
    const float rightTriggerDeadzone = -0.9f;

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
        if (Raylib.IsGamepadAvailable(0)) {
            leftStickX = Raylib.GetGamepadAxisMovement(State.gamepad, GamepadAxis.LeftX);
            leftStickY = Raylib.GetGamepadAxisMovement(State.gamepad, GamepadAxis.LeftY);
            rightStickX = Raylib.GetGamepadAxisMovement(State.gamepad, GamepadAxis.RightX);
            rightStickY = Raylib.GetGamepadAxisMovement(State.gamepad, GamepadAxis.RightY);
            leftTrigger = Raylib.GetGamepadAxisMovement(State.gamepad, GamepadAxis.LeftTrigger);
            rightTrigger = Raylib.GetGamepadAxisMovement(State.gamepad, GamepadAxis.RightTrigger);

            Console.WriteLine("left stick X: " + leftStickX);
            Console.WriteLine("left stick Y: " + leftStickY);
            Console.WriteLine("right stick X: " + rightStickX);
            Console.WriteLine("right stick Y: " + rightStickY);

            // Calculate deadzones
            if (leftStickX > -leftStickDeadzoneX && leftStickX < leftStickDeadzoneX) leftStickX = 0.0f;
            if (leftStickY > -leftStickDeadzoneY && leftStickY < leftStickDeadzoneY) leftStickY = 0.0f;
            if (rightStickX > -rightStickDeadzoneX && rightStickX < rightStickDeadzoneX) rightStickX = 0.0f;
            if (rightStickY > -rightStickDeadzoneY && rightStickY < rightStickDeadzoneY) rightStickY = 0.0f;
            if (leftTrigger < leftTriggerDeadzone) leftTrigger = -1.0f;
            if (rightTrigger < rightTriggerDeadzone) rightTrigger = -1.0f;
        }

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

        if (Raylib.IsKeyPressed(KeyboardKey.Space) && canDash && isMoving) {
            isDashing = true;
            canDash = false;
            dashTimer = dashDuration;
            cooldownTimer = dashCooldown;
        }

        if (isDashing) {
//          if (velocity.X == 0 && velocity.Y == 0) {
//              velocity.X = 1;
//          }
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

        if (spellCount < maxSpells) {
            if (spellFrames % spellCooldown == 0) {
                spellCount++;
                spellFrames = 1;
            }

            spellFrames++; 
        }
 
        if (Raylib.IsMouseButtonPressed(MouseButton.Left)) {
            if (spellCount <= maxSpells && spellCount > 0) {
                if (State.powerSelection1P == (int)State.SpellType.Fireball) {
                    SpellManager.playerSpells.Add(new SpellFireball(Util.GetRectCenter(rect), spellSpeed, angle, Color.White));
                }
                if (State.powerSelection1P == (int)State.SpellType.Waterball) {
                    SpellManager.playerSpells.Add(new SpellWaterball(Util.GetRectCenter(rect), spellSpeed, angle, Color.White));
                }
                if (State.powerSelection1P == (int)State.SpellType.Iceshard) {
                    SpellManager.playerSpells.Add(new SpellIceshard(Util.GetRectCenter(rect), spellSpeed, angle, Color.White));
                }
                if (State.powerSelection1P == (int)State.SpellType.Lightning) {
                    SpellManager.playerSpells.Add(new SpellLighting(Util.GetRectCenter(rect), spellSpeed, angle, Color.White));
                }
                if (State.powerSelection1P == (int)State.SpellType.Bomb) {
                    SpellManager.playerSpells.Add(new SpellBomb(Util.GetRectCenter(rect), spellSpeed, angle, Color.White));
                }
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
            if (animationFrameCounter % 7 == 0) {
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
                invencibility ? Color.Violet : Color.White);

//      Raylib.DrawRectanglePro(rect, Vector2.Zero, 0, invencibility ? Color.LightGray : Color.Red);
//      Raylib.DrawRectangleLinesEx(hitbox, 1f, Color.Red);
        // pointer
//      Raylib.DrawLineV(Util.GetRectCenter(rect), pointerPos, Color.Black);
//      Raylib.DrawCircleV(pointerPos, 8, Color.Black);
//      Raylib.DrawTexturePro(Textures.pointers, 
//                            new Rectangle(0, 16, 16, 16),
//                            new Rectangle(pointerPos.X, pointerPos.Y, pointerSize.X, pointerSize.Y),
//                            new Vector2(pointerSize.X*0.5f, pointerSize.Y*0.5f),
//                            0,
//                            Color.White);

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
//      Raylib.DrawRectangleLinesEx(dst, 1f, Color.Red);
        Vector2 aux = Util.GetRectCenter(rect);
        aux.Y += hitOffset;
//      Raylib.DrawCircleV(aux, hitRadius, Color.Lime);

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

    public Vector2 GetPosition() {
        Vector2 vec = Util.GetRectCenter(rect);
        vec.Y += hitOffset;
        return vec;
    }
}
