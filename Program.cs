using Fordonsbesiktning_EF.Data;
using Fordonsbesiktning_EF.Domain.Models;
using static System.Console;

namespace Fordonsbesiktning_EF
{
    class Program
    {
        static FordonsbesiktningContext Context = new FordonsbesiktningContext();
        private static ConsoleKeyInfo keyPressed;
        static List<Inspection> inspectionList = new List<Inspection>();

        static void Main(string[] args)
        {
            bool shouldExit = false;
            while (!shouldExit)
            {
                WriteLine("");
                WriteLine("1. Ny reservation");
                WriteLine("2. Lista reservationer");
                WriteLine("3. Utför besiktning");
                WriteLine("4. Lista besiktningar");
                WriteLine("5. Avsluta");

                ConsoleKeyInfo keyPressed = ReadKey(true);
                Clear();
                switch (keyPressed.Key)
                {
                    // Ny reservation
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        NyReservation();
                        break;

                    // Lista reservationer
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        ListaReservationer();
                        ReadKey(true);
                        break;

                    // Utför besiktning
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        UtförBesiktning();
                        break;

                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        ListaBesiktningar();
                        ReadKey(true);
                        break;

                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        shouldExit = true;
                        break;
                }

                Clear();
            }
        }

        private static void ListaBesiktningar()
        {
            List<Inspection> inspectionList = FetchAllBesiktninger();
            WriteLine("");
            WriteLine("Fordon           Utfört datum              Resultat");
            WriteLine("---------------------------------------------------------");
            foreach (Inspection inspection in inspectionList)
            {
                string isApproved = "";
                if (inspection.IsApproved)
                {
                    isApproved = "Godkänd";
                }
                else
                {
                    isApproved = "Ej godkänd";
                }
                WriteLine($"{inspection.RegistrationNumber}        {inspection.PerformedAt.ToString()}          {isApproved}");
            }
            WriteLine("....");
            inspectionList.Clear();
        }

        private static List<Inspection> FetchAllBesiktninger()
        {
            return Context.Inspection.ToList();

        }

        private static void UtförBesiktning()
        {
            Inspection inspection = null;
            inspectionList.Clear();
            {
                bool isValidInput = false;
                do
                {
                    Clear();
                    WriteLine("");
                    Write("Registreringsnummer: ");
                    string registrationNumber = ReadLine();

                    Write("Fordonet godkänt? (J)a eller(N)ej");
                    bool validKeyPressed;
                    do
                    {
                        keyPressed = ReadKey(true);
                        validKeyPressed = keyPressed.Key == ConsoleKey.J ||
                                          keyPressed.Key == ConsoleKey.N;
                    } while (!validKeyPressed);
                    List<Reservation> reservation = Find(registrationNumber);
                    if (reservation.Count != 0)
                    {
                        if (keyPressed.Key == ConsoleKey.J)
                        {
                            //godkänt         
                            //inspection = new Inspection(registrationNumber); 
                            //inspection.Passed();
                            inspection = new Inspection(registrationNumber, DateTime.Now, true);
                            inspectionList.Add(inspection);
                            Context.Inspection.Add(inspection);
                            Context.SaveChanges();
                        }
                        if (keyPressed.Key == ConsoleKey.N)
                        {
                            //inspection.Failed();
                            inspection = new Inspection(registrationNumber, DateTime.Now, false);
                            inspectionList.Add(inspection);
                            Context.Inspection.Add(inspection);
                            Context.SaveChanges();
                        }
                        Clear();

                    }
                    else
                    {
                        Clear();
                        WriteLine("Reservation saknas");
                        Thread.Sleep(2000);
                    }
                    isValidInput = true;
                } while (!isValidInput);

            }

        }

        private static List<Reservation> Find(string registrationNumber)
        {

            return Context.Reservation.Where(x => x.RegistrationNumber == registrationNumber).ToList();
        }

        private static void ListaReservationer()
        {
            List<Reservation> reservationList = FetchAll();
            WriteLine("");
            WriteLine("Fordon                     Datum");
            WriteLine("----------------------------------");
            foreach (Reservation reservation in reservationList)
            {
                WriteLine($"{reservation.RegistrationNumber}                {reservation.Date.ToString()}");
            }
            WriteLine("....");
            reservationList.Clear();
        }

        private static List<Reservation> FetchAll()
        {
            return Context.Reservation.Where(x => x.RegistrationNumber != null).ToList();

        }

        private static void NyReservation()
        {
            // Reservation
            bool isValidInput = false;
            do
            {
                Clear();
                WriteLine("");
                Write("Registreringsnummer: ");
                string registrationNumber = ReadLine();
                Write("Datum (yyyy-MM-dd hh:mm): ");
                DateTime date = DateTime.Parse(ReadLine());
                Write("Är detta korrekt ? (J)a eller(N)ej");
                bool validKeyPressed;
                ConsoleKeyInfo keyPressed = ReadKey(true);
                do
                {
                    //keyPressed = ReadKey(true);
                    validKeyPressed = keyPressed.Key == ConsoleKey.J ||
                                      keyPressed.Key == ConsoleKey.N;
                } while (!validKeyPressed);

                if (keyPressed.Key == ConsoleKey.J)
                {
                    Reservation reservation = new Reservation(registrationNumber, date);
                    //reservationList.Add(reservation);
                    Clear();
                    SaveReservation(reservation);
                    WriteLine("Reservation utförd");
                    Thread.Sleep(2000);
                    isValidInput = true;
                }
            } while (!isValidInput);
        }

        private static void SaveReservation(Reservation reservation)
        {
            Context.Reservation.Add(reservation);
            Context.SaveChanges();
        }
    }
}