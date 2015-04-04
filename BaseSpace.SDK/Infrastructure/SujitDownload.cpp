using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumina.BaseSpace.SDK.Infrastructure
{
	class SujitDownload <TResponseItemType>
    {
		api_response_containing_lists<TResponseItemType> get_list(string file_uri_builder, bool returnAllListItems, int limit, CancellationToken  cancelationToken)
		{
			//auto url = file_uri_builder.to_string();
			//string urlForLogging(url.begin(), url.end());
			//TRACE("basespace_client::get_list (" << urlForLogging << ")");
			auto retVal = new api_response_containing_lists<TResponseItemType>();
			/*retVal.QueueOfItems = new producer_consumer_thread_safe_queue<TResponseItemType>();
			retVal.ApiBackgroundTask = () =>
			{
				try
				{
					int numberOfPages = 1;
					int numberOfItemsToBeEnqueued = 1;
					//Get the first page of response and calculate number of remaining pages.
					auto uriForFirstPage = file_uri_builder;
					//uriForFirstPage.append_query(_XPLATSTR("offset"), 0).append_query(_XPLATSTR("limit"), limit);

					auto firstPage = get<api_response<api_list_response<TResponseItemType>>>(uriForFirstPage, cancelationToken).get();


					if (returnAllListItems)
					{
						numberOfPages = (int)ceil(firstPage.Response.TotalCount / (double)limit);
						numberOfItemsToBeEnqueued = firstPage.Response.TotalCount;
					}
					else
					{
						numberOfItemsToBeEnqueued = firstPage.Response.Items.size();
					}

					auto url = uriForFirstPage.to_string();
					string pageUrlForLogging(url.begin(), url.end());
					DEBUG(pageUrlForLogging << " :Received page (" << 1 << "/" << numberOfPages << ")  containing " << firstPage.Response.DisplayedCount << " items.");

					//Put items from first page into queue
					retVal.QueueOfItems->SetExpectedNumberOfItems(numberOfItemsToBeEnqueued);
					for (auto item : firstPage.Response.Items)
					{
						retVal.QueueOfItems->push(std::make_shared<TResponseItemType>(item));
					}
					if (firstPage.Response.Items.size() == firstPage.Response.TotalCount)
					{
						retVal.QueueOfItems->SetProductionComplete();
					}

					//Get the remaining numberOfPages-1 pages if any.
					vector<uri_builder> listQueries;
					for (int i = 0; i < numberOfPages - 1; i++)
					{
						auto uriBuilderDup = file_uri_builder;
						uriBuilderDup.append_query(_XPLATSTR("offset"), (i + 1) * limit).append_query(_XPLATSTR("limit"), limit);
						listQueries.push_back(uri_builder(uriBuilderDup));
					}

					auto ret = shared_ptr<api_response_containing_lists<TResponseItemType>>(&retVal,
						[](api_response_containing_lists<TResponseItemType>* ptr)
					{ /* no deletion, this is just to pass by value */ });


					PARALLELFOR(iteratorX, 0, numberOfPages - 1, 1, MaxThreadsPerListApiCall, [=]()
					{
						TRACE(urlForLogging << " :PARALLELFOR (" << iteratorX + 1 << "/" << numberOfPages << ")");
						auto urlForCurrentPage = listQueries[iteratorX];
						auto page = get<api_response<api_list_response<TResponseItemType>>>(urlForCurrentPage, cancelationToken).get();
						auto url = urlForCurrentPage.to_string();
						string pageUrlForLogging(url.begin(), url.end());
						DEBUG(pageUrlForLogging << " :Received page (" << iteratorX + 2 << "/" << numberOfPages << ")  containing " << page.Response.DisplayedCount << " items.");
						for (auto item : page.Response.Items)
						{
							ret->QueueOfItems->push(std::make_shared<TResponseItemType>(item));
						}
					}, cancelationToken);
					INFO("basespace_client::get_list (" << urlForLogging << ") : production complete");
					retVal.QueueOfItems->SetProductionComplete();
				}
				catch (const exception &)
				{
					WARN("Exception: " << Illumina::Infrastructure::ErrorStringFromExceptionPtr(std::current_exception()));
					retVal.set_error(current_exception());
					WARN("rethrowing exception");
					rethrow_exception(current_exception());
				}
			}
			, cancelationToken));*/
			return retVal;
		}

    }
}
