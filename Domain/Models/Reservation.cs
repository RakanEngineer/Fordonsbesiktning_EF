using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fordonsbesiktning_EF.Domain.Models
{
    class Reservation
    {
        public int Id { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime Date { get; set; }

        public Reservation(string registrationNumber, DateTime date)
        {
            RegistrationNumber = registrationNumber;
            Date = date;
        }
        public Reservation()
        {

        }
        //private string registrationNumber;
        //private DateTime date;

        //public Reservation(string registrationNumber, DateTime date)
        //{
        //    this.registrationNumber = registrationNumber;
        //    this.date = date;
        //}
    }


}