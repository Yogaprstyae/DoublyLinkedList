using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DoublyLinkedList
{
    class Node
    {
        /* Node class represents the node of doubly linked list.
         * It consists of the information part and links to
         * its succeeding and preceeding nodes
         * in terms of next and previous nodes. */
        public int rollNumber;
        public string name;
        public Node next; /*points to the succeeding node*/
        public Node prev; /*points to the preceeding node*/
    }
    class DoubleLinkedList
    {
        Node START;
        public DoubleLinkedList()
        {
            START = null;
        }
        public void addNode() /*adds a new node*/
        {
            int rollNo;
            string nm;
            Console.Write("\n Enter the roll number of the student : ");
            rollNo = Convert.ToInt32(Console.ReadLine());
            Console.Write("\n Enter the name of the student : ");
            nm = Console.ReadLine();
            Node newnode = new Node();
            newnode.rollNumber = rollNo;
            newnode.name = nm;
            /* Checks it the list is empty*/
            if (START == null || rollNo <= START.rollNumber)
            {
                if ((START != null) && (rollNo == START.rollNumber))
                {
                    Console.WriteLine("\n Duplicate roll numbers not allowed");
                    return;
                }
                newnode.next = START;
                if (START != null)
                    START.prev = newnode;
                newnode.prev = null;
                START = newnode;
                return;
            }
            Node previous, current;
            for (current = previous = START; current != null &&
                rollNo >= current.rollNumber; previous = current, current =
                current.next)
            {
                if (rollNo == current.rollNumber)
                {
                    Console.WriteLine("\n Duplicate roll numbers not allowed");
                    return;
                }
            }
            /* On the execution of the above for loop, prev and
             * current wll point to those nodes
            between which the new node is to be inserted. */
            newnode.next = current;
            newnode.prev = previous;

            /* If the node is to be inserted at the end od the list. */
            if (current == null)
            {
                newnode.next = null;
                previous.next = newnode;
                return;
            }
            current.prev = newnode;
            current.next = newnode;
        }
        
        /* Checks wheteher the specified node is present*/
        public bool Search(int rollNo,ref Node previous,ref Node current)
        {
            for (previous = current = START; current !=null &&
                rollNo != current.rollNumber; previous = current,
                current = current.next)
            { }
            /* The above for loop traverser the list. If the specified node
             * is found then the function returns true, otherwisw false. */
            return(current != null);
        }

        public bool delNode(int rollNo) /* Deletes the specified node*/
        {
            Node previous, current;
            previous = current = null;
            if (Search(rollNo,ref previous,ref current) == false)
                return false;
            if (current == START) /* If the first node is to be deleted*/
            {
                START = START.next;
                if (START != null)
                    START.prev = null;
                return true;
            }
            if (current.next == null) /* If the last node is to be deleted*/
            {
                previous.next = null;
                return true;
            }
            /* If the node to be deleted is in between the list the the
            following lines of code is executed*/
            previous.next = current.next;
            current.next.prev = previous;
            return true;
        }

        public void traverse() /* Treverses the list*/
        {
            if (listEmpty())
                Console.WriteLine("\n List is empty");
            else
            {
                Console.WriteLine("\n Records in the accending order of " +
                    "roll numbers are : \n");
                Node currentNode;
                for (currentNode = START; currentNode != null;
                    currentNode = currentNode.next)
                    Console.Write(currentNode.rollNumber + "  "
                        + currentNode.name + "\n");
            }
        }

        /* Traverses the list in the reverse direction*/
        public void revtraverse()
        {
            if (listEmpty())
                Console.WriteLine("\n List is empty");
            else
            {
                Console.WriteLine("\n Records in the descending order of " +
                    "roll numbers are : \n");
                Node currentNode;
                for (currentNode = START; currentNode.next != null;
                    currentNode =currentNode.next)
                { }
                while (currentNode != null)
                {
                    Console.Write(currentNode.rollNumber + "  "
                        + currentNode.name + "\n");
                    currentNode = currentNode.prev;
                }
            }
        }
        public bool listEmpty()
        {
            if (START == null)
                return true;
            else
                return false;
        }
        static void Main(string[] args)
        {
            DoubleLinkedList obj = new DoubleLinkedList();
            while (true)
            {
                try
                {
                    Console.WriteLine("\n Menu" +
                        "\n 1. Add a record to be the list" +
                        "\n 2. Delete a record from the list" +
                        "\n 3. View all records in the ascending order of roll numbers" +
                        "\n 4. View all records in the descending order of roll numbers" +
                        "\n 5. Search for a ecord in the list" +
                        "\n 6. Exit \n" +
                        "\n Enter your choice (1-6) : ");
                    char ch = Convert.ToChar(Console.ReadLine());
                    switch (ch)
                    {
                        case '1':
                            {
                                obj.addNode();
                            }
                            break;
                        case '2':
                            {
                                if (obj.listEmpty())
                                {
                                    Console.WriteLine("\n List is empty");
                                    break;
                                }
                                Console.Write("\n Enter the roll number of the student" +
                                    " whose record is to be deleted : ");
                                int rollNo = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine();
                                if (obj.delNode(rollNo) == false)
                                    Console.WriteLine("Record not found");
                                else
                                    Console.WriteLine("Record with roll number " + rollNo + " deleted \n");
                            }
                            break;
                        case '3':
                            {
                                obj.traverse();
                            }
                            break;
                        case '4':
                            {
                                obj.revtraverse();
                            }
                            break;
                        case '5':
                            {
                                if (obj.listEmpty() == true)
                                {
                                    Console.WriteLine("\n List is empty");
                                    break;
                                }
                                Node prev, curr;
                                prev = curr = null;
                                Console.Write("\n Enter the roll number of the student whose record ypu want to search : ");
                                int num = Convert.ToInt32(Console.ReadLine());
                                if (obj.Search(num, ref prev, ref curr) == false)
                                    Console.WriteLine("\n Record found");
                                else
                                {
                                    Console.WriteLine("\n Roll number : " + curr.rollNumber);
                                    Console.WriteLine("\n Name : " + curr.name);
                                }
                            }
                            break;
                        case '6':
                            return;
                        default:
                            {
                                Console.WriteLine("\n Invalid option");
                            }
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Check for the values entered.");
                }
            }
        }
    }
}
