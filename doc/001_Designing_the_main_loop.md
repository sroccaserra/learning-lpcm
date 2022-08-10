Main Loop
---------

The main loop should run at a constant ms rate, for example every 100 ms.
Every 100 ms, it should compute the necessary samples for the next 100 ms and
wait for the next loop.

Question: what is a good buffer size in order to fit 100 ms?

Question: is a buffer necessary? Could we not simply print the next 100 ms
samples to stdout?

Question: how many times should the main loop run? When should we stop
executing the main loop?

For a non looping music player, the main loop could stop when all the patterns
have ended. But some notes could have long tails, so it's better to wait long
enough, or to insert an empty pattern at the end to ensure that long tails have
had enough time to end.

For a looping music, the main loop never ends.

For a music editing tool, the main loop could never end, when the pattern is
paused we could continuously send silence to stdout. But this implementation is
command-line only, not an editing tool. It could become a text-based editing
tool with Gforth's terminal output facilities.

Let's imagine that this tool's first goal is to play a tune defined by a list
of patterns. Patterns are composed of up to 64 steps. 64 steps is also the
default for patterns lengthes. By default, a beat is composed of 4 steps, and
it could be nice to be able to define 3 or 6 step beats.

This would allow to define a beat per minutes (BPM) value, and to compute the
total length of the tune. So the number of times that the main loop should run,
by slicing the total time in 100 ms slices.
