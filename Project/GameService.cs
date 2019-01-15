using System;
using System.Collections.Generic;
using System.Threading;
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
        case "pull":
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
      //if in second room => go find winning item description
      if (CurrentRoom.Name == "Courtyard")
      {
        CurrentRoom.Exits["south"].Description = "\n\tYou have left Adventure's Keep with out retrieving the Artifact you've been searching for! \n\tGo back and continue your search!";
      }
      WinGame();
      Console.WriteLine("\n\t" + CurrentRoom.Description);
    }

    public void Help()
    {
      Console.WriteLine(@"
                  />_________________________________
        [########[]_________________________________/
                  \>

      Options -- 
       'help' draws a list of possible commands
       'go' requires a direction 'north', 'south', 'east', 'west'
       'look' redraws the area's description
       'take' requires an 'item' to take and adds it to your inventory
       'use' requires an 'item' to use
       'pull' requires and 'item' to pull 
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
      Console.Clear();
      Console.WriteLine("\n\t" + CurrentRoom.Description);
    }

    public void Quit()
    {
      playing = false;
    }

    public void Reset()
    {
      Console.Clear();
      Setup();
      Console.WriteLine("Welcome to Adventure!");
      Console.WriteLine("Lets Play!");
      Console.Write("Enter Player Name to Begin:");
      string playerName = Console.ReadLine();
      CurrentPlayer = new Player(playerName);
      StartGame();
    }

    public void Setup()
    {
      //Creates each Room
      Room cy = new Room("Courtyard", "You're in an open courtyard with 3 doors to the north, east and west. \n\tThere appears to be an shiny ancient arifact in the center, \n\tbut it is protected inside a large cage");
      Room tc = new Room("Treasure Chamber", "You're in a dimmly light chamber filled with gold treasures, precious gems, and rather ordinary looking chest. \n\tInside the chest lies a large ornate key.");
      Room pt = new Room("Pharoh's Tomb", "You're in a dark cob-web filled chamber with a single sarcophagus decorated in golden hieroglyphs. \n\tTwo large mirrors reflect the moonlight and cause the sarcophagus to glow with a golden hue. \n\tA peculiar rope hangs from the ceiling..", true);
      Room sc = new Room("Star Chamber", "You're in a chamber with a large opening in the ceiling. \n\tAllowing a clear view of the night sky, also illuminating the many scrolls and texts piled in the chamber. \n\tA trapdoor lies on the floor");
      Room ent = new Room("Entrance", "After a long journey you have finally reached the home of Adventure. \n\tDirectly to your north stands the door to Adventures Keep \n\tGo inside and retrieve the priceless Artifact you've been searching for.");

      //Creates each Exit
      cy.Exits.Add("west", tc);
      cy.Exits.Add("north", pt);
      cy.Exits.Add("east", sc);
      cy.Exits.Add("south", ent);
      tc.Exits.Add("east", cy);
      pt.Exits.Add("south", cy);
      sc.Exits.Add("west", cy);
      ent.Exits.Add("north", cy);

      //Creates each Item
      Item art = new Item("artifact", "A shiny golden treasure", true);
      Item key = new Item("key", "A key to an unknown door");
      Item rope = new Item("rope", "A mysterious rope hanging from the ceiling");
      Item trap = new Item("trapdoor", "A trap door leading down into the darkness");

      //Adds each Item to a Room
      cy.Items.Add(art);
      tc.Items.Add(key);
      pt.Items.Add(rope);
      sc.Items.Add(trap);
      CurrentRoom = ent;

    }

    public void StartGame()
    {
      Console.Clear();
      Console.WriteLine("\n\t" + CurrentRoom.Description);
      Help();
      while (playing)
      {
        System.Console.WriteLine("\n        What do you do?");
        GetUserInput();
      }
      Console.Clear();
      System.Console.WriteLine("\n\tGoodBye");
    }

    public void TakeItem(string itemName)
    {
      Item foundItem = CurrentRoom.Items.Find(item => item.Name.ToLower() == itemName.ToLower());
      if (foundItem != null)
      {
        if (itemName == "rope" || itemName == "trapdoor" || foundItem.Locked)
        {
          Console.Clear();
          Console.WriteLine("\n\tSorry, you cannot take that Item \n\tIt is either protected at the moment or unavailable");
          return;
        }

        Console.Clear();
        Console.WriteLine($"\n\tThe {itemName} has been added to your Inventory");
        CurrentRoom.Items.Remove(foundItem);
        CurrentPlayer.Inventory.Add(foundItem);
      }
      else
      {
        System.Console.WriteLine("\n\titem not found");
      }
    }
    public void UseItem(string itemName)
    {
      Item roomItem = CurrentRoom.Items.Find(item => item.Name == itemName);
      if (roomItem == null)
      {
        Item useableItem = CurrentPlayer.Inventory.Find(item => item.Name == itemName);
        if (useableItem == null)
        {
          System.Console.WriteLine("invalid item");
          return;
        }
        if (CurrentRoom.Name == "Courtyard")
        {
          CurrentRoom.Exits["north"].LockedRoom = false;
          Console.Clear();
          Console.WriteLine("\n\tYou used the key to unlock the northern door.");
        }
      }
      else
      {
        //rope or trapdoor
        //if roomItem.name == rope
        if (roomItem.Name == "rope")
        {
          if (CurrentRoom.Exits["south"].Items.Count > 0)
          {
            CurrentRoom.Exits["south"].Items[0].Locked = false;
          }
          Console.Clear();
          CurrentRoom.Exits["south"].Description = "\n\tYou're now back in the courtyard. The cage is suspended high in the air exposing the shiny golden artifact! \n\tGrab it and escape the keep!";
          System.Console.WriteLine("\n\tAfter pulling the rope, you hear the sound of chains in the courtyard.");
        }
        else if (roomItem.Name == "trapdoor")
        {

          Console.Clear();
          Console.WriteLine(@"
                            ,--.
                           {    }
                           K,   }
                          /  ~Y`
                     ,   /   /          At the bottom of the trapdoor was a pit filled with spikes!
                    {_'-K.__/           You have fallen to your Death!
                      `/-.__L._         
                      /  ' /`\_}        Would you like to Try Again? yes/no ?
                     /  ' /
             ____   /  ' /
      ,-'~~~~    ~~/  ' /_
    ,'             ``~~~  ',
   (                        Y
  {                         I
 {      -                    `,
 |       ',                   )
 |        |   ,..__      __. Y
 |    .,_./  Y ' / ^Y   J   )|
 \           |' /   |   |   ||
  \          L_/    . _ (_,.'(
   \,   ,      ^^"" / |      )
     \_  \          /,L]     /
       '-_~-,       ` `   ./`
          `'{_            )
              ^^\..___,.--`    
            
        ");
          string response = Console.ReadLine().ToLower();
          if (response == "yes")
          {
            StartGame();
          }
          else if (response == "no")
          {
            Quit();
          }
        }
      }
    }
    public void WinGame()
    {
      if (CurrentRoom.Name == "Entrance")
      {
        Item item = CurrentPlayer.Inventory.Find(i => i.Name == "artifact");
        if (item != null)
        {
          Console.WriteLine(@"
                  /|                                                                              
              /\/ |/\                                                                             
              \  ^   | /\  /\                                                                     
        (\/\  / ^   /\/  )/^ )                                                                    
         \  \/^ /\       ^  /           You have succeeded in escaping Adventure's Keep!                                              
          )^       ^ \     (                  Well Done!                                              
         (   ^   ^      ^\  )                                                                     
          \___\/____/______/                                                                       
          [________________]                                                                      
           |              |                                                                       
           |     //\\     |             Would you like to play again? yes/no ?                                                    
           |    <<()>>    |                                                                       
           |     \\//     |                                                                       
            \____________/                                                                        
                |    |                                                                            
                |    |                                                                            
                |    |                                                                            
                |    |                                                                            
                |    |                                                                            
                |    |                                                                            
                |    |                                                                            
          ");
          string response = Console.ReadLine().ToLower();
          if (response == "yes")
          {
            Reset();
            StartGame();
          }
          else if (response == "no")
          {
            Quit();
          }
        }
      }
    }
  }
}