using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Raylib_cs;
using System.Numerics;
using System.Text.Json;

public static class Util {
    public static Vector2 GetRectCenter(Rectangle rect) {
        return new Vector2(rect.X + rect.Width/2, rect.Y + rect.Height/2); 
    }

    public static bool SaveRoomFile(Room room) {
        using (var stream = File.Open("room.room", FileMode.Create))
        using (var writer = new BinaryWriter(stream)) {
            for (int i = 0; i < room.mat.GetLength(0); i++) {
                for (int j = 0; j < room.mat.GetLength(1); j++) {
                    writer.Write(room.mat[i, j]);
                }
            }
        }

        Console.WriteLine("Room saved");

        return true;
    }
    
    public static Room LoadRoomFile(string roomFile) {
       var room = new Room();

        using (var stream = File.Open(roomFile, FileMode.Open))
        using (var reader = new BinaryReader(stream)) {
            for (int i = 0; i < RoomManager.rows; i++) {
                for (int j = 0; j < RoomManager.cols; j++) {
                    room.mat[i, j] = reader.ReadInt32();
                }
            }
        }
        Console.WriteLine(roomFile + " loaded");

        return room;
    }

    public static void PrintMatrix(int [,]mat) {
        for (int i = 0; i < mat.GetLength(0); i++) {
            for (int j = 0; j < mat.GetLength(1); j++) {
                Console.Write(String.Format("{0} ", mat[i, j]));
            }
            Console.WriteLine("");
        }
    }
}
