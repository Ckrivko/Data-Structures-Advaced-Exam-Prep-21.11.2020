namespace _01.DogVet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DogVet : IDogVet
    {
        private Dictionary<string, Dog> dogsById = new Dictionary<string, Dog>();
        private Dictionary<string, Owner> ownersById = new Dictionary<string, Owner>();
                       

        public DogVet()
        {

        }

        public int Size => this.dogsById.Count;


        public void AddDog(Dog dog, Owner owner)
        {
            if (dogsById.ContainsKey(dog.Id))
            {
                throw new ArgumentException();
            }

            if (!ownersById.ContainsKey(owner.Id))
            {

                ownersById.Add(owner.Id, owner);
            }

            if (owner.Dogs.ContainsKey(dog.Name))
            {
                throw new ArgumentException();
            }                       

            dog.Owner = owner;
            owner.Dogs.Add(dog.Name,dog);
            dogsById.Add(dog.Id, dog);
                        
        }

        public bool Contains(Dog dog)
        {
            if (this.dogsById.ContainsKey(dog.Id))
            {
                return true;
            }

            return false;
        }

        public Dog GetDog(string name, string ownerId)
        {
            this.ThrowExceptIfDogOwnerNoExist(name, ownerId);

            return ownersById[ownerId].Dogs[name];
        }

        public Dog RemoveDog(string name, string ownerId)
        {
            this.ThrowExceptIfDogOwnerNoExist(name, ownerId);

            var dog = ownersById[ownerId].Dogs[name];
                        
            ownersById[ownerId].Dogs.Remove(name);
            dogsById.Remove(dog.Id);

            return dog;
        }

        public IEnumerable<Dog> GetDogsByOwner(string ownerId)
        {
            if (!ownersById.ContainsKey(ownerId))
            {
                throw new ArgumentException();
            }
            return ownersById[ownerId].Dogs.Values;

        }

        public IEnumerable<Dog> GetDogsByBreed(Breed breed)
        {
            var result = this.dogsById.Values.Where(x => x.Breed == breed).ToArray();

            if (result.Length == 0)
            {
                throw new ArgumentException();
            }

            return result;
        }

        public void Vaccinate(string name, string ownerId)
        {

            this.ThrowExceptIfDogOwnerNoExist(name, ownerId);

            var dog = ownersById[ownerId].Dogs[name];
            dog.Vaccines++;

        }

        public void Rename(string oldName, string newName, string ownerId)
        {
            this.ThrowExceptIfDogOwnerNoExist(oldName, ownerId);

            var dog = ownersById[ownerId].Dogs[oldName];
            dog.Name = newName;

            ownersById[ownerId].Dogs.Remove(oldName);
            ownersById[ownerId].Dogs.Add(newName, dog);
        }

        public IEnumerable<Dog> GetAllDogsByAge(int age)
        {
            var result = this.dogsById.Values.Where(x => x.Age == age).ToArray();

            if (result.Length == 0)
            {
                throw new ArgumentException();
            }
            return result;
        }

        public IEnumerable<Dog> GetDogsInAgeRange(int lo, int hi)
        {
            return this.dogsById.Values.Where(x => x.Age >= lo && x.Age <= hi);
            
        }

        public IEnumerable<Dog> GetAllOrderedByAgeThenByNameThenByOwnerNameAscending()
        {
            return this.dogsById.Values
                .OrderBy(x => x.Age)
                .ThenBy(x => x.Name)
                .ThenBy(x => x.Owner.Name);

        }

        private void ThrowExceptIfDogOwnerNoExist(string name, string ownerId)
        {
            if (!ownersById.ContainsKey(ownerId))
            {
                throw new ArgumentException();
            }

            if (!ownersById[ownerId].Dogs.ContainsKey(name))
            {
                throw new ArgumentException();
            }


            return;
        }
    }
}