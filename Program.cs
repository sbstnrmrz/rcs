using Raylib_cs;
using System.Numerics;
using System.Security.Cryptography;

class Program {

    public static void Main() {
        int winWidth = 1280;
        int winHeight = 720;

        bool manualEscape = false;

        Raylib.InitWindow(winWidth, winHeight, "rcs");
        Raylib.InitAudioDevice();
        Raylib.SetTargetFPS(60);

        UInt64 frames = 0;
        float frameTime = 0f;
        Rectangle rect = new Rectangle(100, 100, 32, 32);
        Player player = new Player(new Vector2(100, 100));
        Player player2 = new Player(new Vector2(300, 100));
        Random random = new Random();

        State.Init();

        RoomManager.Init();
        RoomManager.StartNewRoom(player, RoomManager.GetRandomRoom());

        Raylib.PlayMusicStream(Resources.menu);

        while (!Raylib.WindowShouldClose() && !manualEscape) {

            Raylib.SetMusicVolume(Resources.menu, State.musicVolume);
            Raylib.UpdateMusicStream(Resources.menu);

            frameTime = Raylib.GetFrameTime();
            Vector2 mousePos = Raylib.GetMousePosition();


            if (State.gameState == 2) {

            // UPDATE
                if (RoomManager.PlayerEnteredPortal(player)) {
                    RoomManager.StartNewRoom(player, RoomManager.GetRandomRoom());
                    Raylib.DisableCursor();
                }

                if (!State.gamePause) {
                    RoomManager.Update(player);
                    player.Update(0);
                    EnemyManager.Update(player);
                    SpellManager.UpdatePlayerSpells();
                    SpellManager.UpdateEnemySpells();
                    CollisionManager.Update(player);
                    EffectManager.UpdatePlayerEffects();
                    EffectManager.UpdateEnemyEffects();
                    EffectManager.UpdateWorldEffects();
                    State.UpdateCamera();
                } else {
                    
                    Raylib.DrawRectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight(), new Color(0, 0, 0, 188));
                }

                if (Raylib.IsKeyPressed(KeyboardKey.Enter) || Raylib.IsKeyPressed(KeyboardKey.KpEnter)) {
                    State.gamePause = !State.gamePause;
                    if (State.gamePause) {
                        Raylib.EnableCursor();
                    } else {
                        Raylib.DisableCursor();
                    }
                }

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.SkyBlue);
                Raylib.BeginMode2D(State.camera);
                RoomManager.DrawCurrentRoom();
                player.Draw();
                EnemyManager.Draw();
                SpellManager.DrawPlayerSpells();
                SpellManager.DrawEnemySpells();
                EffectManager.DrawPlayerEffects();
                EffectManager.DrawEnemyEffects();
                EffectManager.DrawWorldEffects();

                Raylib.DrawTexturePro(Textures.pointers, 
                        new Rectangle(0, 16, 16, 16),
                        new Rectangle(player.pointerPos.X, player.pointerPos.Y, player.pointerSize.X, player.pointerSize.Y),
                        new Vector2(player.pointerSize.X*0.5f, player.pointerSize.Y*0.5f),
                        0,
                        Color.White);


//              Raylib.DrawFPS(0, 0);
                SpellManager.DrawDebugInfo();
                Raylib.EndMode2D();
                Raylib.EndDrawing();
                frames++; 
            
            } else {
                manualEscape = MainMenu(mousePos);
            }
        }




        Raylib.CloseWindow();
    }

    public static bool MainMenu(Vector2 mousePos) {

        Rectangle mouse = new Rectangle(mousePos, 1, 1);
        String[] info = {"Arriba", "Abajo", "Izquierda", "Derecha", "Dash"};
        Rectangle back = new Rectangle(10, 2, 140, 50);
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.White);

        if (State.gameState == 0) {
            Rectangle newGame = new Rectangle(Raylib.GetScreenWidth()/2 - 110, Raylib.GetScreenHeight()/2 - 30, 220, 75);
            Rectangle options = new Rectangle(Raylib.GetScreenWidth()/2 - 110, Raylib.GetScreenHeight()/2 + 65, 220, 75);
            Rectangle exit = new Rectangle(Raylib.GetScreenWidth()/2 - 110, Raylib.GetScreenHeight()/2 + 157, 220, 75);

            Raylib.DrawTexturePro(Textures.menuRectangles,new Rectangle(0, 0, 220, 62), newGame, Vector2.Zero, 0, Color.White);
            Raylib.DrawText("NUEVA PARTIDA",(int)newGame.X + 16, (int)newGame.Y + 38, 22, Color.Black);
            Raylib.DrawTexturePro(Textures.menuRectangles,new Rectangle(0, 0, 220, 62), options, Vector2.Zero, 0, Color.White);
            Raylib.DrawText("OPCIONES", (int)options.X + 45, (int)options.Y + 38, 26, Color.Black);
            Raylib.DrawTexturePro(Textures.menuRectangles,new Rectangle(0, 0, 220, 62), exit, Vector2.Zero, 0, Color.White);
            Raylib.DrawText("SALIR", (int)exit.X + 72, (int)exit.Y + 38, 26, Color.Black);

            if (Raylib.IsMouseButtonPressed(MouseButton.Left)) {

                if (Raylib.CheckCollisionRecs(newGame, mouse)){
                    State.gameState = 1;
                }
                if (Raylib.CheckCollisionRecs(options, mouse)){
                    State.gameState = 3;
                }
                if (Raylib.CheckCollisionRecs(exit, mouse)){
                    return true;
                }
            }
        }

        if (State.gameState == 1) {
            String text;
            int textWidth;
            int textX;
            Rectangle startGame = new Rectangle(Raylib.GetScreenWidth()/2.9f, Raylib.GetScreenHeight() - 80, 400, 50);
            Rectangle leftChange1P = new Rectangle(Raylib.GetScreenWidth()/6.1f, 460, 40, 40);
            Rectangle rightChange1P = new Rectangle(Raylib.GetScreenWidth()/2.75f, 460, 40, 40);
            Rectangle leftChange2P = new Rectangle(Raylib.GetScreenWidth() - 506, 460, 40, 40);
            Rectangle rightChange2P = new Rectangle(Raylib.GetScreenWidth() - 251, 460, 40, 40);
            Raylib.DrawTexturePro(Textures.menuRectangles,new Rectangle(0, 0, 220, 62), back, Vector2.Zero, 0, Color.White);
            Raylib.DrawText("ATRAS", 38, 25, 24, Color.Black);

            text = "ANTES DE EMPEZAR, ELIGE UN PODER!!";
            textWidth = Raylib.MeasureText(text, 60);
            textX = (Raylib.GetScreenWidth() - textWidth) / 2;
            Raylib.DrawText(text, textX, 75, 60, Color.Black);

            Raylib.DrawTexturePro(Textures.powerSelectRectangle, new Rectangle(0, 0, 500, 430), new Rectangle(107, 150, 500, 430), Vector2.Zero, 0, Color.White);
            text = "JUGADOR 1";
            textWidth = Raylib.MeasureText(text, 30);
            textX = 107 + (500 - textWidth) / 2;
            Raylib.DrawText(text, textX, 160, 30, Color.Black);
            text = "HABILIDAD";
            textWidth = Raylib.MeasureText(text, 24);
            textX = 107 + (500 - textWidth) / 2;

            Raylib.DrawText(text, textX, 430, 24, Color.Black);
            Raylib.DrawRectangleRec(leftChange1P, Color.Pink);
            Raylib.DrawRectangle((int)(Raylib.GetScreenWidth()/4.82f), 460, 185, 60, Color.Orange);
            text = State.spellSelected[State.powerSelection1P];
            textWidth = Raylib.MeasureText(text, 26);
            textX = (int)(Raylib.GetScreenWidth()/4.82f) + (185 - textWidth) / 2;
            Raylib.DrawText(text, textX, 478, 26, Color.Black);
            Raylib.DrawRectangleRec(rightChange1P, Color.Pink);
            
            Raylib.DrawTexturePro(Textures.powerSelectRectangle, new Rectangle(0, 0, 500, 430), new Rectangle(670, 150, 500, 430), Vector2.Zero, 0, Color.White);
            text = "JUGADOR 2";
            textWidth = Raylib.MeasureText(text, 30);
            textX = 670 + (500 - textWidth) / 2;
            Raylib.DrawText(text, textX, 160, 30, Color.Black);
            text = "HABILIDAD";
            textWidth = Raylib.MeasureText(text, 24);
            textX = 670 + (500 - textWidth) / 2;
            Raylib.DrawText(text, textX, 430, 24, Color.Black);
            Raylib.DrawRectangleRec(leftChange2P, Color.Pink);
            Raylib.DrawRectangle((int)(Raylib.GetScreenWidth() - 451), 460, 185, 60, Color.Orange);
            text = State.spellSelected[State.powerSelection2P];
            textWidth = Raylib.MeasureText(text, 26);
            textX = (int)(Raylib.GetScreenWidth() - 451) + (185 - textWidth) / 2;
            Raylib.DrawText(text, textX, 478, 26, Color.Black);
            Raylib.DrawRectangleRec(rightChange2P, Color.Pink);


            Raylib.DrawRectangleRec(startGame, Color.Orange);
            //Raylib.DrawTexturePro(Textures.menuRectangles, new Rectangle(0, 0, 220, 62), startGame, Vector2.Zero, 0, Color.White);
            text = "EMPEZAR EL JUEGO";
            textWidth = Raylib.MeasureText(text, 32);
            textX = (int)startGame.X + ((int)startGame.Width - textWidth) / 2;
            Raylib.DrawText(text, textX, Raylib.GetScreenHeight() - 70, 32, Color.Black);
            Console.WriteLine(State.powerSelection1P + "" + State.powerSelection2P);

            if (Raylib.IsMouseButtonPressed(MouseButton.Left)) {

                if (Raylib.CheckCollisionRecs(back, mouse)) {
                    State.gameState = 0;
                }

                if (Raylib.CheckCollisionRecs(leftChange1P, mouse)) {
                    if (State.powerSelection1P == 0) {
                        State.powerSelection1P = 4;
                    } else {
                        State.powerSelection1P--;
                    }
                }

                if (Raylib.CheckCollisionRecs(rightChange1P, mouse)) {
                    if (State.powerSelection1P == 4) {
                        State.powerSelection1P = 0;
                    } else {
                        State.powerSelection1P++;
                    }
                }

                if (Raylib.CheckCollisionRecs(leftChange2P, mouse)) {
                    if (State.powerSelection2P == 0) {
                        State.powerSelection2P = 4;

                    } else {
                        State.powerSelection2P--;
                    }
                }

                if (Raylib.CheckCollisionRecs(rightChange2P, mouse)) {
                    if (State.powerSelection2P == 4) {
                        State.powerSelection2P = 0;

                    } else {
                        State.powerSelection2P++;
                    }
                }

                if (Raylib.CheckCollisionRecs(startGame, mouse)) {
                    State.gameState = 2;
                }

            }

        }

        if (State.gameState == 3) {
            Rectangle[] changeKey = new Rectangle[10];

            Raylib.DrawTexturePro(Textures.menuRectangles,new Rectangle(0, 0, 220, 62), back, Vector2.Zero, 0, Color.White);
            Raylib.DrawText("ATRAS", 38, 25, 24, Color.Black);
            Raylib.DrawText("MÚSICA", Raylib.GetScreenWidth()/2 - 70, Raylib.GetScreenHeight()/20, 34, Color.Black);
            Raylib.DrawText("EFECTOS", Raylib.GetScreenWidth()/2 - 87, Raylib.GetScreenHeight()/5, 34, Color.Black);
            Raylib.DrawText("ATAJOS", Raylib.GetScreenWidth()/2 - 70, (int)(Raylib.GetScreenHeight()/2.30f), 34, Color.Black);

            State.UpdateScrollbar();
            State.DrawScrollbar();

            int aux = 0;
            for (int i = 0; i < 2; i++) {
                for (int j = 0; j < 5; j++) {

                    if (aux < 5) {
                    changeKey[aux] = new Rectangle(410, 390 + (j * 57), 160, 63);
                     }
                    if (aux >= 5 && aux < 10) {
                    changeKey[aux] = new Rectangle(870, 390 + (j * 57), 160, 63);
                    }      
                    Raylib.DrawTexturePro(Textures.menuRectangles, new Rectangle(0, 0, 220, 60), changeKey[aux], Vector2.Zero, 0, Color.White);
                    Raylib.DrawText(info[j], 255 + (i * 460), 412 + (j * 58), 32, Color.Black);

                    if (State.changingKey != aux) {

                        string text = State.keysString[aux].ToString();
                        
                        int textWidth = Raylib.MeasureText(text, 24);
                        int textX = (int)changeKey[j].X + ((int)changeKey[j].Width - textWidth) / 2;
                        Raylib.DrawText(text, textX + (i * 460) , 420 + (j * 57), 24, Color.Black);

                    }
                    if (State.changingKey == aux) {

                        string text = ". . .";
                        int textWidth = Raylib.MeasureText(text, 45);
                        int textX = (int)changeKey[j].X + ((int)changeKey[j].Width - textWidth) / 2;
                        Raylib.DrawText(text, textX + (i * 460) , 400 + (j * 57), 45, Color.Black);

                    }
                    aux++;
                }
            }

            if (Raylib.IsMouseButtonPressed(MouseButton.Left)) {

                if (Raylib.CheckCollisionRecs(back, mouse)) {
                    State.gameState = 0;
                }

                for (int i = 0; i < 10; i++) {

                    if(Raylib.CheckCollisionRecs(changeKey[i], mouse)) {
                        State.changingKey = i;
                        State.isChanging = true;
                    }
                }
            }

            if (State.isChanging) {

                int intKey = Raylib.GetKeyPressed();

                if (intKey == 0) {
                    Raylib.DrawRectangle(0, Raylib.GetScreenHeight()/2 - 80, 1280, 100, Color.Red);
                }

                if (intKey > 0) {
                    string keyPressed = ((char)Raylib.GetCharPressed()).ToString();

                    if (Raylib.IsKeyPressed(KeyboardKey.Space)) {
                        State.keysString[State.changingKey] = "Space";
                    }
                    else if (Raylib.IsKeyPressed(KeyboardKey.Up)) {
                        State.keysString[State.changingKey] = "Up Arrow";
                    }
                    else if (Raylib.IsKeyPressed(KeyboardKey.Down)) {
                        State.keysString[State.changingKey] = "Down Arrow";
                    }
                    else if (Raylib.IsKeyPressed(KeyboardKey.Left)) {
                        State.keysString[State.changingKey] = "Left Arrow";
                    }
                    else if (Raylib.IsKeyPressed(KeyboardKey.Right)) {
                        State.keysString[State.changingKey] = "Right Arrow";
                    }
                    else if (Raylib.IsKeyPressed(KeyboardKey.LeftShift)) {
                        State.keysString[State.changingKey] = "Left Shift";
                    }
                    else if (Raylib.IsKeyPressed(KeyboardKey.RightShift)) {
                        State.keysString[State.changingKey] = "Right Shift";
                    }
                    else {
                        State.keysString[State.changingKey] = keyPressed;
                    }
                    
                    if (State.changingKey == 0) {State.keys1P.up = (KeyboardKey)intKey;}
                    if (State.changingKey == 1) {State.keys1P.down = (KeyboardKey)intKey;}
                    if (State.changingKey == 2) {State.keys1P.left = (KeyboardKey)intKey;}
                    if (State.changingKey == 3) {State.keys1P.right = (KeyboardKey)intKey;}
                    if (State.changingKey == 4) {State.keys1P.dash = (KeyboardKey)intKey;}
                    if (State.changingKey == 5) {State.keys2P.up = (KeyboardKey)intKey;}
                    if (State.changingKey == 6) {State.keys2P.down = (KeyboardKey)intKey;}
                    if (State.changingKey == 7) {State.keys2P.left = (KeyboardKey)intKey;}
                    if (State.changingKey == 8) {State.keys2P.right = (KeyboardKey)intKey;}
                    if (State.changingKey == 9) {State.keys2P.dash = (KeyboardKey)intKey;}
                    

                    State.changingKey = -1;
                    State.isChanging = false;

                }
            }
        }



        Raylib.EndDrawing();
        return false;
    }
}

