﻿using System;
using System.Collections.Generic;

public class FSMStater<T> where T : struct
{
    public Action<T, T> TransCallBack { get; private set; }
    public T _currentState { get; private set; }

    private Dictionary<T, List<FSMStaterItem<T>>> _transToOtherStateDict;
    private List<FSMStaterItem<T>> _transToOneStateList;

    private bool _isProcessingTransition = false;
    private Queue<FSMTransCommandBase> _transCommandQueue;

    public FSMStater(T initialState, Action<T, T> transCallBack = null)
    {
        _currentState = initialState;
        _transToOtherStateDict = new Dictionary<T, List<FSMStaterItem<T>>>();
        _transToOneStateList = new List<FSMStaterItem<T>>();
        _transCommandQueue = new Queue<FSMTransCommandBase>();

        TransCallBack = transCallBack;
    }

    public void AddLocalTransition(T currentState, FSMTransCommandBase command, T targetState, Func<T, FSMTransCommandBase, T, bool> callBack)
    {
        FSMStaterItem<T> item = new FSMStaterItem<T>(command, targetState, callBack);
        _transToOtherStateDict.TryGetValue(currentState, out List<FSMStaterItem<T>> itemList);
        if (itemList == null)
        {
            itemList = new List<FSMStaterItem<T>>();
            _transToOtherStateDict.Add(currentState, itemList);
        }
        itemList.Add(item);
    }

    public void AddGlobalTransition(FSMTransCommandBase command, T targetState, Func<T, FSMTransCommandBase, T, bool> callBack)
    {
        FSMStaterItem<T> item = new FSMStaterItem<T>(command, targetState, callBack);
        _transToOneStateList.Add(item);
    }

    public void Invoke(FSMTransCommandBase command)
    {
        if (_isProcessingTransition)
        {
            _transCommandQueue.Enqueue(command);
            return;
        }
        _isProcessingTransition = true;

        bool result = false;

        _transToOtherStateDict.TryGetValue(_currentState, out List<FSMStaterItem<T>> itemList);
        if (itemList != null)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                result = ProcessTransition(itemList[i], command);
                if (result)
                {
                    break;
                }
            }
        }

        if (!result)
        {
            for (int i = 0; i < _transToOneStateList.Count; i++)
            {
                result = ProcessTransition(_transToOneStateList[i], command);
                if (result)
                {
                    break;
                }
            }
        }

        _isProcessingTransition = false;

        if (_transCommandQueue.Count > 0)
        {
            FSMTransCommandBase item = _transCommandQueue.Dequeue();
            Invoke(item);
        }
    }

    private bool ProcessTransition(FSMStaterItem<T> item, FSMTransCommandBase command)
    {
        bool result = false;
        if (item.TransCommand.Equals(command))
        {
            if (item.TransCallBack != null)
            {
                result = item.TransCallBack(_currentState, command, item.TargetState);
            }
            else
            {
                result = true;
            }

            if (result)
            {
                T previousState = _currentState;
                _currentState = item.TargetState;
                TransCallBack?.Invoke(previousState, _currentState);
            }
        }
        return result;
    }

    public void ClearTransCommandList()
    {
        _transCommandQueue.Clear();
    }
}
