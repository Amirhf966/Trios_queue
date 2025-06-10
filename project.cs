using System;
using System.Collections.Generic;

namespace DormitoryManagement
{
    public enum EquipName
    {
        Refrigerator = 001,
        Table = 002,
        Chair = 003,
        Bed = 004,
        Closet = 005
    }

    public enum Status
    {
        Healthy = 1,
        Faulty = 2,
        UnderRepair = 3,
    }

    public class Person
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string NationalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public Person(string fName, string lName, string nationalCode, string phoneNumber, string address)
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

        public Student(string fName, string lName, string nationalCode, string phoneNumber, string address,
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

        public DormManager(string fName, string lName, string nationalCode, string phoneNumber, string address,
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

        public BlockManager(string fName, string lName, string nationalCode, string phoneNumber, string address,
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
            Equipments = equipments new List<Equipment>();
            Students = students new List<Student>();
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



    internal class Program
    {
        static void Main(string[] args)
        {
            //Faz2
        }
    }
}
