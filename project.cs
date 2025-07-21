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
        public int Block { get; set; } // This "Block" likely refers to a block number, not a Block object.
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
        public string AssignedStudentNationalCode { get; set; } // This should probably be an int for NationalCode, matching Person.NationalCode.

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
        public string Manager { get; set; } // This "Manager" is just a string name, not a DormManager object.

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
        public string BlockManagerName { get; set; } // This "BlockManagerName" is just a string name, not a BlockManager object.

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
        static List<DormManager> dormManagers_list = new List<DormManager>();
        static List<BlockManager> blockManagers_list = new List<BlockManager>();
        static List<Student> students_list = new List<Student>();

        static void Main(string[] args)
        {
            ShowLogin();
            //ShowMainMenu();
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
                    ShowMainMenu();
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
                Console.WriteLine("4. Property Management"); // This currently just displays a placeholder message
                Console.WriteLine("5. Reports"); // This currently just displays a placeholder message
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
                        PeopleManagementMenu();
                        break;
                    case "4":
                        // TODO: Staff Management Page - This comment is misleading as it's labeled "Property Management"
                        Console.WriteLine("\n[Property Management - Not Implemented Yet]");
                        break;
                    case "5":
                        // TODO: Reports Page
                        Console.WriteLine("\n[Reports - Not Implemented Yet]");
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

            if (!int.TryParse(Console.ReadLine(), out int capacity))
            {
                Console.WriteLine("Invalid capacity. Please enter a number.");
                Console.Write("\nPress Enter to return to Dormitory Menu...");
                Console.ReadKey();
                return;
            }

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
                Console.Write($"New Address (current: {dorm.Address}): ");
                dorm.Address = Console.ReadLine();

                Console.Write($"New Capacity (current: {dorm.Capacity}): ");

                if (!int.TryParse(Console.ReadLine(), out int newCapacity))
                {
                    Console.WriteLine("Invalid capacity. Capacity not updated.");
                }
                else
                {
                    dorm.Capacity = newCapacity;
                }

                Console.Write($"New Manager (current: {dorm.Manager}): ");
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

        // End of case 1 : Dorm Management

        //****************************************************
        // Start of case 2 : Block Management
        // Block Menu

        static void ChooseDorm()
        {
            Console.Clear();
            if (dormitories_list.Count == 0)
            {
                Console.WriteLine("No dormitories found.");
                Console.Write("\nPress Enter to return to Main Menu...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("=== Select a Dormitory ===");
            for (int i = 0; i < dormitories_list.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {dormitories_list[i].Name}");
            }

            Console.Write("\nEnter number of dormitory: ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= dormitories_list.Count)
            {
                string selectedDorm = dormitories_list[choice - 1].Name;
                BlockMenu(selectedDorm);
            }
            else
            {
                Console.WriteLine("Invalid choice. Press Enter to return...");
                Console.ReadKey();
            }
        }

        static void BlockMenu(string dormName)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== Block Management for Dormitory: {dormName} ===");
                Console.WriteLine("1. Add New Block");
                Console.WriteLine("2. Remove Block");
                Console.WriteLine("3. Edit Block");
                Console.WriteLine("4. View All Blocks");
                Console.WriteLine("0. Back");

                Console.Write("\nSelect an option: ");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        AddBlock(dormName);
                        break;
                    case "2":
                        RemoveBlock(dormName);
                        break;
                    case "3":
                        EditBlock(dormName);
                        break;
                    case "4":
                        ViewBlocks(dormName);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid input. Press Enter to try again...");
                        Console.ReadKey();
                        break;
                }
            }
        }
        // 1. Add Block
        static void AddBlock(string dormName)
        {
            Console.Clear();
            Console.WriteLine("--- Add New Block ---");

            Console.Write("Block Name: ");
            string blockName = Console.ReadLine();

            Console.Write("Number of Floors: ");

            if (!int.TryParse(Console.ReadLine(), out int floorCount))
            {
                Console.WriteLine("Invalid number of floors. Please enter a number.");
                Console.Write("\nPress Enter to continue...");
                Console.ReadKey();
                return;
            }

            Console.Write("Number of Rooms: ");

            if (!int.TryParse(Console.ReadLine(), out int roomCount))
            {
                Console.WriteLine("Invalid number of rooms. Please enter a number.");
                Console.Write("\nPress Enter to continue...");
                Console.ReadKey();
                return;
            }

            Console.Write("Block Manager Name: ");
            string manager = Console.ReadLine();
            Block newBlock = new Block(blockName, floorCount, roomCount, manager);

            if (!dormBlocks.ContainsKey(dormName))
            {
                dormBlocks[dormName] = new List<Block>();
            }

            dormBlocks[dormName].Add(newBlock);

            Console.WriteLine("Block added successfully.");
            Console.Write("\nPress Enter to continue...");
            Console.ReadKey();
        }
        // 2. View Block
        static void ViewBlocks(string dormName)
        {
            Console.Clear();
            Console.WriteLine("--- Block List ---");

            if (dormBlocks.ContainsKey(dormName) && dormBlocks[dormName].Count > 0)
            {
                foreach (var block in dormBlocks[dormName])
                {
                    Console.WriteLine(block.ToString());
                }
            }
            else
            {
                Console.WriteLine("No blocks found.");
            }

            Console.Write("\nPress Enter to continue...");
            Console.ReadKey();
        }

        // 3. Remove Block
        static void RemoveBlock(string dormName)
        {
            Console.Clear();
            Console.WriteLine("--- Remove Block ---");

            if (dormBlocks.ContainsKey(dormName) && dormBlocks[dormName].Count > 0)
            {
                for (int i = 0; i < dormBlocks[dormName].Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {dormBlocks[dormName][i].BlockName}");
                }

                Console.Write("\nEnter block number to remove: ");

                if (int.TryParse(Console.ReadLine(), out int index))
                {
                    if (index >= 1 && index <= dormBlocks[dormName].Count)
                    {
                        dormBlocks[dormName].RemoveAt(index - 1);
                        Console.WriteLine("Block removed.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid selection.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
            else
            {
                Console.WriteLine("No blocks to remove.");
            }

            Console.Write("\nPress Enter to continue...");
            Console.ReadKey();
        }

        // 4. Edit Block
        static void EditBlock(string dormName)
        {
            Console.Clear();
            Console.WriteLine("--- Edit Block ---");

            if (dormBlocks.ContainsKey(dormName) && dormBlocks[dormName].Count > 0)
            {
                for (int i = 0; i < dormBlocks[dormName].Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {dormBlocks[dormName][i].BlockName}");
                }

                Console.Write("\nEnter block number to edit: ");

                if (int.TryParse(Console.ReadLine(), out int index))
                {
                    if (index >= 1 && index <= dormBlocks[dormName].Count)
                    {
                        var block = dormBlocks[dormName][index - 1];
                        Console.Write($"New Block Name (current: {block.BlockName}): ");
                        block.BlockName = Console.ReadLine();

                        Console.Write($"New Number of Floors (current: {block.FloorCount}): ");

                        if (!int.TryParse(Console.ReadLine(), out int newFloorCount))
                        {
                            Console.WriteLine("Invalid number of floors. Floor count not updated.");
                        }
                        else
                        {
                            block.FloorCount = newFloorCount;
                        }

                        Console.Write($"New Number of Rooms (current: {block.RoomCount}): ");

                        if (!int.TryParse(Console.ReadLine(), out int newRoomCount))
                        {
                            Console.WriteLine("Invalid number of rooms. Room count not updated.");
                        }
                        else
                        {
                            block.RoomCount = newRoomCount;
                        }

                        Console.Write($"New Block Manager Name (current: {block.BlockManagerName}): ");
                        block.BlockManagerName = Console.ReadLine();

                        Console.WriteLine("Block updated.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid selection.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
            else
            {
                Console.WriteLine("No blocks to edit.");
            }

            Console.Write("\nPress Enter to continue...");
            Console.ReadKey();
        }

        // End of Case 2 (Block Managment)
    }
}