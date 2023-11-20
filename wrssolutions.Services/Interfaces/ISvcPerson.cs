﻿using wrssolutions.Domain.Entities.Person;

namespace wrssolutions.Services.Interfaces
{
    public interface ISvcPerson
    {
        Person? GetPersonByEmail(string email);
        bool Insert(Person person);
        bool Update(Person person);
    }
}
