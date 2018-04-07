using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HRService
{
    [DataContract]
    public class Employee
    {
        public static readonly Dictionary<string, Employee> All = new Dictionary<string, Employee>
        {
            ["Ant-Man"] = new Employee
            {
                RealName = "Scott Lang",
                Intelligence = 4,
                Strength = 5,
                Speed = 3,
                Durability = 5,
                EnergyProjection = 3,
                FightingSkills = 4
            },
            ["Black Panther"] = new Employee
            {
                RealName = "T'Challa",
                Intelligence = 5,
                Strength = 3,
                Speed = 2,
                Durability = 3,
                EnergyProjection = 3,
                FightingSkills = 5
            },
            ["Black Widow"] = new Employee
            {
                RealName = "Natasha Romanoff",
                Intelligence = 3,
                Strength = 3,
                Speed = 2,
                Durability = 3,
                EnergyProjection = 3,
                FightingSkills = 6
            },
            ["Captain America"] = new Employee
            {
                RealName = "Steven Rogers",
                Intelligence = 3,
                Strength = 3,
                Speed = 2,
                Durability = 3,
                EnergyProjection = 1,
                FightingSkills = 6
            },
            ["Dr. Strange"] = new Employee
            {
                RealName = "Stephen Strange",
                Intelligence = 4,
                Strength = 2,
                Speed = 7,
                Durability = 2,
                EnergyProjection = 6,
                FightingSkills = 6
            },
            ["Falcon"] = new Employee
            {
                RealName = "Sam Wilson",
                Intelligence = 2,
                Strength = 2,
                Speed = 3,
                Durability = 2,
                EnergyProjection = 1,
                FightingSkills = 4
            },
            ["Hawkeye"] = new Employee
            {
                RealName = "Clint Barton",
                Intelligence = 3,
                Strength = 2,
                Speed = 2,
                Durability = 2,
                EnergyProjection = 1,
                FightingSkills = 6
            },
            ["Hulk"] = new Employee
            {
                RealName = "Bruce Banner",
                Intelligence = 2,
                Strength = 7,
                Speed = 3,
                Durability = 7,
                EnergyProjection = 5,
                FightingSkills = 4
            },
            ["Iron Man"] = new Employee
            {
                RealName = "Tony Stark",
                Intelligence = 6,
                Strength = 6,
                Speed = 5,
                Durability = 6,
                EnergyProjection = 6,
                FightingSkills = 4
            },
            ["War Machine"] = new Employee
            {
                RealName = "James Rhodes",
                Intelligence = 3,
                Strength = 6,
                Speed = 5,
                Durability = 6,
                EnergyProjection = 6,
                FightingSkills = 4
            },
            ["Quicksilver"] = new Employee
            {
                RealName = "Pietro Maximoff",
                Intelligence = 3,
                Strength = 4,
                Speed = 5,
                Durability = 3,
                EnergyProjection = 1,
                FightingSkills = 4
            },
            ["Scarlet Witch"] = new Employee
            {
                RealName = "Wanda Maximoff",
                Intelligence = 3,
                Strength = 2,
                Speed = 2,
                Durability = 2,
                EnergyProjection = 6,
                FightingSkills = 3
            },
            ["Spider-Man"] = new Employee
            {
                RealName = "Peter Parker",
                Intelligence = 4,
                Strength = 4,
                Speed = 3,
                Durability = 3,
                EnergyProjection = 1,
                FightingSkills = 4
            },
            ["Thor"] = new Employee
            {
                RealName = "Thor Odinson",
                Intelligence = 2,
                Strength = 7,
                Speed = 7,
                Durability = 6,
                EnergyProjection = 6,
                FightingSkills = 4
            },
            ["Vision"] = new Employee
            {
                RealName = "Vision",
                Intelligence = 4,
                Strength = 5,
                Speed = 3,
                Durability = 6,
                EnergyProjection = 6,
                FightingSkills = 3
            },
            ["Winter Soldier"] = new Employee
            {
                RealName = "Bucky Barnes",
                Intelligence = 2,
                Strength = 4,
                Speed = 2,
                Durability = 3,
                EnergyProjection = 1,
                FightingSkills = 6
            },
        };


        [DataMember(Order = 1)]
        public string RealName { get; private set; }

        [DataMember(Order = 2)]
        public int Intelligence { get; private set; }

        [DataMember(Order = 3)]
        public int Strength { get; private set; }

        [DataMember(Order = 4)]
        public int Speed { get; private set; }

        [DataMember(Order = 5)]
        public int Durability { get; private set; }

        [DataMember(Order = 6)]
        public int EnergyProjection { get; private set; }

        [DataMember(Order = 7)]
        public int FightingSkills { get; private set; }
    }
}