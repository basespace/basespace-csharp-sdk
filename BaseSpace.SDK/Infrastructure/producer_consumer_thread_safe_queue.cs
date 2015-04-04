using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Illumina.BaseSpace.SDK.Infrastructure
{
    public class producer_consumer_thread_safe_queue<T>
			{

				public producer_consumer_thread_safe_queue()
				{
					productionComplete_ = false;
				    queue_ = new Queue<T>();
				    lockObj = new object();
				    itemMightBeAvailable = new ManualResetEvent(false);
				    itemsRemainingToBePushed_ = 1;
				}

				public void SetExpectedNumberOfItems(int expectedNumberOfItems)
				{
					itemsRemainingToBePushed_ = expectedNumberOfItems;
					if (0 == itemsRemainingToBePushed_)
					{
						productionComplete_ = true;
						itemMightBeAvailable.Set();
					}
				}

				public void SetProductionComplete()
				{
				    lock (lockObj)
				    {
					    productionComplete_ = true;
					    itemMightBeAvailable.Set();			        
				    }
				}

				public bool pop(ref T item)
				{
					while (size() == 0 && !productionComplete_)
					{
						itemMightBeAvailable.WaitOne();
					}

					if (size() == 0)
					{
						return false;
					}

				    lock (lockObj)
				    {
					    item = queue_.Dequeue();				        
					    if (0==queue_.Count)
						    itemMightBeAvailable.Reset();
					    return true;				        
				    }
				}
	
				public long size()
				{
				    long retVal = 0;
				    lock (lockObj)
				    {
				        retVal = queue_.Count();
				    }
				    return retVal;
				}

				public bool is_production_complete()
				{
					return productionComplete_;
				}

				public bool is_consumption_complete()
				{
					return productionComplete_ && (size() == 0);
				}

				public void push(T item)
				{
				    lock (lockObj)
				    {
					    queue_.Enqueue(item);
					    --itemsRemainingToBePushed_;
					    itemMightBeAvailable.Set();				        
				    }
				}				
							
				private long itemsRemainingToBePushed_;
				private Queue<T> queue_;
				private bool productionComplete_;
				private object lockObj ;
				private ManualResetEvent itemMightBeAvailable;
			};
}
