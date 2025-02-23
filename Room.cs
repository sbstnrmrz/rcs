using Raylib_cs;
using System.Numerics;

public class Room {
    public enum GridID {
        None,
        Wall,
        Door,
        Rock,
        EnemySpellcaster,
        EnemyMelee,
        EnemyBouncer,
    }

    public Vector2 screenPos = Vector2.Zero;
    public const int rows = 20;
    public const int cols = 34;
    public int[,] mat = new int[21, 31]; 
    public int[,] test_mat = {{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                              {1, 3, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                              {1, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                              {1, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                              {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                              {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                              {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                              {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                              {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                              {2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2},
                              {2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2},
                              {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                              {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                              {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                              {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                              {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                              {1, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                              {1, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                              {1, 3, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                              {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    };

    public Room() {
        screenPos.X = ((Raylib.GetScreenWidth() - 32 * cols)/2);   
        screenPos.Y = ((Raylib.GetScreenHeight() - 32 * rows)/2);    
    }

    public void Init() {
        for (int i = 0; i < test_mat.GetLength(0); i++) {
            for (int j = 0; j < test_mat.GetLength(1); j++) {
                if (test_mat[i, j] == (int)GridID.EnemySpellcaster) {
                    EnemyManager.Add(new EnemyRanger(GetEnemyPosFromMat(i, j)));
                    test_mat[i, j] = 0;
                }
                if (test_mat[i, j] == (int)GridID.EnemyMelee) {
                    EnemyManager.Add(new EnemyMelee(GetEnemyPosFromMat(i, j)));
                    test_mat[i, j] = 0;
                }
                if (test_mat[i, j] == (int)GridID.EnemyBouncer) {
                    test_mat[i, j] = 0;
                }
            }
        }
         
    }

    public void Update() {

    }

    public void Draw() {
//      Console.WriteLine(String.Format("rows: {0}, cols: {1}", test_mat.GetLength(0), test_mat.GetLength(1)));
        int counter = 0;
        for (int i = 0; i < test_mat.GetLength(0); i++) {
            for (int j = 0; j < test_mat.GetLength(1); j++) {
                Color color = Color.Blue;
                if (test_mat[i, j] == (int)GridID.Wall) {
                    color = Color.Gold;
                }
                if (test_mat[i, j] == (int)GridID.Door) {
                    color = Color.Gray;
                }
                if (test_mat[i, j] == (int)GridID.Rock) {
                    color = Color.Black;
                }
                Rectangle grid = new Rectangle(((Raylib.GetScreenWidth() - 32 * cols)/2) + j * 32,
                                               ((Raylib.GetScreenHeight() - 32 * rows)/2) + i * 32,
                                               32, 32);
                Raylib.DrawRectangleRec(grid, color);
                Raylib.DrawRectangleLinesEx(grid, 1f, Color.Black);
                Raylib.DrawText(String.Format("{0}", counter), 
                                ((Raylib.GetScreenWidth() - 32 * cols)/2) + j * 32,
                                ((Raylib.GetScreenHeight() - 32 * rows)/2) + i * 32,
                                15,
                                Color.Black);
                counter++;
            }
        }
//      for (int i = 0; i < 21; i++) {
//          Raylib.DrawRectangleRec(new Rectangle(50, 50 + i * 32, 32, 32), Color.Gold);
//          Raylib.DrawRectangleRec(new Rectangle(50 + 32 * 32, 50 + i * 32, 32, 32), Color.Gold);
//      }
//      for (int i = 0; i < 31; i++) {
//          Raylib.DrawRectangleRec(new Rectangle(50 + 32 + i * 32, 50, 32, 32), Color.Gold);
//          Raylib.DrawRectangleRec(new Rectangle(50 + 32 + i * 32, 50 + 32 * 20, 32, 32), Color.Gold);
//      }
    }

    public Vector2 GetEnemyPosFromMat(int i, int j) {
        return new Vector2(screenPos.X + j * 32, screenPos.Y + i * 32);
    }
}
