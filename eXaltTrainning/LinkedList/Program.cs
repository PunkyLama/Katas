using System;
using System.Collections.Generic;
using System.Text;

public class MyNode<T>
{
    public T data;
    public MyNode<T> next;

    public MyNode(T data, MyNode<T> next)
    {
        this.data = data;
        this.next = next;
    }
    public bool IsLast()
    {
        if (this.next == null) return true; return false;
    }
}

public class MyLinkedList<T>
{
    public MyNode<T> _rootNode = null;
    public MyNode<T> _lastNode = null;
    private readonly ReaderWriterLock _lock = new ReaderWriterLock();
    static Random Randomizer = new Random();
    private StringBuilder _stringBuilder = new StringBuilder();

    public MyLinkedList()
    {

    }
    public void AddBefore(MyNode<T> node, MyNode<T> newNode)
    {
        _lock.AcquireWriterLock(1000);
        try
        {
            MyNode<T> Previous = PreviousNodeSearch(node);
            if (Previous != null)
            {
                Previous.next = newNode;
                newNode.next = node;
            }
            else
            {
                AddFirst(newNode);
            }
            if (newNode.IsLast())
                _lastNode = newNode;
            if (node.IsLast())
                _lastNode = node;
        } 
        finally 
        { 
            _lock.ReleaseWriterLock(); 
        }
    }
    public void AddAfter(MyNode<T> node, MyNode<T> newNode)
    {
        _lock.AcquireWriterLock(1000);
        try
        {
            if (node.next != null)
            {
                newNode.next = node.next;
                node.next = newNode;
            }
            else { AddLast(newNode); }
            if (newNode.IsLast())
                _lastNode = newNode;
            if (node.IsLast())
                _lastNode = node;
        }
        finally
        { 
            _lock.ReleaseWriterLock(); 
        }        
    }
    public void AddFirst(MyNode<T> newNode)
    {
        _lock.AcquireWriterLock(1000);
        try
        {
            newNode.next = _rootNode;
            _rootNode = newNode;
        }
        finally
        { 
            _lock.ReleaseWriterLock(); 
        }
    }
    public void AddLast(MyNode<T> newNode)
    {
        _lock.AcquireWriterLock(1000);
        try
        {
            if (_lastNode.next == null)
            {
                MyNode<T> Last = LastNodeSearch();
                Last.next = newNode;
                _lastNode = newNode;
            }
            else
                _lastNode = newNode;
        }
        finally
        { 
            _lock.ReleaseWriterLock(); 
        }
    }
    MyNode<T> PreviousNodeSearch(MyNode<T> node)
    {
        _lock.AcquireReaderLock(1000);
        try 
        {
            MyNode<T> currentNode = _rootNode;
            while (currentNode != null)
            {
                if (currentNode.next == node)
                    break;
                currentNode = currentNode.next;
            }
            return currentNode;
        }
        finally
        { 
            _lock.ReleaseReaderLock(); 
        }        
    }
    MyNode<T> LastNodeSearch()
    {
        _lock.AcquireReaderLock(1000);
        try
        {
            MyNode<T> currentNode = _rootNode;
            while (currentNode != null)
            {
                if (currentNode.next == null)
                    break;
                currentNode = currentNode.next;
            }
            return currentNode;
        }
        finally 
        { 
            _lock.ReleaseReaderLock(); 
        }        
    }

    private MyNode<T> getMiddleElement(MyNode<T> myNode)
    {
        _lock.AcquireReaderLock(1000);
        try
        {
            if (myNode == null)
                return myNode;

            MyNode<T> slow = myNode;
            MyNode<T> fast = myNode.next;

            // Move fast by two in the node and slow by one
            // Finally slow will point to middle node
            while (fast != null)
            {
                fast = fast.next;
                if (fast != null)
                {
                    fast = fast.next;
                    slow = slow.next;
                }
            }
            return slow;
        }
        finally { _lock.ReleaseReaderLock(); }        
    }

   public MyNode<T> SortString<String>(MyNode<T> myNode)
    {
        _lock.AcquireWriterLock(1000);
        try
        {
            if (myNode == null || myNode.next == null)
            {
                return myNode;
            }
            // Get the middle of the node
            MyNode<T> middle = getMiddleElement(myNode);
            MyNode<T> nextMiddle = middle.next;

            middle.next = null;

            // Apply SortString on the left node
            MyNode<T> left = SortString<String>(myNode);

            // Apply SortString on the right node
            MyNode<T> right = SortString<String>(nextMiddle);

            // Merge the left and right node
            MyNode<T> sortedNode = sortedMerge(left, right);
            return sortedNode;
        }
        finally { _lock.ReleaseWriterLock(); }
    }

    private MyNode<T> sortedMerge(MyNode<T> left, MyNode<T> right)
    {
        _lock.AcquireWriterLock(1000);
        try
        {
            MyNode<T> result = null;
            if (left == null)
                return right;
            if (right == null)
                return left;

            int position = string.Compare(left.data.ToString(), right.data.ToString());

            if (position < 0)
            {
                result = left;
                result.next = sortedMerge(left.next, right);
            }
            else
            {
                result = right;
                result.next = sortedMerge(left, right.next);
            }

            return result;
        }
        finally { _lock.ReleaseWriterLock(); }
    }
    public string RandomString()
    {
        _lock.AcquireWriterLock(1000);
        try
        {
            StringBuilder StringBuild = new StringBuilder();
            for (int i = 0; i < 10; i++)
            {
                StringBuild.Append((char)Randomizer.Next((int)'a', ((int)'z') + 1));
            }
            return StringBuild.ToString();
        } finally { _lock.ReleaseWriterLock(); }        
    }

    public void DisplayNode(MyNode<T> node)
    {
        _lock.AcquireWriterLock(1000);
        try
        {
            var sorted = SortString<string>(node);
            if(sorted != null)
            {
                while (sorted.next != null)
                {
                    _stringBuilder.Append(sorted.data.ToString() + " | ");
                    sorted = sorted.next;
                }
            }            
            Console.WriteLine(_stringBuilder);
        } finally { _lock.ReleaseWriterLock(); }        
    }
}
static class Program
{
    static async Task Main(string[] args)
    {
        var list = new MyLinkedList<string>();
        while(true) {
            Task AddNodeTask = Task.Run(() => list.AddFirst(new MyNode<string>(list.RandomString(), null)));
            Task SortNodeTask = Task.Run(() => list.DisplayNode(list._rootNode));            

            await Task.WhenAll(AddNodeTask, SortNodeTask);
            await Task.Delay(1000);
        }
        
    }
}
