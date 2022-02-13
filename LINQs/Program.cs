    // Data source
    using LINQs;

    string[] names = { "Bill", "Steve", "James", "Mohan" };

    // LINQ Query 
    var myLinqQuery = from name in names
                      where name.Contains("ja")
                      select name;

    // Query execution
    foreach (var name in myLinqQuery)
        Console.Write(name + " ");
    // Let's Make an array of Student class type

     Student[] studentArray =
     { 
          new Student() { Id = 1, age = 23, Name = "Asif"},
          new Student() { Id = 2, age = 25, Name = "Aqib" },
          new Student() { Id = 3, age = 27, Name = "Saqib" },
          new Student() { Id = 4, age = 19, Name = "Asif"},
          new Student() { Id = 5, age = 18, Name = "Aqib" },
          new Student() { Id = 6, age = 14, Name = "Saqib" }
     };

    // Use LINQ to find teenager students
    Student[] teenAgerStudents = studentArray.Where(s => s.age > 12 && s.age < 20).ToArray();
        Console.Write(teenAgerStudents + " ");

    // Use LINQ to find first student whose name is Bill 
    Student bill = studentArray.Where(s => s.Name == "Bill").FirstOrDefault();
        Console.Write(bill + " ");

    // Use LINQ to find student whose StudentID is 5
    Student student5 = studentArray.Where(s => s.Id == 5).FirstOrDefault();
        Console.Write(student5 + " ");