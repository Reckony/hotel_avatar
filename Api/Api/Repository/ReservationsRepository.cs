using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repository
{
    public class ReservationsRepository
    {
        private readonly HotelAvatarContext _context;

        public ReservationsRepository(HotelAvatarContext context)
        {
            _context = context;
        }
        
        public IEnumerable<Reservations> GetReservations()
        {
            return _context.Reservations;
        }
        
        public Reservations GetReservations(int id)
        {
            var reservations = _context.Reservations.Find(id);
            if (reservations == null)
            {
                throw new System.Exception("No reservation with this id");
            }
            return reservations;
        }

        public int GetLastReservations()
        {
            IEnumerable<Reservations> reservations = _context.Reservations
                .Where(r => r.LogDate > DateTime.Now.AddMinutes(-1));

            return reservations.Count();
        }

        public async Task PostReservations(Reservations reservations)
        {
            _context.Reservations.Add(reservations);
            await _context.SaveChangesAsync();
        }

        public async Task PutReservations(int id, Reservations reservations)
        {
            _context.Entry(reservations).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
        
        public async Task<Reservations> DeleteReservations(int id)
        {
            var reservations = await _context.Reservations.FindAsync(id);
            if (reservations == null)
            {
                throw new System.Exception("No reservation with this id");
            }
            _context.Reservations.Remove(reservations);
            await _context.SaveChangesAsync();
            return (reservations);
        }

        public bool ReservationsExists(int id)
        {
            return _context.Reservations.Any(e => e.ID == id);
        }
    }
}
