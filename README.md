Learning how to create LPCM encoded signals.

## Use

I'm starting to learn using Gforth and aplay (under Linux) or SoX.

```
$ gforth src/lpcm.fs -e 'main bye' | aplay -t raw -c 1 -f s16_le -r 44100
```

To use with SoX (Windows, macOS, Linux):

```
$ gforth src/lpcm.fs -e 'main bye' | play -t raw -c 1 -e signed-integer -b 16 -L -r 44100 -
```

In the source code I implemented:
- Unsigned 8 bit output (`U8`)
- Little-endian signed 16 bit output (`S16` or `S16_LE`)
- Big-endian signed 16 bit output (`S16_BE`)
- Constant but configurable sample rate

## Run the tests

```
$ gforth src/lpcm-test.fs -e bye
```

## References

- Pulse-code modulation ~ <https://en.wikipedia.org/wiki/Pulse-code_modulation>
- PCM ~ <https://wiki.multimedia.cx/index.php/PCM>
- Gforth ~ <https://gforth.org/manual/>
- Floating Point ~ <https://gforth.org/manual/Floating-Point.html>
- aplay ~ <https://alsa.opensrc.org/Aplay>
- SoX ~ <http://sox.sourceforge.net/>
- Envelope (music) ~ <https://en.wikipedia.org/wiki/Envelope_(music)>
