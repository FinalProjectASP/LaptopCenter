using LaptopCenter.Data;
using LaptopCenter.Models;
using LaptopCenter.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LaptopCenter.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDbContext _context;

        public ContactRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddNewContact(Contact contact)
        {
            try
            {
                _context.Contacts.Add(contact);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Error add new contact");
            }
        }
    }
}
