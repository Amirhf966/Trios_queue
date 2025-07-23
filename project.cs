using System;
using System.Collections.Generic;
using System.Net;
using System.Xml.Linq;

namespace DormitoryManagement
{
    public enum EquipName
    {
        Refrigerator = 1,
        Table = 2,
        Chair = 3,
        Bed = 4,
        Closet = 5,
    }

    public enum Status
    {
        Healthy = 1,
        Faulty = 2,
        UnderRepair = 3
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
            string post ,string dormitoryUnderResponsibility )
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

        public List<Room> Rooms { get; set; } = new List<Room>();

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
        public Status status { get; set; }
        public int? RoomNumber { get; set; }
        public int? AssignedStudentNationalCode { get; set; }
        public int PartNumber { get; set; }

        public Equipment(EquipName type, int partNumber, string equipmentId, Status status, int? roomNumber)
        {
            Type = type;
            PartNumber = partNumber;
            EquipmentId = int.Parse(equipmentId);
            status = status;
            RoomNumber = roomNumber;
            AssignedStudentNationalCode = null;
        }

        public override string ToString()
        {
            return $"Type: {Type}, ID: {EquipmentId}, Part: {PartNumber}, Status: {status}, Room: {RoomNumber?.ToString() ?? "Unassigned"}, Student: {AssignedStudentNationalCode?.ToString() ?? "Unassigned"}";
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

        public List<Room> Rooms { get; set; }
        public Block(string name, int floorCount, int roomCount, string manager)
        {
            BlockName = name;
            FloorCount = floorCount;
            RoomCount = roomCount;
            BlockManagerName = manager;
            Rooms = new List<Room>();
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
        static List<Equipment> allEquipment_list = new List<Equipment>();
        static List<string> repairRequests = new List<string>();
        public static List<Block> blocks_list = new List<Block>();

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
                Console.Title = "Dormitory Management Proram";
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
                        PropertyManagementMenu();
                        break;
                    case "5":
                        ReportsMenu();
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
            Console.WriteLine("--- Add Dormitory ---");

            Console.Write("Dormitory Name: ");
            string name = Console.ReadLine();

            Console.Write("Dormitory Address: ");
            string address = Console.ReadLine();

            Console.Write("Dormitory Capacity: ");
            if (!int.TryParse(Console.ReadLine(), out int capacity))
            {
                Console.WriteLine("Invalid capacity. Please enter a number.");
                Console.ReadKey();
                return;
            }

            Dormitory newDorm = new Dormitory(name, address, capacity, null); 
            dormitories_list.Add(newDorm);

            Console.WriteLine("Dormitory added successfully (no manager assigned yet).");
            Console.Write("\nPress Enter to return to Dormitory Menu...");
            Console.ReadKey();
        }


        // 2. Remove Dormitory
        static void RemoveDormitory()
        {
            Console.Clear();
            Console.WriteLine("--- Remove Dormitory ---");

            if (dormitories_list.Count == 0)
            {
                Console.WriteLine("No dormitories available.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Select a dormitory to remove:");
            for (int i = 0; i < dormitories_list.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {dormitories_list[i].Name}");
            }

            Console.Write("\nEnter number of dormitory to remove: ");
            if (int.TryParse(Console.ReadLine(), out int index) &&
                index >= 1 && index <= dormitories_list.Count)
            {
                var removedDorm = dormitories_list[index - 1];
                dormitories_list.RemoveAt(index - 1);
                Console.WriteLine($"Dormitory '{removedDorm.Name}' removed successfully.");
            }
            else
            {
                Console.WriteLine("Invalid selection.");
            }

            Console.Write("\nPress Enter to return to Dormitory Menu...");
            Console.ReadKey();
        }


        // 3. Edit Dormitory

        static void EditDormitory()
        {
            Console.Clear();
            Console.WriteLine("--- Edit Dormitory ---");

            if (dormitories_list.Count == 0)
            {
                Console.WriteLine("No dormitories available.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Select a dormitory to edit:");
            for (int i = 0; i < dormitories_list.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {dormitories_list[i].Name}");
            }

            Console.Write("\nEnter number of dormitory to edit: ");
            if (int.TryParse(Console.ReadLine(), out int index) &&
                index >= 1 && index <= dormitories_list.Count)
            {
                Dormitory dorm = dormitories_list[index - 1];

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
                Console.WriteLine("Invalid selection.");
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
                    string managerDisplay = string.IsNullOrWhiteSpace(dorm.Manager) ? "None" : dorm.Manager;
                    Console.WriteLine($"Name: {dorm.Name}, Address: {dorm.Address}, Capacity: {dorm.Capacity}, Manager: {managerDisplay}");
                }
            }

            Console.Write("\nPress Enter to return to Dormitory Menu...");
            Console.ReadKey();
        }


        // End of case 1 : Dorm Management
        //****************************************************

        static int GetValidatedIntegerInput(string prompt, string errorMessage)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (int.TryParse(input, out int value))
                    return value;
                Console.WriteLine(errorMessage);
            }
        }


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
                Console.WriteLine("5. Room Management");
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
                    case "5":
                        RoomManagementMenu();
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

            Block newBlock = new Block(blockName, floorCount, roomCount, null);

            if (!dormBlocks.ContainsKey(dormName))
            {
                dormBlocks[dormName] = new List<Block>();
            }
            dormBlocks[dormName].Add(newBlock);

            blocks_list.Add(newBlock);

            Console.WriteLine("Block added successfully (no manager assigned yet).");
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
                    string managerDisplay = string.IsNullOrWhiteSpace(block.BlockManagerName) ? "None" : block.BlockManagerName;


                    Console.WriteLine($"Block Name: {block.BlockName}, Floors: {block.FloorCount}, Rooms: {block.RoomCount}, Manager: {managerDisplay}");
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

        // 5. Room Managment

