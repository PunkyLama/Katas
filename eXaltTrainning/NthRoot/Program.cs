try
{
    //declare an integer variable
    double number = new();
    int root = new();
    //prompt message to take input
    Console.Write("Input an number (double) value: ");
    number = Convert.ToDouble(Console.ReadLine());
    if (number<=1.00)
    {
        Console.WriteLine("Error: The number must be positive.");
        return;
    }
    Console.Write("Input an root (integer) value: ");
    root = Convert.ToInt32(Console.ReadLine());
    if (root<=1)
    {
        Console.WriteLine("Error: The root must be positive.");
        return;
    }
    //calculate the nth root of the number
    double x = number / root;
    double y = (1.00 / root) * (((root - 1) * x) + (number / Power(x, root - 1)));
    // 4.94065645841247E-324 is equal to double.Epsilon
    while (x - y > 4.94065645841247E-324)
    {
        x = y;
        y = (1.00 / root) * (((root - 1) * x) + (number / Power(x, root - 1)));
    }    
    //display the result
    Console.WriteLine("The nth root is : " + y);
}
catch (Exception ex)
{
    Console.WriteLine("Error: " + ex.ToString());
}

double Power(double number, int power)
{
    double result = 1.00;
    while (power --> 0)
    {
        result *= number;
    }
    return result;
}