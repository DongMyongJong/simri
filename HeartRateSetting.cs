using System;
using System.Collections.Generic;
using System.Collections;

namespace SEMS_FlashLibrary
{
    /// <summary>
    /// 心率值的计算
    /// </summary>
    public class HeartRateSetting
    {
        private   Queue HrQueue;
        //private int DiffState = 1;//上一个差值的状态，正常范围为1，否则为0
        private int BeyondRangeCount;//超出范围的差值的个数
        private int NaturalCount;//正常差值个数
        private int ValueCount;//采珍珠个数

        public int   BeyondRangeCountConst;//够几个不正常，传出一个状态 
        public int NatureCountConst;//够几个正常差值时，传状态
        public float MinRange;//差值平均值的下限
        public float MaxRange;//差值平均值的上限
        public void ClearHrQueue()
        {
            HrQueue.Clear();
        }
        public HeartRateSetting()
        {
            HrQueue = new Queue();
        }
        /// <summary>
        /// 心率计算
        /// </summary>
        /// <param name="hrValue">心率值</param>
        /// <returns>1,进格；－1，退格</returns>
        public int HrSet(float hrValue)
        {
            HrQueue.Enqueue(hrValue);
            if (HrQueue.Count == 2)
            {//够两个数时，算差值，判断是否在范围内
                //算差值，移除第一个数
                float dif = Math.Abs((float)HrQueue.Dequeue() - (float)HrQueue.Peek());
                if (dif >= MinRange && dif <= MaxRange)
                { //在范围内
                    NaturalCount++;
                    BeyondRangeCount = 0;
                    if (NaturalCount >= NatureCountConst)
                    {//够个数
                        //*******恢复初始状态
                        BeyondRangeCount = 0;
                        NaturalCount = 0;
                        //********
                        return 1;
                    }
                }
                else
                {
                    BeyondRangeCount++;
                    NaturalCount = 0;
                    if (BeyondRangeCount >= BeyondRangeCountConst)
                    {
                        //*******恢复初始状态
                        BeyondRangeCount = 0;
                        NaturalCount = 0;
                        //********
                        return -1;
                    }
                }
            }
            return 0;
        }
        /// <summary>
        /// 采珍珠计算
        /// </summary>
        /// <param name="hrValue">心率值</param>
        /// <returns>珍珠个数.传－100时，不作响应</returns>
        public int ZhenzhuHrSet(float hrValue)
        {
            HrQueue.Enqueue(hrValue);
            if (HrQueue.Count == 2)
            {
                //计算前后的差值
                float dif = Math.Abs((float)HrQueue.Dequeue() - (float)HrQueue.Peek());
                if (dif >= MinRange && dif <= MaxRange)
                {//正常范围
                    this.NaturalCount++;
                }
                if (++ValueCount >= 5)
                {
                    int temp = this.NaturalCount;

                    this.NaturalCount = 0;//还原
                    ValueCount = 0;
                    return temp;
                }
            }
            return -100;
        }
        public int JXHeartSet(float fHrValue)
        {
            HrQueue.Enqueue(fHrValue);
             if (HrQueue.Count == 2)
             {//够两个数时，算差值，判断是否在范围内
                 //算差值，移除第一个数
                 float dif = Math.Abs((float)HrQueue.Dequeue() - (float)HrQueue.Peek());

                 if (dif >= MinRange && dif <= MaxRange)
                 { //在范围内
                     return 1;
                 }
                 else return -1;
             }
             return -100;
        }
    }
}