        public static void RoomManagementMenu()
        {
            Console.Clear();
            if (blocks_list.Count == 0)
            {
                Console.WriteLine("No blocks available. Add blocks first.");
                Console.ReadKey(); return;
            }

            Console.WriteLine("*** Select Block for Room Management ***");
            for (int i = 0; i < blocks_list.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Block {blocks_list[i].BlockName}");
            }

            Console.Write("Enter number: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= blocks_list.Count)
            {
                Block selectedBlock = blocks_list[index - 1];
                ManageRoomsInBlock(selectedBlock);
            }
            else
            {
                Console.WriteLine("Invalid selection.");
                Console.ReadKey();
            }
        }


        public static void ManageRoomsInBlock(Block block)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== Room Management for Block {block.BlockName} ===");
                Console.WriteLine("1. Add Room");
                Console.WriteLine("2. Remove Room");
                Console.WriteLine("3. Edit Room");
                Console.WriteLine("4. View Rooms");
                Console.WriteLine("0. Back");

                Console.Write("Select an option: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1": AddRoomToBlock(block); break;
                    case "2": RemoveRoomFromBlock(block); break;
                    case "3": EditRoomInBlock(block); break;
                    case "4": ViewRoomsInBlock(block); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid input."); Console.ReadKey(); break;
                }
            }
        }


        public static void AddRoomToBlock(Block block)
        {
            Console.Clear();
            Console.WriteLine($"Add Room to Block {block.BlockName}");

            //Console.Write("Enter Room Number: ");
            int roomNumber = GetValidatedIntegerInput("Enter Room Number: ", "Invalid Room Number. Please enter a number.");

            Console.Write("Enter Floor: ");
            int floor = GetValidatedIntegerInput("Enter floor: ", "Invalid floor. Please enter a number.");


            Console.Write("Enter Capacity: ");
            int capacity = GetValidatedIntegerInput("Enter Capacity: ", "Invalid Capacity. Please enter a number.");

            block.Rooms.Add(new Room(roomNumber, floor, capacity, new List<Equipment>(), new List<Student>()));
            Console.WriteLine("Room added.");
            Console.ReadKey();
        }

        public static void RemoveRoomFromBlock(Block block)
        {
            Console.Clear();
            Console.WriteLine($"Remove Room from Block {block.BlockName}");

            Console.Write("Enter Room Number: ");
            int roomNumber = int.Parse(Console.ReadLine());

            var room = block.Rooms.Find(r => r.RoomNumber == roomNumber);
            if (room != null)
            {
                block.Rooms.Remove(room);
                Console.WriteLine("Room removed.");
            }
            else Console.WriteLine("Room not found.");

            Console.ReadKey();
        }

        public static void EditRoomInBlock(Block block)
        {
            Console.Clear();
            Console.WriteLine($"Edit Room in Block {block.BlockName}");

            Console.Write("Enter Room Number: ");
            int roomNumber = int.Parse(Console.ReadLine());

            var room = block.Rooms.Find(r => r.RoomNumber == roomNumber);
            if (room != null)
            {
                Console.Write($"New Floor (current: {room.Floor}): ");
                room.Floor = int.Parse(Console.ReadLine());

                Console.Write($"New Capacity (current: {room.Capacity}): ");
                room.Capacity = int.Parse(Console.ReadLine());

                Console.WriteLine("Room updated.");
            }
            else Console.WriteLine("Room not found.");

            Console.ReadKey();
        }

        public static void ViewRoomsInBlock(Block block)
        {
            Console.Clear();
            Console.WriteLine($"Rooms in Block {block.BlockName}");

            if (block.Rooms.Count == 0)
            {
                Console.WriteLine("No rooms.");
            }
            else
            {
                foreach (var room in block.Rooms)
                {
                    Console.WriteLine($"Room {room.RoomNumber} | Floor: {room.Floor} | Capacity: {room.Capacity}");
                }
            }

            Console.ReadKey();
        }





       

        // End of Case 2 (Block Managment)

        // start of case 3 (people managment)
        static void PeopleManagementMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("***People Management***");
                Console.WriteLine("1. Dorm Manager Management");
                Console.WriteLine("2. Block Manager Management");
                Console.WriteLine("3. Student Management");
                Console.WriteLine("0. Back to Main Menu");
                Console.Write("\nSelect an option: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        DormManagerManagementMenu();
                        break;
                    case "2":
                        BlockManagerManagementMenu();
                        break;
                    case "3":
                        StudentManagementMenu();
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

        // 3 => 1
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
                        AddDormManager();
                        break;
                    case "2":
                        RemoveDormManager();
                        break;
                    case "3":
                        EditDormManager();
                        break;
                    case "4":
                        ViewDormManagers();
                        break;
                    case "0": return;
                    default:
                        Console.WriteLine("Invalid input. Press Enter to try again.");
                        Console.ReadKey();
                        break;

                }
            }
        }

        // 3=>1=>1
        static void AddDormManager()
        {
            Console.Clear();
            Console.WriteLine("*** Add New Dorm Manager ***");

            Console.Write("First name: ");
            string fName = Console.ReadLine();

            Console.Write("Last Name: ");
            string lName = Console.ReadLine();

            Console.Write("National Code: ");
            if (!int.TryParse(Console.ReadLine(), out int nationalCode))
            {
                Console.WriteLine("Invalid National Code. Please enter a number.");
                Console.Write("\nPress Enter to return to Dorm Manager Menu...");
                Console.ReadKey();
                return;
            }

            Console.Write("Phone Number: ");
            if (!int.TryParse(Console.ReadLine(), out int phoneNumber))
            {
                Console.WriteLine("Invalid Phone Number. Please enter a number.");
                Console.Write("\nPress Enter to return to Dorm Manager Menu...");
                Console.ReadKey();
                return;
            }

            Console.Write("Address: ");
            string address = Console.ReadLine();

            Console.Write("Post (e.g., Head Manager, Assistant Manager): ");
            string post = Console.ReadLine();

            if (dormitories_list.Count == 0)
            {
                Console.WriteLine("No dormitory defined! Please add a dormitory first.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nAvailable dormitories:");
            for (int i = 0; i < dormitories_list.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {dormitories_list[i].Name}");
            }

            Console.Write("Which dormitory would you like the person to be assigned as manager of?\nSelect the desired dormitory number: ");
            if (int.TryParse(Console.ReadLine(), out int dormIndex) && dormIndex > 0 && dormIndex <= dormitories_list.Count)
            {
                Dormitory selectedDorm = dormitories_list[dormIndex - 1];
                string dormResponsibility = selectedDorm.Name;

                DormManager newManager = new DormManager(fName, lName, nationalCode, phoneNumber, address, post, dormResponsibility);
                dormManagers_list.Add(newManager);

                selectedDorm.Manager = $"{fName} {lName}";

                Console.WriteLine("Dorm Manager added and assigned successfully.");
            }
            else
            {
                Console.WriteLine("Invalid selection. Operation cancelled.");
            }

            Console.Write("\nPress Enter to return to Dorm Manager Menu...");
            Console.ReadKey();
        }



        // 3=>1=>2
        static void RemoveDormManager()
        {
            Console.Clear();
            Console.WriteLine("*** Remove Dorm Manager ***");

            Console.Write("Enter National Code of Dorm Manager to Remove: ");
            if (int.TryParse(Console.ReadLine(), out int nationalCodeToRemove))
            {
                DormManager target = dormManagers_list.Find(dm => dm.NationalCode == nationalCodeToRemove);
                if (target != null)
                {
                    dormManagers_list.Remove(target);
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

        // 3=>1=>3
        public static void EditDormManager()
        {
            Console.Clear();
            Console.WriteLine("*** Edit Dorm Manager ***");

            Console.Write("Enter National Code to edit: ");
            if (int.TryParse(Console.ReadLine(), out int code))
            {

                var target = dormManagers_list.Find(dm => dm.NationalCode == code);
                if (target != null)
                {
                    Console.WriteLine($"Editing {target.FName} {target.LName}");
                    Console.Write($"New Phone Number (current: {target.PhoneNumber}): ");

                    if (int.TryParse(Console.ReadLine(), out int newPhone))
                    {
                        target.PhoneNumber = newPhone;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Phone Number. Phone number not updated.");
                    }

                    Console.Write($"New Address (current: {target.Address}): ");
                    target.Address = Console.ReadLine();

                    Console.Write($"New Post (current: {target.Post}): ");
                    target.Post = Console.ReadLine();

                    Console.Write($"New Dormitory (current: {target.DormitoryUnderResponsibility}): ");
                    target.DormitoryUnderResponsibility = Console.ReadLine();
                    Console.WriteLine("Dorm Manager updated.");
                }
                else
                {
                    Console.WriteLine("Dorm Manager not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number for National Code.");
            }

            Console.Write("\nPress Enter to return...");
            Console.ReadKey();
        }

        // 3=>1=>4
        public static void ViewDormManagers()
        {
            Console.Clear();
            Console.WriteLine("*** List of Dorm Managers ***");


            if (dormManagers_list.Count == 0)
            {
                Console.WriteLine("No Dorm Managers found.");
            }
            else
            {
                foreach (var dm in dormManagers_list)
                {
                    Console.WriteLine($"Name: {dm.FName} {dm.LName} | Code: {dm.NationalCode} | Post: {dm.Post} | Dorm: {dm.DormitoryUnderResponsibility}");
                }
            }

            Console.Write("\nPress Enter to return...");
            Console.ReadKey();
        }

        // 3=>2
        public static void BlockManagerManagementMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== Block Manager Management ====");
                Console.WriteLine("1. Add New Block Manager (from Students)");
                Console.WriteLine("2. Remove Block Manager");
                Console.WriteLine("3. Change Block Manager");
                Console.WriteLine("4. View All Block Managers");
                Console.WriteLine("0. Back");

                Console.Write("\nSelect an option: ");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1": AddBlockManager(); break;
                    case "2": RemoveBlockManager(); break;
                    case "3": ChangeBlockManager(); break;
                    case "4": ViewBlockManagers(); break;
                    case "0": return;
                    default:
                        Console.WriteLine("Invalid input. Press Enter to try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        //3=>2=>1
        public static void AddBlockManager()
        {
            Console.Clear();
            Console.WriteLine("*** Add New Block Manager ***");

            if (students_list.Count == 0)
            {
                Console.WriteLine("No students available. Add students first.");
                Console.ReadKey(); return;
            }

            Console.WriteLine("Select a student to assign as Block Manager:");
            for (int i = 0; i < students_list.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {students_list[i].FName} {students_list[i].LName} (ID: {students_list[i].Id})");
            }

            Console.Write("Enter student number: ");
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > students_list.Count)
            {
                Console.WriteLine("Invalid input.");
                Console.ReadKey();
                return;
            }

            Student selected = students_list[choice - 1];

            if (blockManagers_list.Exists(bm => bm.NationalCode == selected.NationalCode))
            {
                Console.WriteLine("Student is already a Block Manager.");
                Console.ReadKey();
                return;
            }

          
            if (blocks_list.Count == 0)
            {
                Console.WriteLine("No blocks available. Add blocks first.");
                Console.ReadKey(); return;
            }

            Console.WriteLine("Select a block to assign:");
            for (int i = 0; i < blocks_list.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {blocks_list[i].BlockName}");
            }

            Console.Write("Enter block number: ");
            if (!int.TryParse(Console.ReadLine(), out int blockChoice) || blockChoice < 1 || blockChoice > blocks_list.Count)
            {
                Console.WriteLine("Invalid block selection.");
                Console.ReadKey();
                return;
            }

            Block selectedBlock = blocks_list[blockChoice - 1];

            blockManagers_list.Add(new BlockManager(selected.FName, selected.LName, selected.NationalCode, selected.PhoneNumber, selected.Address, "Block Manager", selectedBlock.BlockName));


            selectedBlock.BlockManagerName = $"{selected.FName} {selected.LName}";


            Console.WriteLine("Block Manager added and assigned successfully.");
            Console.ReadKey();
        }


        // 3=>2=>2
        public static void RemoveBlockManager()
        {
            Console.Clear();
            Console.WriteLine("*** Remove Block Manager ***");

            Console.Write("Enter National Code to remove: ");
            if (int.TryParse(Console.ReadLine(), out int code))
            {

                var target = blockManagers_list.Find(bm => bm.NationalCode == code);
                if (target != null)
                {
                    blockManagers_list.Remove(target);
                    Console.WriteLine("Block Manager removed.");
                }
                else Console.WriteLine("Not found.");
            }
            else Console.WriteLine("Invalid input. Please enter a number.");

            Console.ReadKey();
        }

        // 3=>2=>3
        public static void ChangeBlockManager()
        {
            Console.Clear();
            Console.WriteLine("*** Change Block Manager ***");


            if (blockManagers_list.Count == 0)
            {
                Console.WriteLine("No Block Manager to change.");
                Console.ReadKey(); return;
            }

            Console.Write("Enter current Block Manager National Code: ");
            if (int.TryParse(Console.ReadLine(), out int code))
            {

                var current = blockManagers_list.Find(bm => bm.NationalCode == code);
                if (current != null)
                {
                    Console.WriteLine($"Current: {current.FName} {current.LName}, Block: {current.BlockUnderResponsibility}");
                    Console.WriteLine("Select a new student:");

                    for (int i = 0; i < students_list.Count; i++)
                    {
                        if (!blockManagers_list.Exists(bm => bm.NationalCode == students_list[i].NationalCode))
                        {
                            Console.WriteLine($"{i + 1}. {students_list[i].FName} {students_list[i].LName} (ID: {students_list[i].Id})");
                        }
                    }

                    Console.Write("Enter number: ");

                    if (int.TryParse(Console.ReadLine(), out int sel) && sel > 0 && sel <= students_list.Count)
                    {
                        Student selected = students_list[sel - 1];
                        blockManagers_list.Remove(current);
                        blockManagers_list.Add(new BlockManager(selected.FName, selected.LName, selected.NationalCode, selected.PhoneNumber, selected.Address, "Block Manager", current.BlockUnderResponsibility));
                        Console.WriteLine("Block Manager changed.");
                    }
                    else Console.WriteLine("Invalid selection.");
                }
                else Console.WriteLine("Manager not found.");
            }
            else Console.WriteLine("Invalid input. Please enter a number for National Code.");

            Console.ReadKey();
        }

        // 3=>2=>4
        public static void ViewBlockManagers()
        {
            Console.Clear();
            Console.WriteLine("*** List of Block Managers ***");


            if (blockManagers_list.Count == 0)
                Console.WriteLine("None found.");
            else
            {
                foreach (var bm in blockManagers_list)
                {
                    Console.WriteLine($"Name: {bm.FName} {bm.LName}, Code: {bm.NationalCode}, Block: {bm.BlockUnderResponsibility}");
                }
            }

            Console.ReadKey();
        }

        // 3=>3
        public static void StudentManagementMenu()
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
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1": AddStudent(); break;
                    case "2": RemoveStudent(); break;
                    case "3": EditStudent(); break;
                    case "4": SearchStudent(); break;
                    case "5": ViewFullStudentInformation(); break;
                    case "6": RegisterStudentInDorm(); break;
                    case "7": MoveStudent(); break;
                    case "0": return;
                    default:
                        Console.WriteLine("Invalid input. Press Enter to try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // 3=>3=>1
        public static void AddStudent()
        {
            Console.Clear();
            Console.WriteLine("*** Add New Student ***");

            Console.Write("First Name: ");
            string fName = Console.ReadLine();

            Console.Write("Last Name: ");
            string lName = Console.ReadLine();
            Console.Write("Student ID: ");

            if (!int.TryParse(Console.ReadLine(), out int studentId))
            {
                Console.WriteLine("Invalid Student ID. Please enter a number.");
                Console.ReadKey();
                return;
            }

            Console.Write("National Code: ");

            if (!int.TryParse(Console.ReadLine(), out int nationalCode))
            {
                Console.WriteLine("Invalid National Code. Please enter a number.");
                Console.ReadKey();
                return;
            }

            Console.Write("Phone Number: ");

            if (!int.TryParse(Console.ReadLine(), out int phoneNumber))
            {
                Console.WriteLine("Invalid Phone Number. Please enter a number.");
                Console.ReadKey();
                return;
            }

            Console.Write("Address: ");
            string address = Console.ReadLine();

            students_list.Add(new Student(fName, lName, nationalCode, phoneNumber, address, studentId, 0, 0, "Not Assigned"));
            Console.WriteLine("Student added successfully.");
            Console.ReadKey();
        }

        // 3=>3=>2
        public static void RemoveStudent()
        {
            Console.Clear();
            Console.WriteLine("*** Remove Student ***");

            if (students_list.Count == 0)
            {
                Console.WriteLine("No students found.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Select a student to remove:");
            for (int i = 0; i < students_list.Count; i++)
            {
                var s = students_list[i];
                Console.WriteLine($"{i + 1}. {s.FName} {s.LName} - ID: {s.Id}");
            }

            Console.Write("\nEnter the number of the student to remove: ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= students_list.Count)
            {
                var selectedStudent = students_list[choice - 1];
                students_list.Remove(selectedStudent);
                Console.WriteLine("Student removed successfully.");
            }
            else
            {
                Console.WriteLine("Invalid selection.");
            }

            Console.ReadKey();
        }


        // 3=>3=>3
        public static void EditStudent()
        {
            Console.Clear();
            Console.WriteLine("*** Edit Student ***");

            Console.Write("Enter Student ID to edit: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var student = students_list.Find(s => s.Id == id);
                if (student != null)
                {
                    Console.WriteLine($"Editing {student.FName} {student.LName}");
                    Console.Write($"New Phone Number (current: {student.PhoneNumber}): ");
                    if (int.TryParse(Console.ReadLine(), out int newPhone))
                        student.PhoneNumber = newPhone;
                    else
                    {
                        Console.WriteLine("Invalid Phone Number. Phone number not updated.");
                    }
                    Console.Write($"New Address (current: {student.Address}): ");
                    student.Address = Console.ReadLine();

                    Console.WriteLine("Student info updated.");
                }
                else Console.WriteLine("Student not found.");
            }
            else Console.WriteLine("Invalid ID. Please enter a number.");

            Console.ReadKey();
        }

        // 3=>3=>4
        public static void SearchStudent()
        {
            Console.Clear();
            Console.WriteLine("*** Search Student ***");

            Console.Write("Enter Name or Student ID: ");
            string term = Console.ReadLine();
            var results = students_list.FindAll(s =>
                s.FName.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                s.LName.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                s.Id.ToString() == term
            );
            if (results.Count == 0)
                Console.WriteLine("No matches.");
            else
            {
                foreach (var s in results)
                {
                    Console.WriteLine($"Name: {s.FName} {s.LName}, ID: {s.Id}, Dorm: {s.Dorm}, Block: {s.Block}, Room: {s.RoomNumber}");
                    if (s.Equipment.Count > 0)
                    {
                        Console.WriteLine("  Equipment:");
                        foreach (var eq in s.Equipment)
                        {
                            Console.WriteLine($"   - {eq.Type} (ID: {eq.EquipmentId}, Status: {eq.status})");
                        }
                    }
                }
            }

            Console.ReadKey();
        }

        //3=>3=>5
        public static void ViewFullStudentInformation()
        {
            Console.Clear();
            Console.WriteLine("*** All Students ***");
            if (students_list.Count == 0)
            {
                Console.WriteLine("No students.");
            }
            else
            {
                foreach (var s in students_list)
                {
                    Console.WriteLine($"Name: {s.FName} {s.LName}, ID: {s.Id}, Dorm: {s.Dorm}, Block: {s.Block}, Room: {s.RoomNumber}");
                }
            }

            Console.ReadKey();
        }

        //3=>3=>6
        public static void RegisterStudentInDorm()
        {
            Console.Clear();
            Console.WriteLine("*** Register Student in Dorm ***");

            if (students_list.Count == 0)
            {
                Console.WriteLine("No students found.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Select a student to register:");
            for (int i = 0; i < students_list.Count; i++)
            {
                var s = students_list[i];
                Console.WriteLine($"{i + 1}. {s.FName} {s.LName} - ID: {s.Id}");
            }

            Console.Write("\nEnter the number of the student: ");
            if (!int.TryParse(Console.ReadLine(), out int studentChoice) || studentChoice < 1 || studentChoice > students_list.Count)
            {
                Console.WriteLine("Invalid selection.");
                Console.ReadKey();
                return;
            }

            var selectedStudent = students_list[studentChoice - 1];

            if (dormitories_list.Count == 0)
            {
                Console.WriteLine("No dormitories found. Please add a dormitory first.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nAvailable Dormitories:");
            for (int i = 0; i < dormitories_list.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {dormitories_list[i].Name}");
            }

            Console.Write("\nSelect the dormitory number: ");
            if (!int.TryParse(Console.ReadLine(), out int dormChoice) || dormChoice < 1 || dormChoice > dormitories_list.Count)
            {
                Console.WriteLine("Invalid dormitory selection.");
                Console.ReadKey();
                return;
            }

            string selectedDorm = dormitories_list[dormChoice - 1].Name;

            if (!dormBlocks.ContainsKey(selectedDorm) || dormBlocks[selectedDorm].Count == 0)
            {
                Console.WriteLine("No blocks found for this dormitory.");
                Console.ReadKey();
                return;
            }

            var blocks = dormBlocks[selectedDorm];

            Console.WriteLine("\nAvailable Blocks:");
            for (int i = 0; i < blocks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {blocks[i].BlockName}");
            }

            Console.Write("Select the block number: ");
            if (!int.TryParse(Console.ReadLine(), out int blockChoice) || blockChoice < 1 || blockChoice > blocks.Count)
            {
                Console.WriteLine("Invalid block selection.");
                Console.ReadKey();
                return;
            }

            var selectedBlock = blocks[blockChoice - 1];

            if (selectedBlock.Rooms == null || selectedBlock.Rooms.Count == 0)
            {
                Console.WriteLine("No rooms found in this block.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nAvailable Rooms:");
            for (int i = 0; i < selectedBlock.Rooms.Count; i++)
            {
                var room = selectedBlock.Rooms[i];
                Console.WriteLine($"{i + 1}. Room {room.RoomNumber} - Floor: {room.Floor}, Capacity: {room.Capacity}");
            }

            Console.Write("Select the room number: ");
            if (!int.TryParse(Console.ReadLine(), out int roomChoice) || roomChoice < 1 || roomChoice > selectedBlock.Rooms.Count)
            {
                Console.WriteLine("Invalid room selection.");
                Console.ReadKey();
                return;
            }

            var selectedRoom = selectedBlock.Rooms[roomChoice - 1];

            selectedStudent.Dorm = selectedDorm;
            selectedStudent.Block = blockChoice;
            selectedStudent.RoomNumber = selectedRoom.RoomNumber;

            selectedRoom.Students.Add(selectedStudent);

            Console.WriteLine("Student registered in dorm successfully.");
            Console.ReadKey();
        }



        // 3=>3=>7
        public static void MoveStudent()
        {
            Console.Clear();
            Console.WriteLine("*** Move Student to Another Room ***");

            Console.Write("Enter Student ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var student = students_list.Find(s => s.Id == id);
                if (student != null)
                {
                    Console.WriteLine($"Current Room: {student.RoomNumber} in Block {student.Block}, Dorm: {student.Dorm}");
                    Console.Write($"Enter New Dorm (current: {student.Dorm}): ");
                    student.Dorm = Console.ReadLine();

                    Console.Write($"Enter New Block (current: {student.Block}): ");
                    if (!int.TryParse(Console.ReadLine(), out int newBlock))
                    {
                        Console.WriteLine("Invalid Block Number. Block not updated.");
                        Console.ReadKey();
                        return;
                    }
                    student.Block = newBlock;

                    Console.Write($"Enter New Room Number (current: {student.RoomNumber}): ");
                    if (!int.TryParse(Console.ReadLine(), out int newRoomNumber))
                    {
                        Console.WriteLine("Invalid Room Number. Room not updated.");
                        Console.ReadKey();
                        return;
                    }
                    student.RoomNumber = newRoomNumber;

                    Console.WriteLine("Student moved.");
                }
                else Console.WriteLine("Student not found.");
            }
            else Console.WriteLine("Invalid ID. Please enter a number.");

            Console.ReadKey();
        }




        static void PropertyManagementMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== Property Management ====");
                Console.WriteLine("1. Register New Equipment"); 
                Console.WriteLine("2. Assign Equipment to Room"); 
                Console.WriteLine("3. Assign Personal Equipment to Student"); 
                Console.WriteLine("4. Manage Equipment Movement"); 
                Console.WriteLine("5. Manage Repairs"); 
                Console.WriteLine("0. Back to Main Menu");

                Console.Write("\nSelect an option: ");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        RegisterNewEquipment();
                        break;
                    case "2":
                        AssignEquipmentToRoom();
                        break;
                    case "3":
                        AssignPersonalEquipmentToStudent();
                        break;
                    case "4":
                        ManageEquipmentMovement();
                        break;
                    case "5":
                        ManageRepairs();
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

        static void RegisterNewEquipment()
        {
            Console.Clear();
            Console.WriteLine("--- Register New Equipment ---");

            Console.Write("Equipment Type (1: Refrigerator, 2: Table, 3: Chair, 4: Bed, 5: Closet): ");
            if (!int.TryParse(Console.ReadLine(), out int typeInt) || !Enum.IsDefined(typeof(EquipName), typeInt))
            {
                Console.WriteLine("Invalid equipment type. Please enter a valid number (1-5).");
                Console.ReadKey();
                return;
            }
            EquipName type = (EquipName)typeInt;

            Console.Write("Part Number (001 to 005): ");
            if (!int.TryParse(Console.ReadLine(), out int partNumber) || partNumber < 1 || partNumber > 5)
            {
                Console.WriteLine("Invalid Part Number. Please enter a number between 1 and 5.");
                Console.ReadKey();
                return;
            }

            string equipmentId = GenerateEquipmentId(partNumber);
            Console.WriteLine($"Generated Equipment ID: {equipmentId}");

            Equipment newEquipment = new Equipment(type, partNumber, equipmentId, Status.Healthy, null);
            allEquipment_list.Add(newEquipment);
            Console.WriteLine("Equipment registered successfully.");
            Console.ReadKey();
        }


        // Helper to generate 8-digit equipment ID

        static string GenerateEquipmentId(int partNumber)
        {
            // Simple generation, could be more complex (e.g., using a counter per part number)
            //For demonstration, let's just use current timestamp + part number
            Random rand = new Random();
            string timestamp = DateTime.Now.Ticks.ToString();
            // Take last 5 digits of timestamp for uniqueness, append part number and then pad to 8 digits
            string idSuffix = timestamp.Substring(Math.Max(0, timestamp.Length - 5));
            return $"{partNumber.ToString().PadLeft(3, '0')}{idSuffix}".PadRight(8, '0').Substring(0, 8); // Ensure 8 digits
        }

        //static string GenerateEquipmentId(int partNumber)
        //{
        //    Random rand = new Random();
        //    int randomNumber = rand.Next(0, 99999); 

        //    string part = partNumber.ToString().PadLeft(3, '0');      
        //    string random = randomNumber.ToString().PadLeft(5, '0');  

        //    return part + random; 
        //}


        // 4 => 2: Assign Equipment to Room 
        static void AssignEquipmentToRoom()
        {
            Console.Clear();
            Console.WriteLine("*** Assign Equipment to Room ***");

            
            List<Equipment> unassignedEquipments = allEquipment_list
                .Where(e => !e.RoomNumber.HasValue && !e.AssignedStudentNationalCode.HasValue)
                .ToList();

            if (unassignedEquipments.Count == 0)
            {
                Console.WriteLine("No unassigned equipment available. Please register equipment first.");
                Console.ReadKey();
                return;
            }

            // ساخت لیست همه اتاق‌ها
            List<(Room room, Block block, Dormitory dorm)> allRooms = new();
            foreach (var dorm in dormitories_list)
            {
                if (dormBlocks.ContainsKey(dorm.Name))
                {
                    foreach (var block in dormBlocks[dorm.Name])
                    {
                        foreach (var room in block.Rooms)
                        {
                            allRooms.Add((room, block, dorm));
                        }
                    }
                }
            }

            if (allRooms.Count == 0)
            {
                Console.WriteLine("No rooms available. Please add rooms first.");
                Console.ReadKey();
                return;
            }

            
            Console.WriteLine("Available Equipment:");
            for (int i = 0; i < unassignedEquipments.Count; i++)
            {
                var eq = unassignedEquipments[i];
                //Console.WriteLine($"{i + 1}. ID: {eq.EquipmentId} | Type: {eq.Type} | Part: {eq.PartNumber} | Status: {eq.status}");
                Console.WriteLine($"{i + 1}. ID: {eq.EquipmentId.ToString("D8")} | Type: {eq.Type} | Part: {eq.PartNumber} | Status: {eq.status}");

            }

            Console.Write("Select the number of equipment to assign: ");
            if (!int.TryParse(Console.ReadLine(), out int selectedEquipIndex) || selectedEquipIndex < 1 || selectedEquipIndex > unassignedEquipments.Count)
            {
                Console.WriteLine("Invalid equipment selection.");
                Console.ReadKey();
                return;
            }

            Equipment selectedEquipment = unassignedEquipments[selectedEquipIndex - 1];

            
            Console.WriteLine("\nAvailable Rooms:");
            for (int i = 0; i < allRooms.Count; i++)
            {
                var entry = allRooms[i];
                Console.WriteLine($"{i + 1}. Room {entry.room.RoomNumber} - Block: {entry.block.BlockName}, Dorm: {entry.dorm.Name}");
            }

            Console.Write("Select the room number to assign equipment: ");
            if (!int.TryParse(Console.ReadLine(), out int selectedRoomIndex) || selectedRoomIndex < 1 || selectedRoomIndex > allRooms.Count)
            {
                Console.WriteLine("Invalid room selection.");
                Console.ReadKey();
                return;
            }

            var selectedRoom = allRooms[selectedRoomIndex - 1].room;

            
            selectedEquipment.RoomNumber = selectedRoom.RoomNumber;
            selectedRoom.Equipments.Add(selectedEquipment);

            Console.WriteLine($"Equipment ID {selectedEquipment.EquipmentId} assigned to Room {selectedRoom.RoomNumber}.");
            Console.ReadKey();
        }




        // 4 => 3: Assign Personal Equipment to Student [cite: 228]
        static void AssignPersonalEquipmentToStudent()
        {
            Console.Clear();
            Console.WriteLine("--- Assign Personal Equipment to Student ---");

            if (allEquipment_list.Count == 0)
            {
                Console.WriteLine("No equipment registered yet.");
                Console.ReadKey();
                return;
            }

            if (students_list.Count == 0)
            {
                Console.WriteLine("No students registered yet.");
                Console.ReadKey();
                return;
            }

            // Display students
            Console.WriteLine("Select a student:");
            for (int i = 0; i < students_list.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {students_list[i].FName} {students_list[i].LName} (ID: {students_list[i].Id})");
            }
            Console.Write("Enter student number: ");
            if (!int.TryParse(Console.ReadLine(), out int studentChoice) || studentChoice <= 0 || studentChoice > students_list.Count)
            {
                Console.WriteLine("Invalid student selection.");
                Console.ReadKey();
                return;
            }
            Student selectedStudent = students_list[studentChoice - 1];

            // Display unassigned personal equipment types (Bed, Closet, Table, Chair) [cite: 302]
            List<EquipName> personalEquipTypes = new List<EquipName> { EquipName.Bed, EquipName.Closet, EquipName.Table, EquipName.Chair };

            Console.WriteLine("Select personal equipment type to assign:");
            for (int i = 0; i < personalEquipTypes.Count; i++)
            {
                // Check if student already has this type of equipment [cite: 302]
                if (!selectedStudent.Equipment.Exists(e => e.Type == personalEquipTypes[i]))
                {
                    Console.WriteLine($"{i + 1}. {personalEquipTypes[i]}");
                }

            }

            Console.Write("Enter equipment type number: ");
            if (!int.TryParse(Console.ReadLine(), out int equipTypeChoice) || equipTypeChoice <= 0 || equipTypeChoice > personalEquipTypes.Count)
            {
                Console.WriteLine("Invalid equipment type selection.");
                Console.ReadKey();
                return;
            }
            EquipName selectedEquipType = personalEquipTypes[equipTypeChoice - 1];

            // Find an unassigned equipment of the selected type
            Equipment availableEquipment = allEquipment_list.Find(e => e.Type == selectedEquipType && !e.RoomNumber.HasValue && !e.AssignedStudentNationalCode.HasValue);

            if (availableEquipment == null)
            {
                Console.WriteLine($"No unassigned {selectedEquipType} available. Please register new equipment first or assign it from an unassigned pool.");
                Console.ReadKey();
                return;
            }

            // Assign to student
            availableEquipment.AssignedStudentNationalCode = selectedStudent.NationalCode;
            selectedStudent.Equipment.Add(availableEquipment); // Add to student's personal equipment list
            availableEquipment.RoomNumber = selectedStudent.RoomNumber; // Assuming personal equipment is in student's room

            Console.WriteLine($"{selectedEquipType} (ID: {availableEquipment.EquipmentId}) assigned to {selectedStudent.FName} {selectedStudent.LName}.");
            Console.ReadKey();
        }

        // 4 => 4: Manage Equipment Movement 
        static void ManageEquipmentMovement()
        {
            Console.Clear();
            Console.WriteLine("--- Manage Equipment Movement ---");

            Console.WriteLine("1. Move Equipment Between Rooms"); 
            Console.WriteLine("2. Exchange Student's Personal Equipment"); 
            Console.WriteLine("0. Back");

            Console.Write("\nSelect an option: ");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    MoveEquipmentBetweenRooms();
                    break;
                case "2":
                    ExchangeStudentsPersonalEquipment();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid input. Press Enter to try again.");
                    Console.ReadKey();
                    break;
            }
        }

        static void MoveEquipmentBetweenRooms()
        {
            Console.Clear();
            Console.WriteLine("--- Move Equipment Between Rooms ---");

            //  Creating a list of real rooms
            List<(Room room, Block block, Dormitory dorm)> allRooms = new();
            foreach (var dorm in dormitories_list)
            {
                if (dormBlocks.ContainsKey(dorm.Name))
                {
                    foreach (var block in dormBlocks[dorm.Name])
                    {
                        foreach (var room in block.Rooms)
                        {
                            allRooms.Add((room, block, dorm));
                        }
                    }
                }
            }

            
            if (allRooms.Count < 2)
            {
                Console.WriteLine("At least two rooms are required to move equipment.");
                Console.ReadKey();
                return;
            }

            
            var roomEquipment = allEquipment_list.Where(e => e.RoomNumber.HasValue).ToList();
            if (roomEquipment.Count == 0)
            {
                Console.WriteLine("No equipment currently assigned to rooms.");
                Console.ReadKey();
                return;
            }

            
            Console.WriteLine("Select equipment to move:");
            for (int i = 0; i < roomEquipment.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {roomEquipment[i].Type} (ID: {roomEquipment[i].EquipmentId.ToString("D8")}) - Current Room: {roomEquipment[i].RoomNumber}");
            }

            Console.Write("Enter equipment number: ");
            if (!int.TryParse(Console.ReadLine(), out int equipChoice) || equipChoice < 1 || equipChoice > roomEquipment.Count)
            {
                Console.WriteLine("Invalid selection.");
                Console.ReadKey();
                return;
            }

            Equipment selectedEquipment = roomEquipment[equipChoice - 1];

            // Select the destination room from the actual list
            Console.WriteLine("\nSelect the destination room:");
            for (int i = 0; i < allRooms.Count; i++)
            {
                var entry = allRooms[i];
                Console.WriteLine($"{i + 1}. Room {entry.room.RoomNumber} - Block: {entry.block.BlockName}, Dorm: {entry.dorm.Name}");
            }

            Console.Write("Enter destination room number: ");
            if (!int.TryParse(Console.ReadLine(), out int roomIndex) || roomIndex < 1 || roomIndex > allRooms.Count)
            {
                Console.WriteLine("Invalid room selection.");
                Console.ReadKey();
                return;
            }

            Room newRoom = allRooms[roomIndex - 1].room;

            
            
            Room currentRoom = allRooms.Find(r => r.room.RoomNumber == selectedEquipment.RoomNumber).room;
            currentRoom.Equipments.Remove(selectedEquipment);

            newRoom.Equipments.Add(selectedEquipment);
            selectedEquipment.RoomNumber = newRoom.RoomNumber;

            Console.WriteLine($"Equipment {selectedEquipment.Type} (ID: {selectedEquipment.EquipmentId.ToString("D8")}) moved to Room {newRoom.RoomNumber}.");
            Console.ReadKey();
        }


        static void ExchangeStudentsPersonalEquipment()
        {
            Console.Clear();
            Console.WriteLine("--- Exchange Student's Personal Equipment ---");

            if (students_list.Count == 0)
            {
                Console.WriteLine("No students registered.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Select a student to exchange equipment for:");
            for (int i = 0; i < students_list.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {students_list[i].FName} {students_list[i].LName} (ID: {students_list[i].Id})");
            }
            Console.Write("Enter student number: ");
            if (!int.TryParse(Console.ReadLine(), out int studentChoice) || studentChoice <= 0 || studentChoice > students_list.Count)
            {
                Console.WriteLine("Invalid student selection.");
                Console.ReadKey();
                return;
            }
            Student selectedStudent = students_list[studentChoice - 1];

            if (selectedStudent.Equipment.Count == 0)
            {
                Console.WriteLine("This student has no personal equipment assigned.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Student's current personal equipment:");
            for (int i = 0; i < selectedStudent.Equipment.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {selectedStudent.Equipment[i].Type} (ID: {selectedStudent.Equipment[i].EquipmentId})");
            }

            Console.Write("Select equipment to exchange: ");
            if (!int.TryParse(Console.ReadLine(), out int equipToExchangeChoice) || equipToExchangeChoice <= 0 || equipToExchangeChoice > selectedStudent.Equipment.Count)
            {
                Console.WriteLine("Invalid equipment selection.");
                Console.ReadKey();
                return;
            }
            Equipment oldEquipment = selectedStudent.Equipment[equipToExchangeChoice - 1];

            // Find a new unassigned equipment of the same type
            Equipment newEquipment = allEquipment_list.Find(e => e.Type == oldEquipment.Type && !e.RoomNumber.HasValue && !e.AssignedStudentNationalCode.HasValue);

            if (newEquipment == null)
            {
                Console.WriteLine($"No unassigned {oldEquipment.Type} available for exchange.");
                Console.ReadKey();
                return;
            }

            // Perform the exchange
            selectedStudent.Equipment.Remove(oldEquipment); // Remove old from student
            oldEquipment.AssignedStudentNationalCode = null; // Unassign old
            oldEquipment.RoomNumber = null; // Unassign old from room as well if it was there

            selectedStudent.Equipment.Add(newEquipment); // Add new to student
            newEquipment.AssignedStudentNationalCode = selectedStudent.NationalCode; // Assign new
            newEquipment.RoomNumber = selectedStudent.RoomNumber; // Assign new to student's room

            Console.WriteLine($"Equipment {oldEquipment.Type} (ID: {oldEquipment.EquipmentId}) exchanged with {newEquipment.Type} (ID: {newEquipment.EquipmentId}) for {selectedStudent.FName} {selectedStudent.LName}.");
            Console.ReadKey();
        }

        // 4 => 5: Manage Repairs 
        static void ManageRepairs()
        {
            Console.Clear();
            Console.WriteLine("--- Manage Repairs ---");

            Console.WriteLine("1. Submit Repair Request (by Part Number)"); 
            Console.WriteLine("2. Track Repair Status"); 
            Console.WriteLine("3. Mark Equipment as Faulty"); 
            Console.WriteLine("4. Mark Equipment as Healthy (Repaired)");
            Console.WriteLine("0. Back");

            Console.Write("\nSelect an option: ");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    SubmitRepairRequest();
                    break;
                case "2":
                    TrackRepairStatus();
                    break;
                case "3":
                    MarkEquipmentAsFaulty();
                    break;
                case "4":
                    MarkEquipmentAsHealthy();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid input. Press Enter to try again.");
                    Console.ReadKey();
                    break;
            }
        }

        static void SubmitRepairRequest()
        {
            Console.Clear();
            Console.WriteLine("--- Submit Repair Request ---");

            Console.Write("Enter Part Number of equipment needing repair: ");
            if (!int.TryParse(Console.ReadLine(), out int partNumber))
            {
                Console.WriteLine("Invalid Part Number. Please enter a number.");
                Console.ReadKey();
                return;
            }

            // In a real system, you'd match by EquipmentId, not just Part Number, as Part Number can be duplicated.
            // For now, let's find all equipment with this part number.
            List<Equipment> equipmentsToRepair = allEquipment_list.FindAll(e => e.PartNumber == partNumber && e.status != Status.UnderRepair);

            if (equipmentsToRepair.Count == 0)
            {
                Console.WriteLine("No equipment found with this Part Number or all are already under repair.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Select equipment to request repair for:");
            for (int i = 0; i < equipmentsToRepair.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {equipmentsToRepair[i].Type} (ID: {equipmentsToRepair[i].EquipmentId}) - Status: {equipmentsToRepair[i].status}");
            }
            Console.Write("Enter equipment number: ");
            if (!int.TryParse(Console.ReadLine(), out int equipChoice) || equipChoice <= 0 || equipChoice > equipmentsToRepair.Count)
            {
                Console.WriteLine("Invalid selection.");
                Console.ReadKey();
                return;
            }
            Equipment selectedEquipment = equipmentsToRepair[equipChoice - 1];

            selectedEquipment.status = Status.UnderRepair;
            repairRequests.Add($"Repair request submitted for: {selectedEquipment.Type} (ID: {selectedEquipment.EquipmentId}, Part Number: {selectedEquipment.PartNumber})");
            Console.WriteLine("Repair request submitted successfully.");
            Console.ReadKey();
        }

        static void TrackRepairStatus()
        {
            Console.Clear();
            Console.WriteLine("--- Repair Status ---");

            if (repairRequests.Count == 0)
            {
                Console.WriteLine("No repair requests submitted.");
            }
            else
            {
                foreach (var request in repairRequests)
                {
                    Console.WriteLine(request);
                }
            }

            Console.WriteLine("\nCurrently under repair (from equipment list):");
            List<Equipment> underRepairList = allEquipment_list.FindAll(e => e.status == Status.UnderRepair);
            if (underRepairList.Count == 0)
            {
                Console.WriteLine("No equipment currently marked as 'Under Repair'.");
            }
            else
            {
                foreach (var equip in underRepairList)
                {
                    Console.WriteLine($"Type: {equip.Type}, ID: {equip.EquipmentId}, Part Number: {equip.PartNumber}");
                }
            }
            Console.ReadKey();
        }

        static void MarkEquipmentAsFaulty()
        {
            Console.Clear();
            Console.WriteLine("--- Mark Equipment as Faulty ---");

            if (allEquipment_list.Count == 0)
            {
                Console.WriteLine("No equipment registered.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Select equipment to mark as faulty:");
            List<Equipment> healthyEquipment = allEquipment_list.FindAll(e => e.status == Status.Healthy);
            if (healthyEquipment.Count == 0)
            {
                Console.WriteLine("No healthy equipment to mark as faulty.");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < healthyEquipment.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {healthyEquipment[i].Type} (ID: {healthyEquipment[i].EquipmentId}) - Current Status: {healthyEquipment[i].status}");
            }

            Console.Write("Enter equipment number: ");
            if (!int.TryParse(Console.ReadLine(), out int equipChoice) || equipChoice <= 0 || equipChoice > healthyEquipment.Count)
            {
                Console.WriteLine("Invalid selection.");
                Console.ReadKey();
                return;
            }
            Equipment selectedEquipment = healthyEquipment[equipChoice - 1];

            selectedEquipment.status = Status.Faulty;
            Console.WriteLine($"Equipment {selectedEquipment.Type} (ID: {selectedEquipment.EquipmentId}) marked as Faulty.");
            Console.ReadKey();
        }

        static void MarkEquipmentAsHealthy()
        {
            Console.Clear();
            Console.WriteLine("--- Mark Equipment as Healthy (Repaired) ---");

            if (allEquipment_list.Count == 0)
            {
                Console.WriteLine("No equipment registered.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Select equipment to mark as Healthy:");
            List<Equipment> nonHealthyEquipment = allEquipment_list.FindAll(e => e.status != Status.Healthy);
            if (nonHealthyEquipment.Count == 0)
            {
                Console.WriteLine("No equipment currently marked as Faulty or Under Repair.");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < nonHealthyEquipment.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {nonHealthyEquipment[i].Type} (ID: {nonHealthyEquipment[i].EquipmentId}) - Current Status: {nonHealthyEquipment[i].status}");
            }

            Console.Write("Enter equipment number: ");
            if (!int.TryParse(Console.ReadLine(), out int equipChoice) || equipChoice <= 0 || equipChoice > nonHealthyEquipment.Count)
            {
                Console.WriteLine("Invalid selection.");
                Console.ReadKey();
                return;
            }
            Equipment selectedEquipment = nonHealthyEquipment[equipChoice - 1];

            selectedEquipment.status = Status.Healthy;
            Console.WriteLine($"Equipment {selectedEquipment.Type} (ID: {selectedEquipment.EquipmentId}) marked as Healthy.");
            // Remove from repair requests if it was specifically logged there
            repairRequests.RemoveAll(r => r.Contains($"ID: {selectedEquipment.EquipmentId}"));
            Console.ReadKey();
        }

        // End of case 4 : Property Management

        // Start of case 5 : Reports 
        static void ReportsMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== Reports ====");
                Console.WriteLine("1. Accommodation Status Report"); 
                Console.WriteLine("2. Property Report"); 
                Console.WriteLine("3. Specialized Reports"); 
                Console.WriteLine("0. Back to Main Menu");

                Console.Write("\nSelect an option: ");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        AccommodationStatusReport();
                        break;
                    case "2":
                        PropertyReport();
                        break;
                    case "3":
                        SpecializedReportsMenu();
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

        // 5 => 1: Accommodation Status Report [cite: 241]
        static void AccommodationStatusReport()
        {
            Console.Clear();
            Console.WriteLine("--- Accommodation Status Report ---");

            // Overall student accommodation statistics [cite: 242]
            int totalStudents = students_list.Count;
            int assignedStudents = students_list.Count(s => !string.IsNullOrEmpty(s.Dorm) && s.Dorm != "Not Assigned");
            Console.WriteLine($"Total Students: {totalStudents}");
            Console.WriteLine($"Students Assigned to Dorms: {assignedStudents}");
            Console.WriteLine($"Students Not Assigned: {totalStudents - assignedStudents}");

            Console.WriteLine("\n--- Room Status ---");
            // This part requires Room objects to accurately track capacity and occupancy.
            // Since Room objects are not fully integrated into the main lists/flow for easy lookup,
            // this will be a simplified view based on students' room assignments.
            // In a real system, you would have a list of all Room objects.

            // For now, let's just list rooms students are assigned to and unassigned rooms.
            // This would need proper Room objects to show empty/full status accurately. [cite: 244]
            var occupiedRooms = students_list.Where(s => s.RoomNumber != 0).Select(s => $"{s.Dorm}-{s.Block}-{s.RoomNumber}").Distinct().ToList();
            Console.WriteLine($"Occupied Rooms (based on student assignments): {occupiedRooms.Count}");
            foreach (var room in occupiedRooms)
            {
                Console.WriteLine($"- {room}");
            }

            Console.WriteLine("\n--- Remaining Capacity per Dorm/Block ---"); // [cite: 245]
            foreach (var dorm in dormitories_list)
            {
                Console.WriteLine($"Dormitory: {dorm.Name} (Total Capacity: {dorm.Capacity})");
                int currentOccupancyDorm = students_list.Count(s => s.Dorm == dorm.Name);
                Console.WriteLine($"  Current Occupancy: {currentOccupancyDorm}");
                Console.WriteLine($"  Remaining Capacity: {dorm.Capacity - currentOccupancyDorm}");

                if (dormBlocks.ContainsKey(dorm.Name))
                {
                    foreach (var block in dormBlocks[dorm.Name])
                    {
                        Console.WriteLine($"  Block: {block.BlockName}");
                        // This calculation for block occupancy is also simplified.
                        // A Block object should contain a list of Room objects, which in turn contain students.
                        int currentOccupancyBlock = students_list.Count(s => s.Dorm == dorm.Name && s.Block == block.BlockName.GetHashCode()); // HACK: Block name to int for comparison
                        Console.WriteLine($"    Current Occupancy (approx): {currentOccupancyBlock}");
                        // No direct block capacity in your current Block class, so cannot calculate remaining accurately.
                    }
                }
            }
            Console.ReadKey();
        }

        // 5 => 2: Property Report [cite: 246]
        static void PropertyReport()
        {
            Console.Clear();
            Console.WriteLine("--- Property Report ---");

            Console.WriteLine("\n--- All Equipment List ---");
            if (allEquipment_list.Count == 0)
            {
                Console.WriteLine("No equipment registered.");
            }
            else
            {
                foreach (var equip in allEquipment_list)
                {
                    Console.WriteLine(equip.ToString());
                }
            }

            Console.WriteLine("\n--- Equipment Assigned to Rooms ---");
            var roomAssignedEquipment = allEquipment_list.Where(e => e.RoomNumber.HasValue).ToList();
            if (roomAssignedEquipment.Count == 0)
            {
                Console.WriteLine("No equipment assigned to rooms.");
            }
            else
            {
                foreach (var equip in roomAssignedEquipment)
                {
                    Console.WriteLine($"{equip.Type} (ID: {equip.EquipmentId}) in Room: {equip.RoomNumber.Value}");
                }
            }

            Console.WriteLine("\n--- Equipment Assigned to Students ---"); // [cite: 249]
            var studentAssignedEquipment = allEquipment_list.Where(e => e.AssignedStudentNationalCode.HasValue).ToList();
            if (studentAssignedEquipment.Count == 0)
            {
                Console.WriteLine("No personal equipment assigned to students.");
            }
            else
            {
                foreach (var equip in studentAssignedEquipment)
                {
                    // Find the student to display name
                    var student = students_list.Find(s => s.NationalCode == equip.AssignedStudentNationalCode.Value);
                    string studentInfo = student != null ? $"({student.FName} {student.LName})" : "";
                    Console.WriteLine($"{equip.Type} (ID: {equip.EquipmentId}) assigned to Student National Code: {equip.AssignedStudentNationalCode.Value} {studentInfo}");
                }
            }

            Console.WriteLine("\n--- Faulty and Under Repair Equipment ---"); // [cite: 250]
            var faultyOrUnderRepairEquipment = allEquipment_list.Where(e => e.status == Status.Faulty || e.status == Status.UnderRepair).ToList();
            if (faultyOrUnderRepairEquipment.Count == 0)
            {
                Console.WriteLine("No faulty or under repair equipment.");
            }
            else
            {
                foreach (var equip in faultyOrUnderRepairEquipment)
                {
                    Console.WriteLine($"{equip.Type} (ID: {equip.EquipmentId}) - Status: {equip.status}");
                }
            }
            Console.ReadKey();
        }

        // 5 => 3: Specialized Reports 
        static void SpecializedReportsMenu()
        {
            Console.Clear();
            Console.WriteLine("--- Specialized Reports ---");
            Console.WriteLine("1. Repair Request Report"); // گزارش درخواست های تعمیرات 
            Console.WriteLine("2. Student Accommodation History Report"); // گزارش تاریخچه اسکان دانشجویان 
            Console.WriteLine("0. Back");

            Console.Write("\nSelect an option: ");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    RepairRequestReport();
                    break;
                case "2":
                    StudentAccommodationHistoryReport();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid input. Press Enter to try again.");
                    Console.ReadKey();
                    break;
            }
        }

        static void RepairRequestReport()
        {
            Console.Clear();
            Console.WriteLine("--- Repair Request Report ---");
            if (repairRequests.Count == 0)
            {
                Console.WriteLine("No repair requests submitted.");
            }
            else
            {
                foreach (var request in repairRequests)
                {
                    Console.WriteLine(request);
                }
            }
            Console.ReadKey();
        }

        static void StudentAccommodationHistoryReport()
        {
            Console.Clear();
            Console.WriteLine("--- Student Accommodation History Report ---");
            // This report would require storing historical data of student moves.
            // Your current Student class only stores the current assignment.
            // To implement this fully, you'd need a separate class or log for student movements (e.g., StudentMovementLog).
            Console.WriteLine("This report requires historical data of student assignments, which is not fully implemented in the current data structure.");
            Console.WriteLine("Current Student Assignments:");
            if (students_list.Count == 0)
            {
                Console.WriteLine("No students registered.");
            }
            else
            {
                foreach (var s in students_list)
                {
                    if (!string.IsNullOrEmpty(s.Dorm) && s.Dorm != "Not Assigned")
                    {
                        Console.WriteLine($"Student: {s.FName} {s.LName} (ID: {s.Id}) - Current Location: Dorm: {s.Dorm}, Block: {s.Block}, Room: {s.RoomNumber}");
                    }
                    else
                    {
                        Console.WriteLine($"Student: {s.FName} {s.LName} (ID: {s.Id}) - Not currently assigned to a dorm.");
                    }
                }
            }
            Console.ReadKey();
        }
        // End of case 5 : Reports
    }
}
