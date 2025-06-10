using System;
using System.Linq;
using System.Reflection;

public class Person
{
    public string FName { get; set; }
    public string LName { get; set; }
    public int PhoneNumber { get; set; }
    public string Address { get; set; }
    public Person(string fName, string lName, int phoneNumber, string address)
    {
        FName = fName;
        LName = lName;
        PhoneNumber = phoneNumber;
        Address = address;
    }
}
public class DormManager : Person 
{
    public string Post { get; set; }

    public string Dormitory_under_responsibility {  get; set; }

    public DormManager(string fName, string lName, int phoneNumber, string address ,string post , string dormitory_under_responsibility)
        : base(fName, lName, phoneNumber, address )
    {
        Post = post;
        Dormitory_under_responsibility = dormitory_under_responsibility; 
    }

}
public class BlockManager : Student
{
    public string Post { get; set; }
    public string Block_under_responsibility { get; set; }
    public BlockManager(string fName, string lName, int phoneNumber, string address, int id, int room_number, int block, string dorm , string post , string block_under_responsibility)
        :base(fName, lName, phoneNumber, address ,id , room_number , block , dorm)
    {
        Post =post;
        Block_under_responsibility = block_under_responsibility;
    }
}
public class Student : Person
{
    public int Id { get; set; }
    public int Room_number { get; set; }
    public int Block { get; set; }
    public string Dorm { get; set; }
    public List<Equipment> equipment { get; set; }

    public Student(string fName, string lName, int phoneNumber, string address, int id , int room_number , int block , string dorm)
        :base(fName, lName, phoneNumber, address)
    {
        Id = id;
        Room_number = room_number;
        Block = block;
        Dorm = dorm;
    }
}
