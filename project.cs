using System;
using System.Collections.Generic;

namespace DormitoryManagement
{
    public enum EquipName
    {
        Refrigerator = 1,
        Table,
        Chair,
        Bed,
        Closet
    }

    public enum Status
    {
        Healthy = 1,
        Faulty,
        UnderRepair
    }

    public class Person
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public int NationalCode { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }

        public Person(string fName, string lName, int nationalCode, int phoneNumber, string address)
        {
            FName = fName;
            LName = lName;
            NationalCode = nationalCode;
            PhoneNumber = phoneNumber;
            Address = address;
        }
    }

    public class Student : Person
    {
        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public int Block { get; set; }
        public string Dorm { get; set; }
        public List<Equipment> Equipment { get; set; }

        public Student(string fName, string lName, int nationalCode, int phoneNumber, string address,
            int id, int roomNumber, int block, string dorm)
            : base(fName, lName, nationalCode, phoneNumber, address)
        {
            Id = id;
            RoomNumber = roomNumber;
            Block = block;
            Dorm = dorm;
            Equipment = new List<Equipment>();
        }
    }

    public class DormManager : Person
    {
        public string Post { get; set; }
        public string DormitoryUnderResponsibility { get; set; }

        public DormManager(string fName, string lName, int nationalCode, int phoneNumber, string address,
            string post, string dormitoryUnderResponsibility)
            : base(fName, lName, nationalCode, phoneNumber, address)
        {
            Post = post;
            DormitoryUnderResponsibility = dormitoryUnderResponsibility;
        }
    }

    public class BlockManager : Person
    {
        public string Post { get; set; }
        public string BlockUnderResponsibility { get; set; }

        public BlockManager(string fName, string lName, int nationalCode, int phoneNumber, string address,
            string post, string blockUnderResponsibility)
            : base(fName, lName, nationalCode, phoneNumber, address)
        {
            Post = post;
            BlockUnderResponsibility = blockUnderResponsibility;
        }
    }

    public class Equipment
    {
        public EquipName Type { get; set; }
        public int EquipmentId { get; set; }
        public Status Status { get; set; }
        public int RoomNumber { get; set; }
        public string AssignedStudentNationalCode { get; set; }

        public Equipment(EquipName type, int equipmentId, Status status, int roomNumber, string studentNationalCode)
        {
            Type = type;
            EquipmentId = equipmentId;
            Status = status;
            RoomNumber = roomNumber;
            AssignedStudentNationalCode = studentNationalCode;
        }
    }

    public class Room
    {
        public int RoomNumber { get; private set; }
        public int Floor { get; set; }
        public int Capacity { get; set; }
        public List<Equipment> Equipments { get; set; }
        public List<Student> Students { get; set; }

        public Room(int roomNumber, int floor, int capacity, List<Equipment> equipments, List<Student> students)
        {
            RoomNumber = roomNumber;
            Floor = floor;
            Capacity = capacity;
            Equipments = equipments ?? new List<Equipment>();
            Students = students ?? new List<Student>();
        }
    }


    public class Dormitory
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int Capacity { get; set; }
        public string Manager { get; set; }

        public Dormitory(string name, string address, int capacity, string manager)
        {
            Name = name;
            Address = address;
            Capacity = capacity;
            Manager = manager;
        }

        public override string ToString()
        {
            return $"Dormitory: {Name}, Address: {Address}, Capacity: {Capacity}, Manager: {Manager}";
        }
    }

    public class Block
    {
        public string BlockName { get; set; }
        public int FloorCount { get; set; }
        public int RoomCount { get; set; }
        public string BlockManagerName { get; set; }

        public Block(string name, int floorCount, int roomCount, string manager)
        {
            BlockName = name;
            FloorCount = floorCount;
            RoomCount = roomCount;
            BlockManagerName = manager;
        }

        public override string ToString()
        {
            return $"Block: {BlockName}, Floors: {FloorCount}, Rooms: {RoomCount}, Manager: {BlockManagerName}";
        }
    }


    //---------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------
    class Program
    {
        const string SYSTEM_PASSWORD = "123";
        static List<Dormitory> dormitories_list = new List<Dormitory>();
        static Dictionary<string, List<Block>> dormBlocks = new Dictionary<string, List<Block>>();

        static void Main(string[] args)
        {
            ShowLogin();
            ShowMainMenu();
        }

        static void ShowLogin()
        {
            Console.Title = "Dormitory Management Login";

            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== Dormitory Management System ====");
                Console.Write("Enter system password: ");
                string input = Console.ReadLine();

                if (input == SYSTEM_PASSWORD)
                {
                    Console.WriteLine("\nAccess granted! Press Enter to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                }
                else
                {
                    Console.WriteLine("\nIncorrect password. Try again.");
                    Console.ReadKey();
                }
            }
        }

        static void ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== Main Menu ====");
                Console.WriteLine("1. Dorm Management");
                Console.WriteLine("2. Block Management");
                Console.WriteLine("3. People Management");
                Console.WriteLine("4. Property Management");
                Console.WriteLine("5. Reports");
                Console.WriteLine("6. Log Out");
                Console.WriteLine("0. Exit");

                Console.Write("\nSelect an option (0-6): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DormitoryMenu();
                        break;

                    case "2":
                        ChooseDorm();
                        break;
                    case "3":
                        // TODO: Equipment Management Page
                        Console.WriteLine("\n[Equipment Management]");
                        break;
                    case "4":
                        // TODO: Staff Management Page
                        Console.WriteLine("\n[Staff Management]");
                        break;
                    case "5":
                        // TODO: Reports Page
                        Console.WriteLine("\n[Reports]");
                        break;
                    case "6":
                        ShowLogin();
                        break;
                    case "0":
                        Console.WriteLine("\nExiting...");
                        return;
                    default:
                        Console.WriteLine("\nInvalid choice. Please try again.");
                        break;
                }

                Console.WriteLine("\nPress Enter to return to the main menu...");
                Console.ReadKey();
            }
        }

        //********************************************************
        // Start of case 1 : Dorm Management
        // DormitoryMenu

        static void DormitoryMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== Dormitory Management ====");
                Console.WriteLine("1. Add New Dormitory");
                Console.WriteLine("2. Remove Dormitory");
                Console.WriteLine("3. Edit Dormitory");
                Console.WriteLine("4. View All Dormitories");
                Console.WriteLine("0. Back to Main Menu");

                Console.Write("\nSelect an option: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddDormitory();
                        break;
                    case "2":
                        RemoveDormitory();
                        break;
                    case "3":
                        EditDormitory();
                        break;
                    case "4":
                        ViewDormitories();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid input. Press Enter to try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }
        // 1. Add Dormitory
        static void AddDormitory()
        {
            Console.Clear();
            Console.WriteLine("--- Add New Dormitory ---");

            Console.Write("Dormitory Name: ");
            string name = Console.ReadLine();

            Console.Write("Address: ");
            string address = Console.ReadLine();

            Console.Write("Capacity: ");
            int capacity = int.Parse(Console.ReadLine());

            Console.Write("Manager Name: ");
            string manager = Console.ReadLine();

            dormitories_list.Add(new Dormitory(name, address, capacity, manager));
            Console.WriteLine("Dormitory added successfully.");
            Console.Write("\nPress Enter to return to Dormitory Menu...");
            Console.ReadKey();
        }

        // 2. Remove Dormitory
        static void RemoveDormitory()
        {
            Console.Clear();
            Console.WriteLine("--- Remove Dormitory ---");

            Console.Write("Enter Dormitory Name to Remove: ");
            string name = Console.ReadLine();

            Dormitory target = dormitories_list.Find(d => d.Name == name);
            if (target != null)
            {
                dormitories_list.Remove(target);
                Console.WriteLine("Dormitory removed.");
            }
            else
            {
                Console.WriteLine("!! Dormitory not found !!");
            }
            Console.Write("\nPress Enter to return to Dormitory Menu...");
            Console.ReadKey();
        }

        // 3. Edit Dormitory

        static void EditDormitory()
        {
            Console.Clear();
            Console.WriteLine("--- Edit Dormitory ---");

            Console.Write("Enter Dormitory Name to Edit: ");
            string name = Console.ReadLine();

            Dormitory dorm = dormitories_list.Find(d => d.Name == name);
            if (dorm != null)
            {
                Console.Write("New Address: ");
                dorm.Address = Console.ReadLine();

                Console.Write("New Capacity: ");
                dorm.Capacity = int.Parse(Console.ReadLine());

                Console.Write("New Manager: ");
                dorm.Manager = Console.ReadLine();

                Console.WriteLine("Dormitory updated.");
            }
            else
            {
                Console.WriteLine("!! Dormitory not found !!");
            }
            Console.Write("\nPress Enter to return to Dormitory Menu...");
            Console.ReadKey();
        }

        // 4. View Dormitories

        static void ViewDormitories()
        {
            Console.Clear();
            Console.WriteLine("--- List of Dormitories ---");

            if (dormitories_list.Count == 0)
            {
                Console.WriteLine("No dormitories found.");
            }
            else
            {
                foreach (var dorm in dormitories_list)
                {
                    Console.WriteLine(dorm.ToString());
                }
            }
            Console.Write("\nPress Enter to return to Dormitory Menu...");
            Console.ReadKey();
        }
    }
}

        // End of case 1 : Dorm Management