using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LinkedList<T>
{
    public LinkedList<T> next = null;
    public T data;
    public int length;

    public LinkedList(T data)
    {
        this.data = data;
        length = 1;
    }

    public void Add(T data)
    {
        if (next == null)
        {
            next = new LinkedList<T>(data);
            return;
        }

        length++;
        next.Add(data);
    }

    public void DeletLast()
    {
        if (next.next == null)
        {
            next = null;
            return;
        }
        
        length--;
        next.DeletLast();
    }

    public T LastData
    {
        get
        {
            if (next == null)
            {
                return data;
            }

            return next.LastData;
        }
    }
}