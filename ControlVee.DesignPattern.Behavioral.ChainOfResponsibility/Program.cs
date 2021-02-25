namespace ControlVee.DesignPattern.Behavioral.ChainOfResponsibility
{
    using System;

    class Program
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

            while (true)
            {
                handlerLowPriority.HandleRequest(r.Next(requestLevelMinValue, requestLevelMaxValue));
                System.Threading.Thread.Sleep(4000);
                Console.Clear();

                Console.WriteLine("Proccessing random numbers.");
                System.Threading.Thread.Sleep(2000);
                Console.WriteLine("Proccessing random numbers..");
                Console.Clear();

                Console.WriteLine("Proccessing random numbers...");
                System.Threading.Thread.Sleep(2000);
                Console.WriteLine("Proccessing random numbers....");
                Console.Clear();

                Console.WriteLine("Proccessing random numbers...");
                System.Threading.Thread.Sleep(2000);
                Console.WriteLine("Proccessing random numbers..");
                Console.Clear();

                Console.WriteLine("Proccessing random numbers.");
                System.Threading.Thread.Sleep(2000);
                Console.WriteLine("Proccessing random numbers");
                Console.Clear();
            }
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
                // TODO: Study "this" keyword.
                Console.WriteLine($"{this.GetType().Name} handled level: {request}");
            }
            else if (_successor != null)
            {
                // TODO: How does this get passed to the other successors?
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
                Console.WriteLine($"{this.GetType().Name} handled level: {request}");
            }
            else if (_successor != null)
            {
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
                Console.WriteLine($"{this.GetType().Name} handled level: {request}");
            }
            else if (_successor != null) 
            {
                _successor.HandleRequest(request);
            }
        }
    }
}
