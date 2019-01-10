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
      switch (choice)
      {
        case "quit":
          playing = false;
          break;
          // case "go":
          //   Go(options)
          //   break;
      }
    }

    public void Go(string direction)
    {
      var nextRoom = CurrentRoom.ChangeRoom(direction);
      if (nextRoom == null)
      {

      }
    }

    public void Help()
    {
    }

    public void Inventory()
    {
    }

    public void Look()
    {
    }

    public void Quit()
    {
    }

    public void Reset()
    {
    }

    public void Setup()
    {
      //Creates each Room
      Room cy = new Room("Courtyard", "An open courtyard with 3 doors. There appears to be an shiny ancient arifact in the center, but it is locked inside a large cage");
      Room tc = new Room("Treasure Chamber", "A dimmly light chamber filled with misc. gold treasures, precious gems, and rather ordinary looking chest");
      Room pt = new Room("Pharoh's Tomb", "A dark cob-web filled chamber with a single sarcophagus decorated with shining metals and hieroglyphs");
      Room sc = new Room("Star Chamber", "A chamber with a large opening in the ceiling. Allowing a clear view of the night sky and illuminating the many scrolls and texts piled in the chamber");
      Room ent = new Room("Entrance", "A chamber with two wall mounted eternally burning torchs and ancient Sumarian glyphs that read 'None Shall Enter'.");

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
      while (playing)
      {
        Console.Clear();
        System.Console.WriteLine(CurrentRoom.Description);
        GetUserInput();
      }
      System.Console.WriteLine("GoodBye");
    }

    public void TakeItem(string itemName)
    {
    }

    public void UseItem(string itemName)
    {
    }
  }
}