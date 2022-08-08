Learning how to create LPCM encoded signals.

## Use

I'm starting to learn using Gforth and aplay (under Linux) or SoX.

```
$ gforth src/lpcm.fs -e 'main bye' | aplay -f s16 -r 44100
```

To use with SoX (Windows, macOS, Linux):

```
$ gforth src/lpcm.fs -e 'main bye' | play -t raw -e signed-integer -b 16 -L -c 1 -r 44100 -
```

In the source code I implemented:
- Unsigned 8 bit output (`U8`)
- Little-endian signed 16 bit output (`S16` or `S16_LE`)
- Big-endian signed 16 bit output (`S16_BE`)
- Constant but configurable sample rate

## References

- Pulse-code modulation ~ <https://en.wikipedia.org/wiki/Pulse-code_modulation>
- PCM ~ <https://wiki.multimedia.cx/index.php/PCM>
- Gforth ~ <https://gforth.org/manual/>
- Floating Point ~ <https://gforth.org/manual/Floating-Point.html>
- aplay ~ <https://alsa.opensrc.org/Aplay>
- SoX ~ <http://sox.sourceforge.net/>
