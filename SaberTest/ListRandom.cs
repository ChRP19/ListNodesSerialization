﻿using System.Collections.Generic;
using System.IO;

namespace SaberTest
{
    class ListRandom
    {
        public ListNode Head;
        public ListNode Tail;
        public int Count;

        public void Serialize(Stream s)
        {
            var current = Head;
            using var writer = new StreamWriter(s);
            do
            {
                var randomId = 0;

                var temp = current.Random;

                while (temp.Previous is not null)
                {
                    temp = temp.Previous;
                    randomId++;
                }

                writer.WriteLine($"{current.Data}:" +
                                 $"{randomId}:" +
                                 $"{current.Random.Data}"); //test
                current = current.Next;
            }
            while (current != null);
        }

        public void Deserialize(Stream s)
        {
            var listNodes = new List<ListNode>();
            var listIndex = new List<string>();

            using (var reader = new StreamReader(s))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var items = line.Split(':');
                    listNodes.Add(new ListNode { Data = items[0] });
                    listIndex.Add(items[1]);
                }
            }

            Head = listNodes[0];
            Tail = listNodes[listNodes.Count - 1];
            Count = listNodes.Count;

            var temp = Head;

            for (var i = 0; i < listNodes.Count; i++)
            {
                temp.Next = i + 1 == listNodes.Count ? null : listNodes[i + 1];

                temp.Previous = i == 0 ? null : listNodes[i - 1];

                temp.Random = listNodes[int.Parse(listIndex[i])];
                temp = temp.Next;
            }
        }

        public void Add(string data)
        {
            var newNode = new ListNode
            {
                Data = data
            };

            if (Head is null)
            {
                newNode.Data = "Head";
                Head = newNode;
                Tail = Head;
            }
            else
            {
                newNode.Previous = Tail;
                Tail.Next = newNode;
                Tail = newNode;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is not ListRandom other)
                return false;

            var temp1 = this.Head;
            var temp2 = other.Head;

            bool isEqual = true;

            while (isEqual)
            {
                isEqual = temp1.Data == temp2.Data;
                temp1 = temp1.Next;
                temp2 = temp2.Next;
                if (temp1 == null && temp2 == null)
                {
                    break;
                }
            }

            return isEqual;
        }
    }
}