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
}
