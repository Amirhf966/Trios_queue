using System;
using System.Collections.Generic;

namespace DormitoryManagement
{
    public enum EquipName
    {
        Refrigerator = 1,
        Table = 2,
        Chair = 3,
        Bed = 4,
        Closet = 5
    }
    public enum Status
    {
        Healthy = 1,
        Faulty = 2,
        UnderRepair = 3
    }
    public class Equipment
    {
        public EquipName Type {  get; set; }
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


    //********************************************************************************
    static void PeopleManagementMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("***People Magement***");
                Console.WriteLine("1. Dorm Manager Management");
                Console.WriteLine("2. Block Manager Management");
                Console.WriteLine("3. Student Management");
                Console.WriteLine("0. Back to Main Menu");

                Console.Write("\nSelect an option: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        DormitoryManagementMenu(); break;
                    case "2":
                        BlockManagerManagementMenu(); break;
                    case "3":
                        StudentManagementMenu(); break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid input. Press Enter to try again.");
                        Console.ReadKey();
                        break;

                }
            }
        }

        static void DormManagerManagementMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== Dorm Manager Management ====");
                Console.WriteLine("1. Add New Dorm Manager");
                Console.WriteLine("2. Remove Dorm Manager");
                Console.WriteLine("3. Edit Dorm Manager Information");
                Console.WriteLine("4. View All Dorm Managers");
                Console.WriteLine("0. Back to People Management Menu");


                Console.Write("\nSelect an option: ");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        AddDormManager(); break;
                    case "2":
                        RemoveDormManager(); break;
                    case "3":
                        EditDormManager(); break;
                    case "4":
                        ViewDormManagers();
                    case "0": return;
                    default:
                        Console.WriteLine("Invalid input. Press Enter to try again.");
                        Console.ReadKey();
                        break;

                }
            }
        }


        static void BlockManagerManagementMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== Block Manager Management ====");
                Console.WriteLine("1. Add New Block Manager (from Students)");
                Console.WriteLine("2. Remove Block Manager");
                Console.WriteLine("3. Change Block Manager");
                Console.WriteLine("4. View All Block Managers");
                Console.WriteLine("0. Back to People Management Menu");


                Console.Write("\nSelect an option: ");
                string input = Console.ReadLine();


                switch (input)
                {
                    case "1":
                        AddBlockManager(); break;
                    case "2":
                        RemoveBlockManager(); break;
                    case "3":
                        ChangeBlockManager(); break;
                    case "4":
                        ViewBlockManagers(); break;
                    case "0": return;
                    default:
                        Console.WriteLine("Invalid input. Press Enter to try again.");
                        Console.ReadKey();
                        break;


                }
            }
        }


        static void StudentManagementMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== Student Management ====");
                Console.WriteLine("1. Add New Student");
                Console.WriteLine("2. Remove Student");
                Console.WriteLine("3. Edit Student Information");
                Console.WriteLine("4. Search Student");
                Console.WriteLine("5. View Full Student Information");
                Console.WriteLine("6. Register Student in Dorm");
                Console.WriteLine("7. Move Student");
                Console.WriteLine("0. Back to People Management Menu");

                Console.Write("\nSelect an option: ");
                string input = Console.Readline();

                switch (input)
                {
                    case "1":
                        AddStudent(); break;
                    case "2":
                        RemoveStudent(); break;
                    case "3":
                        EditStudent(); break;
                    case "4":
                        SearchStudent(); break;
                    case "5":
                        ViewFullStudentInformation(); break;
                    case "6":
                        RegisterStudentInDorm(); break;
                    case "7":
                        MoveStudent(); break;
                    case "0": return;
                    default:
                        Console.WriteLine("Invalid input. Press Enter to try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }
        static void AddDormManager()
        {
            Console.Clear();
            Console.WriteLine("*** Add New Dorm Manager ***");

            Console.Write("First name: ");
            string fName = Console.ReadLine();

            Console.Write("Last Name: ");
            string lName = Console.Readline();

            Console.Write("National Code: ");
            int nationalCode = int.Parse(Console.ReadLine());

            Console.Write("Phone Number: ");
            int phoneNumber = int.Parse(Console.Readline());

            Console.Write("Address: ");
            string address = Console.ReadLine();

            Console.Write("Post (e.g., Head Manager, Assistant Manager): ");
            string post = Console.ReadLine();

            Console.Write("Dormitory Under Responsibility: ");
            string dormResponsibility = Console.ReadLine();

            dormManagers_list.Add(new DormManager(fName, lName, nationalCode, phoneNumber, address, post, dormResponsibility));
            Console.WriteLine("Dorm Manager added successfully.");
            Console.Write("\nPress Enter to return to Dorm Manager Menu...");
            Console.ReadKey();
        }

        static void RemoveDormManager()
        {
            Console.Clear();
            Console.WriteLine("*** Remove Dorm Mnager ***");

            Console.Write("Enter Nation Code of Dorm Manager to Remove: ");
            if (int.tryParse(Console.ReaadLine, out int nationalCodeToRemove))
            {
                DormManager target = dormManagers_list.Find(dm => dm.NationalCode == nationalCodeToRemove);
                if (target != null)
                {
                    dormManager_list.Remove(target);
                    Console.WriteLine("Dorm Manager removed successfully.");
                }
                else
                {
                    Console.WriteLine("Dorm Manager not found !!!!");
                }
            }
            else
            {
                Console.WriteLine("Invalid National Code. Please enter a number.");
            }
            Console.Write("\nPress Enter to return to Dorm Manager Menu...");
            Console.ReadKey();
        }

        static void EditDormManager()
        {
            Console.Clear();
            Console.WriteLine("*** Edit Dorm Manager Information ***");

            Console.Write("Enter National Code of Dorm Manager to Edite: ");
            if (int.TryParse(Console.ReadLine(), out int nationalCodeToEdite))
            {
                DormManager target = dormManagers_list.Find(dm => dm.NationalCode == nationalCodeToEdit);
                if (target != null)
                {
                    Console.WriteLine($"Editing information for {target.FName} {target.LName}");

                    Console.Write($"New Phone Number (current: {target.PhoneNumber}): ");
                    
                    if (int.TryParse(Console.ReadLine(),out int newPhoneNumber))
                    {
                        target.PhoneNumber = newPhoneNumber;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Phone number not chaged.");
                    }
                    Console.Write($"New Address (current: {target.Address}):");
                    Console.ReadLine();

                    Console.Write($"New Post (current: {target.Post}): ");
                    target.Post = Console.ReadLine();

                    Console.Write($"New Dormitory Under Responsibility (current: {target.DormitoryUnderResponsibility}): ");
                    target.DormitoryUnderResponsibility = Console.ReadLine();

                    Console.WriteLine("Dorm Manager information updated successfully.");
                }
                else
                {
                    Console.WriteLine("Dorm Manager not found !!!");
                }
            }
            else
            {
                Console.WriteLine("Invalid National Code. Please enter a number.");
            }
            Console.Write("\nPress Enter to return to Dorm Manager Menu...");
            Console.ReadKey();
        }


        static void ViewDormManagers()
        {
            Console.Clear();
            Console.WriteLine("*** List of Dorm Manager ***");

            if (dormManagers_list.Conunt == 0)
            {
                Console.WriteLine("No Dorm Manager found.");
            }
            else
            {
                foreach (var manager in dormManagers_list)
                {
                    Console.WriteLine($"Name: {manager.FName} {manager.LName}, National Code: {manager.NationalCode}, Post: {manager.Post}, Dorm: {manager.DormitoryUnderResponsibility}");
                }
            }
            Console.Write("\nPress Enter to return to Dorm Manager Menu...");
            Console.ReadKey();
        }