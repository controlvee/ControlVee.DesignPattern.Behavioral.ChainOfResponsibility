namespace ControlVee.DesignPattern.Behavioral.ChainOfResponsibility
{
    using System;

    /// <summary>
    /*  
         Participants:
        
            The classes and objects participating in this pattern are:
                Handler (Approver)
                    - defines an interface for handling the requests
                    - (optional) implements the successor link

                ConcreteHandler (Director|Low, VicePresident|Medium, President|High)
                    - handles requests it is responsible for
                    - can access its successor
                    - if the ConcreteHandler can handle the request, it does so; 
                    otherwise it forwards the request to its successor

                Client (ChainApp)
                    - initiates the request to a ConcreteHandler object on the chain 
    */
    /// </summary>
    static class Program
    {
        static void Main()
        {
            Random r = new Random();
            int requestLevelMinValue = 0;
            int requestLevelMaxValue = 55;

            // Build the actual chain of responsibility (CoR).
            Handler handlerLowPriority = new ConcreteHandlerLevelLow();
            Handler handlerMediumPriority = new ConcreteHandlerLevelMedium();
            Handler handlerHighPriority = new ConcreteHandlerLevelHigh();

            handlerLowPriority.SetSuccessor(handlerMediumPriority);
            handlerMediumPriority.SetSuccessor(handlerHighPriority);

            // Time a loop and create new random priority level.
            while (true)
            {
                int randomPriorityLevel = r.Next(requestLevelMinValue, requestLevelMaxValue);
                Program.RunDisplayTimer($"{CreatePadding()} Passing {randomPriorityLevel} to Priority Handler lvl Low");
                handlerLowPriority.HandleRequest(randomPriorityLevel);
                Console.Clear();
            }
        }

        public static void RunDisplayTimer(string baseMessage)
        {
            var vertical = "|";
            var forward = "/";
            var horizontal = "_";
            var backward = @"\";

            Console.WriteLine(baseMessage);

            int i;
            for (i = 0; i < 2; i++)
            {
                Console.Write($"{CreatePadding()} {vertical}");
                System.Threading.Thread.Sleep(500);
                ClearLastLine();
                Console.Write($"{CreatePadding()} {forward}");
                System.Threading.Thread.Sleep(500);
                ClearLastLine();
                Console.Write($"{CreatePadding()} {horizontal}");
                System.Threading.Thread.Sleep(500);
                ClearLastLine();
                Console.Write($"{CreatePadding()} {backward}");
                System.Threading.Thread.Sleep(500);
                ClearLastLine();
            }
        }

        public static void ClearLastLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }

        public static string CreatePadding()
        {
            return $"\r\n\n     ";
        }
    }
    
    // This is the parent/base type that allows the
    // child/derived concrete handlers to pass requests on
    // to other concrete handlers if they determine
    // that the request is of a higher status than
    // they are allowed to handle.
    abstract class Handler
    {
        // TODO: Study this field - why protected?
        protected Handler _successor;

        public void SetSuccessor(Handler successor)
        {
            // TODO: _successor instead of this.successor?
            _successor = successor;
        }

        // TODO: Why is this abstract and the other method not?
        public abstract void HandleRequest(int request);
    }

    class ConcreteHandlerLevelLow : Handler
    {
        public override void HandleRequest(int request)
        {
            if (request >= 0 && request < 10)
            {
                Program.RunDisplayTimer($"{Program.CreatePadding()} Priority Handler Level Low handled lvl {request}");
            }
            else if (_successor != null)
            {
                // TODO: How does this get passed to the other successors?
                Program.RunDisplayTimer($"{Program.CreatePadding()} Value too high for handler" +
                    $"{Program.CreatePadding()} Passing {request} to Priority Handler lvl Medium");

                _successor.HandleRequest(request);
            }
        }
    }

    class ConcreteHandlerLevelMedium : Handler
    {
        public override void HandleRequest(int request)
        {
            if (request >= 10 && request < 20)
            {
                Program.RunDisplayTimer($"{Program.CreatePadding()} Priority Handler Level Medium handled lvl {request}");
            }
            else if (_successor != null)
            {
                Program.RunDisplayTimer($"{Program.CreatePadding()} Value too high for handler" +
                    $"{Program.CreatePadding()} Passing {request} to Priority Handler lvl High");

                _successor.HandleRequest(request);
            }
        }
    }

    class ConcreteHandlerLevelHigh : Handler
    {
        public override void HandleRequest(int request)
        {
            if (request >= 20 && request < 30)
            {
                Program.RunDisplayTimer($"{Program.CreatePadding()} Priority Handler Level High handled lvl {request}");
            }
            else
            {
                Program.RunDisplayTimer($"{Program.CreatePadding()} Value too high for all handlers");
            }
        }
    }
}
