using System;
using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;
using CastleGrimtol.Project.Models;

namespace CastleGrimtol.Project
{
  public class GameService : IGameService
  {
    public IRoom CurrentRoom { get; set; }

    public Player CurrentPlayer { get; set; }
    private bool playing = true;
    public void GetUserInput()
    {
      var choice = Console.ReadLine();
      //based upon this choice what do you do
      // how can you seperate the command and the options
      string[] arrChoice = choice.ToLower().Split(" ");
      string command = arrChoice[0];
      string option = "";
      if (arrChoice.Length > 1)
      {
        option = arrChoice[1];
      }
      switch (command)
      {
        case "quit":
          Quit();
          break;
        case "go":
          Go(option);
          break;
        case "look":
          Look();
          break;
        case "take":
          TakeItem(option);
          break;
        case "inventory":
          Inventory();
          break;
        case "use":
          UseItem(option);
          break;
        case "reset":
          Reset();
          break;
        case "help":
          Help();
          break;
      }
    }

    public void Go(string direction)
    {
      //CurrentRoom.Exits.ContainsKey("north")
      Console.Clear();
      CurrentRoom = CurrentRoom.ChangeRoom(direction);
      Console.WriteLine(CurrentRoom.Description);


    }

    public void Help()
    {
      Console.WriteLine(@"
      Options --
       'help' draws a list of possible commands
       'go' takes a direction 'north', 'south', 'east', 'west'
       'look' redraws the room description
       'take' requires an 'item' to take and adds it to your inventory
       'use' requires an 'item' to use
       'inventory' shows the item you currently have in your inventory
       'reset' starts a new game
       'quit' ends the game");
    }

    public void Inventory()
    {
      Console.WriteLine("Your Inventory contains: ");
      foreach (Item item in CurrentPlayer.Inventory)
      {
        Console.WriteLine(item.Name);
      }
    }

    public void Look()
    {
      Console.WriteLine(CurrentRoom.Description);
    }

    public void Quit()
    {
      playing = false;
    }

    public void Reset()
    {
      Console.Clear();
      Setup();
      Console.WriteLine("Welcome to Adventure Game!");
      Console.WriteLine("Lets Play!");
      Console.Write("Enter Player Name to Begin:");
      string playerName = Console.ReadLine();
      CurrentPlayer = new Player(playerName);
      StartGame();
    }

    public void Setup()
    {
      //Creates each Room
      Room cy = new Room("Courtyard", "\nYoure in an open courtyard with 3 doors. There appears to be an shiny ancient arifact in the center, but it is protected inside a large cage");
      Room tc = new Room("Treasure Chamber", "You're in a dimmly light chamber filled with gold treasures, precious gems, and rather ordinary looking chest. Inside the chest lies a large ornate key.");
      Room pt = new Room("Pharoh's Tomb", "You're in a dark cob-web filled chamber with a single sarcophagus decorated with shining metals and hieroglyphs", true);
      Room sc = new Room("Star Chamber", "You're in a chamber with a large opening in the ceiling. Allowing a clear view of the night sky and illuminating the many scrolls and texts piled in the chamber");
      Room ent = new Room("Entrance", "You're in a chamber with torchs mounted on the walls. Directly in front of you stands the door to Adventures Keep");

      //Creates each Exit
      cy.Exits.Add("west", tc);
      cy.Exits.Add("north", pt);
      cy.Exits.Add("east", sc);
      tc.Exits.Add("east", cy);
      pt.Exits.Add("south", cy);
      sc.Exits.Add("west", cy);
      ent.Exits.Add("north", cy);

      //Creates each Item
      Item art = new Item("Ancient Artifact", "A shiny golden treasure");
      Item key = new Item("Key", "An unknown key");
      Item rope = new Item("Rope", "A mysterious rope hanging from the ceiling");
      Item lever = new Item("Lever", "A dangerous looking lever on the wall");
      //Adds each Item to a Room
      cy.Items.Add(art);
      tc.Items.Add(key);
      pt.Items.Add(rope);
      sc.Items.Add(lever);
      CurrentRoom = ent;

    }

    public void StartGame()
    {
      Console.Clear();
      Console.WriteLine(CurrentRoom.Description);
      while (playing)
      {
        Help();
        System.Console.WriteLine("What do you do?");
        GetUserInput();
      }
      System.Console.WriteLine("GoodBye");
    }

    public void TakeItem(string itemName)
    {
      Item foundItem = CurrentRoom.Items.Find(item => item.Name.ToLower() == itemName.ToLower());
      if (foundItem != null)
      {
        Console.WriteLine($"'{itemName}' has been added to your Inventory");
        CurrentRoom.Items.Remove(foundItem);
        CurrentPlayer.Inventory.Add(foundItem);
      }
      else
      {
        System.Console.WriteLine("item not found");
      }
    }
    public void UseItem(string itemName)
    {
      Item useableItem = CurrentPlayer.Inventory.Find(item => item.Name.ToLower() == itemName.ToLower());
      if (useableItem == null)
      {
        System.Console.WriteLine("That item does not exist");
      }
      else
      {
        if (CurrentRoom.Name == "Courtyard")
        {
          CurrentRoom.Exits["north"].LockedRoom = false;
          System.Console.WriteLine("You used the key to unlock the northern door.");
        }
      }
    }
  }
}