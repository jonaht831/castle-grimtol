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
      Console.WriteLine("Welcome to Adventure Time!");
      Console.WriteLine("Lets Play!");
      Console.Write("Enter Player Name to Begin :");
      string PlayerName = Console.ReadLine();
      gameService.StartGame();
    }
  }
}
