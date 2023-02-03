using System.Collections.Generic;

namespace _02.FitGym
{
    public class Trainer
    {
        public Trainer(int id, string name, int popularity)
        {
            this.Id = id;
            this.Name = name;
            this.Popularity = popularity;
            this.Members = new HashSet<Member>();

        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Popularity { get; set; }

       // public Dictionary<int, Member> Members = new Dictionary<int, Member>();

        public HashSet<Member> Members { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as Trainer;

            if (other == null || other.Id != this.Id)
            {
                return false;
            }
            
            return true;
        }
    }
}