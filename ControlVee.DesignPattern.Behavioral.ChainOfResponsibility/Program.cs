namespace ControlVee.DesignPattern.Behavioral.ChainOfResponsibility
{
    using System;

    class Program
    {
        static void Main()
        {
        }
    }
    
    // This is the parent/base type that allows the
    // child/derived concrete handlers to pass requests on
    // to other concrete handlers if they determine
    // that the request is of a higher status than
    // they are allowed to handle.
    abstract class Handler
    {
        // Study this field.
        // Why protected?
        protected Handler _successor;

        public void SetSuccessor(Handler successor)
        {
            // _successor instead of this.successor?
            _successor = successor;
        }

        // Why is this abstract and the other method not?
        public abstract void HandleRequest(int request);
    }

    class ConcreteHandlerLevelLow : Handler
    {
        public override void HandleRequest(int request)
        {
            if (request >= 0 && request < 10)
            {
                // Study "this" keyword.
                Console.WriteLine($"{this.GetType().Name} handled level: {request}");
            }
            else if (!(_successor.Equals(null))) // vs if (_successor != null){}
            {
                // How does this get passed to the other successors?
                _successor.HandleRequest(request);
            }
        }
    }
}
