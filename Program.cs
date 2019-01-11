using System;
using System.Threading;
using CastleGrimtol.Project;
using CastleGrimtol.Project.Models;

namespace CastleGrimtol
{
  public class Program
  {
    public static void Main(string[] args)
    {
      Console.Clear();
      Console.Title = "Welcome to Adventure's Keep!";
      GameService gameService = new GameService();
      gameService.Setup();
      Console.WriteLine("\n\tDo you Dare to enter??");
      Console.Write("\n\tEnter Player Name to Begin:");
      string playerName = Console.ReadLine();
      gameService.CurrentPlayer = new Player(playerName);
      gameService.StartGame();
    }
  }
}
