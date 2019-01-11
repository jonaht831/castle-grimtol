using System.Collections.Generic;

namespace CastleGrimtol.Project.Interfaces
{
  public interface IItem
  {
    string Name { get; set; }
    string Description { get; set; }
    bool Locked { get; set; }

  }
}