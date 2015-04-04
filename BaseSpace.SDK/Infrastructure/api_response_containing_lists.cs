using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Illumina.BaseSpace.SDK.Infrastructure
{
    class api_response_containing_lists<TItem>
			{
				bool _hasError;
				Exception  _error;
			

				public producer_consumer_thread_safe_queue<TItem> QueueOfItems;
				public Task ApiBackgroundTask;

				public Exception Error() 
				{
					return _error;
				}

				bool has_error()
				{				    
				    if (!QueueOfItems.is_production_complete())
				    {                        
				        throw new Exception("The producer must have completed before calling has_error()");
				    }
					return _hasError;
				}

				void set_error(Exception error)
				{
					_error=error;
					_hasError=true;
					QueueOfItems.SetProductionComplete();
				}

				public api_response_containing_lists()
				{
				}

			/*	api_response_containing_lists(const api_response_containing_lists & rhs):
					_hasError(rhs._hasError)
				{
					QueueOfItems = rhs.QueueOfItems;
					ApiBackgroundTask = rhs.ApiBackgroundTask;
					_error = rhs._error;
				}
        
				~api_response_containing_lists()
				{
					try
					{
						ApiBackgroundTask->get();
					}
					catch (...)
					{
						set_error(std::current_exception());
					}
				}*/
			};
}
