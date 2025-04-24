using TaskOne;

TaskOneVarOne();


static void TaskOneVarOne()
{
    TaskOneArray taskOneArray = new TaskOneArray();

    var array1 = taskOneArray.VariantOne();
    Console.WriteLine($"испытание по умолчанию");
    taskOneArray.PrintConsole(array1);


    Random random = new Random();

    //проверим  работу
    for (int i = 3; i < 10; i++)
    {
        Console.WriteLine($"испытание {i -2}");
        TaskOneArray taskOneArrays = new TaskOneArray(i, random.Next(-4, 10));
        var array = taskOneArrays.VariantOne();
        taskOneArray.PrintConsole(array);

    }
}