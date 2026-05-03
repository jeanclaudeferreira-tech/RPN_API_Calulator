namespace RPN_Api_V1
{
    public class RPNStacksService
    {
        private Dictionary<int, Stack<double>> _stacks = new();
        private int id = 0; //save last index
        public string GetAvailableStacksId()
        {
            return string.Join("; ", _stacks.Keys);
        }

        public bool StackIdExists(int id)
        {
            return _stacks.ContainsKey(id);
        }

        public string GetStackId(int id)
        {
            string operands = string.Empty;
            if (StackIdExists(id))
                operands = string.Join("; ", _stacks[id]); 
            return $"Stack Id {id} content: {operands}";
        }

        public int CreateNewStack()
        {
            _stacks.Add(id, new Stack<double>());
            return id++;
        }

        public bool DeleteStack(int id)
        {
            return _stacks.Remove(id);
        }

    }
}
