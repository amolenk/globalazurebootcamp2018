using System.Runtime.Serialization;

namespace BackEnd
{
    [DataContract]
    public class Team
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string[] Members { get; set; }

        [DataMember]
        public int Score { get; set; }

        [DataMember]
        public PowerGrid PowerGrid { get; set; }
    }

    [DataContract]
    public class PowerGrid
    {
        [DataMember]
        public int Intelligence { get; set; }

        [DataMember]
        public int Strength { get; set; }

        [DataMember]
        public int Speed { get; set; }

        [DataMember]
        public int Durability { get; set; }

        [DataMember]
        public int EnergyProjection { get; set; }

        [DataMember]
        public int FightingSkills { get; set; }
    }
}
