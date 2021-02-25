namespace ControlVee.DesignPattern.Behavioral.ChainOfResponsibility
{
    using System;

    static class Program
    {
        static void Main()
        {
            Random r = new Random();
            int requestLevelMinValue = 0;
            int requestLevelMaxValue = 30;

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
                Program.RunDisplayTimer($"\r\n\n     Passing value {randomPriorityLevel} to Concrete Handler lvl Low.");
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
                Console.Write($"\r\n\n     {vertical}");
                System.Threading.Thread.Sleep(500);
                ClearLastLine();
                Console.Write($"\r\n\n     {forward}");
                System.Threading.Thread.Sleep(500);
                ClearLastLine();
                Console.Write($"\r\n\n     {horizontal}");
                System.Threading.Thread.Sleep(500);
                ClearLastLine();
                Console.Write($"\r\n\n     {backward}");
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
                Program.RunDisplayTimer($"\r\n\n     Concrete Handler Level Low handled lvl {request}");
            }
            else if (_successor != null)
            {
                // TODO: How does this get passed to the other successors?
                Program.RunDisplayTimer($"\r\n\n     Passing value {request} to Concrete Handler lvl Medium.");
                _successor.HandleRequest(request);
            }
        }
    }

    class ConcreteHandlerLevelMedium: Handler
    {
        public override void HandleRequest(int request)
        {
            if (request >= 10 && request < 20)
            {
                Program.RunDisplayTimer($"\r\n\n     Concrete Handler Level Medium handled lvl {request}");
            }
            else if (_successor != null)
            {
                Program.RunDisplayTimer($"\r\n\n     Passing value {request} to Concrete Handler lvl High.");
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
                Program.RunDisplayTimer($"\r\n\n     Concrete Handler Level High handled lvl {request}");
            }
            else if (_successor != null) 
            {
                // TODO: Why does highest successor need this?
                Program.RunDisplayTimer($"\r\n\n");
                _successor.HandleRequest(request);
            }
        }
    }
}
