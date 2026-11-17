using System;
using NUnit.Framework;
using FrontierDispatch.Core;

namespace FrontierDispatch.Tests
{
    public sealed class MPQtests
    {
        [Test]
        public void return_lowest_test()
        {
            var queue = new MinPriorityQueue<string>();
            queue.Enqueue("expensive", 9);
            queue.Enqueue("cheap", 2);
            Assert.That(queue.Peek(), Is.EqualTo("cheap"));
            Assert.That(queue.Count, Is.EqualTo(2));
        }

        [Test]
        public void empty_queue_test()
        {
            var queue = new MinPriorityQueue<int>();
            Assert.Throws<InvalidOperationException>(() => queue.Dequeue());
        }

        [Test]
        public void many_items_test()
        {
            var queue = new MinPriorityQueue<int>();
            var random = new Random(42);
            for (int i = 0; i < 1000; ++i)
            {
                int priority = random.Next(0, 100);
                queue.Enqueue(priority, priority);
            }

            int previous = int.MinValue;
            while (queue.Count > 0)
            {
                int current = queue.Dequeue();
                Assert.That(current, Is.GreaterThanOrEqualTo(previous));
                previous = current;
            }
        }
    }
}