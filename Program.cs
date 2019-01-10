using System;
using CastleGrimtol.Project;
using CastleGrimtol.Project.Models;

namespace CastleGrimtol
{
  public class Program
  {
    public static void Main(string[] args)
    {
      Console.Clear();
      GameService gameService = new GameService();
      gameService.Setup();
      Console.WriteLine("Welcome to Adventure!");
      Console.WriteLine("Lets Play!");
      Console.Write("Enter Player Name to Begin:");
      string playerName = Console.ReadLine();
      gameService.CurrentPlayer = new Player(playerName);
      gameService.StartGame();
    }
  }
}
