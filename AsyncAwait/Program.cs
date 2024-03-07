// See https://aka.ms/new-console-template for more information
//Asynchronous programming is a programming technique that allows code to be executed concurrently without blocking the execution of the calling thread.
//In other words, asynchronous code can run in the background while other code is executing.
//We can run all the methods parallelly using simple thread programming, but it will block UI and wait to complete all the tasks. 

//1. Method1 and Method2 are not waiting for each other.
//M1();
//M2();

//2. Method3 requires one parameter, which is the return type of Method1. Here, the await keyword is vital in waiting for Method1 task completion
//await callMethod();

//3. Here, we are using async programming to read all the contents from the file, so it will not wait to get a return value from this method and execute the other lines of code. 
//However, it still has to wait for the line of code given below because we are using await keywords, and we will use the return value for the line of code below
//Task task = new Task(CallMethod);
//task.Start();
//task.Wait();

CallMethod();
Console.ReadLine();
static async Task M1()
{
    await Task.Run(() =>
    {
        for (int i = 0; i < 100; i++)
        {
            Console.WriteLine(" Method 1");
            // Do something
            // Delay Асинхронно ожидать 0.1 секунду
            // Wait Синхронное блокирование с ожиданием завершения async-метода
            Task.Delay(100).Wait();
        }
    });
}


static void M2()
{
    for (int i = 0; i < 25; i++)
    {
        Console.WriteLine(" Method 2");
        // Do something
        Task.Delay(100).Wait();
    }
}

static async Task callMethod()
{
    Method2();
    var count = await Method1();
    Method3(count);
}

static async Task<int> Method1()
{
    int count = 0;
    await Task.Run(() =>
    {
        for (int i = 0; i < 100; i++)
        {
            Console.WriteLine(" Method 1");
            count += 1;
        }
    });
    return count;
}

static void Method2()
{
    for (int i = 0; i < 25; i++)
    {
        Console.WriteLine(" Method 2");
    }
}

static void Method3(int count)
{
    Console.WriteLine("Total count is " + count);
}

static async void CallMethod()
{
    string filePath = "sampleFile.txt";
    Task<int> task = ReadFile(filePath);

    Console.WriteLine(" Other Work 1");
    Console.WriteLine(" Other Work 2");
    Console.WriteLine(" Other Work 3");

    int length =  await task;
    Console.WriteLine(" Total length: " + length);

    Console.WriteLine(" After work 1");
    Console.WriteLine(" After work 2");
}

static async Task<int> ReadFile(string file)
{
    int length = 0;

    Console.WriteLine(" File reading is stating");
    using (StreamReader reader = new StreamReader(file))
    {
        // Reads all characters from the current position to the end of the stream asynchronously
        // and returns them as one string.
        string s = await reader.ReadToEndAsync();

        length = s.Length;
    }
    Console.WriteLine(" File reading is completed");
    return length;
}

//Consider a real-world business case, a WPF UI binding data to the data grid by fetching a large number of rows from a database.
//While data is being fetched and bound to a grid, the rest of the UI should continue to be responsive. Any attempt at interaction with other UI controls must not be blocked and data loading and binding must continue in parallel..
//Refer to "Figure 1-1 Synchronous Behavior" below.
//async void LoadEmployee_Click(object sender, RoutedEventArgs e)
//{
//    // ...
//    await viewer.LoadEmplployeeAsync();
//    // ...
//}