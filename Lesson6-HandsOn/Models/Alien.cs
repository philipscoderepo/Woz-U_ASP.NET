using System;
using System.Collections.Generic;

#nullable disable

namespace Lesson6_HandsOn.Models{

    public enum Planets{Mercury, Venus, Earth, Mars, WhatOnceWas, Jupiter, Saturn, Neptune, Uranus, TheUnappreciatedPluto}
    public class Alien{
        public int Id {get; set;}
        public int NumArms { get; set; }
        public int NumHeads { get; set; }
        public int NumLegs { get; set; }
        public string BirthDate { get; set; }
        public Planets PlanetOfOrigin { get; set; }
    }
}