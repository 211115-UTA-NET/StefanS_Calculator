/*
Calculator By Stefan Schweers
Due Tuesday Nov 30th
Functionality: basic operations, data validation, looping, floats, exit operation
*/

class Program{
  static void Main(String[] args){

    //create variables
    string input;
    bool isNum;
    bool isOp;
    float firstNum;
    float newFirst;
    string opInput;
    float secondNum;
    float output = 0;
    string exitKey = "";

    //intro text
    Console.WriteLine("Calculator Initialized");
    Console.WriteLine("----------------------\n");
    Console.WriteLine("Enter first number then press 'ENTER': ");

    //take first input
    input = Console.ReadLine();
    isNum = float.TryParse(input, out firstNum);

    //Checks for float input
    while (isNum == false){
      Console.WriteLine("Not a valid input. Enter first Number:");
      input = Console.ReadLine();
      isNum = float.TryParse(input, out firstNum);
    }

    //Sets first number in equation outside of do while loop
    output = firstNum;

    //do while loop for exit
    do{
      //refreshes first number by output of final calculation
      newFirst = output;
      //take next input
      Console.WriteLine("\nChoose from the following operators");
      Console.WriteLine("(a) -- Add");
      Console.WriteLine("(s) -- Subtract");
      Console.WriteLine("(m) -- Multiply");
      Console.WriteLine("(d) -- Divide");

      //Operator input
      opInput = Console.ReadLine();
      isOp = opChecker(opInput);

      //Operator entry validation
      while (isOp == false){
        Console.WriteLine("Not valid operation. Enter from the following: (a,s,m,d)");
        opInput = Console.ReadLine();
        isOp = opChecker(opInput);
      }

      //take second number
      Console.WriteLine("\nEnter second number then press enter: ");

      //takes second input
      input = Console.ReadLine();
      isNum = float.TryParse(input, out secondNum);

      //Checks for float input
      while (isNum == false){
        Console.WriteLine("Not a valid input. Enter first Number:");
        input = Console.ReadLine();
        isNum = float.TryParse(input, out secondNum);
      }

      //calculate using doCalculation method
      output = doCalculation(output, opInput, secondNum);
      Console.WriteLine("\nOutput: " + output + "\n");
      Console.WriteLine("Press 'Enter' continue or type 'EXIT' to end:");
      exitKey = Console.ReadLine();
    }
    //ExitKey to leave calculator
    while (exitKey != "EXIT");
  }

  //Calculations
  static float doCalculation(float first, string op, float second){
    float calc = 0;

    switch(op){

      case "a":
        calc = first + second;
        return calc;
        break;

      case "s":
        calc = first - second;
        return calc;
        break;

      case "d":
        calc = first / second;
        return calc;
        break;

      case "m":
        calc = first * second;
        return calc;
        break;

      default:
        calc = 000000;
        return calc;
        break;
    }
  }

  static bool opChecker(string op){
    switch(op){
      case "a":
        return true;
        break;

      case "s":
        return true;
        break;

      case "m":
        return true;
        break;

      case "d":
        return true;
        break;

      default:
        return false;
        break;
    }
  }
}
/*Practice Project: Due Tuseday 30th
Create a Calculator
Requirements:
  1. can accept multiple numbers (done)
  2. perform selected operation on those numbers (done)
  3. display result of the operation (done)
  4. repeat until ready to close (done)
Stretch goals:
  1. accept number values in written format (one plus one)
  2. accept mixed format (one plus 3)
  3. accept multiple values (2+3+4)
  4. multiple operations (1+2-3)
  5. Store calculaton history to a file
  6. accept inputs from a file, calculate, then repost to file

This is the MVP file
*/
