using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;

namespace CastleGrimtol.Project.Models
{
  public class Room : IRoom
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Item> Items { get; set; }
    public Dictionary<string, IRoom> Exits { get; set; }
    public Room(string name, string description)
    {
      Name = name;
      Description = description;
      Items = new List<Item>();
      Exits = new Dictionary<string, IRoom>();
    }
    public IRoom ChangeRoom(string direction)
    {
      if (Exits.ContainsKey(direction))
      {
        return Exits[direction];
      }
      else
      {
        System.Console.WriteLine("not a valid room");
      }
      return null; //this returns the room that called ChangeRoom in the case that there isn't an exit that direction
    }
  }

}