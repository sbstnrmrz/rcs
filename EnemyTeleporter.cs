using Raylib_cs;
using System.Numerics;

public class EnemyTeleporter : Enemy {
    public int teleportationFrames;
    public int attackDelayFrames = 0;
    public int spellSpeed = 5;
    public bool isteleported = false;
    public Rectangle nextPos;
    public List<Spell> spells;

    public EnemyTeleporter(Vector2 initialPos) : base(initialPos) {
        this.hp = 25;
        this.speed = 2;
        this.teleportationFrames = 1;
        this.nextPos = new Rectangle(0,0, rect.Width, rect.Height);
        spells = [];
    }

    public override void Update(Player player, float deltaTime) {
        if (teleportationFrames > 220 && !isPosEffect){
            float nextPosX = RoomManager.roomScreenPos.X  + 32 + State.random.Next(0,32) * 32;
            float nextPosY = RoomManager.roomScreenPos.Y  + 32 + State.random.Next(0,18) * 32;
            nextPos.Position = new Vector2(nextPosX, nextPosY);

            while (Raylib.CheckCollisionRecs(nextPos, player.rect)) {
                nextPosX = RoomManager.roomScreenPos.X  + 32 + State.random.Next(0,32) * 32;
                nextPosY = RoomManager.roomScreenPos.Y  + 32 + State.random.Next(0,18) * 32;
                nextPos.Position = new Vector2(nextPosX, nextPosY);
            }

            foreach (Enemy enemy in EnemyManager.enemies) {
                if (Raylib.CheckCollisionRecs(nextPos, enemy.rect)) {
                    nextPosX = RoomManager.roomScreenPos.X  + 32 + State.random.Next(0,32) * 32;
                    nextPosY = RoomManager.roomScreenPos.Y  + 32 + State.random.Next(0,18) * 32;
                    nextPos.Position = new Vector2(nextPosX, nextPosY);
                }
            }
   
            nextPos.Position = new Vector2(nextPosX, nextPosY);
            changePos(nextPos.Position);
            rect.Position = nextPos.Position;
            hitbox = rect; 
            isteleported = true;
            teleportationFrames = 0;
        }

        if (isteleported && attackDelayFrames > 40) {
            base.Update(player, 0);
            SpellManager.enemySpells.Add(new SpellFireball(Util.GetRectCenter(rect), spellSpeed, angle, Color.Magenta));
            SpellManager.enemySpells.Add(new SpellIceshard(Util.GetRectCenter(rect), spellSpeed, angle, Color.Magenta));
            isteleported = false;
            attackDelayFrames = 0;
        }
        
        if (isteleported) {
            attackDelayFrames++;
        }

        if (!isPosEffect && !isteleported) {
            teleportationFrames++;

        }

        float opposite = Util.GetRectCenter(player.rect).Y - Util.GetRectCenter(rect).Y;
        float adjacent = Util.GetRectCenter(player.rect).X - Util.GetRectCenter(rect).X;
        angle = (float)Math.Atan2(opposite, adjacent);
        float angleInDeg = float.RadiansToDegrees(angle);
        dirVec =  new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));

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

        if (animationFrameCounter % 12 == 0) {
            currentSprite++;
            if (currentSprite > 8) {
                currentSprite = 0;
                animationFrameCounter = 0;
            }
        }
        animationFrameCounter++;

    }
    
    public override void Draw() {
        base.Draw();
        Rectangle src = new Rectangle(currentSprite * 25, 0, 24, 53);
        if (isFacingUp) {
            src.Y = 108;
        }
        if (isFacingDown) {
            src.Y = 162;
        }
        if (isFacingLeft) {
            src.Y = 0;
        }
        if (isFacingRight) {
            src.Y = 54;
        }
        
        Rectangle dst = new Rectangle(rect.X - (24*2 - rect.Width)/2, rect.Y - (53*2 - rect.Height)/2, 24*2, 53*2);

        Raylib.DrawTexturePro(Textures.enemyTeleporter, 
                              src,
                              dst,
                              Vector2.Zero,
                              0,
                              Color.White);
//      Raylib.DrawRectangleLinesEx(dst, 1f, Color.Black);
    }
}
