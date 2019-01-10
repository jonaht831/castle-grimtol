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
    public bool LockedRoom { get; set; }
    public Room(string name, string description, bool lockedroom = false)
    {
      Name = name;
      Description = description;
      LockedRoom = lockedroom;
      Items = new List<Item>();
      Exits = new Dictionary<string, IRoom>();
    }
    public IRoom ChangeRoom(string direction)
    {
      if (!Exits.ContainsKey(direction))
      {
        System.Console.WriteLine("Sorry, you cannot go in that direction. Try another way.\n");
      }
      else
      {
        IRoom room = Exits[direction];
        if (room.LockedRoom == true)
        {
          System.Console.WriteLine("Sorry this door is locked. Go find the key.");
        }
        else
        {
          return room;
        }
      }
      return this; //this returns the room that called ChangeRoom in the case that there isn't an exit that direction
    }
  }

}