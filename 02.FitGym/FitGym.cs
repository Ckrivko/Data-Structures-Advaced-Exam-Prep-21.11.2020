namespace _02.FitGym
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class FitGym : IGym
    {
        private Dictionary<int, Member> membersById = new Dictionary<int, Member>();
        private Dictionary<int, Trainer> trainersById = new Dictionary<int, Trainer>();


        public FitGym()
        {

        }
        public void AddMember(Member member)
        {

            if (this.membersById.ContainsKey(member.Id))
            {
                throw new ArgumentException();
            }
            this.membersById.Add(member.Id, member);
        }

        public void HireTrainer(Trainer trainer)
        {
            if (this.trainersById.ContainsKey(trainer.Id))
            {
                throw new ArgumentException();
            }
            this.trainersById.Add(trainer.Id, trainer);

        }

        public void Add(Trainer trainer, Member member)
        {
            if (!this.trainersById.ContainsKey(trainer.Id))
            {
                throw new ArgumentException();
            }

            if (!this.membersById.ContainsKey(member.Id))
            {
                this.membersById.Add(member.Id, member);
            }


            if (member.Trainer != null)
            {
                throw new ArgumentException();
            }

            member.Trainer = trainer;
            trainer.Members.Add( member);

        }

        public bool Contains(Member member)
        {
            if (this.membersById.ContainsKey(member.Id))
            {
                return true;
            }
            return false;
        }

        public bool Contains(Trainer trainer)
        {
            if (this.trainersById.ContainsKey(trainer.Id))
            {
                return true;
            }
            return false;
        }

        public Trainer FireTrainer(int id)
        {
            if (!trainersById.ContainsKey(id))
            {
                throw new ArgumentException();
            }

            var trainer = this.trainersById[id];

            foreach (var member in trainer.Members)
            {
                member.Trainer = null;
            }

            this.trainersById.Remove(id);

            return trainer;
        }

        public Member RemoveMember(int id)
        {
            if (!membersById.ContainsKey(id))
            {
                throw new ArgumentException();
            }

            var member = membersById[id];
            var trainer = member.Trainer;

            if (trainer != null)
            {
                trainer.Members.Remove(member);
                member.Trainer = null;

            }

            this.membersById.Remove(id);

            return member;
        }

        public int MemberCount => this.membersById.Count;
        public int TrainerCount => this.trainersById.Count;

        public IEnumerable<Member>
            GetMembersInOrderOfRegistrationAscendingThenByNamesDescending()
        {

            return membersById.Values
                .OrderBy(x => x.RegistrationDate)
                .ThenByDescending(x => x.Name);
        }

        public IEnumerable<Trainer> GetTrainersInOrdersOfPopularity()
        {
            return this.trainersById.Values
                 .OrderBy(x => x.Popularity);

        }

        public IEnumerable<Member>
            GetTrainerMembersSortedByRegistrationDateThenByNames(Trainer trainer)
        {

            if (!this.trainersById.ContainsKey(trainer.Id))
            {
                throw new ArgumentException();
            }

            return trainer.Members
                .OrderBy(x=>x.RegistrationDate)
                .ThenBy(x => x.Name);
        }

        public IEnumerable<Member>
            GetMembersByTrainerPopularityInRangeSortedByVisitsThenByNames(int lo, int hi)
        {

            return this.membersById.Values
                .Where(x => x.Trainer.Popularity >= lo && x.Trainer.Popularity <= hi)
                .OrderBy(x => x.Visits)
                .ThenBy(x => x.Name);

        }

        public Dictionary<Trainer, HashSet<Member>>
            GetTrainersAndMemberOrderedByMembersCountThenByPopularity()
        {
                       
            var result = new Dictionary<Trainer, HashSet<Member>>();

            foreach (var trainer in trainersById.Values)
            {

                result.Add(trainer, trainer.Members );

            }

            return result.OrderBy(x => x.Key.Members.Count)
                .ThenBy(x => x.Key.Popularity).ToDictionary(x => x.Key, x => x.Value);

        }
    }
}