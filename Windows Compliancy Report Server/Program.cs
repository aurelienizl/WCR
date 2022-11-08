namespace Windows_Compliancy_Report_Server;

class Program
{
    public static int Main()
    {
		try
		{
            FileReceiver fileReceiver = new FileReceiver("0.0.0.0", 443);
            fileReceiver.Start();
            return 0;
        }
		catch (Exception ex)
		{
            Console.WriteLine("[CRITICAL] : Server crashed");
            Console.WriteLine(ex.Message);
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
            return 1;
		}
    }
}