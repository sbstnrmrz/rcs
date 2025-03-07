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

    }
    
    public override void Draw() {
        base.Draw();
    }
}
