using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg2.Models;

namespace dotnet_rpg2.Dtos.Character
{
    public class AddCharacterDTO
    {

        public string Name { get; set; } = "Frodo";
        public int Hitpoints { get; set; }
        public int Strength { get; set; }
        public int Defence { get; set; }

        public int Intelligence { get; set; }
        public RpgClass Class { get; set; } = RpgClass.Elf;
    }
}