namespace AsyncProject
{
    class User
    {
        public string? Name { set; get; }
    }

    class BankClient
    {
        int account = 0;
        public event EventHandler<string> AddAccount;
        public void Put(int summa)
        {
            this.account += summa;
            AddAccount?.Invoke(this, $"account added {summa}");
        }
    }
    internal class Program
    {
        async static void ConsoleAdding(object? sender, string message)
        {
            await Task.Delay(2000);
            Console.WriteLine(message);
        }
        async static Task LongMethod()
        {
            Console.WriteLine("Long time method is start.");
            //Thread.Sleep(3000);
            await Task.Delay(3000);
            Console.WriteLine("Long time method is finish.");
        }

        async static Task AsyncMethod()
        {
            Console.WriteLine("Async method is start.");
            await Task.Run(LongMethod);
            Console.WriteLine("Async method is finish.");
        }
        
        static void UserPrint(User user)
        {
            Console.WriteLine(user.Name);
            Thread.Sleep(1000);
        }

        async static Task UserPrintAsync(User user)
        {
            await Task.Delay(2000);
            Console.WriteLine(user.Name);
        }

        async static Task<int> SumGauss(int n)
        {
            await Task.Delay(0);
            int res = 0;
            for (int i = 1; i <= n; i++)
            {
                res += i;
                Console.WriteLine($"gauss method {i}");
            }
            return res;
        }

        static Task<int> AddAsync(int a, int b)
        {
            return Task.FromResult(a + b);
        }

        static ValueTask<int> AddAsincV(int a, int b)
        {
            return new ValueTask<int>(a + b);
        }

        static async Task Main(string[] args)
        {
            int result = await AddAsync(10, 20);

            //await AsyncMethod();
            //Console.WriteLine("Main thread work");

            //DateTime tStart = DateTime.Now;
            //UserPrint(new User { Name = "Bob" });
            //UserPrint(new User { Name = "Joe" });
            //UserPrint(new User { Name = "Tim" });
            //DateTime tFinish = DateTime.Now;
            //Console.WriteLine($"time = {(tFinish - tStart).Seconds}");

            //tStart = DateTime.Now;
            //await UserPrintAsync(new User { Name = "Bob" });
            //await UserPrintAsync(new User { Name = "Joe" });
            //await UserPrintAsync(new User { Name = "Tim" });
            //tFinish = DateTime.Now;
            //Console.WriteLine($"time = {(tFinish - tStart).Seconds}");

            //DateTime tStart = DateTime.Now;
            Task userBob = UserPrintAsync(new User { Name = "Bob" });
            Task userJoe = UserPrintAsync(new User { Name = "Joe" });
            //Task userTim = UserPrintAsync(new User { Name = "Tim" });
            ////await userBob;
            ////await userJoe;
            ////await userTim;
            Task<int> gaussTask = SumGauss(10);
            await userBob;
            await userJoe;
            int res = await gaussTask;

            Console.WriteLine($"Main final {res}");
            //await userTim;
            //DateTime tFinish = DateTime.Now;

            //Console.WriteLine($"time = {(tFinish - tStart).Seconds}");

            //BankClient client = new BankClient();
            //client.AddAccount += ConsoleAdding;

            //client.Put(1000);
            //Console.WriteLine("Main continue");

            //await Task.Delay(3000);

            //Console.WriteLine("Main finish");
        }
    }
}