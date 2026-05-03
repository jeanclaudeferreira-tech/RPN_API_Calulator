namespace RPN_Api_V1
{
    public class RPNStacksService
    {
        private Dictionary<int, Stack<double>> _stacks = new();
        private int id = 0; //save last index
        public string GetAvailableStacksId()
        {
            return string.Empty;
        }

        public string GetStackId(int id)
        {
            return $"Stack Id {id} content: ";
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
