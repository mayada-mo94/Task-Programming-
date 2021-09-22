# Task-Programming

In this Repo I covered (MultipleThreading/ Async Programming) 
I revisited topics [ParallelTasks , Cancellation Token , Async Await inside C# Locks, Use direct Threads]

# Let's start with ParallelTasks
Task is a higher level of abstraction, Tasks provide two primary benefits:
	1- More efficient and more scalable use of system resources.
	2- More programmatic control than is possible with a thread or work item.

In ParallelTask.cs you can find 5 region:


	First Region
	[Use The Parallel.Invoke method provides a convenient way to run any number of arbitrary statements concurrently.
	also, I used set maximum number of concurrent tasks enabled by this ParallelOptions instance.]

	Second Region
	[Use The TaskFactory class that provided static methods that encapsulate some common patterns for creating and starting 
	tasks and continuation tasks.]

	-- The default TaskFactory can be accessed as a static property on the Task class or Task<TResult> class,
	You can also instantiate a TaskFactory directly and specify various options that include a CancellationToken.
	That recovered In Third Region

	Fourth Region 
	[Just appy the Parallel Foreach ]
	Executes a foreach operation in which iterations may run in parallel.
		
	Fifth Region 
	--When you need to pass additional state into the task that you can retrieve through its Task.AsyncState property
	aslo, use WaitAll for Waiting for tasks to finish

# Next TaskCancellation.cs

We managed cancellation tokens retrieved from its CancellationTokenSource.Token property.

The following example uses a random number generator to emulate a data collection application that reads 10 integral values from eleven different instruments.
A value of zero indicates that the measurement has failed for one instrument, 
in which case the operation should be cancelled and no overall mean should be computed.


# AsyncAwaitLocks.cs

In the File I tried to write manual safe code [by lock] then using thread safe.

# Finally Thread.cs

Foreground and background threads

Foreground threads are threads which will continue to run until the last foreground thread is terminated. 
In another way, the application is closed when all the foreground threads are stopped.

Background threads are threads which will get terminated when all foreground threads are closed.
The application won't wait for them to be completed.

Note: 
By default, the threads are foreground threads. So when we create a thread the default value of IsBackground property would be false.

-- Joining Threads


