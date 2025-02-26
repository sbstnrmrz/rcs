using Raylib_cs;
using System.Numerics;

public class EnemyTeleporter : Enemy {
    public int teleportationFrames;
    public Rectangle nextPos;
    public List<Spell> spells;

    public EnemyTeleporter(Vector2 initialPos) : base(initialPos) {
        this.hp = 100;
        this.speed = 2;
        this.teleportationFrames = 1;
        this.nextPos = new Rectangle(0,0, rect.Width, rect.Height);
        spells = [];
    }

    public override void Update(Player player, float deltaTime) {

        if (teleportationFrames % 250 == 0 && !isPosEffect){
            float nextPosX = RoomManager.roomScreenPos.X  + 32 + State.random.Next(0,32) * 32;
            float nextPosY = RoomManager.roomScreenPos.Y  + 32 + State.random.Next(0,18) * 32;
            nextPos.Position = new Vector2(nextPosX, nextPosY);

            while (Raylib.CheckCollisionRecs(nextPos, player.rect)) {
                nextPosX = RoomManager.roomScreenPos.X  + 32 + State.random.Next(0,32) * 32;
                nextPosY = RoomManager.roomScreenPos.Y  + 32 + State.random.Next(0,18) * 32;
                nextPos.Position = new Vector2(nextPosX, nextPosY);
                Console.WriteLine("coincidimosAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            }

            foreach (Enemy enemy in EnemyManager.enemies) {
                if (Raylib.CheckCollisionRecs(nextPos, enemy.rect)) {
                    nextPosX = RoomManager.roomScreenPos.X  + 32 + State.random.Next(0,32) * 32;
                    nextPosY = RoomManager.roomScreenPos.Y  + 32 + State.random.Next(0,18) * 32;
                    nextPos.Position = new Vector2(nextPosX, nextPosY);
                    Console.WriteLine("UUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUU");
                }
            }

            nextPos.Position = new Vector2(nextPosX, nextPosY);
            changePos(nextPos.Position);
            rect.Position = nextPos.Position;
            hitbox = rect; 
            teleportationFrames = 0;
        }
        
        if (!isPosEffect) {
            teleportationFrames++;
            Console.WriteLine(teleportationFrames);
        }
    }
    
    public override void Draw() {
        base.Draw();
    }
}
