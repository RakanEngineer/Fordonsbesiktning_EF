using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fordonsbesiktning_EF.Domain.Models
{
    class Inspection
    {
        public int Id { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime PerformedAt { get; set; }
        public bool IsApproved { get; private set; }
        public Inspection()
        {

        }
        public Inspection(string registrationNumber)
        {
            RegistrationNumber = registrationNumber;
            PerformedAt = DateTime.Now;
        }
        public Inspection(int id, string registrationNumber, DateTime performedAt, bool passed)
        {
            Id = id;
            RegistrationNumber = registrationNumber;
            PerformedAt = DateTime.Now;
            IsApproved = passed;
        }
        public Inspection(string registrationNumber, DateTime performedAt, bool passed)
        {
            RegistrationNumber = registrationNumber;
            PerformedAt = DateTime.Now;
            IsApproved = passed;
        }
        public void Passed()
        {
            IsApproved = true;
        }
        public void Failed()
        {
            IsApproved = false;
        }
    }
}