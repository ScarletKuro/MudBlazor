using System;
using System.Threading.Tasks;
using MudBlazor.Exceptions;

namespace MudBlazor
{
    public enum TaskOption
    {
        None,
        Safe
    }

    public static class TaskExtensions
    {
        public static async void AndForget(this Task task, IMudComponentException componentException)
        {
            try
            {
                await task;
            }
            catch (Exception exception)
            {
               componentException.ProcessError(exception);
            }
        }


        /// <summary>
        /// Task will be awaited and exceptions will be logged to console (TaskOption.Safe) or managed by the Blazor framework (TaskOption.None).
        /// </summary>
        public static async void AndForget(this Task task, TaskOption option = TaskOption.None)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                if (option != TaskOption.Safe)
                    throw;

                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// ValueTask will be awaited and exceptions will be logged to console (TaskOption.Safe) or managed by the Blazor framework (TaskOption.None).
        /// </summary>
        public static async void AndForget(this ValueTask task, TaskOption option = TaskOption.None)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                if (option != TaskOption.Safe)
                    throw;

                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// ValueTask(bool) will be awaited and exceptions will be logged to console (TaskOption.Safe) or managed by the Blazor framework (TaskOption.None).
        /// </summary>
        public static async void AndForget(this ValueTask<bool> task, TaskOption option = TaskOption.None)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                if (option != TaskOption.Safe)
                    throw;

                Console.WriteLine(ex);
            }
        }
    }
}
